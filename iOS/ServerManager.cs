using System;
using Parse;

namespace Trukman
{
	public class ServerManager : IServerManager
	{
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
	}
}

