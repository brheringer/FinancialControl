angular
.module('importingService')
.factory('ImportingService', ['GenericService',
	function (GenericService) 
	{
		var service = {};

		service.import = function (data)
		{
			var url = 'importing/';
			return GenericService.myPost(url, data);
		};
 
		return service;
	}]
);