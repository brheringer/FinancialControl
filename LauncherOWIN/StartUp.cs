﻿using System;
using Owin;
using Microsoft.Owin.Cors;
using System.Web.Http;

[assembly: Microsoft.Owin.OwinStartup(typeof(FinancialControl.LauncherOWIN.Startup))]

namespace FinancialControl.LauncherOWIN
{
	public class Startup
	{
		public void Configuration(IAppBuilder appBuilder)
		{
			HttpConfiguration config = ConfigureWebAPI();

			config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
			config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

			appBuilder.UseCors(CorsOptions.AllowAll);
			appBuilder.UseWebApi(config);

			DoDependencyInjection();
		}

		private HttpConfiguration ConfigureWebAPI()
		{
			HttpConfiguration config = new HttpConfiguration();
			config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
			//config.Formatters.Add(Newtonsoft.Json.JsonConverter);
			return config;
		}

		private static void DoDependencyInjection()
		{
			string springConfigFilePath = FinancialControlConfiguration.SpringConfigFilePath;
			FinancialControl.DependencyInjection.DependencyInjectionContext.InitiateApplicationContext(springConfigFilePath);
		}

	}
}
