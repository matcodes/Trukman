﻿using System;
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
using KAS.Trukman.Data.API.Requests;
using System.Net;
using KAS.Trukman.Data.API.Responses;

namespace KAS.Trukman.Storage
{
    public class RestAPIExternalStorage : IExternalStorage
    {
        private static readonly string API_BASE_URI = "http://193.124.114.38/Trukman.Server/";

        private static readonly string OWNER_LOGIN_ENDPOINT = "accounts/owner";
        private static readonly string DRIVER_LOGIN_ENDPOINT = "accounts/driver";
        private static readonly string VERIFICATION_ENDPOINT = "accounts/verification";
        private static readonly string SELECT_COMPANIES_BY_FILTER_ENDPOINT = "accounts/selectcompaniesbyfilter";

        private static readonly string GET_DRIVER_REQUESTS_ENDPOINT = "owners/getdriverrequests";
        private static readonly string ANSWER_DRIVER_REQUEST_ENDPOINT = "owners/answerdriverrequest";
        private static readonly string CREATE_TASK_ENDPOINT = "owners/createtask";
        private static readonly string CREATE_TASK_REQUEST_ENDPOINT = "owners/createtaskrequest";
        private static readonly string CHECK_TASK_REQUEST_ENDPOINT = "owners/checktaskrequest";

        private static readonly string ADD_DRIVER_REQUEST_ENDPOINT = "drivers/adddriverrequest";
        private static readonly string GET_DRIVER_COMPANY_ENDPOINT = "drivers/getdrivercompany";
        private static readonly string GET_LAST_DRIVER_REQUEST_ENDPOINT = "drivers/getlastdriverrequest";
        private static readonly string FIND_TASK_REQUEST_ENDPOINT = "drivers/findtaskrequest";
        private static readonly string ANSWER_TASK_REQUEST_ENDPOINT = "drivers/answertaskrequest";

        private User _currentUser = null;
        private string _token { get; set; }

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

        private async Task<T> ExecuteRequestAsync<T>(HttpRequestMessage requestMessage) where T : BaseResponse
        {
            T result = default(T);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    if (_currentUser != null && !string.IsNullOrEmpty(_currentUser.Token))
                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _currentUser.Token);

                    var response = await httpClient.SendAsync(requestMessage).ConfigureAwait(false);
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        if (content != null)
                            result = DeserializeObject<T>(content);
                        else
                            throw new Exception("Response content is Empty!");
                    }
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.BadGateway)
                            throw new Exception("Server not available!");
                        else if (response.StatusCode == HttpStatusCode.Unauthorized)
                            throw new Exception("Unauthorized request.");
                        else
                            throw new Exception(response.ReasonPhrase);
                    }
                }
            }
            catch (TaskCanceledException canceledException)
            {
                Console.WriteLine(canceledException.Message);
                throw new Exception("The server is not available (Timeout connection).");
            }

            if (!String.IsNullOrEmpty(result.ErrorText))
                throw new Exception(result.ErrorText);

            return result;
        }

        private Uri CreateRequestUri(string resource, params string[] args)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(API_BASE_URI);

            if (args != null)
                builder.AppendFormat(resource, args);
            else
                builder.Append(resource);

            string url = builder.ToString();
            return new Uri(url);
        }

        //private async Task<Company> SaveCompanyAsync(Company company)
        //{
        //    var saved = await this.SelectCompanyByIdAsync(company.ID);
        //    if (saved == null)
        //        saved = new ProxyCompany();
        //    saved.Owner = _currentUser.UserName;
        //    saved.Name = company.Name;
        //    saved.DisplayName = company.DisplayName;
        //    saved.FleetSize = company.FleetSize;

        //    var uri = CreateRequestUri(_saveCompanyEndpoint);
        //    var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
        //    string jsonContent = SerializeObject(saved);
        //    requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8);
        //    var proxyCompany = await ExecuteRequestAsync<ProxyCompany>(requestMessage);

        //    return this.ProxyCompanyToCompany(proxyCompany);
        //}

        //private async Task<bool> DriverIsJoinedToCompany(ProxyCompany company)
        //{
        //    var joined = false;

        //    //var companyUser = await company.Drivers.Query
        //    //    .WhereEqualTo("username", ParseUser.CurrentUser.Username)
        //    //    .FirstOrDefaultAsync();

        //    //joined = (companyUser != null);
        //    return await Task.FromResult(joined);
        //}

        //private async Task JoinDriverToCompany(ProxyCompany company)
        //{
        //    //var companyUser = await company.Requestings.Query
        //    //    .WhereEqualTo("username", ParseUser.CurrentUser.Username)
        //    //    .FirstOrDefaultAsync();

        //    //if (companyUser == null)
        //    //{
        //    //    company.Requestings.Add(ParseUser.CurrentUser);
        //    //    await company.SaveAsync();
        //    //}

        //}

        //private async Task<string> SelectProxyUserCompanyAsync(string userId)
        //{
        //    var uri = CreateRequestUri(_getSelectUserCompanyEndpoint, userId);
        //    var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

        //    //TODO: проверить возвращаются ли компании для водителей, которые в статусе waiting
        //    var companyId = await ExecuteRequestAsync<string>(requestMessage);
        //    //if (parseCompany == null)
        //    //{
        //    //    query = new ParseQuery<ParseCompany>()
        //    //        .WhereEqualTo("requesting", ParseUser.CurrentUser);
        //    //    parseCompany = await query.FirstOrDefaultAsync();
        //    //}

        //    return companyId;
        //}

        private async Task<Owner> GetDriverCompany(Guid driverId)
        {
            var getDriverCompanyRequest = new GetDriverCompanyRequest
            {
                DriverId = driverId
            };
            var requestContent = SerializeObject(getDriverCompanyRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(GET_DRIVER_COMPANY_ENDPOINT, null);
            var result = await ExecuteRequestAsync<GetDriverCompanyResponse>(request);
            return result.Company;
        }

        //private async Task<ProxyCompany> SelectCompanyByIdAsync(string companyId)
        //{
        //    var uri = CreateRequestUri(_getCompanyByIdEndpoint, companyId);
        //    var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        //    var proxyCompany = await ExecuteRequestAsync<ProxyCompany>(requestMessage);
        //    return proxyCompany;
        //}

        //private Company ProxyCompanyToCompany(ProxyCompany proxyCompany)
        //{
        //    return new Company
        //    {
        //        DisplayName = proxyCompany.Name,
        //        ID = proxyCompany.Id,
        //        FleetSize = proxyCompany.FleetSize,
        //        Name = proxyCompany.Name
        //    };
        //}

        //private User ProxyUserToUser(ProxyUser proxyUser)
        //{
        //    return new User
        //    {
        //        Email = proxyUser.EMail,
        //        FirstName = proxyUser.FirstName,
        //        LastName = proxyUser.LastName,
        //        Phone = proxyUser.Phone,
        //        Password = proxyUser.Password,
        //        Role = (UserRole)(Enum.Parse(typeof(UserRole), proxyUser.Role, true)),
        //        Status = proxyUser.Status,
        //        UserName = proxyUser.UserName,
        //        ID = proxyUser.Id
        //    };
        //}

        //private async Task<ProxyJob> GetProxyJobByID(string id)
        //{
        //    var uri = CreateRequestUri(_getJobByIdEndpoint, id);
        //    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        //    var proxyJob = await ExecuteRequestAsync<ProxyJob>(requestMessage);
        //    return proxyJob;
        //}

        //private async Task SaveProxyJob(ProxyJob proxyJob)
        //{
        //    var uri = CreateRequestUri(_save)

        //}

        //private Trip ProxyJobToTrip(ProxyJob proxyJob)
        //{
        //    //var shipper = new Contractor
        //    //{
        //    //    ID = (proxyJob.Shipper != null ? parseJob.Shipper.ObjectId : Guid.NewGuid().ToString()),
        //    //    Name = (parseJob.Shipper != null ? parseJob.Shipper.Name : ""),
        //    //    Phone = (parseJob.Shipper != null ? parseJob.Shipper.Phone : ""),
        //    //    Fax = (parseJob.Shipper != null ? parseJob.Shipper.Fax : ""),
        //    //    Address = (parseJob.Shipper != null ? parseJob.Shipper.Address : parseJob.FromAddress),
        //    //    SpecialInstruction = (parseJob.Shipper != null ? parseJob.Shipper.SpecialInstruction : "")
        //    //};
        //    //var receiver = new Contractor
        //    //{
        //    //    ID = (parseJob.Receiver != null ? parseJob.Receiver.ObjectId : Guid.NewGuid().ToString()),
        //    //    Name = (parseJob.Receiver != null ? parseJob.Receiver.Name : ""),
        //    //    Phone = (parseJob.Receiver != null ? parseJob.Receiver.Phone : ""),
        //    //    Fax = (parseJob.Receiver != null ? parseJob.Receiver.Fax : ""),
        //    //    Address = (parseJob.Receiver != null ? parseJob.Receiver.Address : parseJob.ToAddress),
        //    //    SpecialInstruction = (parseJob.Receiver != null ? parseJob.Receiver.SpecialInstruction : "")
        //    //};
        //    //User driver = null;
        //    //if (parseJob.Driver != null)
        //    //    driver = this.ParseUserToUser(parseJob.Driver);
        //    //User broker = null;
        //    //if (parseJob.Broker != null)
        //    //    broker = this.ParseUserToUser(parseJob.Broker);
        //    //Company company = null;
        //    //if (parseJob.Company != null)
        //    //    company = this.ParseCompanyToCompany(parseJob.Company);

        //    var trip = new Trip
        //    {
        //        ID = proxyJob.Id,
        //        //ID = proxyJob.ObjectId,
        //        //DeclineReason = parseJob.DeclineReason,
        //        //DeliveryDatetime = parseJob.DeliveryDatetime,
        //        DriverAccepted = proxyJob.DriverAccepted,
        //        //IsDelivery = (parseJob.DriverOnTimeDelivery != 0),
        //        //IsPickup = (parseJob.DriverOnTimePickup != 0),
        //        //JobCancelled = parseJob.JobCancelled,
        //        //JobCompleted = parseJob.JobCompleted,
        //        //IsDeleted = parseJob.IsDeleted,
        //        //PickupDatetime = parseJob.PickupDatetime,
        //        //Points = parseJob.Price,
        //        //Shipper = shipper,
        //        //Receiver = receiver,
        //        //JobRef = parseJob.JobRef,
        //        //FromAddress = parseJob.FromAddress,
        //        //ToAddress = parseJob.ToAddress,
        //        //Weight = parseJob.Weight,
        //        //Location = new Position(parseJob.Location.Latitude, parseJob.Location.Longitude),
        //        //UpdateTime = (parseJob.UpdatedAt != null ? (DateTime)parseJob.UpdatedAt : DateTime.Now),
        //        //Driver = driver,
        //        //Broker = broker,
        //        //Company = company,
        //        //InvoiceUri = (parseJob.Invoice != null && parseJob.Invoice.File != null ? parseJob.Invoice.File.Url.ToString() : null),
        //        //DriverDisplayName = (driver != null ? driver.UserName : "")
        //    };

        //    return trip;
        //}

        //private async Task SaveProxyPhoto(ProxyPhoto photo)
        //{
        //    var uri = CreateRequestUri(_saveJobPhotoEndpoint);
        //    var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
        //    var content = SerializeObject(photo);
        //    requestMessage.Content = new StringContent(content, Encoding.UTF8);

        //    var successMessage = await ExecuteRequestAsync<string>(requestMessage);
        //    //if (!successMessage.IsNullOrEmpty())
        //    //{

        //    //}
        //}

        #region IExternalStorage
        public async Task AcceptDriverToCompany(string companyID, string driverID)
        {
            await AnswerDriverRequest(Guid.Parse(companyID), Guid.Parse(driverID), true);
        }

        public async Task<Trip> AcceptTrip(string id)
        {
            Trip trip = null;

            //var proxyJob = await this.GetProxyJobByID(id);
            //if (proxyJob != null)
            //{
            //    proxyJob.DriverAccepted = true;

            //    var uri = CreateRequestUri(_acceptJobEndpoint, id);
            //    var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            //    var successMessage = await ExecuteRequestAsync<string>(requestMessage);
            //    if (!successMessage.IsNullOrEmpty())
            //        trip = this.ProxyJobToTrip(proxyJob);
            //}
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

        public void Become(User user)
        {
            _currentUser = user;
        }

        public Task CancelComcheckRequestAsync(string tripID, ComcheckRequestType requestType)
        {
            throw new NotImplementedException();
        }

        public async Task<Trip> CheckNewTripForDriver(string userID)
        {
            Trip trip = null;

            //var uri = CreateRequestUri(_getNewJobForDriver);
            //var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            //var tripId = await ExecuteRequestAsync<string>(requestMessage);
            //var proxyJob = await this.GetProxyJobByID(tripId);
            //if (proxyJob != null)
            //    trip = this.ProxyJobToTrip(proxyJob);

            return trip;
        }

        public async Task<Trip> CompleteTrip(string id)
        {
            Trip trip = null;
            //var proxyJob = await this.GetProxyJobByID(id);
            //if (proxyJob != null)
            //{
            //    proxyJob.JobCompleted = true;
            //    var uri = CreateRequestUri(_completeTripEndpoint, id);
            //    var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);
            //    var successMessage = await ExecuteRequestAsync<string>(requestMessage);
            //    if (!successMessage.IsNullOrEmpty())
            //        trip = this.ProxyJobToTrip(proxyJob);
            //}
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

        public async Task<bool> AnswerDriverRequest(Guid ownerId, Guid driverId, bool isAllowed)
        {
            var answerDriverRequestRequest = new AnswerDriverRequestRequest
            {
                OwnerId = ownerId,
                DriverId = driverId,
                IsAllowed = isAllowed
            };
            var requestContent = SerializeObject(answerDriverRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(ANSWER_DRIVER_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<AnswerDriverRequestResponse>(request);
            return true;
        }

        public async Task DeclineDriverToCompany(string companyID, string driverID)
        {
            await AnswerDriverRequest(Guid.Parse(companyID), Guid.Parse(driverID), false);
        }

        public async Task<Trip> DeclineTrip(string id, string reasonText)
        {
            Trip trip = null;
            //var proxyJob = await this.GetProxyJobByID(id);
            //if (proxyJob != null)
            //{
            //    proxyJob.DeclineReason = reasonText;
            //    var uri = CreateRequestUri(_declineTripEndpoint, id);
            //    var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);
            //    // TODO: передать decline reason
            //    var successMessage = await ExecuteRequestAsync<string>(requestMessage);
            //    if (!successMessage.IsNullOrEmpty())
            //        trip = this.ProxyJobToTrip(proxyJob);
            //}
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
            return Task.FromResult(_currentUser);
        }

        private async Task<DriverRequest> GetLastDriverRequest(Guid ownerId, Guid driverId)
        {
            var getLastDriverRequestRequest = new GetLastDriverRequestRequest
            {
                OwnerId = ownerId,
                DriverId = driverId
            };
            var requestContent = SerializeObject(getLastDriverRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(GET_LAST_DRIVER_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<GetLastDriverRequestResponse>(request);
            return result.DriverRequest;
        }

        public async Task<DriverState> GetDriverState(string companyID, string driverID)
        {
            var lastDriverRequest = await GetLastDriverRequest(Guid.Parse(companyID), Guid.Parse(driverID));
            var answer = lastDriverRequest.Answer;

            var state = DriverState.Waiting;
            if (answer == (int)DriverRequestAnswers.Accept)
                state = DriverState.Joined;
            else if (answer == (int)DriverRequestAnswers.Decline)
                state = DriverState.Declined;

            _currentUser.Status = (int)state;
            return state;
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
            if (_currentUser != null)
                return Task.FromResult(_currentUser.Token);
            else
                return Task.FromResult(string.Empty);
        }

        public void InitializeOwnerNotification()
        {
            throw new NotImplementedException();
        }

        public Task<User> LogInAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        private async Task<Owner> OwnerLoginAsync(Owner owner)
        {
            var ownerLoginRequest = new OwnerLoginRequest
            {
                Owner = owner
            };
            var requestContent = SerializeObject(ownerLoginRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(OWNER_LOGIN_ENDPOINT, null);
            var result = await ExecuteRequestAsync<OwnerLoginResponse>(request);
            _token = result.Token;
            return result.Owner;
        }

        private async Task<bool> Verification(Guid accountId, string code)
        {
            var verificationRequest = new VerificationRequest
            {
                AccountId = accountId,
                Code = code
            };
            var requestContent = SerializeObject(verificationRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(VERIFICATION_ENDPOINT, null);
            var result = await ExecuteRequestAsync<DriverLoginResponse>(request);
            _token = result.Token;
            return true;
        }

        public async Task<Company> RegisterCompany(CompanyInfo companyInfo)
        {
            var owner = new Owner
            {
                Name = companyInfo.Name,
                Address = companyInfo.Address,
                DBA = companyInfo.DBA,
                Email = companyInfo.EMail,
                Phone = companyInfo.Phone,
                FeetSize = companyInfo.FleetSize
            };
            owner = await OwnerLoginAsync(owner);
            Console.WriteLine("Компания: {0}", owner);
            Console.WriteLine();

            if (string.IsNullOrEmpty(_token))
            {
                await Verification(owner.Id, "5555");
                Console.WriteLine("Телефон подтвержден: {0}", owner.Phone);
                Console.WriteLine();
            }

            Console.WriteLine("Token: {0}", _token);
            Console.WriteLine();

            _currentUser = new User
            {
                ID = owner.Id.ToString(),
                UserName = owner.Name, // TODO: phone???
                Role = UserRole.Owner,
                Email = owner.Email,
                Phone = owner.Phone,
                Token = _token
            };

            var company = new Company
            {
                DisplayName = owner.Name,
                FleetSize = owner.FeetSize,
                ID = owner.Id.ToString(),
                Name = owner.Name,
                UpdateTime = DateTime.Now
            };

            return company;
        }

        private async Task<Driver> DriverLoginAsync(Driver driver)
        {
            var driverLoginRequest = new DriverLoginRequest
            {
                Driver = driver
            };
            var requestContent = SerializeObject(driverLoginRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(DRIVER_LOGIN_ENDPOINT, null);
            var result = await ExecuteRequestAsync<DriverLoginResponse>(request);
            _token = result.Token;
            return result.Driver;
        }

        private async Task<bool> AddDriverRequest(Guid ownerId, Guid driverId)
        {
            var addDriverRequest = new AddDriverRequestRequest
            {
                OwnerId = ownerId,
                DriverId = driverId
            };
            var requestContent = SerializeObject(addDriverRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(ADD_DRIVER_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<AddDriverRequestResponse>(request);
            return true;
        }

        public async Task<Company> RegisterDriver(DriverInfo driverInfo)
        {
            var _driver = new Driver
            {
                FirstName = driverInfo.FirstName,
                LastName = driverInfo.LastName,
                Phone = driverInfo.Phone,
            };
            _driver = await DriverLoginAsync(_driver);
            Console.WriteLine("Водитель: {0}", _driver);
            Console.WriteLine();

            if (string.IsNullOrEmpty(_token))
            {
                await Verification(_driver.Id, "5555");
                Console.WriteLine("Телефон подтвержден: {0}", _driver.Phone);
                Console.WriteLine();
            }

            Console.WriteLine("Token: {0}", _token);
            Console.WriteLine();

            _currentUser = new User
            {
                ID = _driver.Id.ToString(),
                UserName = string.Format("{0} {1}", driverInfo.FirstName.Trim(), driverInfo.LastName.Trim()).ToLower(), // TODO: phone???
                Phone = driverInfo.Phone,
                Role = UserRole.Driver,
                FirstName = driverInfo.FirstName,
                LastName = driverInfo.LastName,
                Email = driverInfo.EMail,
                Token = _token,
                Status = (int)DriverState.Waiting
            };

            var _company = await GetDriverCompany(_driver.Id);
            if ((_company != null) && (_company.Id == Guid.Parse(driverInfo.Company.ID)))
                throw new Exception(String.Format("Your are approved to company {0}.", _company.Name));

            if (_company == null)
            {
                var driverRequest = await GetLastDriverRequest(Guid.Parse(driverInfo.Company.ID), _driver.Id);
                if ((driverRequest == null) || (driverRequest.Answer != (int)DriverRequestAnswers.None))
                {
                    await AddDriverRequest(Guid.Parse(driverInfo.Company.ID), _driver.Id);
                    Console.WriteLine("Добавлен запрос на прием на работу в компанию {0}...", driverInfo.Company.Name);
                    Console.WriteLine();
                }

                return driverInfo.Company;
            }
            else
            {
                return new Company
                {
                    ID = _company.Id.ToString(),
                    DisplayName = _company.DBA,
                    Name = _company.Name,
                    FleetSize = _company.FeetSize
                };
            }
        }

        public Task<Trip> SaveLocation(string id, Position location)
        {
            throw new NotImplementedException();
        }

        public async Task<Trip[]> SelectActiveTrips()
        {
            //var uri = CreateRequestUri(_selectActiveTripsEndpoint);
            //var requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);
            //var successMessage = await ExecuteRequestAsync<string>(requestMessage);
            //// TODO: возвращается successMessage, а нужен массив id
            //if (!successMessage.IsNullOrEmpty())
            //{
            //}
            return new Trip[] { };
        }

        public Task<User[]> SelectBrockersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Company[]> SelectCompanies(string filter)
        {
            var verificationRequest = new SelectCompaniesByFilterRequest
            {
                Filter = filter
            };
            var requestContent = SerializeObject(verificationRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(SELECT_COMPANIES_BY_FILTER_ENDPOINT, null);
            var result = await ExecuteRequestAsync<SelectCompaniesByFilterResponse>(request);
            List<Company> companies = new List<Company>();
            foreach (var owner in result.Companies)
            {
                var company = new Company
                {
                    ID = owner.Id.ToString(),
                    Name = owner.Name,
                    DisplayName = owner.DBA,
                    FleetSize = owner.FeetSize
                };

                companies.Add(company);
            }

            return companies.ToArray();
        }

        public async Task<Company> SelectCompanyByName(string name)
        {
            //var uri = CreateRequestUri(_getCompanyByNameEndpoint, name);
            //HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            //requestMessage.Headers.Add("Authorization", _currentUser.Token);
            //var proxyCompany = await ExecuteRequestAsync<ProxyCompany>(requestMessage);
            //var company = this.ProxyCompanyToCompany(proxyCompany);
            //return company;
            return null;
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

        private async Task<DriverRequest[]> GetDriverRequests(Guid ownerId)
        {
            var getDriverRequestsRequest = new GetDriverRequestsRequest
            {
                OwnerId = ownerId
            };
            var requestContent = SerializeObject(getDriverRequestsRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(GET_DRIVER_REQUESTS_ENDPOINT, null);
            var result = await ExecuteRequestAsync<GetDriverRequestsResponse>(request);
            return result.DriverRequests;
        }

        public async Task<User> SelectRequestedUser(string companyID)
        {
            var driverRequests = await GetDriverRequests(Guid.Parse(companyID));
            if (driverRequests.Length > 0)
            {
                return new User
                {
                    ID = driverRequests[0].Driver.Id.ToString(),
                    UserName = string.Format("{0} {1}", driverRequests[0].Driver.FirstName, driverRequests[0].Driver.LastName),
                    //Role = (UserRole)role,
                    //Status = status,
                    FirstName = driverRequests[0].Driver.FirstName,
                    LastName = driverRequests[0].Driver.LastName
                };
            }

            return null;
        }

        public async Task<Trip> SelectTripByID(string id)
        {
            Trip trip = null;
            //var proxyJob = await this.GetProxyJobByID(id);
            //if (proxyJob != null)
            //    trip = this.ProxyJobToTrip(proxyJob);
            return trip;
        }

        public async Task<Company> SelectUserCompanyAsync()
        {
            Company company = null;

            //string companyId = await this.SelectProxyUserCompanyAsync(_currentUser.ID);
            //if (!companyId.IsNullOrEmpty())
            //{
            //    var proxyCompany = await SelectCompanyByIdAsync(companyId);
            //    if (proxyCompany != null)
            //        company = ProxyCompanyToCompany(proxyCompany);
            //}

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
            //ProxyJob proxyJob = await this.GetProxyJobByID(id);
            //if (proxyJob != null)
            //{
            //    var photo = new ProxyPhoto
            //    {
            //        Kind = kind,
            //        Data = data,
            //        //Job = job,
            //        //Company = job.Company
            //    };
            //    await this.SaveProxyPhoto(photo);
            //    trip = this.ProxyJobToTrip(proxyJob);
            //}
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
            //var uri = CreateRequestUri(_signUpUserEndpoint);
            //HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            //ProxyUser proxyUser = new ProxyUser
            //{
            //    UserName = user.UserName,
            //    Phone = user.Phone,
            //    Password = user.Password,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Role = user.Role.ToString().ToLower(),
            //    Status = user.Status
            //};

            //string jsonContent = this.SerializeObject(proxyUser);
            //requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //proxyUser = await ExecuteRequestAsync<ProxyUser>(requestMessage);

            //return ProxyUserToUser(proxyUser);
            return null;
        }

        public Task<Trip> TripInDelivery(string id, int minutes)
        {
            throw new NotImplementedException();
        }

        public Task<Trip> TripInPickup(string id, int minutes)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExistAsync(string userName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
