using Spring.Context.Support;

namespace FinancialControl.DependencyInjection
{
    public class DependencyInjectionContext
    {
		public static XmlApplicationContext ApplicationContext { get; set; }

		public static void InitiateApplicationContext(string configPath)
		{
			ApplicationContext = new XmlApplicationContext(configPath);
			//string path = System.Web.HttpContext.Current.Request.MapPath(configPath);
		}
    }
}
