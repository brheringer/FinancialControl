angular
.module('core.services.genericService')
.factory('GenericService', ['$http', 'Session', 'AppConfig',
	function ($http, Session, AppConfig)
	{
		var service = {};

		getHeaders = function ()
		{
			var sessionInfo = Session.get();
			return { 'SessionId': sessionInfo.sessionId, 'UserName': sessionInfo.userName };
		}

		getFullUrl = function (serviceUrl, data)
		{
			var fullUrl = AppConfig.serverBaseUrl + serviceUrl;
			if (data)
				fullUrl = fullUrl + '/' + data;
			return fullUrl;
		}

		service.dealWithSuccess = function (response)
		{
			if (response.data.Response.HasException)
			{
				return service.dealWithFailure(response);
			}
			else
			{
				return response.data;
			}
		}

		service.dealWithFailure = function (response)
		{
			var msg = '';
			if (response.data && response.data.Response)
			{
				if (response.data.Response.HasException)
				{
					msg += response.data.Response.Exception;
				}
				else
				{
					if (response.data.Message)
						msg += response.data.Message;
					if (response.data.ExceptionMessage)
						msg += response.data.ExceptionMessage;
				}
				if (msg == '')
					msg = response; //TODO teoricamente isso nunca acontece
			}
			else
			{
				msg = 'sem resposta remota ou sem autenticacao'; //TODO tratar caso sem autenticacao separado
			}
			return { error: msg };
		}

		service.myGet = function (url, data)
		{
			var config = { headers: getHeaders() };

			return $http
				.get(getFullUrl(url, data), config)
				.then(function (response)
				{
					return service.dealWithSuccess(response);
				},
				function ()
				{
					return service.dealWithFailure(response);
				});
		}

		service.myPost = function (url, dto)
		{
			var config = { headers: getHeaders() };

			return $http
				.post(getFullUrl(url), dto, config)
				.then(function (response)
				{
					return service.dealWithSuccess(response);
				},
				function (response)
				{
					return service.dealWithFailure(response);
				});
		}

		service.myDelete = function (url, data)
		{
			var config = { headers: getHeaders() };

			return $http
				.delete(getFullUrl(url, data), config)
				.then(function (response)
				{
					return service.dealWithSuccess(response);
				},
				function (response)
				{
					return service.dealWithFailure(response);
				});
		}

		return service;
	}]
);