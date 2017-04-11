using System;
using System.Text;
using System.Threading.Tasks;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Enums;
using KAS.Trukman.Data.Infos;
using Xamarin.Forms.Maps;
using System.Net.Http;
using KAS.Trukman.Data.API;
using Newtonsoft.Json;
using KAS.Trukman.Droid.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace KAS.Trukman.Storage
{
    public class RestAPIExternalStorage : IExternalStorage
    {
        private readonly string _baseUri = "http://api.trukman.com/api/";
        private readonly string _acceptJobEndpoint = "job/accept?id={0}";
        private readonly string _completeTripEndpoint = "job/completeTrip?id={0}";
        private readonly string _declineTripEndpoint = "job/DeclineTrip?id={0}";
        private readonly string _getCompanyByIdEndpoint = "company/{0}";
        private readonly string _getCompanyByNameEndpoint = "company/name/{0}";
        private readonly string _getCompaniesEndpoint = "company/";
        private readonly string _getJobByIdEndpoint = "job/{0}";
        private readonly string _getNewJobForDriver = "job/new/driver";
        private readonly string _getSelectUserCompanyEndpoint = "users/company?userId={0}";
        private readonly string _getUserByNameEndpoint = "users/username?username={0}";
        private readonly string _loginEndpoint = "auth/login";
        private readonly string _saveCompanyEndpoint = "company/";
        //private readonly string _saveJobEndpoint = "job/";
        private readonly string _saveJobPhotoEndpoint = "job/savePhoto";
        private readonly string _selectActiveTripsEndpoint = "job/activeTrips";
        private readonly string _signUpUserEndpoint = "users/";

        private User _currentUser = null;

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient();

            return client;
        }

        private string SerializeObject(object instance)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            return JsonConvert.SerializeObject(instance, settings);
        }

        private T DeserializeObject<T>(string json)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        private async Task<T> ExecuteRequestAsync<T>(HttpRequestMessage requestMessage)
        {
            try
            {
                T result = default(T);
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(60);
                    if (_currentUser != null)
                        client.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _currentUser.Token);
                            //Add("Authorization", _currentUser.Token);
                    var responseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false);
                    var content = await responseMessage.Content.ReadAsStringAsync();
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        if (!content.IsNullOrEmpty())
                            result = DeserializeObject<T>(content);
                        else
                            result = default(T);
                    }
                    else
                    {
                        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(content);
                        Console.WriteLine("Request error ({0}): {1}", errorResponse.Message, errorResponse.Stack);
                        var exceptionMessage = errorResponse.GetDisplayText();
                        throw new Exception(exceptionMessage);
                    }
                }

                return result;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                throw new Exception("The server is not available (Timeout connection).");
            }
        }

        private Uri CreateRequestUri(string resource, params string[] args)
        {
            string endpoint = _baseUri + resource;
            if (args != null && args.Length > 0)
                endpoint = endpoint.format(args);

            endpoint = Uri.EscapeUriString(endpoint);
            return new Uri(endpoint);
        }

        private async Task<Company> SaveCompanyAsync(Company company)
        {
            var saved = await this.SelectCompanyByIdAsync(company.ID);
            if (saved == null)
                saved = new ProxyCompany();
            saved.Owner = _currentUser.UserName;
            saved.Name = company.Name;
            saved.DisplayName = company.DisplayName;
            saved.FleetSize = company.FleetSize;

            var uri = CreateRequestUri(_saveCompanyEndpoint);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            string jsonContent = SerializeObject(saved);
            requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8);
            var proxyCompany = await ExecuteRequestAsync<ProxyCompany>(requestMessage);

            return this.ProxyCompanyToCompany(proxyCompany);
        }

        private async Task<bool> DriverIsJoinedToCompany(ProxyCompany company)
        {
            var joined = false;

            //var companyUser = await company.Drivers.Query
            //    .WhereEqualTo("username", ParseUser.CurrentUser.Username)
            //    .FirstOrDefaultAsync();

            //joined = (companyUser != null);
            return await Task.FromResult(joined);
        }

        private async Task JoinDriverToCompany(ProxyCompany company)
        {
            //var companyUser = await company.Requestings.Query
            //    .WhereEqualTo("username", ParseUser.CurrentUser.Username)
            //    .FirstOrDefaultAsync();

            //if (companyUser == null)
            //{
            //    company.Requestings.Add(ParseUser.CurrentUser);
            //    await company.SaveAsync();
            //}

        }

        private async Task<string> SelectProxyUserCompanyAsync(string userId)
        {
            var uri = CreateRequestUri(_getSelectUserCompanyEndpoint, userId);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            //TODO: проверить возвращаются ли компании для водителей, которые в статусе waiting
            var companyId = await ExecuteRequestAsync<string>(requestMessage);
            //if (parseCompany == null)
            //{
            //    query = new ParseQuery<ParseCompany>()
            //        .WhereEqualTo("requesting", ParseUser.CurrentUser);
            //    parseCompany = await query.FirstOrDefaultAsync();
            //}

            return companyId;
        }

        private async Task<ProxyCompany> SelectCompanyByIdAsync(string companyId)
        {
            var uri = CreateRequestUri(_getCompanyByIdEndpoint, companyId);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            var proxyCompany = await ExecuteRequestAsync<ProxyCompany>(requestMessage);
            return proxyCompany;
        }

        private Company ProxyCompanyToCompany(ProxyCompany proxyCompany)
        {
            return new Company
            {
                DisplayName = proxyCompany.Name,
                ID = proxyCompany.Id,
                FleetSize = proxyCompany.FleetSize,
                Name = proxyCompany.Name
            };
        }

        private User ProxyUserToUser(ProxyUser proxyUser)
        {
            return new User
            {
                Email = proxyUser.EMail,
                FirstName = proxyUser.FirstName,
                LastName = proxyUser.LastName,
                Phone = proxyUser.Phone,
                Password = proxyUser.Password,
                Role = (UserRole)(Enum.Parse(typeof(UserRole), proxyUser.Role, true)),
                Status = proxyUser.Status,
                UserName = proxyUser.UserName,
                ID = proxyUser.Id
            };
        }

        private async Task<ProxyJob> GetProxyJobByID(string id)
        {
            var uri = CreateRequestUri(_getJobByIdEndpoint, id);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            var proxyJob = await ExecuteRequestAsync<ProxyJob>(requestMessage);
            return proxyJob;
        }

        //private async Task SaveProxyJob(ProxyJob proxyJob)
        //{
        //    var uri = CreateRequestUri(_save)

        //}

        private Trip ProxyJobToTrip(ProxyJob proxyJob)
        {
            //var shipper = new Contractor
            //{
            //    ID = (proxyJob.Shipper != null ? parseJob.Shipper.ObjectId : Guid.NewGuid().ToString()),
            //    Name = (parseJob.Shipper != null ? parseJob.Shipper.Name : ""),
            //    Phone = (parseJob.Shipper != null ? parseJob.Shipper.Phone : ""),
            //    Fax = (parseJob.Shipper != null ? parseJob.Shipper.Fax : ""),
            //    Address = (parseJob.Shipper != null ? parseJob.Shipper.Address : parseJob.FromAddress),
            //    SpecialInstruction = (parseJob.Shipper != null ? parseJob.Shipper.SpecialInstruction : "")
            //};
            //var receiver = new Contractor
            //{
            //    ID = (parseJob.Receiver != null ? parseJob.Receiver.ObjectId : Guid.NewGuid().ToString()),
            //    Name = (parseJob.Receiver != null ? parseJob.Receiver.Name : ""),
            //    Phone = (parseJob.Receiver != null ? parseJob.Receiver.Phone : ""),
            //    Fax = (parseJob.Receiver != null ? parseJob.Receiver.Fax : ""),
            //    Address = (parseJob.Receiver != null ? parseJob.Receiver.Address : parseJob.ToAddress),
            //    SpecialInstruction = (parseJob.Receiver != null ? parseJob.Receiver.SpecialInstruction : "")
            //};
            //User driver = null;
            //if (parseJob.Driver != null)
            //    driver = this.ParseUserToUser(parseJob.Driver);
            //User broker = null;
            //if (parseJob.Broker != null)
            //    broker = this.ParseUserToUser(parseJob.Broker);
            //Company company = null;
            //if (parseJob.Company != null)
            //    company = this.ParseCompanyToCompany(parseJob.Company);

            var trip = new Trip
            {
                ID = proxyJob.Id,
                //ID = proxyJob.ObjectId,
                //DeclineReason = parseJob.DeclineReason,
                //DeliveryDatetime = parseJob.DeliveryDatetime,
                DriverAccepted = proxyJob.DriverAccepted,
                //IsDelivery = (parseJob.DriverOnTimeDelivery != 0),
                //IsPickup = (parseJob.DriverOnTimePickup != 0),
                //JobCancelled = parseJob.JobCancelled,
                //JobCompleted = parseJob.JobCompleted,
                //IsDeleted = parseJob.IsDeleted,
                //PickupDatetime = parseJob.PickupDatetime,
                //Points = parseJob.Price,
                //Shipper = shipper,
                //Receiver = receiver,
                //JobRef = parseJob.JobRef,
                //FromAddress = parseJob.FromAddress,
                //ToAddress = parseJob.ToAddress,
                //Weight = parseJob.Weight,
                //Location = new Position(parseJob.Location.Latitude, parseJob.Location.Longitude),
                //UpdateTime = (parseJob.UpdatedAt != null ? (DateTime)parseJob.UpdatedAt : DateTime.Now),
                //Driver = driver,
                //Broker = broker,
                //Company = company,
                //InvoiceUri = (parseJob.Invoice != null && parseJob.Invoice.File != null ? parseJob.Invoice.File.Url.ToString() : null),
                //DriverDisplayName = (driver != null ? driver.UserName : "")
            };

            return trip;
        }

        private async Task SaveProxyPhoto(ProxyPhoto photo)
        {
            var uri = CreateRequestUri(_saveJobPhotoEndpoint);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            var content = SerializeObject(photo);
            requestMessage.Content = new StringContent(content, Encoding.UTF8);

            var successMessage = await ExecuteRequestAsync<string>(requestMessage);
            //if (!successMessage.IsNullOrEmpty())
            //{

            //}
        }

        #region IExternalStorage
        public Task AcceptDriverToCompany(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<Trip> AcceptTrip(string id)
        {
            Trip trip = null;

            var proxyJob = await this.GetProxyJobByID(id);
            if (proxyJob != null)
            {
                proxyJob.DriverAccepted = true;

                var uri = CreateRequestUri(_acceptJobEndpoint, id);
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
                var successMessage = await ExecuteRequestAsync<string>(requestMessage);
                if (!successMessage.IsNullOrEmpty())
                    trip = this.ProxyJobToTrip(proxyJob);
            }
            return trip;
        }

        public Task<Trip> AddLocation(string id, Position location)
        {
            throw new NotImplementedException();
        }

        public Task AddPointsAsync(string jobID, string text, int points)
        {
            throw new NotImplementedException();
        }

        public User Become(string session)
        {
            throw new NotImplementedException();
        }

        public Task CancelComcheckRequestAsync(string tripID, ComcheckRequestType requestType)
        {
            throw new NotImplementedException();
        }

        public async Task<Trip> CheckNewTripForDriver(string userID)
        {
            Trip trip = null;

            var uri = CreateRequestUri(_getNewJobForDriver);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            var tripId = await ExecuteRequestAsync<string>(requestMessage);
            var proxyJob = await this.GetProxyJobByID(tripId);
            if (proxyJob != null)
                trip = this.ProxyJobToTrip(proxyJob);

            return trip;
        }

        public async Task<Trip> CompleteTrip(string id)
        {
            Trip trip = null;
            var proxyJob = await this.GetProxyJobByID(id);
            if (proxyJob != null)
            {
                proxyJob.JobCompleted = true;
                var uri = CreateRequestUri(_completeTripEndpoint, id);
                var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);
                var successMessage = await ExecuteRequestAsync<string>(requestMessage);
                if (!successMessage.IsNullOrEmpty())
                    trip = this.ProxyJobToTrip(proxyJob);
            }
            return trip;
        }

        public Task<string> CreateInvoiceForJobAsync(string tripID)
        {
            throw new NotImplementedException();
        }

        public Task<Trip> CreateTripAsync(Trip trip)
        {
            throw new NotImplementedException();
        }

        public Task DeclineDriverToCompany(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<Trip> DeclineTrip(string id, string reasonText)
        {
            Trip trip = null;
            var proxyJob = await this.GetProxyJobByID(id);
            if (proxyJob != null)
            {
                proxyJob.DeclineReason = reasonText;
                var uri = CreateRequestUri(_declineTripEndpoint, id);
                var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);
                // TODO: передать decline reason
                var successMessage = await ExecuteRequestAsync<string>(requestMessage);
                if (!successMessage.IsNullOrEmpty())
                    trip = this.ProxyJobToTrip(proxyJob);
            }
            return trip;
        }

        public Task<string> GetComcheckAsync(string tripID, ComcheckRequestType requestType)
        {
            throw new NotImplementedException();
        }

        public Task<ComcheckRequestState> GetComcheckStateAsync(string tripID, ComcheckRequestType requestType)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public Task<DriverState> GetDriverState()
        {
            throw new NotImplementedException();
        }

        public Task<Notification> GetNotification()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetPointsByDriverIDAsync(string driverID)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetPointsByJobIDAsync(string jobID)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSessionToken()
        {
            throw new NotImplementedException();
        }

        public void InitializeOwnerNotification()
        {
            throw new NotImplementedException();
        }

        public async Task<User> LogInAsync(string userName, string password)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = HttpMethod.Post;
            requestMessage.RequestUri = CreateRequestUri(_loginEndpoint);
            ProxyLogin loginInfo = new Data.API.ProxyLogin { UserName = userName, Password = password };
            string jsonContent = this.SerializeObject(loginInfo);
            requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var token = await ExecuteRequestAsync<ProxyToken>(requestMessage);

            if (token != null)
                return new User { UserName = userName, Phone = password, Token = token.Token, ID = token.Id };
            else
                return new User();
        }

        public async Task<Company> RegisterCompany(CompanyInfo companyInfo)
        {
            var userName = companyInfo.EMail;
            User user = null;
            if (await this.UserExistAsync(userName))
            {
                user = await this.LogInAsync(userName, companyInfo.MCCode.Trim());
            }
            else
            {
                user = new User
                {
                    UserName = userName,
                    Phone = companyInfo.Phone,
                    Password = companyInfo.MCCode.Trim(),
                    Email = companyInfo.EMail,
                    Role = UserRole.Owner,
                    Status = (int)DriverState.Joined
                };
                user = await this.SignUpAsync(user);
                // Login to take user token
                user = await this.LogInAsync(userName, companyInfo.MCCode);
            }

            _currentUser = user;
            var company = await this.SelectCompanyByName(companyInfo.Name);
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
            var userName = driverInfo.EMail;
            Company company = null;
            User user = null;
            if (await this.UserExistAsync(userName))
            {
                user = await this.LogInAsync(userName, driverInfo.Phone.Trim());
                _currentUser = user;
                company = await this.SelectUserCompanyAsync();
            }
            else
            {
                user = new User
                {
                    UserName = userName,
                    Phone = driverInfo.Phone,
                    Password = driverInfo.Phone,
                    Role = UserRole.Driver,
                    FirstName = driverInfo.FirstName,
                    LastName = driverInfo.LastName,
                    Status = (int)DriverState.Waiting,
                    Email = driverInfo.EMail
                };
                //user = await this.SignUpAsync(user); //TODO: uncomment
                // Login to take user token
                var userToken = await this.LogInAsync(userName, driverInfo.Phone);

                _currentUser = user;
                _currentUser.ID = userToken.ID;
                _currentUser.Token = userToken.Token;

                var companyId = await this.SelectProxyUserCompanyAsync(_currentUser.ID);
                ProxyCompany proxyCompany = null;
                if (!companyId.IsNullOrEmpty())
                    proxyCompany = await SelectCompanyByIdAsync(companyId);
                if (proxyCompany != null && proxyCompany.Id == driverInfo.Company.ID)
                    throw new Exception("Your are approved to company {0}.".format(proxyCompany.Name));

                if (proxyCompany == null)
                    proxyCompany = await this.SelectCompanyByIdAsync(driverInfo.Company.ID);

                var joined = await this.DriverIsJoinedToCompany(proxyCompany);
                if (!joined)
                    await this.JoinDriverToCompany(proxyCompany);

                company = this.ProxyCompanyToCompany(proxyCompany);
            }

            return company;
        }

        public Task<Trip> SaveLocation(string id, Position location)
        {
            throw new NotImplementedException();
        }

        public async Task<Trip[]> SelectActiveTrips()
        {
            var uri = CreateRequestUri(_selectActiveTripsEndpoint);
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);
            var successMessage = await ExecuteRequestAsync<string>(requestMessage);
            // TODO: возвращается successMessage, а нужен массив id
            if (!successMessage.IsNullOrEmpty())
            {
            }
            return new Trip[] { };
        }

        public Task<User[]> SelectBrockersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Company[]> SelectCompanies(string filter)
        {
            var uri = CreateRequestUri(_getCompaniesEndpoint);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            var responseArr = await ExecuteRequestAsync<ProxyCompany[]>(requestMessage);
            var proxyCompanies = responseArr.Where(c => c.Name.ToLower().Contains(filter.ToLower())).
                OrderBy(c => c.Name).Take(10).ToArray<ProxyCompany>();

            var companies = new List<Company>();
            foreach (var proxyComp in proxyCompanies)
            {
                var company = this.ProxyCompanyToCompany(proxyComp);
                companies.Add(company);
            }
            return companies.ToArray();
        }

        public async Task<Company> SelectCompanyByName(string name)
        {
            var uri = CreateRequestUri(_getCompanyByNameEndpoint, name);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            requestMessage.Headers.Add("Authorization", _currentUser.Token);
            var proxyCompany = await ExecuteRequestAsync<ProxyCompany>(requestMessage);
            var company = this.ProxyCompanyToCompany(proxyCompany);
            return company;
        }

        public Task<Trip[]> SelectCompletedTrips()
        {
            throw new NotImplementedException();
        }

        public Task<Position> SelectDriverPosition(string tripID)
        {
            throw new NotImplementedException();
        }

        public Task<User[]> SelectDriversAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Advance[]> SelectFuelAdvancesAsync(int requestType)
        {
            throw new NotImplementedException();
        }

        public Task<JobAlert[]> SelectJobAlertsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<JobPoint[]> SelectJobPointsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Photo[]> SelectPhotosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> SelectRequestedUser(string companyID)
        {
            throw new NotImplementedException();
        }

        public async Task<Trip> SelectTripByID(string id)
        {
            Trip trip = null;
            var proxyJob = await this.GetProxyJobByID(id);
            if (proxyJob != null)
                trip = this.ProxyJobToTrip(proxyJob);
            return trip;
        }

        public async Task<Company> SelectUserCompanyAsync()
        {
            Company company = null;

            string companyId = await this.SelectProxyUserCompanyAsync(_currentUser.ID);
            if (!companyId.IsNullOrEmpty())
            {
                var proxyCompany = await SelectCompanyByIdAsync(companyId);
                if (proxyCompany != null)
                    company = ProxyCompanyToCompany(proxyCompany);
            }

            return company;
        }

        public Task SendComcheckRequestAsync(string tripID, ComcheckRequestType requestType)
        {
            throw new NotImplementedException();
        }

        public Task SendJobAlertAsync(string tripID, int alertType, string alertText)
        {
            throw new NotImplementedException();
        }

        public Task SendNotification(Trip trip, string message)
        {
            throw new NotImplementedException();
        }

        public async Task<Trip> SendPhoto(string id, byte[] data, string kind)
        {
            Trip trip = null;
            ProxyJob proxyJob = await this.GetProxyJobByID(id);
            if (proxyJob != null)
            {
                var photo = new ProxyPhoto
                {
                    Kind = kind,
                    Data = data,
                    //Job = job,
                    //Company = job.Company
                };
                await this.SaveProxyPhoto(photo);
                trip = this.ProxyJobToTrip(proxyJob);
            }
            return trip;
        }

        public Task SetAdvanceStateAsync(Advance advance)
        {
            throw new NotImplementedException();
        }

        public Task SetJobAlertIsViewedAsync(string jobAlertID, bool isViewed)
        {
            throw new NotImplementedException();
        }

        public async Task<User> SignUpAsync(User user)
        {
            var uri = CreateRequestUri(_signUpUserEndpoint);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            ProxyUser proxyUser = new ProxyUser
            {
                UserName = user.UserName,
                Phone = user.Phone,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString().ToLower(),
                Status = user.Status
            };

            string jsonContent = this.SerializeObject(proxyUser);
            requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            proxyUser = await ExecuteRequestAsync<ProxyUser>(requestMessage);

            return ProxyUserToUser(proxyUser);
        }

        public Task<Trip> TripInDelivery(string id, int minutes)
        {
            throw new NotImplementedException();
        }

        public Task<Trip> TripInPickup(string id, int minutes)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserExistAsync(string userName)
        {
            Uri uri = CreateRequestUri(_getUserByNameEndpoint, userName);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            var user = await ExecuteRequestAsync<User>(requestMessage);
            return (user != null);
        }
        #endregion
    }
}
