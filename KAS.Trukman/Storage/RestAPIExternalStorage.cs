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

        private static readonly string GET_OWNER_COMPANY_ENDPOINT = "owners/getcompany";
        private static readonly string GET_DRIVER_REQUESTS_ENDPOINT = "owners/getdriverrequests";
        private static readonly string ANSWER_DRIVER_REQUEST_ENDPOINT = "owners/answerdriverrequest";
        private static readonly string SELECT_DRIVERS_ENDPOINT = "owners/selectdrivers";
        //private static readonly string CREATE_TASK_ENDPOINT = "owners/createtask";
        //private static readonly string CREATE_TASK_REQUEST_ENDPOINT = "owners/createtaskrequest";
        //private static readonly string CHECK_TASK_REQUEST_ENDPOINT = "owners/checktaskrequest";
        //private static readonly string CANCEL_TASK_REQUEST_ENDPOINT = "owners/canceltaskrequest";

        private static readonly string ADD_DRIVER_REQUEST_ENDPOINT = "drivers/adddriverrequest";
        private static readonly string CANCEL_DRIVER_REQUEST_ENDPOINT = "drivers/canceldriverrequest";
        private static readonly string GET_DRIVER_COMPANY_ENDPOINT = "drivers/getdrivercompany";
        private static readonly string GET_LAST_DRIVER_REQUEST_ENDPOINT = "drivers/getlastdriverrequest";
        private static readonly string FIND_TASK_REQUEST_ENDPOINT = "drivers/findtaskrequest";
        private static readonly string CHECK_DRIVER_TASK_REQUEST_ENDPOINT = "drivers/checktaskrequest";
        private static readonly string ANSWER_TASK_REQUEST_ENDPOINT = "drivers/answertaskrequest";
        private static readonly string GET_DRIVER_POINTS_ENDPOINT = "drivers/getpointsbydriver";
        private static readonly string TASK_ARRIVAL_LOADING_ENDPOINT = "drivers/taskarrivalloading";
        private static readonly string TASK_ARRIVAL_UNLOADING_ENDPOINT = "drivers/taskarrivalunloading";
        private static readonly string TASK_DONE_LOADING_ENDPOINT = "drivers/taskdoneloading";
        private static readonly string TASK_DONE_UNLOADING_ENDPOINT = "drivers/taskdoneunloading";
        private static readonly string TASK_DONE_ENDPOINT = "drivers/taskdone";
        private static readonly string ADD_TASK_LOCATION_ENDPOINT = "drivers/addtasklocation";

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

        private Company OwnerToCompany(Owner owner)
        {
            return new Company
            {
                DisplayName = owner.Name,
                FleetSize = owner.FeetSize,
                ID = owner.Id.ToString(),
                Name = owner.Name,
            };
        }

        #region IExternalStorage
        public async Task AcceptDriverToCompany(string companyID, string driverID)
        {
            await AnswerDriverRequest(Guid.Parse(companyID), Guid.Parse(driverID), true);
        }

        private async Task<TrukmanTask> AnswerTaskRequest(Guid taskRequestId, int answer, 
            int declineReason = (int)TaskRequestDeclineReasons.None, string declineText = "")
        {
            var answerTaskRequestRequest = new AnswerTaskRequestRequest
            {
                TaskRequestId = taskRequestId,
                Answer = answer,
                DeclineReason = declineReason,
                DeclineText = declineText
            };
            var requestContent = SerializeObject(answerTaskRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(ANSWER_TASK_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<AnswerTaskRequestResponse>(request);
            return result.Task;
        }

        public async Task<Trip> AcceptTrip(string id)
        {
            var taskRequest = await this.GetTaskRequestByTaskID(id);
            var trukmanTask = await this.AnswerTaskRequest(taskRequest.Id, (int)TaskRequestAnswers.Accept);

            Trip trip = null;
            if (trukmanTask != null)
            {
                taskRequest.Answer = (int)TaskRequestAnswers.Accept;
                taskRequest.Task = trukmanTask;
                trip = TaskRequestToTrip(taskRequest);
            }

            return trip;
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

        private Trip TaskRequestToTrip(TaskRequest taskRequest)
        {
            var shipper = new Contractor
            {
                ID = Guid.NewGuid().ToString(),
                Name = taskRequest.Task.LoadingName,
                Phone = "",
                Fax = "",
                Address = taskRequest.Task.LoadingAddress,
                SpecialInstruction = ""
            };
            var receiver = new Contractor
            {
                ID = Guid.NewGuid().ToString(),
                Name = taskRequest.Task.UnloadingName,
                Phone = "",
                Fax = "",
                Address = taskRequest.Task.UnloadingAddress,
                SpecialInstruction = ""
            };

            TaskLocation location = null;
            //if (taskRequest.Task.TaskLocations != null)
            //    location = taskRequest.Task.TaskLocations.OrderByDescending(l => l.CreateTime).FirstOrDefault();

            return new Trip
            {
                ID = taskRequest.TaskId.ToString(),
                DeclineReason = "", // taskRequest.DeclineReason.ToString(),
                DeliveryDatetime = taskRequest.Task.UnloadingPlanTime.ToLocalTime(),
                DriverAccepted = (taskRequest.Answer == (int)TaskRequestAnswers.Accept),
                IsDelivery = (taskRequest.Task.UnloadingRealTime.GetValueOrDefault() != DateTime.MinValue),
                IsPickup = (taskRequest.Task.LoadingRealTime.GetValueOrDefault() != DateTime.MinValue),
                JobCancelled = taskRequest.IsCancelled,
                //JobCompleted = taskRequest.JobCompleted,
                //IsDeleted = taskRequest.IsDeleted,
                PickupDatetime = taskRequest.Task.LoadingPlanTime.ToLocalTime(),
                Points = taskRequest.Task.PlanPoints,
                Shipper = shipper,
                Receiver = receiver,
                JobRef = "", //taskRequest.Task.JobRef, 
                FromAddress = taskRequest.Task.LoadingAddress,
                ToAddress = taskRequest.Task.UnloadingAddress,
                //Weight = taskRequest.Task.Weight,
                Location = (location != null ? new Position((double)location.Latitude, (double)location.Longitude) : default(Position)),
                UpdateTime = DateTime.Now,
                Driver = new User
                {
                    ID = taskRequest.Driver.Id.ToString(),
                    UserName = string.Format("{0} {1}", taskRequest.Driver.FirstName, taskRequest.Driver.LastName),
                    FirstName = taskRequest.Driver.FirstName,
                    LastName = taskRequest.Driver.LastName,
                    Phone = taskRequest.Driver.Phone
                },
                Broker = new User(), // broker,
                Company = this.OwnerToCompany(taskRequest.Task.Owner),
                InvoiceUri = "", //(parseJob.Invoice != null && parseJob.Invoice.File != null ? parseJob.Invoice.File.Url.ToString() : null),
                DriverDisplayName = (taskRequest.Driver != null ? string.Format("{0} {1}", taskRequest.Driver.FirstName, taskRequest.Driver.LastName) : "")
            };
        }

        public async Task<Trip> CheckNewTripForDriver(string userID)
        {
            var findTaskRequestRequest = new FindTaskRequestRequest
            {
                DriverId = Guid.Parse(userID)
            };
            var requestContent = SerializeObject(findTaskRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(FIND_TASK_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<FindTaskRequestResponse>(request);
            
            Trip trip = null;
            if (result.TaskRequest != null)
                trip = TaskRequestToTrip(result.TaskRequest);

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

        public async Task<Trip> DeclineTrip(string id, int declineReason, string reasonText)
        {
            var taskRequest = await this.GetTaskRequestByTaskID(id);
            var trukmanTask = await this.AnswerTaskRequest(taskRequest.Id, (int)TaskRequestAnswers.Decline, declineReason, reasonText);

            Trip trip = null;
            if (trukmanTask != null)
            {
                taskRequest.Task = trukmanTask;
                trip = TaskRequestToTrip(taskRequest);
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

        public async Task CancelDriverRequest(string companyID, string driverID)
        {
            var cancelDriverRequest = new CancelDriverRequestRequest
            {
                OwnerId = Guid.Parse(companyID),
                DriverId = Guid.Parse(driverID)
            };
            var requestContent = SerializeObject(cancelDriverRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(CANCEL_DRIVER_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<CancelDriverRequestResponse>(request);
        }

        public Task<Notification> GetNotification()
        {
            return Task.FromResult<Notification>(default(Notification));
            // TODO: throw new NotImplementedException();
        }

        public async Task<int> GetPointsByDriverIDAsync(string driverID)
        {
            var getPointsByDriverRequest = new GetPointsByDriverRequest
            {
                DriverId = Guid.Parse(driverID)
            };
            var requestContent = SerializeObject(getPointsByDriverRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(GET_DRIVER_POINTS_ENDPOINT, null);
            var response = await ExecuteRequestAsync<GetPointsByDriverResponse>(request);

            return response.Points;
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
                UserName = owner.Name,
                Role = UserRole.Owner,
                Email = owner.Email,
                Phone = owner.Phone,
                Token = _token
            };

            var company = OwnerToCompany(owner);
            company.UpdateTime = DateTime.Now;

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
            if ((_company != null) && (_company.Id != Guid.Parse(driverInfo.Company.ID)))
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
                await this.GetDriverState(_company.Id.ToString(), _currentUser.ID);

                return OwnerToCompany(_company);
            }
        }

        public async Task<Trip> AddLocation(string id, Position location)
        {
            var addTaskLocationRequest = new AddTaskLocationRequest
            {
                TaskId = Guid.Parse(id),
                Latitude = (decimal)location.Latitude,
                Longtitude = (decimal)location.Longitude,
                Speed = 0,
                CreateTime = DateTime.UtcNow
            };
            var requestContent = SerializeObject(addTaskLocationRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(ADD_TASK_LOCATION_ENDPOINT, null);
            var result = await ExecuteRequestAsync<AddTaskLocationResponse>(request);

            Trip trip = null;
            if (result.TaskRequest != null)
                trip = this.TaskRequestToTrip(result.TaskRequest);

            trip.Location = location;

            return trip;
        }

        public async Task<Trip> SaveLocation(string id, Position location)
        {
            // TODO: refresh last location
            Trip trip = await this.SelectTripByID(id);
            trip.Location = location;

            return trip;
        }

        public async Task<Trip[]> SelectActiveTrips()
        {
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
                var company = OwnerToCompany(owner);

                companies.Add(company);
            }

            return companies.ToArray();
        }

        public async Task<Company> SelectCompanyByName(string name)
        {
            throw new NotImplementedException();
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
                    FirstName = driverRequests[0].Driver.FirstName,
                    LastName = driverRequests[0].Driver.LastName
                };
            }

            return null;
        }

        private async Task<TaskRequest> GetTaskRequestByTaskID(string taskId)
        {
            var checkTaskRequestRequest = new CheckTaskRequestRequest
            {
                TaskId = Guid.Parse(taskId)
            };
            var requestContent = SerializeObject(checkTaskRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(CHECK_DRIVER_TASK_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<CheckTaskRequestResponse>(request);
            return result.TaskRequest;
        }

        public async Task<Trip> SelectTripByID(string id)
        {
            Trip trip = null;
            var taskRequest = await this.GetTaskRequestByTaskID(id);
            if (taskRequest != null)
                trip = this.TaskRequestToTrip(taskRequest);
            return trip;
        }

        private async Task<Owner> GetCompanyById(string companyId)
        {
            var getCompantRequest = new GetCompanyRequest
            {
                CompanyId = Guid.Parse(companyId)
            };
            var requestContent = SerializeObject(getCompantRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(GET_OWNER_COMPANY_ENDPOINT, null);
            var result = await ExecuteRequestAsync<GetCompanyResponse>(request);
            return result.Company;
        }

        public async Task<Company> SelectUserCompanyAsync()
        {
            Company company = null;
            Owner owner = null;
            if (_currentUser.Role == UserRole.Owner)
                owner = await GetCompanyById(_currentUser.ID);
            else if (_currentUser.Role == UserRole.Dispatch)
            {
            }
            else if (_currentUser.Role == UserRole.Driver)
                owner = await GetDriverCompany(Guid.Parse(_currentUser.ID));

            if (owner != null)
                company = OwnerToCompany(owner);

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

        public async Task SendNotification(Trip trip, string message)
        {
            // TODO: Send notification
            await Task.Delay(0);
            //throw new NotImplementedException();
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
            return await Task.FromResult<User>(default(User));
        }

        public Task<Trip> TripInDelivery(string id, int minutes)
        {
            return Task.FromResult(new Trip { });
            //throw new NotImplementedException();
        }

        public async Task<Trip> TripInPickup(string id, int minutes)
        {
            var taskArrivalLoadingRequest = new TaskArrivalLoadingRequest
            {
                TaskId = Guid.Parse(id),
                ArrivalTime = DateTime.UtcNow
            };
            var requestContent = SerializeObject(taskArrivalLoadingRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(TASK_ARRIVAL_LOADING_ENDPOINT, null);
            var result = await ExecuteRequestAsync<TaskArrivalLoadingResponse>(request);

            Trip trip = null;
            var taskRequest = await this.GetTaskRequestByTaskID(result.Task.Id.ToString());
            if (taskRequest != null && taskRequest.Task != null)
                trip = this.TaskRequestToTrip(taskRequest);

            return trip;
        }

        public Task<bool> UserExistAsync(string userName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
