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
using System.Linq;
using System.Collections.Generic;
using KAS.Trukman.Data.API.Requests;
using System.Net;
using KAS.Trukman.Data.API.Responses;
using System.Net.Http.Headers;
using KAS.Trukman.Data.Route;

namespace KAS.Trukman.Storage
{
    public class RestAPIExternalStorage : IExternalStorage
    {
        private static readonly string API_BASE_URI = "http://194.58.71.17/trukman.server/";

        private static readonly string OWNER_LOGIN_ENDPOINT = "accounts/owner";
        private static readonly string DISPATCHER_LOGIN_ENDPOINT = "accounts/dispatcher";
        private static readonly string DRIVER_LOGIN_ENDPOINT = "accounts/driver";
        private static readonly string VERIFICATION_ENDPOINT = "accounts/verification";
        private static readonly string RESEND_VERIFICATION_CODE_ENDPOINT = "accounts/resendverificationcode";
        private static readonly string SELECT_COMPANIES_BY_FILTER_ENDPOINT = "accounts/selectcompaniesbyfilter";

        private static readonly string GET_OWNER_COMPANY_ENDPOINT = "owners/getcompany";
        private static readonly string GET_DRIVER_REQUESTS_ENDPOINT = "owners/getdriverrequests";
        private static readonly string GET_DISPATCHER_REQUESTS_ENDPOINT = "owners/getdispatcherrequests";
        private static readonly string ANSWER_DRIVER_REQUEST_ENDPOINT = "owners/answerdriverrequest";
        private static readonly string ANSWER_DISPATCHER_REQUEST_ENDPOINT = "owners/answerdispatcherrequest";
        private static readonly string SELECT_DRIVERS_ENDPOINT = "owners/selectdrivers";
        private static readonly string SELECT_BROKERS_ENDPOINT = "owners/selectbrokers";
        private static readonly string FIND_FUEL_REQUESTS_ENDPOINT = "owners/findfuelrequests";
        private static readonly string FIND_LUMPER_REQUESTS_ENDPOINT = "owners/findlumperrequests";
        private static readonly string ANSWER_FUEL_REQUEST_ENDPOINT = "owners/answerfuelrequest";
        private static readonly string ANSWER_LUMPER_REQUEST_ENDPOINT = "owners/answerlumperrequest";
        private static readonly string SELECT_TASK_BY_ID_ENDPOINT = "owners/selecttaskbyid";
        private static readonly string SELECT_NOTIFICATIONS_ENDPOINT = "owners/selectnotifications";
        //private static readonly string SET_NOTIFICATION_IS_VIEWED_ENDPOINT = "owners/setnotificationisviewed";
        private static readonly string SELECT_TASKS_BY_OWNER_ID_ENDPOINT = "owners/selecttasksbyownerid";
        private static readonly string SAVE_BROKER_ENDPOINT = "owners/savebroker";

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
        private static readonly string ADD_FUEL_REQUEST_ENDPOINT = "drivers/addfuelrequest";
        private static readonly string CHECK_FUEL_REQUEST_ENDPOINT = "drivers/checkfuelrequest";
        private static readonly string CANCEL_FUEL_REQUEST_ENDPOINT = "drivers/cancelfuelrequest";
        private static readonly string ADD_LUMPER_REQUEST_ENDPOINT = "drivers/addlumperrequest";
        private static readonly string CHECK_LUMPER_REQUEST_ENDPOINT = "drivers/checklumperrequest";
        private static readonly string CANCEL_LUMPER_REQUEST_ENDPOINT = "drivers/cancellumperrequest";
        private static readonly string ADD_LOCATION_ENDPOINT = "drivers/addlocation";
        private static readonly string ADD_TASK_ALERT_ENDPOINT = "drivers/addtaskalert";
        private static readonly string SELECT_TASKS_BY_DRIVER_ID_ENDPOINT = "drivers/selecttasksbydriverid";

        private static readonly string GET_DISPATCHER_COMPANY_ENDPOINT = "dispatchers/getdispatchercompany";
        private static readonly string GET_LAST_DISPATCHER_REQUEST_ENDPOINT = "dispatchers/getlastdispatcherrequest";
        private static readonly string ADD_DISPATCHER_REQUEST_ENDPOINT = "dispatchers/adddispatcherrequest";
        private static readonly string CANCEL_DISPATCHER_REQUEST_ENDPOINT = "dispatchers/canceldispatcherrequest";

        private static readonly string GOOGLE_GEOCODE_ENDPOINT = "google/geocode";
        private static readonly string GOOGLE_REVERSE_GEOCODE_ENDPOINT = "google/reversegeocode";
        private static readonly string GOOGLE_DIRECTION_ENDPOINT = "google/direction";

        public static readonly string DIRECTION_MOVE_TYPE_DRIVING = "driving";

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
                        {
                            result = DeserializeObject<T>(content);
                            if (!string.IsNullOrEmpty(result.ErrorText))
                                throw new Exception(result.ErrorText);
                        }
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

        private async Task<Owner> GetDispatcherCompany(Guid dispatcherId)
        {
            var getDispatcherCompanyRequest = new GetDispatcherCompanyRequest
            {
                DispatcherId = dispatcherId
            };
            var requestContent = SerializeObject(getDispatcherCompanyRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(GET_DISPATCHER_COMPANY_ENDPOINT, null);
            var result = await ExecuteRequestAsync<GetDispatcherCompanyResponse>(request);
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
        public async Task AcceptUserToCompany(string companyID, User user)
        {
            if (user.Role == UserRole.Driver)
                await AnswerDriverRequest(Guid.Parse(companyID), Guid.Parse(user.ID), true);
            else
                await AnswerDispatcherRequest(Guid.Parse(companyID), Guid.Parse(user.ID), true);
        }

        public async Task DeclineUserToCompany(string companyID, User user)
        {
            if (user.Role == UserRole.Driver)
                await AnswerDriverRequest(Guid.Parse(companyID), Guid.Parse(user.ID), false);
            else
                await AnswerDispatcherRequest(Guid.Parse(companyID), Guid.Parse(user.ID), false);
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
                trip = TaskToTrip(trukmanTask);

            return trip;
        }

        //public Task AddPointsAsync(string jobID, string text, int points)
        //{
        //    // not used
        //    throw new NotImplementedException();
        //}

        public void Become(User user)
        {
            _currentUser = user;
        }

        private async Task CancelFuelRequest(Guid taskId)
        {
            var cancelFuelRequestRequest = new CancelFuelRequestRequest
            {
                TaskId = taskId
            };
            var requestContent = SerializeObject(cancelFuelRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(CANCEL_FUEL_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<CancelFuelRequestResponse>(request);
        }

        private async Task CancelLumperRequest(Guid taskId)
        {
            var cancelLumperRequestRequest = new CancelLumperRequestRequest
            {
                TaskId = taskId
            };
            var requestContent = SerializeObject(cancelLumperRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(CANCEL_LUMPER_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<CancelLumperRequestResponse>(request);
        }

        public async Task CancelComcheckRequestAsync(string tripID, ComcheckRequestType requestType)
        {
            if (requestType == ComcheckRequestType.FuelAdvance)
                await this.CancelFuelRequest(Guid.Parse(tripID));
            else if (requestType == ComcheckRequestType.Lumper)
                await this.CancelLumperRequest(Guid.Parse(tripID));
        }

        private Trip TaskToTrip(TrukmanTask task)
        {
            var shipper = new Contractor
            {
                ID = Guid.NewGuid().ToString(),
                Name = task.LoadingName,
                Phone = task.LoadingPhone,
                Fax = task.LoadingFax,
                Address = task.LoadingAddress,
                SpecialInstruction = task.LoadingInstructions,
            };
            var receiver = new Contractor
            {
                ID = Guid.NewGuid().ToString(),
                Name = task.UnloadingName,
                Phone = task.UnloadingPhone,
                Fax = task.UnloadingFax,
                Address = task.UnloadingAddress,
                SpecialInstruction = task.UnloadingInstructions
            };

            User driver = null;
            if (task.Driver != null)
                driver = this.DriverToUser(task.Driver);

            User broker = null;
            if (task.Broker != null)
                broker = this.BrokerToUser(task.Broker);

            Company company = null;
            if (task.Owner != null)
                company = this.OwnerToCompany(task.Owner);

            return new Trip
            {
                ID = task.Id.ToString(),
                DeliveryDatetime = task.UnloadingPlanTime.ToLocalTime(),
                DriverAccepted = (task.StartTime.GetValueOrDefault() != DateTime.MinValue),
                IsDelivery = (task.UnloadingRealTime.GetValueOrDefault() != DateTime.MinValue),
                IsPickup = (task.LoadingRealTime.GetValueOrDefault() != DateTime.MinValue),
                JobCancelled = (task.CancelTime.GetValueOrDefault() != DateTime.MinValue ? true : false),
                JobCompleted = (task.CompleteTime.GetValueOrDefault() != DateTime.MinValue ? true : false),
                IsDeleted = (task.RemovedTime.GetValueOrDefault() != DateTime.MinValue ? true : false),
                PickupDatetime = task.LoadingPlanTime.ToLocalTime(),
                Points = task.PlanPoints,
                Shipper = shipper,
                Receiver = receiver,
                JobRef = task.Id.ToString(), // task.Number,
                FromAddress = task.LoadingAddress,
                ToAddress = task.UnloadingAddress,
                Weight = task.Weight,
                Location = new Position(task.Latitude, task.Longitude),
                UpdateTime = DateTime.Now,
                Driver = driver,
                Broker = broker,
                Company = company,
                InvoiceUri = task.InvoiceUri,
                ReportUri = task.ReportUri,
                DriverDisplayName = (driver != null ? driver.UserName : "")
            };
        }

        private User BrokerToUser(Broker broker)
        {
            return new User
            {
                ID = broker.Id.ToString(),
                UserName = broker.Name,
                FirstName = broker.Name,
                LastName = broker.ContactName,
                Phone = broker.Phone,
                Role = UserRole.Broker
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
                trip = TaskToTrip(result.TaskRequest.Task);

            return trip;
        }

        public async Task<Trip> CompleteTrip(string id)
        {
            var taskDoneRequest = new TaskDoneRequest
            {
                TaskId = Guid.Parse(id),
                DoneTime = DateTime.UtcNow
            };
            var requestContent = SerializeObject(taskDoneRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(TASK_DONE_ENDPOINT, null);
            var result = await ExecuteRequestAsync<TaskDoneResponse>(request);

            Trip trip = null;
            if (result.Task != null)
                trip = TaskToTrip(result.Task);

            return trip;
        }

        public async Task<string> CreateInvoiceForJobAsync(string tripID)
        {
            string uri = "";
            var task = await this.SelectTaskById(Guid.Parse(tripID));
            if (task != null)
                uri = task.ReportUri;

            return uri;
        }

        public Task<Trip> CreateTripAsync(Trip trip)
        {
            // TODO: 
            return Task.FromResult<Trip>(new Trip());
        }

        private async Task<bool> AnswerDriverRequest(Guid ownerId, Guid driverId, bool isAllowed)
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

        private async Task<bool> AnswerDispatcherRequest(Guid ownerId, Guid dispatcherId, bool isAllowed)
        {
            var answerDispatcherRequestRequest = new AnswerDispatcherRequestRequest
            {
                OwnerId = ownerId,
                DispatcherId = dispatcherId,
                IsAllowed = isAllowed
            };
            var requestContent = SerializeObject(answerDispatcherRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(ANSWER_DISPATCHER_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<AnswerDispatcherRequestResponse>(request);
            return true;
        }

        public async Task<Trip> DeclineTrip(string id, int declineReason, string reasonText)
        {
            var taskRequest = await this.GetTaskRequestByTaskID(id);
            var trukmanTask = await this.AnswerTaskRequest(taskRequest.Id, (int)TaskRequestAnswers.Decline, declineReason, reasonText);

            Trip trip = null;
            if (trukmanTask != null)
                trip = TaskToTrip(trukmanTask);

            return trip;
        }

        public async Task<string> GetComcheckAsync(string tripID, ComcheckRequestType requestType)
        {
            if (requestType == ComcheckRequestType.FuelAdvance)
            {
                var fuelRequest = await this.CheckFuelRequest(Guid.Parse(tripID));
                if (fuelRequest != null)
                    return fuelRequest.Comcheck;
                else
                    return "";
            }
            else if (requestType == ComcheckRequestType.Lumper)
            {
                var lumperRequest = await this.CheckLumperRequest(Guid.Parse(tripID));
                if (lumperRequest != null)
                    return lumperRequest.Comcheck;
                else
                    return "";
            }

            return "";
        }

        public async Task<FuelRequest> CheckFuelRequest(Guid taskId)
        {
            var checkFuelRequestRequest = new CheckFuelRequestRequest
            {
                TaskId = taskId
            };
            var requestContent = SerializeObject(checkFuelRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(CHECK_FUEL_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<CheckFuelRequestResponse>(request);
            return result.FuelRequest;
        }

        public async Task<LumperRequest> CheckLumperRequest(Guid taskId)
        {
            var checkLumperRequestRequest = new CheckLumperRequestRequest
            {
                TaskId = taskId
            };
            var requestContent = SerializeObject(checkLumperRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(CHECK_LUMPER_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<CheckLumperRequestResponse>(request);
            return result.LumperRequest;
        }

        public async Task<ComcheckRequestState> GetComcheckStateAsync(string tripID, ComcheckRequestType requestType)
        {
            if (requestType == ComcheckRequestType.FuelAdvance)
            {
                var fuelRequest = await this.CheckFuelRequest(Guid.Parse(tripID));
                if (fuelRequest != null)
                {
                    if (fuelRequest.Answer == (int)FuelRequestAnswers.None)
                        return ComcheckRequestState.Requested;
                    else if (fuelRequest.Answer == (int)FuelRequestAnswers.Accept)
                        return ComcheckRequestState.Visible;
                    else
                        return ComcheckRequestState.None;
                }
                else
                    return ComcheckRequestState.None;
            }
            else if (requestType == ComcheckRequestType.Lumper)
            {
                var lumperRequest = await this.CheckLumperRequest(Guid.Parse(tripID));
                if (lumperRequest != null)
                {
                    if (lumperRequest.Answer == (int)LumperRequestAnswers.None)
                        return ComcheckRequestState.Requested;
                    else if (lumperRequest.Answer == (int)LumperRequestAnswers.Accept)
                        return ComcheckRequestState.Visible;
                    else
                        return ComcheckRequestState.None;
                }
                else
                    return ComcheckRequestState.None;
            }

            return ComcheckRequestState.None;
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

        private async Task<DispatcherRequest> GetLastDispatcherRequest(Guid ownerId, Guid dispatcherId)
        {
            var getLastDispatcherRequestRequest = new GetLastDispatcherRequestRequest
            {
                OwnerId = ownerId,
                DispatcherId = dispatcherId
            };
            var requestContent = SerializeObject(getLastDispatcherRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(GET_LAST_DISPATCHER_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<GetLastDispatcherRequestResponse>(request);
            return result.DispatcherRequest;
        }

        public async Task<UserState> GetDriverState(string companyID, string driverID)
        {
            var lastDriverRequest = await GetLastDriverRequest(Guid.Parse(companyID), Guid.Parse(driverID));
            var answer = lastDriverRequest.Answer;

            var state = UserState.Waiting;
            if (answer == (int)DriverRequestAnswers.Accept)
                state = UserState.Joined;
            else if (answer == (int)DriverRequestAnswers.Decline)
                state = UserState.Declined;

            _currentUser.Status = (int)state;
            return state;
        }

        public async Task<UserState> GetDispatcherState(string companyID, string dispatcherID)
        {
            var lastDispatcherRequest = await GetLastDispatcherRequest(Guid.Parse(companyID), Guid.Parse(dispatcherID));
            var answer = lastDispatcherRequest.Answer;

            var state = UserState.Waiting;
            if (answer == (int)DispatcherRequestAnswers.Accept)
                state = UserState.Joined;
            else if (answer == (int)DispatcherRequestAnswers.Decline)
                state = UserState.Declined;

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

        public async Task CancelDispatcherRequest(string companyID, string dispatcherID)
        {
            var cancelDispatcherRequest = new CancelDispatcherRequestRequest
            {
                OwnerId = Guid.Parse(companyID),
                DispatcherId = Guid.Parse(dispatcherID)
            };
            var requestContent = SerializeObject(cancelDispatcherRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(CANCEL_DISPATCHER_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<CancelDispatcherRequestResponse>(request);
        }

        private JobNotification NotificationToJobNotification(Notification notification)
        {
            Trip trip = null;
            User sender = null;
            User receiver = null;

            if (notification.Task != null)
                trip = this.TaskToTrip(notification.Task);
            if (notification.Task != null && notification.Task.Driver != null)
                sender = this.DriverToUser(notification.Task.Driver);
            if (notification.Task != null && notification.Task.Owner != null)
                sender = this.OwnerToUser(notification.Task.Owner);

            var jobNotification = new JobNotification
            {
                ID = notification.Id.ToString(),
                Text = notification.Text,
                //IsSending = notification.IsSending,
                //IsReading = notification.IsReading,
                Trip = trip,
                Sender = sender,
                Receiver = receiver,
                Time = notification.Time
            };
            return jobNotification;
        }

        //private async Task<Notification> SetNotificationIsViewed(Guid notificationId)
        //{
        //    var setNotificationIsViewedRequest = new SetNotificationIsViewedRequest
        //    {
        //        NotificationId = notificationId
        //    };
        //    var requestContent = SerializeObject(setNotificationIsViewedRequest);
        //    var request = new HttpRequestMessage();
        //    request.Method = HttpMethod.Post;
        //    request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
        //    request.RequestUri = CreateRequestUri(SET_NOTIFICATION_IS_VIEWED_ENDPOINT);
        //    var result = await ExecuteRequestAsync<SetNotificationIsViewedResponse>(request);
        //    return result.Notification;
        //}

        public async Task<JobNotification> GetNotification(DateTime fromUtcTime)
        {
            var selectNotificationsRequest = new SelectNotificationsRequest
            {
                ReceiverId = Guid.Parse(_currentUser.ID),
                Skip = 0,
                Limit = 0
            };
            var requestContent = SerializeObject(selectNotificationsRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(SELECT_NOTIFICATIONS_ENDPOINT, null);
            // Get ordered notifications
            var result = await ExecuteRequestAsync<SelectNotificationsResponse>(request);

            JobNotification jobNotification = null;
            if (result.Notifications != null)
            {
                var notification = result.Notifications.FirstOrDefault(n => n.Time > fromUtcTime);
                if (notification != null)
                {
                    // this.SetNotificationIsViewed(notification.Id);  // Notifications are marked as viewed only from web app, not from mobile app!!!
                    jobNotification = this.NotificationToJobNotification(notification);
                }
            }
            return jobNotification;
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

        //public Task<int> GetPointsByJobIDAsync(string jobID)
        //{
        //    // not used
        //    throw new NotImplementedException();
        //}

        //public Task<string> GetSessionToken()
        //{
        //    // not used
        //    if (_currentUser != null)
        //        return Task.FromResult(_currentUser.Token);
        //    else
        //        return Task.FromResult(string.Empty);
        //}

        public void InitializeOwnerNotification()
        {
        }

        //public Task<User> LogInAsync(string userName, string password)
        //{
        //    // not used
        //    throw new NotImplementedException();
        //}

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

        public async Task<bool> Verification(Guid accountId, string code)
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
            if (_currentUser != null)
                _currentUser.Token = _token;

            return true;
        }

        public async Task<bool> ResendVerificationCode(Guid accountId)
        {
            var resendVerificationCodeRequest = new ResendVerificationCodeRequest
            {
                UserId = accountId
            };
            var requestContent = SerializeObject(resendVerificationCodeRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(RESEND_VERIFICATION_CODE_ENDPOINT);
            var result = await ExecuteRequestAsync<ResendVerificationCodeResponse>(request);
            return (result != null);
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

        private async Task<bool> AddDispatcherRequest(Guid ownerId, Guid dispatcherId)
        {
            var addDispatcherRequest = new AddDispatcherRequestRequest
            {
                OwnerId = ownerId,
                DispatcherId = dispatcherId
            };
            var requestContent = SerializeObject(addDispatcherRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(ADD_DISPATCHER_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<AddDispatcherRequestResponse>(request);
            return true;
        }

        public async Task<User> DriverLogin(DriverInfo driverInfo)
        {
            var _driver = new Driver
            {
                FirstName = driverInfo.FirstName,
                LastName = driverInfo.LastName,
                Phone = driverInfo.Phone
            };
            _driver = await DriverLoginAsync(_driver);
            Console.WriteLine("Водитель: {0}", _driver);
            Console.WriteLine();

            _currentUser = new User
            {
                ID = _driver.Id.ToString(),
                UserName = string.Format("{0} {1}", driverInfo.FirstName.Trim(), driverInfo.LastName.Trim()).ToLower(),
                Phone = driverInfo.Phone,
                Role = UserRole.Driver,
                FirstName = driverInfo.FirstName,
                LastName = driverInfo.LastName,
                Email = driverInfo.EMail,
                Token = _token,
                //Status = (int)DriverState.Waiting
            };

            return _currentUser;
        }

        private async Task<Dispatcher> DispatcherLoginAsync(Dispatcher dispatcher)
        {
            var dispatcherLoginRequest = new DispatcherLoginRequest
            {
                Dispatcher = dispatcher
            };
            var requestContent = SerializeObject(dispatcherLoginRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(DISPATCHER_LOGIN_ENDPOINT, null);
            var result = await ExecuteRequestAsync<DispatcherLoginResponse>(request);
            _token = result.Token;
            return result.Dispatcher;
        }

        public async Task<User> DispatcherLogin(DispatcherInfo dispatcherInfo)
        {
            var _dispatcher = new Dispatcher
            {
                FirstName = dispatcherInfo.FirstName,
                LastName = dispatcherInfo.LastName,
                Phone = dispatcherInfo.Phone,
                Email = dispatcherInfo.EMail
            };
            _dispatcher = await DispatcherLoginAsync(_dispatcher);
            Console.WriteLine("Диспетчер: {0}", _dispatcher);
            Console.WriteLine();

            _currentUser = new User
            {
                ID = _dispatcher.Id.ToString(),
                UserName = string.Format("{0} {1}", _dispatcher.FirstName.Trim(), _dispatcher.LastName.Trim()).ToLower(),
                Phone = _dispatcher.Phone,
                Role = UserRole.Dispatch,
                FirstName = _dispatcher.FirstName,
                LastName = _dispatcher.LastName,
                Email = _dispatcher.Email,
                Token = _token,
                //Status = (int)DriverState.Waiting
            };

            return _currentUser;
        }

        public async Task<Company> RegisterDriver(DriverInfo driverInfo)
        {
            //var _driver = new Driver
            //{
            //    FirstName = driverInfo.FirstName,
            //    LastName = driverInfo.LastName,
            //    Phone = driverInfo.Phone,
            //};
            //_driver = await DriverLoginAsync(_driver);
            //Console.WriteLine("Водитель: {0}", _driver);
            //Console.WriteLine();

            //if (string.IsNullOrEmpty(_token))
            //{
            //    await Verification(_driver.Id, "5555");
            //    Console.WriteLine("Телефон подтвержден: {0}", _driver.Phone);
            //    Console.WriteLine();
            //}

            //Console.WriteLine("Token: {0}", _token);
            //Console.WriteLine();

            //_currentUser = new User
            //{
            //    ID = _driver.Id.ToString(),
            //    UserName = string.Format("{0} {1}", driverInfo.FirstName.Trim(), driverInfo.LastName.Trim()).ToLower(),
            //    Phone = driverInfo.Phone,
            //    Role = UserRole.Driver,
            //    FirstName = driverInfo.FirstName,
            //    LastName = driverInfo.LastName,
            //    Email = driverInfo.EMail,
            //    Token = _token,
            //    Status = (int)DriverState.Waiting
            //};

            var userId = Guid.Parse(_currentUser.ID);
            _currentUser.Status = (int)UserState.Waiting;
            var _company = await GetDriverCompany(userId);
            if ((_company != null) && (_company.Id != Guid.Parse(driverInfo.Company.ID)))
                throw new Exception(String.Format("Your are approved to company {0}.", _company.Name));

            if (_company == null)
            {
                var driverRequest = await GetLastDriverRequest(Guid.Parse(driverInfo.Company.ID), userId);
                if ((driverRequest == null) || (driverRequest.Answer != (int)DriverRequestAnswers.None))
                {
                    await AddDriverRequest(Guid.Parse(driverInfo.Company.ID), userId);
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

        public async Task<Company> RegisterDispatcher(DispatcherInfo dispatcherInfo)
        {
            var userId = Guid.Parse(_currentUser.ID);
            _currentUser.Status = (int)UserState.Waiting;
            var _company = await GetDispatcherCompany(userId);
            if ((_company != null) && (_company.Id != Guid.Parse(dispatcherInfo.Company.ID)))
                throw new Exception(String.Format("Your are approved to company {0}.", _company.Name));

            if (_company == null)
            {
                var dispatcherRequest = await GetLastDispatcherRequest(Guid.Parse(dispatcherInfo.Company.ID), userId);
                if ((dispatcherRequest == null) || (dispatcherRequest.Answer != (int)DispatcherRequestAnswers.None))
                {
                    await AddDispatcherRequest(Guid.Parse(dispatcherInfo.Company.ID), userId);
                    Console.WriteLine("Добавлен запрос на прием на работу в компанию {0}...", dispatcherInfo.Company.Name);
                    Console.WriteLine();
                }

                return dispatcherInfo.Company;
            }
            else
            {
                await this.GetDispatcherState(_company.Id.ToString(), _currentUser.ID);

                return OwnerToCompany(_company);
            }
        }

        public async Task<Trip> AddLocation(string id, Position location)
        {

            var addLocationRequest = new AddLocationRequest
            {
                DriverId = Guid.Parse(_currentUser.ID),
                Latitude = (decimal)location.Latitude,
                Longtitude = (decimal)location.Longitude,
                Speed = 0,
                CreateTime = DateTime.UtcNow
            };
            var requestContent = SerializeObject(addLocationRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(ADD_LOCATION_ENDPOINT, null);
            var result = await ExecuteRequestAsync<AddLocationResponse>(request);

            Trip trip = null;
            if (result.Task != null && result.Task != null)
                trip = this.TaskToTrip(result.Task);

            return trip;
        }

        public async Task<Trip> SaveLocation(string id, Position location)
        {
            return await this.SelectTripByID(id);
        }

        public async Task<Trip[]> SelectActiveTrips(Guid ownerId)
        {
            var tasks = await this.SelectTasksByOwnerId(ownerId, 0, 100);
            tasks = tasks.Where(t => t.CompleteTime.GetValueOrDefault() == DateTime.MinValue).Take(3).ToList<TrukmanTask>();
            var trips = new List<Trip>();
            foreach (var task in tasks)
                trips.Add(this.TaskToTrip(task));

            return trips.ToArray();
        }

        public async Task<User[]> SelectBrockersAsync()
        {
            var selectBrokersRequest = new SelectBrokersRequest();
            var requestContent = SerializeObject(selectBrokersRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(SELECT_BROKERS_ENDPOINT, null);
            var result = await ExecuteRequestAsync<SelectBrokersResponse>(request);
            List<User> users = new List<User>();
            if (result.Brokers != null)
            {
                foreach (var broker in result.Brokers)
                    users.Add(this.BrokerToUser(broker));
            }

            return users.ToArray();
        }

        public async Task<User> SaveBrokerAsync(BrokerInfo brokerInfo)
        {
            var saveBrokerRequest = new SaveBrokerRequest
            {
                Broker = new Broker
                {
                    Address = brokerInfo.Address,
                    ContactName = brokerInfo.ContactName,
                    ContactTitle = brokerInfo.ContactTitle,
                    DocketNumber = brokerInfo.DocketNumber,
                    Email = brokerInfo.Email,
                    Name = brokerInfo.Name,
                    Phone = brokerInfo.Phone,
                    State = brokerInfo.State,
                    ZIP = brokerInfo.ZIP
                }
            };
            var requestContent = SerializeObject(saveBrokerRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(SAVE_BROKER_ENDPOINT, null);
            var result = await ExecuteRequestAsync<SaveBrokerResponse>(request);
            if (result.Broker != null)
                return this.BrokerToUser(result.Broker);
            else
                return null;
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

        //public async Task<Company> SelectCompanyByName(string name)
        //{
        //    // not used
        //    throw new NotImplementedException();
        //}

        public async Task<Trip[]> SelectCompletedTrips(Guid ownerId)
        {
            var tasks = await this.SelectTasksByOwnerId(ownerId, 0, 100);
            tasks = tasks.Where(t => t.CompleteTime.GetValueOrDefault() != DateTime.MinValue).Take(3).ToList();
            var trips = new List<Trip>();
            foreach (var task in tasks)
                trips.Add(this.TaskToTrip(task));

            return trips.ToArray();
        }

        public async Task<Position> SelectDriverPosition(string tripID)
        {
            var trip = await this.SelectTripByID(tripID);
            if (trip != null)
                return trip.Location;
            else
                return new Position();
        }

        private User DriverToUser(Driver driver)
        {
            return new User
            {
                ID = driver.Id.ToString(),
                UserName = string.Format("{0} {1}", driver.FirstName, driver.LastName),
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                Phone = driver.Phone,
                Role = UserRole.Driver
            };
        }

        private User DispatcherToUser(Dispatcher dispatcher)
        {
            return new User
            {
                ID = dispatcher.Id.ToString(),
                UserName = string.Format("{0} {1}", dispatcher.FirstName, dispatcher.LastName),
                FirstName = dispatcher.FirstName,
                LastName = dispatcher.LastName,
                Phone = dispatcher.Phone,
                Role = UserRole.Dispatch
            };
        }

        private User OwnerToUser(Owner owner)
        {
            return new User
            {
                ID = owner.Id.ToString(),
                UserName = owner.Name,
                FirstName = owner.Name,
                LastName = owner.Name,
                Phone = owner.Phone,
                Role = UserRole.Owner
            };
        }

        public async Task<User[]> SelectDriversAsync()
        {
            var selectDriversRequest = new SelectDriversRequest
            {
                OwnerId = Guid.Parse(_currentUser.ID)
            };
            var requestContent = SerializeObject(selectDriversRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(SELECT_DRIVERS_ENDPOINT, null);
            var result = await ExecuteRequestAsync<SelectDriversResponse>(request);
            var driverUsers = new User[] { };
            if (result.Drivers != null)
            {
                driverUsers = result.Drivers.Select(driver =>
                {
                    User user = DriverToUser(driver);

                    return user;
                }).ToArray<User>();
            }

            return driverUsers;
        }

        private async Task<Advance[]> SelectFuelAdvances()
        {
            var findFuelRequestsRequest = new FindFuelRequestsRequest
            {
                OwnerId = Guid.Parse(_currentUser.ID)
            };
            var requestContent = SerializeObject(findFuelRequestsRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(FIND_FUEL_REQUESTS_ENDPOINT, null);
            var result = await ExecuteRequestAsync<FindFuelRequestsResponse>(request);
            List<Advance> advances = new List<Advance>();
            if (result.FuelRequests != null)
            {
                foreach (var fuelRequest in result.FuelRequests)
                {
                    var trip = this.TaskToTrip(fuelRequest.Task);
                    var driver = trip.Driver;

                    var advance = new Advance
                    {
                        ID = fuelRequest.Id.ToString(),
                        Comcheck = fuelRequest.Comcheck,
                        Driver = driver,
                        Trip = trip,
                        RequestDateTime = fuelRequest.RequestTime,
                        RequestType = (int)ComcheckRequestType.FuelAdvance,
                        State = fuelRequest.Answer
                    };

                    advances.Add(advance);
                }
            }

            return advances.ToArray();
        }

        private async Task<Advance[]> SelectLumperAdvances()
        {
            var findLumperRequestsRequest = new FindLumperRequestsRequest
            {
                OwnerId = Guid.Parse(_currentUser.ID)
            };
            var requestContent = SerializeObject(findLumperRequestsRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(FIND_LUMPER_REQUESTS_ENDPOINT, null);
            var result = await ExecuteRequestAsync<FindLumperRequestsResponse>(request);
            List<Advance> advances = new List<Advance>();
            if (result.LumperRequests != null)
            {
                foreach (var lumperRequest in result.LumperRequests)
                {
                    var trip = this.TaskToTrip(lumperRequest.Task);
                    var driver = trip.Driver;

                    var advance = new Advance
                    {
                        ID = lumperRequest.Id.ToString(),
                        Comcheck = lumperRequest.Comcheck,
                        Driver = driver,
                        Trip = trip,
                        RequestDateTime = lumperRequest.RequestTime,
                        RequestType = (int)ComcheckRequestType.Lumper,
                        State = lumperRequest.Answer
                    };

                    advances.Add(advance);
                }
            }

            return advances.ToArray();
        }

        public async Task<Advance[]> SelectFuelAdvancesAsync(int requestType)
        {
            Advance[] advances = null;
            if (requestType == (int)ComcheckRequestType.FuelAdvance)
                advances = await this.SelectFuelAdvances();
            else if (requestType == (int)ComcheckRequestType.Lumper)
                advances = await this.SelectLumperAdvances();

            return advances;
        }

        private JobAlert[] TaskAlertsToJobAlerts(TrukmanTask task)
        {
            Company company = null;
            if (task.Owner != null)
                company = this.OwnerToCompany(task.Owner);
            Trip job = this.TaskToTrip(task);

            var jobAlerts = task.Alerts.Select(alert =>
            {
                var jobAlert = new JobAlert
                {
                    ID = alert.Id.ToString(),
                    AlertType = alert.Kind,
                    AlertText = alert.Text,
                    Company = company,
                    Job = job,
                    //IsViewed = alert.IsViewed
                };

                return jobAlert;
            }).ToArray<JobAlert>();

            return jobAlerts;
        }

        public async Task<JobAlert[]> SelectJobAlertsAsync(Guid ownerId)
        {
            var tasks = await this.SelectTasksByOwnerId(ownerId, 0, 100);

            var jobAlerts = tasks.SelectMany(task => this.TaskAlertsToJobAlerts(task)).ToArray<JobAlert>();
            return jobAlerts;
        }

        private async Task<TrukmanTask[]> SelectTasksByDriverId(Guid driverId, int skip, int limit)
        {
            var selectTasksByDriverIdRequest = new SelectTasksByDriverIdRequest
            {
                DriverId = driverId,
                Skip = skip,
                Limit = limit
            };
            var requestContent = SerializeObject(selectTasksByDriverIdRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(SELECT_TASKS_BY_DRIVER_ID_ENDPOINT);
            var result = await ExecuteRequestAsync<SelectTasksByDriverIdResponse>(request);
            return result.Tasks;
        }

        private string GetPointKindText(int kind)
        {
            if (kind == 0)
                return "Arrival Loading";
            else if (kind == 1)
                return "End Loading";
            else if (kind == 2)
                return "Arrival Unloading";
            else if (kind == 3)
                return "End Unloading";
            else if (kind == 4)
                return "Done Task";
            else if (kind == 5)
                return "Arrival Loading In Time";
            else if (kind == 6)
                return "Arrival Loading 15 Min Early";
            else if (kind == 7)
                return "Arrival Loading Late";
            else if (kind == 8)
                return "Arrival Unloading In Time";
            else if (kind == 9)
                return "Arrival Unloading 15 Min Early";
            else if (kind == 10)
                return "Arrival Unloading Late";
            else if (kind == 11)
                return "Send Photo";
            else
                return "";
        }

        private JobPoint[] TaskPointsToJobPoints(TrukmanTask task)
        {
            Trip job = null;
            if (task != null)
                job = this.TaskToTrip(task);

            User driver = null;
            if (task.Driver != null)
                driver = this.DriverToUser(task.Driver);

            Company company = null;
            if (task.Owner != null)
                company = this.OwnerToCompany(task.Owner);

            var jobPoints = task.TaskPoints
                .Select(point =>
                {
                    var jobPoint = new JobPoint
                    {
                        ID = point.Id.ToString(),
                        // TODO: kind to text
                        Text = this.GetPointKindText(point.Kind),
                        Value = point.Points,
                        Job = job,
                        Driver = driver,
                        Company = company
                    };

                    return jobPoint;
                }).ToArray<JobPoint>();

            return jobPoints;
        }

        private async Task<IEnumerable<TrukmanTask>> SelectTasksByOwnerId(Guid ownerId, int skip, int limit)
        {
            var selectTasksByOwnerIdRequest = new SelectTasksByOwnerIdRequest
            {
                OwnerId = ownerId,
                Skip = skip,
                Limit = limit
            };
            var requestContent = SerializeObject(selectTasksByOwnerIdRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(SELECT_TASKS_BY_OWNER_ID_ENDPOINT, null);
            var result = await ExecuteRequestAsync<SelectTasksByOwnerIdResponse>(request);
            return result.Tasks;
        }

        public async Task<JobPoint[]> SelectJobPointsAsync()
        {
            var tasks = await this.SelectTasksByDriverId(Guid.Parse(_currentUser.ID), 0, 3);

            var jobPoints = tasks.SelectMany(task => this.TaskPointsToJobPoints(task)).ToArray<JobPoint>();
            return jobPoints;
        }

        private Photo[] TaskPhotosToPhotos(TrukmanTask task)
        {
            var photos = task.TaskPhotos.Select(taskPhoto =>
            {
                var job = this.TaskToTrip(task);
                var company = this.OwnerToCompany(task.Owner);

                var photo = new Photo
                {
                    ID = taskPhoto.Id.ToString(),
                    PhotoID = taskPhoto.Id.ToString(),
                    TripID = task.Id.ToString(),
                    //UpdateTime = taskPhoto.data
                    Job = job,
                    Company = company,
                    Type = taskPhoto.Kind,
                    Uri = new Uri(taskPhoto.Uri),
                    //IsViewed = taskPhoto.IsViewed
                };

                return photo;
            }).ToArray<Photo>();

            return photos;
        }

        public async Task<Photo[]> SelectPhotosAsync(Guid ownerId)
        {
            var tasks = await this.SelectTasksByOwnerId(ownerId, 0, 100);

            var jobPhotos = tasks.SelectMany(task => this.TaskPhotosToPhotos(task)).ToArray<Photo>();
            return jobPhotos;
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

        private async Task<DispatcherRequest[]> GetDispatcherRequests(Guid ownerId)
        {
            var getDispatcherRequestsRequest = new GetDispatcherRequestsRequest
            {
                OwnerId = ownerId
            };
            var requestContent = SerializeObject(getDispatcherRequestsRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(GET_DISPATCHER_REQUESTS_ENDPOINT, null);
            var result = await ExecuteRequestAsync<GetDispatcherRequestsResponse>(request);
            return result.DispatcherRequests;
        }

        public async Task<User> SelectRequestedUser(string companyID)
        {
            if (_currentUser.Role == UserRole.Owner)
            {
                var dispatcherRequests = await GetDispatcherRequests(Guid.Parse(companyID));
                if (dispatcherRequests.Length > 0)
                    return DispatcherToUser(dispatcherRequests[0].Dispatcher);
                else
                {
                    var driverRequests = await GetDriverRequests(Guid.Parse(companyID));
                    if (driverRequests.Length > 0)
                        return DriverToUser(driverRequests[0].Driver);
                }
            }
            else if (_currentUser.Role == UserRole.Dispatch)
            {
                var driverRequests = await GetDriverRequests(Guid.Parse(companyID));
                if (driverRequests.Length > 0)
                    return DriverToUser(driverRequests[0].Driver);
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
            TrukmanTask task = await SelectTaskById(Guid.Parse(id));

            Trip trip = null;
            if (task != null)
                trip = this.TaskToTrip(task);
            return trip;
        }

        private async Task<TrukmanTask> SelectTaskById(Guid taskId)
        {
            var selectTaskByIdRequest = new SelectTaskByIdRequest
            {
                TaskId = taskId
            };
            var requestContent = SerializeObject(selectTaskByIdRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(SELECT_TASK_BY_ID_ENDPOINT, null);
            var result = await ExecuteRequestAsync<SelectTaskByIdResponse>(request);
            return result.Task;
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
                owner = await GetDispatcherCompany(Guid.Parse(_currentUser.ID));
            else if (_currentUser.Role == UserRole.Driver)
                owner = await GetDriverCompany(Guid.Parse(_currentUser.ID));

            if (owner != null)
                company = OwnerToCompany(owner);

            return company;
        }

        private async Task AddFuelRequest(Guid taskId, DateTime requestTime)
        {
            var addFuelRequestRequest = new AddFuelRequestRequest
            {
                TaskId = taskId,
                RequestTime = requestTime
            };
            var requestContent = SerializeObject(addFuelRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(ADD_FUEL_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<AddFuelRequestResponse>(request);
        }

        private async Task AddLumperRequest(Guid taskId, DateTime requestTime)
        {
            var addLumperRequestRequest = new AddLumperRequestRequest
            {
                TaskId = taskId,
                RequestTime = requestTime
            };
            var requestContent = SerializeObject(addLumperRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(ADD_LUMPER_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<AddLumperRequestResponse>(request);
        }

        public async Task SendComcheckRequestAsync(string tripID, ComcheckRequestType requestType)
        {
            if (requestType == ComcheckRequestType.FuelAdvance)
                await this.AddFuelRequest(Guid.Parse(tripID), DateTime.UtcNow);
            else if (requestType == ComcheckRequestType.Lumper)
                await this.AddLumperRequest(Guid.Parse(tripID), DateTime.UtcNow);
        }

        public async Task SendJobAlertAsync(string tripID, int alertType, string alertText)
        {
            var addTaskAlertRequest = new AddTaskAlertRequest
            {
                TaskId = Guid.Parse(tripID),
                Time = DateTime.UtcNow,
                Kind = alertType,
                Text = alertText
            };
            var requestContent = SerializeObject(addTaskAlertRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(ADD_TASK_ALERT_ENDPOINT, null);
            var result = await ExecuteRequestAsync<AddTaskAlertResponse>(request);
        }

        public Task SendNotification(Trip trip, string message)
        {
            return Task.Delay(0);
        }

        private async Task<TrukmanTask> TaskDoneLoading(Guid taskId, DateTime doneTime, byte[] photoData)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();

            var fileContent = new ByteArrayContent(photoData);

            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = Guid.NewGuid() + ".jpg"
            };
            content.Add(fileContent);

            content.Add(new StringContent(taskId.ToString()), "TaskId");
            content.Add(new StringContent(doneTime.ToString()), "DoneTime");

            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = content;
            request.RequestUri = CreateRequestUri(TASK_DONE_LOADING_ENDPOINT, null);
            var result = await ExecuteRequestAsync<TaskDoneLoadingResponse>(request);

            return result.Task;
        }

        private async Task<TrukmanTask> TaskDoneUnloading(Guid taskId, DateTime doneTime, byte[] photoData)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();

            var fileContent = new ByteArrayContent(photoData);

            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = Guid.NewGuid() + ".jpg"
            };
            content.Add(fileContent);

            content.Add(new StringContent(taskId.ToString()), "TaskId");
            content.Add(new StringContent(doneTime.ToString()), "DoneTime");

            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = content;
            request.RequestUri = CreateRequestUri(TASK_DONE_UNLOADING_ENDPOINT, null);
            var result = await ExecuteRequestAsync<TaskDoneUnloadingResponse>(request);
            return result.Task;
        }

        public async Task<Trip> SendPhoto(string id, byte[] data, PhotoKind kind)
        {
            var doneDate = DateTime.UtcNow;
            TrukmanTask task = null;
            if (kind == PhotoKind.Pickup)
                task = await this.TaskDoneLoading(Guid.Parse(id), doneDate, data);
            else if (kind == PhotoKind.Delivery)
                task = await this.TaskDoneUnloading(Guid.Parse(id), doneDate, data);

            Trip trip = null;
            if (task != null)
                trip = this.TaskToTrip(task);

            return trip;
        }

        private async Task SetFuelAdvanceState(Advance advance)
        {
            int answer = 0;
            if (advance.State == (int)ComcheckRequestState.Visible)
                answer = (int)FuelRequestAnswers.Accept;
            else //if (advance.State == (int)ComcheckRequestState.Requested)
                answer = (int)FuelRequestAnswers.None;

            var answerFuelRequestRequest = new AnswerFuelRequestRequest
            {
                TaskId = Guid.Parse(advance.Trip.ID),
                Answer = answer,
                AnswerTime = DateTime.UtcNow,
                Comcheck = advance.Comcheck
            };
            var requestContent = SerializeObject(answerFuelRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(ANSWER_FUEL_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<AnswerFuelRequestResponse>(request);
        }

        private async Task SetLumperAdvanceState(Advance advance)
        {
            int answer = 0;
            if (advance.State == (int)ComcheckRequestState.Visible)
                answer = (int)LumperRequestAnswers.Accept;
            else //if (advance.State == (int)ComcheckRequestState.Requested)
                answer = (int)LumperRequestAnswers.None;

            var answerLumperRequestRequest = new AnswerLumperRequestRequest
            {
                TaskId = Guid.Parse(advance.Trip.ID),
                Answer = answer,
                AnswerTime = DateTime.UtcNow,
                Comcheck = advance.Comcheck
            };
            var requestContent = SerializeObject(answerLumperRequestRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(ANSWER_LUMPER_REQUEST_ENDPOINT, null);
            var result = await ExecuteRequestAsync<AnswerLumperRequestResponse>(request);
        }

        public async Task SetAdvanceStateAsync(Advance advance)
        {
            if (advance.RequestType == (int)ComcheckRequestType.FuelAdvance)
                await this.SetFuelAdvanceState(advance);
            else if (advance.RequestType == (int)ComcheckRequestType.Lumper)
                await this.SetLumperAdvanceState(advance);
        }

        public Task SetJobAlertIsViewedAsync(string jobAlertID, bool isViewed)
        {
            //var taskSetDeletedStateRequest = new TaskSetIsReceiveStateRequest
            //{
            //    TaskId = taskId,
            //    NewState = isViewed
            //};
            //var requestContent = TrukmanAPI.SerializeObject(taskSetDeletedStateRequest);
            //var request = new HttpRequestMessage();
            //request.Method = HttpMethod.Post;
            //request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            //request.RequestUri = CreateRequestUri(TASK_SET_IS_RECEIVE_STATE_ENDPOINT, null);
            //var result = await ExecuteRequestAsync<TaskSetIsReceiveStateResponse>(request);
            //return result.Task;

            // TODO: 
            return Task.Delay(0);
        }

        public async Task<Trip> TripInDelivery(string id, int minutes)
        {
            var taskArrivalUnloadingRequest = new TaskArrivalUnloadingRequest
            {
                TaskId = Guid.Parse(id),
                ArrivalTime = DateTime.UtcNow
            };
            var requestContent = SerializeObject(taskArrivalUnloadingRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(TASK_ARRIVAL_UNLOADING_ENDPOINT, null);
            var result = await ExecuteRequestAsync<TaskArrivalUnloadingResponse>(request);

            Trip trip = null;
            if (result.Task != null)
                trip = this.TaskToTrip(result.Task);

            return trip;
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
            if (result.Task != null)
                trip = this.TaskToTrip(result.Task);

            return trip;
        }

        public async Task<Position> GetPositionByAddress(string address)
        {
            var trukmanGeocodeRequest = new TrukmanGeocodeRequest
            {
                Address = address
            };
            var requestContent = SerializeObject(trukmanGeocodeRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(GOOGLE_GEOCODE_ENDPOINT, null);
            var response = await ExecuteRequestAsync<TrukmanGeocodeResponse>(request);

            Position position = default(Position);
            if (response.Result != null && response.Result.Results != null && response.Result.Results.Length > 0)
            {
                var location = response.Result.Results[0].Geometry.Location;
                position = new Position(location.Latitude, location.Longitude);
            }
            return position;
        }

        public async Task<string> GetAddressByPosition(Position position)
        {
            var trukmanReverseGeocodeRequest = new TrukmanReverseGeocodeRequest
            {
                Latitude = position.Latitude,
                Longitude = position.Longitude
            };
            var requestContent = SerializeObject(trukmanReverseGeocodeRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(GOOGLE_REVERSE_GEOCODE_ENDPOINT, null);
            var response = await ExecuteRequestAsync<TrukmanReverseGeocodeResponse>(request);

            var address = "";
            var addressInfo = (response.Result != null && response.Result.Results != null && response.Result.Results.Length > 0 ? response.Result.Results[0] : null);
            if (addressInfo != null)
                address = addressInfo.FormattedAddress;

            return address;
        }

        public async Task<RouteResult> GetMapRoute(Position startPosition, Position endPosition)
        {
            var trukmanDirectionRequest = new TrukmanDirectionRequest
            {
                StartLatitude = startPosition.Latitude,
                StartLongitude = startPosition.Longitude,
                EndLatitude = endPosition.Latitude,
                EndLongitude = endPosition.Longitude,
                MoveType = DIRECTION_MOVE_TYPE_DRIVING
            };
            var requestContent = SerializeObject(trukmanDirectionRequest);
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
            request.RequestUri = CreateRequestUri(GOOGLE_DIRECTION_ENDPOINT, null);
            var result = await ExecuteRequestAsync<TrukmanDirectionResponse>(request);

            return result.Result;
        }
        #endregion
    }
}
