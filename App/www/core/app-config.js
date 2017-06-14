angular
.module('core')
.service('AppConfig', ['$cookies', function($cookies)
{
	var config = {
		'language': $cookies.lang,
		'serverBaseUrl': 'http://localhost:58452/api/'
	};

	return config;
}]);