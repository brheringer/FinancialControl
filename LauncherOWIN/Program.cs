using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace FinancialControl.LauncherOWIN
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				StartWebApp();
				StartWebApiHost();
			}
			catch(Exception ex)
			{
				Console.Write(ex.ToString());
				Console.ReadLine();
			}
		}

		private static void StartWebApiHost()
		{
			string baseAddress = "http://localhost:" + FinancialControlConfiguration.Port;

			// Start OWIN host 
			using (WebApp.Start<Startup>(url: baseAddress))
			{
				Console.WriteLine("Service running at " + baseAddress + ". Press Enter to quit.");
				Console.ReadLine();
			}
		}

		private static void StartWebApp()
		{
			string path = FinancialControlConfiguration.AppPath;
			if (!string.IsNullOrEmpty(path))
			{
				if (System.IO.File.Exists(path))
				{
					System.Diagnostics.Process.Start(path);
				}
				else
				{
					throw new InvalidOperationException("Check the 'path' in app.config. It refers to a file that does not exists:" + path);
				}
			}
			else
			{
				throw new InvalidOperationException("Set the 'path' in app.config.");
			}
		}
	}
}
