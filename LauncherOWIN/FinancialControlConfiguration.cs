using System;
using System.Configuration;

namespace FinancialControl.LauncherOWIN
{
	public static class FinancialControlConfiguration
	{
		public static string AppPath
		{
			get
			{
				return ConfigurationManager.AppSettings["appPath"];
			}
		}

		public static int Port
		{
			get
			{
				int port;
				if (!int.TryParse(ConfigurationManager.AppSettings["port"], out port))
					port = 9000;
				return port;
			}
		}

		public static string SpringConfigFilePath
		{
			get
			{
				return ConfigurationManager.AppSettings["springConfigFilePath"];
			}
		}
	}
}
