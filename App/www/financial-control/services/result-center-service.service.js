angular
.module('resultCenterService')
.factory('ResultCenterService', ['GenericService',
	function (GenericService) 
	{
		var service = {};

		service.delete = function (id)
		{
			var url = 'resultCenter';
			return GenericService.myDelete(url, id);
		};
		 
		service.load = function (id)
		{
			var url = 'resultCenter';
			return GenericService.myGet(url, id);
		};

		service.save = function (tipo)
		{
			var url = 'resultCenter/update';
			return GenericService.myPost(url, tipo);
		};

		service.search = function (filtros)
		{
			var url = 'resultCenter/search';
			return GenericService.myPost(url, filtros);
		};
  
		return service;
	}]
);