using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;
using System.Net.Http;

namespace Trukman.Helpers
{
	public static class MCQuery
	{
		private static string endpoint = "http://safersys.org/query.asp?searchtype=ANY&query_type=queryCarrierSnapshot&query_param=MC_MX&query_string=";
		public static async Task<MCResponse> verifyMC(string mc) 
		{
			string url = endpoint + mc;

			HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);
			message.Headers.Add("ContentType", "application/x-www-form-urlencoded");

			HttpResponseMessage response = GetHttpResponseMessage(message);
			response.EnsureSuccessStatusCode();
			string result = await response.Content.ReadAsStringAsync();
			MCResponse mcr = new MCResponse(mc);
			Regex dataRegex = new Regex("Legal Name:</A></TH>\\r\\n    <TD class=\\\"queryfield\\\" valign=top colspan=3>(?<Name>[a-zA-Z0-9\\s]{3,})&nbsp;</TD>\\r\\n   </TR><TR>\\r\\n    <TH SCOPE=\\\"ROW\\\" class=\\\"querylabelbkg\\\" align=right><A class=\\\"querylabel\\\" href=\\\"saferhelp.aspx#DBAName\\\">DBA Name:</A></TH>\\r\\n    <TD class=\\\"queryfield\\\" valign=top colspan=3>(?<DBA>[a-zA-Z0-9\\s]{3,})&nbsp;</TD>\\r\\n   </TR><TR>\\r\\n    <TH SCOPE=\\\"ROW\\\" class=\\\"querylabelbkg\\\" align=right><A class=\\\"querylabel\\\" href=\\\"saferhelp.aspx#PhysicalAddress\\\">Physical Address:</A></TH>\\r\\n    <TD class=\\\"queryfield\\\" valign=top colspan=3>\\r\\n     (?<Address>[a-zA-Z0-9\\s(<br>)(\\\\r),&;]{3,})/TD>\\r\\n   </TR><TR>\\r\\n    <TH SCOPE=\\\"ROW\\\" class=\\\"querylabelbkg\\\" align=right><A class=\\\"querylabel\\\" href=\\\"saferhelp.aspx#Phone\\\">Phone:</A></TH>\\r\\n    <TD class=\\\"queryfield\\\" valign=top colspan=3>(?<Phone>[0-9(-)-\\s]{6,})");
			Match data = dataRegex.Match(result);
			mcr.success = data.Success;
			if (data.Success)
			{
				mcr.name = data.Groups[1].Value;
				mcr.DBA = data.Groups[2].Value;
				mcr.address = new StringBuilder(data.Groups[3].Value)
					.Replace(@"<br>", "")
					.Replace("\\r\\n", "")
					.Replace("<", "")
					.Replace("&nbsp;", "")
					.ToString();
				mcr.phone = data.Groups[4].Value;
			}

			return mcr;
		}

		static HttpResponseMessage GetHttpResponseMessage (HttpRequestMessage httpRequestMessage)
		{
			using (HttpClient httpClient = new HttpClient ())
			{
				httpClient.Timeout = new TimeSpan(0, 0, 30);

				Task<HttpResponseMessage> responseMessageTask = httpClient.SendAsync (httpRequestMessage);
				HttpResponseMessage httpResponseMessage = responseMessageTask.Result;

				return httpResponseMessage;
			}
		}
	}

	public class MCResponse {
		public bool success { get; set; }
		public string name { get; set; }
		public string DBA { get; set; }
		public string phone { get; set; }
		public string address { get; set; }
		public string mcCode { get; set; }

		public MCResponse() { }

		public MCResponse(string mc) 
		{
			mcCode = mc;
		}
	}
}
