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
		Timer timerForRequst;

		public ServerManager ()
		{
		}

		public void Init () {
			ParseClient.Initialize ("NsNjjvCGhqVKOZqCro2WOEr6gZHGTC9YlVB5jZqe", "WvSfa6MIvTb9L6BucGIiCQgV1zBc4OCR0UTS7D2L");
		}

		async public Task LogIn(string name, string pass) {
			await ParseUser.LogInAsync (name, pass);
		}

		async public Task Register(string name, string pass) {
			ParseUser user = new ParseUser {
				Username = name,
				Password = pass
			};
			await user.SignUpAsync ();
		}

		async public Task AddCompany (string name) {
			ParseObject company = new ParseObject (ServerCompany);
			company [ServerName] = name;
			company [ServerOwner] = ParseUser.CurrentUser;
			await company.SaveAsync ();
		}
			
		public async Task RequestToJoinCompany (string name) {
			var query = ParseObject.GetQuery(ServerCompany)
				.WhereEqualTo(ServerName, name);
			ParseObject company = await query.FirstAsync();
			ParseRelation<ParseUser> companyRequesting = company.GetRelation<ParseUser> (ServerRequesting);
			companyRequesting.Add (ParseUser.CurrentUser);
			await company.SaveAsync ();
		}

		public async void CheckRequests () {
			var query = ParseObject.GetQuery (ServerCompany)
				.WhereEqualTo (ServerOwner, ParseUser.CurrentUser);
			ParseObject company = await query.FirstAsync ();
			var userRelation = company.GetRelation<ParseUser> (ServerRequesting);
			IEnumerable<ParseUser> users = await userRelation.Query.FindAsync ();
			foreach (ParseUser user in users) {
				NSObject test = new NSObject ();
				test.InvokeOnMainThread (async delegate {
					Boolean answer;
					answer = await AlertHandler.ShowCheckDriver (user.Username);
					if (answer) {
						Console.WriteLine ("YES");
					} else {
						Console.WriteLine ("NO");
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