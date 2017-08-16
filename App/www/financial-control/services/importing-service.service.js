angular
.module('importingService')
.factory('ImportingService', ['GenericService',
	function (GenericService) 
	{
		var service = {};

		service.import = function (file)
		{
			var url = 'importing/';
			return GenericService.myPost(url, file);
		};
 
		return service;
	}]
);