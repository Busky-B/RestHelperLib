using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace RestHelperLib
{
	public class RestHelper // <T> Detta kan vidareutvecklas
	{
		// Skapar en statisk httpclient som kan användas genom hela projektet.
		private static readonly HttpClient client = new HttpClient();
		// TODO, kom på ett sätt att hantera apisträng så att helpern inte är låst till denna specifika API
		// Connectionstring till api.
		private static readonly string api = "http://informatik12.ei.hv.se/grupp5v2/api/";

		// API / GET
		public static async Task<List<T>> ApiGet<T>(string apiPath)
		{
			if (apiPath == null) 
			{
				return NotFound();
			}
			// Generisk lista som kan spara olika typer av klasser.
			var returnList = new List<T>();
			try
			{
				var res = await client.GetAsync(api + apiPath);

				var jRes = await res.Content.ReadAsStringAsync();

				returnList = JsonConvert.DeserializeObject<List<T>>(jRes);

			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine("Something went wrong!!!: \n \t " + e.ToString());
				throw;
			}

			return returnList;
		}

		// API / GET / ID - Överladdad get-metod för att hämta enskilt objekt.
		public static async Task<T> ApiGet<T>(string apiPath, int? id)
		{
			var res = await client.GetAsync(api + apiPath + id);

			var jRes = await res.Content.ReadAsStringAsync();

			var returnObj = JsonConvert.DeserializeObject<T>(jRes);

			return returnObj;

		}
		// API / POST
		public static async Task ApiCreate<T>(string apiPath, T newObj)
		{
			await client.PostAsJsonAsync(api + apiPath, newObj);
		}

		// API / PUT
		public static async Task ApiEdit<T>(string apiPath, T oldObj)
		{
			await client.PutAsJsonAsync(api + apiPath, oldObj);
		}

		// API / DELETE
		public static async Task ApiDelete<T>(string apiPath, int? id)
		{
			await client.DeleteAsync(api + apiPath + id);
		}


		/// <summary>
		/// Metod som skriver ut namn på properties som finns i ett object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"> Valfritt object, måste specificeras vid methodcall</param>
		/// <returns> sammansatt sträng av alla props och values </returns>


		// TODO --> Fixa så att det inte crashar när ett foreignkey objekt skall finnas i en modell. Detta skapar nullreference exception.
		// Kanske fixa helt enkelt med en try/catch? enkel lösning så kan vi jobba vidare senare med smartare lösning.
		
		
		public static String PrintObjProps<T>(T obj)
		{
			string s = "";
			var filler = new String('#', 20);

			foreach (var pInfo in obj.GetType().GetProperties())
			{
				var propType = pInfo.Name;
				var propVal = pInfo.GetValue(obj, null);
				
				Console.WriteLine(propType.ToString() + ": " + propVal.ToString());
				s += propType.ToString() + ": " + propVal.ToString() + "\n";
			}

			Console.WriteLine(filler);
			s += filler;

			return s;
		}
		// överladdad version som tar emot en lista istället för enskilt object.
		public static String PrintObjProps<T>(List<T> objList)
		{
			string s = "";
			var filler = new String('#', 20);
			foreach (var obj in objList)
			{
				foreach (var pInfo in obj.GetType().GetProperties())
				{
					var propType = pInfo.Name;
					var propVal = pInfo.GetValue(obj, null);

					Console.WriteLine(propType.ToString() + ": " + propVal.ToString());
					s += propType.ToString() + ": " + propVal.ToString() + "\n";
				}
				// Markerar slutet på ett object
				s += filler;
				Console.WriteLine(filler);
			}
			
			return s;
		}
	}
}
