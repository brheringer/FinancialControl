angular
.module('accountsTotalizationsReportService')
.factory('AccountsTotalizationsReportService', ['GenericService',
	function (GenericService) 
	{
		var service = {};

		service.generateReport = function (filtros)
		{
			var url = 'accountsTotalizationsReport';
			return GenericService.myPost(url, filtros);
		};
  
		return service;
	}]
);