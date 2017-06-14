angular
.module('core.services.user')
.factory('UserService', ['GenericService', 'Session',
	function (GenericService, Session)
	{
		var authService = {};
 
		authService.login = function (credentials) 
		{
			return GenericService.myPost('login', credentials).then(function (response)
			{
				if (response.error)
				{
					Session.clear();
					return response;
				}
				else
				{
					var userSession = response;
					Session.set(userSession.UserName, userSession.UserSessionId);
					return userSession.UserName;
				}
			});
		};

		authService.register = function (credentials)
		{
			return GenericService.myPost('login/register', credentials);
		};
  
		return authService;
	}]
);
//https://medium.com/opinionated-angularjs/techniques-for-authentication-in-angularjs-applications-7bbf0346acec#.yoegreljb
