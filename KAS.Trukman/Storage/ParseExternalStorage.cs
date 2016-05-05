using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using KAS.Trukman.Data.Classes;
using Parse;
using KAS.Trukman.Data.Interfaces;
using System.Threading.Tasks;
using KAS.Trukman.Storage.ParseClasses;
using Xamarin.Forms.Maps;
using KAS.Trukman.Data.Infos;
using KAS.Trukman.Data.Enums;

namespace KAS.Trukman.Storage
{
    #region ParseExternalStorage
    public class ParseExternalStorage : IExternalStorage
    {
        #region Static members
        public static readonly string PARSE_APPLICATION_ID = "NsNjjvCGhqVKOZqCro2WOEr6gZHGTC9YlVB5jZqe";
        public static readonly string PARSE_DOTNET_KEY = "WvSfa6MIvTb9L6BucGIiCQgV1zBc4OCR0UTS7D2L";

        public static readonly string PARSE_TRIP_CLASS_NAME = "";

        #endregion

        private User _currentUser = null;

        public ParseExternalStorage()
        {
            ParseObject.RegisterSubclass<ParseGeoLocation>();
            ParseObject.RegisterSubclass<ParseContractor>();
            ParseObject.RegisterSubclass<ParseJob>();
            ParseObject.RegisterSubclass<ParsePhoto>();
            ParseObject.RegisterSubclass<ParseCompany>();
            ParseObject.RegisterSubclass<ParseComcheck>();
            ParseObject.RegisterSubclass<ParseNotification>();
            ParseClient.Initialize(PARSE_APPLICATION_ID, PARSE_DOTNET_KEY);
            //            this.SaveInstallation();

            // To do: 
            //            if (ParseUser.CurrentUser != null)
            //                if (App.ServerManager.GetCurrentUserRole() == UserRole.UserRoleOwner)
            //                    this.InitializeOwnerNotification();
        }

        private void SaveInstallation()
        {
            Task.Run(async () =>
            {
                try
                {
                    await ParseInstallation.CurrentInstallation.SaveAsync();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            });
        }

        private ParseJob GetNewJobForDriver()
        {
            ParseJob job = null;
            Task.Run(async () =>
            {
                try
                {
                    var jquery = new ParseQuery<ParseJob>()
                        .Include("Shipper")
                        .Include("Receiver")
                        .WhereEqualTo("Driver", ParseUser.CurrentUser)
                        .WhereEqualTo("DriverAccepted", false)
                        .WhereEqualTo("JobCancelled", false)
                        .WhereEqualTo("JobCompleted", false)
                        .WhereDoesNotExist("DeclineReason")
                        .WhereGreaterThan("DeliveryDatetime", DateTime.Now)
                        .OrderBy("PickupDatetime");
                    job = await jquery.FirstOrDefaultAsync();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw exception;
                }
            }).Wait();
            return job;
        }

        public ParseJob GetParseJobByID(string id)
        {
            ParseJob job = null;
            Task.Run(async () =>
            {
                try
                {
                    var jquery = new ParseQuery<ParseJob>()
                        .Include("Shipper")
                        .Include("Receiver")
                        .WhereEqualTo("objectId", id);
                    job = await jquery.FirstOrDefaultAsync();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw exception;
                }
            }).Wait();
            return job;
        }

        public void SaveParseJob(ParseJob job)
        {
            Task.Run(async () =>
            {
                try
                {
                    await job.SaveAsync();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw exception;
                }
            }).Wait();
        }

        public void SaveParsePhoto(ParsePhoto photo)
        {
            Task.Run(async () =>
            {
                try
                {
                    await photo.SaveAsync();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw exception;
                }
            }).Wait();
        }

        public void SaveParseGeoLocation(ParseGeoLocation location)
        {
            Task.Run(async () =>
            {
                try
                {
                    await location.SaveAsync();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw exception;
                }
            }).Wait();
        }

        private Trip ParseJobToTrip(ParseJob parseJob)
        {
            var shipper = new Contractor
            {
                ID = (parseJob.Shipper != null ? parseJob.Shipper.ObjectId : Guid.NewGuid().ToString()),
                Name = (parseJob.Shipper != null ? parseJob.Shipper.Name : ""),
                Phone = (parseJob.Shipper != null ? parseJob.Shipper.Phone : ""),
                Fax = (parseJob.Shipper != null ? parseJob.Shipper.Fax : ""),
                Address = (parseJob.Shipper != null ? parseJob.Shipper.Address : parseJob.FromAddress)
            };
            var receiver = new Contractor
            {
                ID = (parseJob.Receiver != null ? parseJob.Receiver.ObjectId : Guid.NewGuid().ToString()),
                Name = (parseJob.Receiver != null ? parseJob.Receiver.Name : ""),
                Phone = (parseJob.Receiver != null ? parseJob.Receiver.Phone : ""),
                Fax = (parseJob.Receiver != null ? parseJob.Receiver.Fax : ""),
                Address = (parseJob.Receiver != null ? parseJob.Receiver.Address : parseJob.ToAddress)
            };
            var driverDisplayName = (parseJob != null && parseJob.Driver != null ? parseJob.Driver.Username : "");
            var trip = new Trip
            {
                ID = parseJob.ObjectId,
                DeclineReason = parseJob.DeclineReason,
                DeliveryDatetime = parseJob.DeliveryDatetime,
                DriverAccepted = parseJob.DriverAccepted,
                IsDelivery = (parseJob.DriverOnTimeDelivery != 0),
                IsPickup = (parseJob.DriverOnTimePickup != 0),
                JobCancelled = parseJob.JobCancelled,
                JobCompleted = parseJob.JobCompleted,
                PickupDatetime = parseJob.PickupDatetime,
                Points = parseJob.Price,
                Shipper = shipper,
                Receiver = receiver,
                Location = new Position(parseJob.Location.Latitude, parseJob.Location.Longitude),
                UpdateTime = (parseJob.UpdatedAt != null ? (DateTime)parseJob.UpdatedAt : DateTime.Now),
                DriverDisplayName = driverDisplayName
            };

            return trip;
        }

        public Company ParseCompanyToCompany(ParseCompany parseCompany)
        {
            var company = new Company
            {
                ID = parseCompany.ObjectId,
                Name = parseCompany.Name,
                DisplayName = parseCompany.DisplayName
            };
            if (String.IsNullOrEmpty(company.DisplayName))
                company.DisplayName = company.Name;
            return company;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            if ((_currentUser == null) && (ParseUser.CurrentUser != null))
            {
                var query = ParseUser.Query
                    .WhereEqualTo("objectId", ParseUser.CurrentUser.ObjectId);

                var parseUser = await query.FirstOrDefaultAsync();
                await parseUser.FetchAsync();

                _currentUser = this.ParseUserToUser(parseUser);
            }

            return _currentUser;
        }

        private User ParseUserToUser(ParseUser parseUser)
        {
            var role = 0;
            var status = 0;
            var firstName = "";
            var lastName = "";

            parseUser.TryGetValue<int>("role", out role);
            parseUser.TryGetValue<int>("status", out status);
            parseUser.TryGetValue<string>("firstName", out firstName);
            parseUser.TryGetValue<string>("LastName", out lastName);

            var user = new User
            {
                ID = parseUser.ObjectId,
                UserName = parseUser.Username,
                Role = (UserRole)role,
                Status = status,
                Email = parseUser.Email,
                FirstName = firstName,
                LastName = lastName
            };
            return user;
        }

        public async Task<string> GetSessionToken()
        {
            var parseSession = await ParseSession.GetCurrentSessionAsync();
            return parseSession.SessionToken;
        }

        private async Task<ParseCompany> SelectParseCompanyByIDAsync(string id)
        {
            var query = new ParseQuery<ParseCompany>()
                .WhereEqualTo("objectId", id);
            var parseCompany = await query.FirstOrDefaultAsync();
            return parseCompany;
        }

        private async Task<Company> SaveCompanyAsync(Company company)
        {
            var saved = await this.SelectParseCompanyByIDAsync(company.ID);
            if (saved == null)
                saved = new ParseCompany();
            saved.DisplayName = company.DisplayName;
            saved.FleetSize = company.FleetSize;
            await saved.SaveAsync();
            return this.ParseCompanyToCompany(saved);
        }

        #region IExternalStorage
        public void LogIn(User user)
        {
            Task.Run(async () =>
            {
                await ParseUser.LogInAsync(user.UserName, user.Phone);
            }).Wait();
        }

        public Trip AcceptTrip(string id)
        {
            Trip trip = null;
            var parseJob = this.GetParseJobByID(id);
            if (parseJob != null)
            {
                parseJob.DriverAccepted = true;
                this.SaveParseJob(parseJob);
                trip = this.ParseJobToTrip(parseJob);
            }
            return trip;
        }

        public Trip CheckNewTripForDriver(string userID)
        {
            Trip trip = null;
            var parseJob = this.GetNewJobForDriver();

            if (parseJob != null)
                trip = this.ParseJobToTrip(parseJob);
            return trip;
        }

        public Trip DeclineTrip(string id, string reasonText)
        {
            Trip trip = null;
            var parseJob = this.GetParseJobByID(id);
            if (parseJob != null)
            {
                parseJob.DeclineReason = reasonText;
                this.SaveParseJob(parseJob);
                trip = this.ParseJobToTrip(parseJob);
            }
            return trip;
        }

        public Trip CompleteTrip(string id)
        {
            Trip trip = null;
            var parseJob = this.GetParseJobByID(id);
            if (parseJob != null)
            {
                parseJob.JobCompleted = true;
                this.SaveParseJob(parseJob);
                trip = this.ParseJobToTrip(parseJob);
            }
            return trip;
        }

        public Trip SelectTripByID(string id)
        {
            Trip trip = null;
            var parseJob = this.GetParseJobByID(id);
            if (parseJob != null)
                trip = this.ParseJobToTrip(parseJob);
            return trip;
        }

        public Trip SendPhoto(string id, byte[] data, string kind)
        {
            Trip trip = null;
            ParseJob job = this.GetParseJobByID(id);
            if (job != null)
            {
                var photo = new ParsePhoto
                {
                    Kind = kind,
                    Data = new ParseFile(kind + ".jpg", data),
                    Job = job
                };
                this.SaveParsePhoto(photo);
                job.Photos.Add(photo);
                this.SaveParseJob(job);
                trip = this.ParseJobToTrip(job);
            }
            return trip;
        }

        public Trip TripInDelivery(string id, int minutes)
        {
            Trip trip = null;
            var parseJob = this.GetParseJobByID(id);
            if (parseJob != null)
            {
                parseJob.DriverOnTimeDelivery = minutes;
                this.SaveParseJob(parseJob);
                trip = this.ParseJobToTrip(parseJob);
            }
            return trip;
        }

        public Trip TripInPickup(string id, int minutes)
        {
            Trip trip = null;
            var parseJob = this.GetParseJobByID(id);
            if (parseJob != null)
            {
                parseJob.DriverOnTimePickup = minutes;
                this.SaveParseJob(parseJob);
                trip = this.ParseJobToTrip(parseJob);
            }
            return trip;
        }

        public Trip AddLocation(string id, Position position)
        {
            Trip trip = null;
            ParseJob job = this.GetParseJobByID(id);
            if (job != null)
            {
                var location = new ParseGeoLocation
                {
                    Location = new ParseGeoPoint(position.Latitude, position.Longitude),
                    PointCreatedAt = DateTime.Now
                };
                this.SaveParseGeoLocation(location);
                job.Location = new ParseGeoPoint(position.Latitude, position.Longitude);
                job.Locations.Add(location);
                this.SaveParseJob(job);
                trip = this.ParseJobToTrip(job);
            }
            return trip;
        }

        public Trip SaveLocation(string id, Position position)
        {
            Trip trip = null;
            ParseJob job = this.GetParseJobByID(id);
            if (job != null)
            {
                job.Location = new ParseGeoPoint(position.Latitude, position.Longitude);
                this.SaveParseJob(job);
                trip = this.ParseJobToTrip(job);
            }
            return trip;
        }

        public async Task<Company[]> SelectCompanies(string filter)
        {
            var query = new ParseQuery<ParseCompany>()
                .WhereContains("name", filter.ToLower())
                .Limit(10)
                .OrderBy("name");
            var parseCompanies = await query.FindAsync();
            var companies = new List<Company>();
            foreach (var parseCompany in parseCompanies)
                companies.Add(this.ParseCompanyToCompany(parseCompany));
            return companies.ToArray();
        }

        public async Task<Company> SelectCompanyByName(string name)
        {
            Company company = null;
            var query = new ParseQuery<ParseCompany>()
                .WhereEqualTo("name", name.ToLower());
            var parseCompany = await query.FirstOrDefaultAsync();
            if (parseCompany != null)
                company = this.ParseCompanyToCompany(parseCompany);
            return company;
        }

        private async Task<ParseCompany> SelectUserParseCompanyAsync()
        {
            var query = new ParseQuery<ParseCompany>()
                .WhereEqualTo("owner", ParseUser.CurrentUser);
            var parseCompany = await query.FirstOrDefaultAsync();

            if (parseCompany == null)
            {
                query = new ParseQuery<ParseCompany>()
                    .WhereEqualTo("drivers", ParseUser.CurrentUser);
                parseCompany = await query.FirstOrDefaultAsync();
            }

            if (parseCompany == null)
            {
                query = new ParseQuery<ParseCompany>()
                    .WhereEqualTo("requesting", ParseUser.CurrentUser);
                parseCompany = await query.FirstOrDefaultAsync();
            }

            return parseCompany;
        }

        private async Task<ParseUser> SelectParseUserByUserNameAsync(string userName)
        {
            var query = ParseUser.Query
                .WhereEqualTo("username", userName);

            var parseUser = await query.FirstOrDefaultAsync();

            return parseUser;
        }

        public async Task<Company> SelectUserCompanyAsync()
        {
            Company company = null;

            var parseCompany = await this.SelectUserParseCompanyAsync();

            if (parseCompany != null)
                company = this.ParseCompanyToCompany(parseCompany);

            return company;
        }

        public async Task<Trip[]> SelectActiveTrips()
        {
            var trips = new List<Trip>();
            try
            {
                var companyQuery = new ParseQuery<ParseCompany>()
                    .WhereEqualTo("owner", ParseUser.CurrentUser);
                var parseCompany = await companyQuery.FirstOrDefaultAsync();

                if (parseCompany != null)
                {
                    var query = new ParseQuery<ParseJob>()
                        .Include("Shipper")
                        .Include("Receiver")
                        .Include("Driver")
                        .WhereEqualTo("Company", parseCompany)
                        .WhereEqualTo("DriverAccepted", true)
                        .WhereEqualTo("JobCompleted", false)
                        .OrderBy("DeliveryDatetime");
                    var jobs = await query.FindAsync();
                    foreach (var job in jobs)
                        trips.Add(this.ParseJobToTrip(job));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw exception;
            }
            return trips.ToArray();
        }

        public async Task<Position> SelectDriverPosition(string tripID)
        {
            var position = new Position(0, 0);
            try
            {
                var query = new ParseQuery<ParseJob>()
                    .WhereEqualTo("objectId", tripID);
                var job = await query.FirstOrDefaultAsync();
                if (job != null)
                    position = new Position(job.Location.Latitude, job.Location.Longitude);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw exception;
            }

            return position;
        }

        public void InitializeOwnerNotification()
        {
            Task.Run(async () =>
            {
                try
                {
                    var company = await this.SelectUserCompanyAsync();
                    await ParsePush.SubscribeAsync(company.Name.Replace(" ", ""));
                    ParsePush.ParsePushNotificationReceived += ParsePush.DefaultParsePushNotificationReceivedHandler;
                    //ParseInstallation installation = ParseInstallation.CurrentInstallation;
                    //installation.AddUniqueToList("channels", company.Name.Replace(" ", ""));
                    //await installation.SaveAsync();

                    //ParsePush.ParsePushNotificationReceived += (sender, args) =>
                    //{
                    //};
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            });
        }

        public async Task<User> BecomeAsync(string session)
        {
            await ParseUser.BecomeAsync(session);

            var currentUser = await this.GetCurrentUserAsync();

            return currentUser;
        }

        public async Task<User> SignUpAsync(User user)
        {
            var parseUser = new ParseUser
            {
                Username = user.UserName,
                Password = user.Phone,
                Email = user.Email
            };
            parseUser["firstName"] = user.FirstName;
            parseUser["LastName"] = user.LastName;
            parseUser["role"] = (int)user.Role;

            await parseUser.SignUpAsync();

            var currentUser = await this.GetCurrentUserAsync();

            return currentUser;
        }

        public async Task<User> LogInAsync(string userName, string password)
        {
            await ParseUser.LogInAsync(userName, password);

            var currentUser = await this.GetCurrentUserAsync();

            return currentUser;
        }

        public async Task<bool> UserExistAsync(string userName)
        {
            var parseQuery = ParseUser.Query
                .WhereEqualTo("username", userName);
            var user = await parseQuery.FirstOrDefaultAsync();

            return (user != null);
        }

        public async Task<Company> RegisterCompany(CompanyInfo companyInfo)
        {
            var userName = companyInfo.Name.ToLower().Trim();
            User user = null;
            if (await this.UserExistAsync(userName))
                user = await this.LogInAsync(userName, companyInfo.MCCode.Trim());
            else
            {
                user = new User
                {
                    UserName = userName,
                    Phone = companyInfo.MCCode.Trim(),
                    Email = companyInfo.EMail,
                    Role = UserRole.UserRoleOwner
                };
                user = await this.SignUpAsync(user);
            }

            var company = await this.SelectCompanyByName(userName);
            if (company == null)
                company = new Company
                {
                    Name = userName
                };

            company.DisplayName = companyInfo.Name;
            company.FleetSize = companyInfo.FleetSize;

            company = await this.SaveCompanyAsync(company);

            return company;
        }

        public async Task<Company> RegisterDriver(DriverInfo driverInfo)
        {
            var userName = String.Format("{0} {1}", driverInfo.FirstName.Trim(), driverInfo.LastName.Trim()).ToLower();
            Company company = null;
            User user = null;
            if (await this.UserExistAsync(userName))
            {
                user = await this.LogInAsync(userName, driverInfo.Phone.Trim());
                company = await this.SelectUserCompanyAsync();
            }
            else
            {
                user = new User
                {
                    UserName = userName,
                    Phone = driverInfo.Phone,
                    Role = UserRole.UserRoleDriver,
                    FirstName = driverInfo.FirstName,
                    LastName = driverInfo.LastName
                };
                user = await this.SignUpAsync(user);

                var userCompany = await this.SelectUserParseCompanyAsync();
                if ((userCompany != null) && (userCompany.ObjectId == driverInfo.Company.ID))
                    throw new Exception(String.Format("Your are approved to company {0}.", userCompany.DisplayName));

                if (userCompany == null)
                    userCompany = await this.SelectParseCompanyByIDAsync(driverInfo.Company.ID);

                var joined = await this.DriverIsJoinedToCompany(userCompany);
                if (!joined)
                    await this.JoinDriverToCompany(userCompany);

                company = this.ParseCompanyToCompany(userCompany);
            }


            return company;
        }

        public async Task<DriverState> GetDriverState()
        {
            var state = DriverState.Declined;
            var company = await this.SelectUserParseCompanyAsync();
            if (company != null)
            {
                var joined = await this.DriverIsJoinedToCompany(company);
                state = (joined ? DriverState.Joined : DriverState.Waiting);
            }
            return state;
        }

        private async Task<bool> DriverIsJoinedToCompany(ParseCompany company)
        {
            var joined = false;

            var companyUser = await company.Drivers.Query
                .WhereEqualTo("username", ParseUser.CurrentUser.Username)
                .FirstOrDefaultAsync();

            joined = (companyUser != null);
            return joined;
        }

        private async Task JoinDriverToCompany(ParseCompany company)
        {
            var companyUser = await company.Requestings.Query
                .WhereEqualTo("username", ParseUser.CurrentUser.Username)
                .FirstOrDefaultAsync();

            if (companyUser == null)
            {
                company.Requestings.Add(ParseUser.CurrentUser);
                await company.SaveAsync();
            }
        }

        public async Task<User> GetCurrentUser()
        {
            User user = null;
            var parseUser = ParseUser.CurrentUser;
            if (parseUser != null)
            {
                await parseUser.FetchAsync();
                user = this.ParseUserToUser(parseUser);
            }
            return user;
        }

        public async Task<User> SelectRequestedUser(string companyID)
        {
            User user = null;
            var company = await this.SelectParseCompanyByIDAsync(companyID);
            if (company != null)
            {
                var parseUser = await company.Requestings.Query
                    .WhereEqualTo("role", (int)UserRole.UserRoleDriver)
                    .FirstOrDefaultAsync();

                if (parseUser != null)
                    user = this.ParseUserToUser(parseUser);

                //var userEnum = await company.Requestings.Query.FindAsync();
                //var driversEnum = await company.Drivers.Query.FindAsync();

                //var users = new List<ParseUser>();
                //var drivers = new List<ParseUser>();

                //foreach (var parseUser in userEnum)
                //    users.Add(parseUser);

                //foreach (var driver in driversEnum)
                //    drivers.Add(driver);

                //foreach (var parseUser in users)
                //{
                //    var driver = drivers.FirstOrDefault(d => d.Username == parseUser.Username);
                //    if (driver != null)
                //    {
                //        user = this.ParseUserToUser(driver);
                //        break;
                //    }
                //}
            }
            return user;
        }

        public async Task AcceptDriverToCompany(User user)
        {
            var company = await this.SelectUserParseCompanyAsync();
            ParseUser parseUser = await this.SelectParseUserByUserNameAsync(user.UserName);
            company.Drivers.Add(parseUser);
            company.Requestings.Remove(parseUser);
            await company.SaveAsync();
        }

        public async Task DeclineDriverToCompany(User user)
        {
            var company = await this.SelectUserParseCompanyAsync();
            ParseUser parseUser = await this.SelectParseUserByUserNameAsync(user.UserName);
            company.Requestings.Remove(parseUser);
            await company.SaveAsync();
        }

        public async Task<Notification> GetNotification()
        {
            Notification notification = null;

            var query = new ParseQuery<ParseNotification>()
                .WhereEqualTo("Receiver", ParseUser.CurrentUser)
                .WhereEqualTo("isSending", false);

            var parseNotification = await query.FirstOrDefaultAsync();

            if (parseNotification != null)
            {
                notification = this.ParseNotificationToNotification(parseNotification);
                parseNotification.IsSending = true;
                await parseNotification.SaveAsync();
            }

            return notification;
        }

        public async Task SendNotification(Trip trip, string message)
        {
            var jobQuery = new ParseQuery<ParseJob>()
                .Include("Company")
                .Include("Company.owner")
                .WhereEqualTo("objectId", trip.ID);

            var job = await jobQuery.FirstOrDefaultAsync();
            if (job == null)
                throw new Exception("Job not found.");

            ParseUser receiver = null;
            if (job.Company != null)
                receiver = job.Company.Owner;

            if (receiver == null)
                throw new Exception("Receiver not set.");

            var parseNotification = new ParseNotification
            {
                Text = message,
                IsSending = false,
                IsReading = false,
                Trip = job,
                Sender = ParseUser.CurrentUser,
                Receiver = receiver
            };

            await parseNotification.SaveAsync();

            job.Notifications.Add(parseNotification);
            await job.SaveAsync();
        }

        private Notification ParseNotificationToNotification(ParseNotification parseNotification)
        {
            Trip trip = null;
            User sender = null;
            User receiver = null;

            if (parseNotification.Trip != null)
                trip = this.ParseJobToTrip(parseNotification.Trip);
            if (parseNotification.Sender != null)
                sender = this.ParseUserToUser(parseNotification.Sender);
            if (parseNotification.Receiver != null)
                sender = this.ParseUserToUser(parseNotification.Receiver);

            var notification = new Notification
            {
                ID = parseNotification.ObjectId,
                Text = parseNotification.Text,
                IsSending = parseNotification.IsSending,
                IsReading = parseNotification.IsReading,
                Trip = trip,
                Sender = sender,
                Receiver = receiver
            };
            return notification;
        }
        #endregion
    }
    #endregion
}
