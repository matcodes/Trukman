using System;
using Parse;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Foundation;

namespace Trukman
{

	public class ServerManager : IServerManager
	{
		static string ServerCompany = "Company";
		static string ServerName = "name";
		static string ServerUserName = "username";
		static string ServerRequesting = "requesting";
		static string ServerOwner = "owner";
		static string ServerRole = "role";
		static string ServerDispatchers = "dispatchers";
		static string ServerDrivers = "drivers";
		Timer timerForRequst;

		public ServerManager ()
		{
		}

		public void Init () {
			ParseClient.Initialize ("NsNjjvCGhqVKOZqCro2WOEr6gZHGTC9YlVB5jZqe", "WvSfa6MIvTb9L6BucGIiCQgV1zBc4OCR0UTS7D2L");
		}

		public async Task LogIn(string name, string pass) {
			await ParseUser.LogInAsync (name, pass);
		}

		public async Task Register(string name, string pass, UserRole role) {
			ParseUser user = new ParseUser {
				Username = name,
				Password = pass
			};
			user [ServerRole] = (int)role;
			await user.SignUpAsync ();
		}

		public async Task AddCompany (string name) {
			ParseObject company = new ParseObject (ServerCompany);
			company [ServerName] = name;
			company [ServerOwner] = ParseUser.CurrentUser;
			await company.SaveAsync ();
		}
			
		public async Task RequestToJoinCompany (string name) {
			var query = ParseObject.GetQuery(ServerCompany)
				.WhereEqualTo(ServerName, name);
			IEnumerator<ParseObject> companyEnum = (await query.FindAsync()).GetEnumerator();
			companyEnum.MoveNext();
			ParseObject company = companyEnum.Current;
			if (company != null) {
				ParseRelation<ParseUser> companyRequesting = company.GetRelation<ParseUser> (ServerRequesting);
				companyRequesting.Add (ParseUser.CurrentUser);
				await company.SaveAsync ();
			}
		}



		public async void CheckRequests () {
			var query = ParseObject.GetQuery (ServerCompany)
				.WhereEqualTo (ServerOwner, ParseUser.CurrentUser);
			ParseObject company = await query.FirstAsync ();
			var requestRelation = company.GetRelation<ParseUser> (ServerRequesting);
			IEnumerable<ParseUser> users = await requestRelation.Query.FindAsync ();
			foreach (ParseUser user in users) {
				NSObject excecuter = new NSObject ();
				excecuter.InvokeOnMainThread (async delegate {
					UserRole role = (UserRole)user.Get<int> (ServerRole);

					Boolean answer = false;
					if (role == UserRole.UserRoleDispatch)
						answer = await AlertHandler.ShowCheckDispatch (user.Username);
					else if (role == UserRole.UserRoleDriver)
						answer = await AlertHandler.ShowCheckDriver (user.Username);
					if (answer) {
						requestRelation.Remove (user);
						if (role == UserRole.UserRoleDispatch) {
							var dispatchRelation = company.GetRelation<ParseObject> (ServerDispatchers);
							dispatchRelation.Add(user);
							await company.SaveAsync ();
						} else if (role == UserRole.UserRoleDriver) {
							var dispatchRelation = company.GetRelation<ParseObject> (ServerDrivers);
							dispatchRelation.Add(user);
							await company.SaveAsync ();
						}
					} else {
						requestRelation.Remove (user);
					}
				});
			}
		}

		void TimerForRequestFires (object state) {
			CheckRequests ();
		}

		public void StartTimerForRequest () {
			TimerCallback callback = new TimerCallback (TimerForRequestFires);
			timerForRequst = new Timer (callback, null, 0, 5000);
		}
	}
}