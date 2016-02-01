using System;
using Parse;
using System.Collections;
using System.Threading;

namespace Trukman
{
	public class ServerManager : IServerManager
	{
		static string ServerCompany = "Company";
		static string ServerName = "name";
		static string ServerRequesting = "requesting";
		static string ServerOwner = "owner";
		static Timer timerForRequst;

		public ServerManager ()
		{
		}

		public void Init () {
			ParseClient.Initialize ("NsNjjvCGhqVKOZqCro2WOEr6gZHGTC9YlVB5jZqe", "WvSfa6MIvTb9L6BucGIiCQgV1zBc4OCR0UTS7D2L");
		}

		public void LogIn(string name, string pass) {
			ParseUser.LogInAsync (name, pass);
		}

		public void Register(string name, string pass) {
			ParseUser user = new ParseUser {
				Username = name,
				Password = pass
			};
			user.SignUpAsync ();
		}

		public void AddCompany (string name) {
			ParseObject company = new ParseObject (ServerCompany);
			company [ServerName] = name;
			company [ServerOwner] = ParseUser.CurrentUser;
			company.SaveAsync ();
		}
			
		public async void RequestToJoinCompany (string name) {
			var query = ParseObject.GetQuery(ServerCompany)
				.WhereEqualTo(ServerName, name);
			ParseObject company = await query.FirstAsync();
			var relation = company.GetRelation<ParseObject> (ServerRequesting);
			relation.Add (ParseUser.CurrentUser);
		}

		public async void CheckRequests () {
//			var query = ParseObject.GetQuery(ServerCompany)
//				.WhereEqualTo (
		}

//		void TimerForRequestFires () {
//
//		}
//
//		public void StartTimerForRequest () {
//			TimerCallback callback = new TimerCallback (TimerForRequestFires);
//			timerForRequst = new Timer (callback, null, 0, 5000);
//		}

	}
}

