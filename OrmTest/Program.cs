using Linq2Db;
using LinqToDB.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace OrmTest
{
	public class Program
	{
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args)
		{
			DataConnection.DefaultSettings = new MySettings();
			return WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
		}
	}
}