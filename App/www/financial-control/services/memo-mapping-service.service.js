angular
.module('memoMappingService')
.factory('MemoMappingService', ['GenericService',
	function (GenericService) 
	{
		var service = {};

		service.delete = function (id)
		{
			var url = 'memoMapping';
			return GenericService.myDelete(url, id);
		};
		 
		service.load = function (id)
		{
			var url = 'memoMapping';
			return GenericService.myGet(url, id);
		};

		service.save = function (tipo)
		{
			var url = 'memoMapping/update';
			return GenericService.myPost(url, tipo);
		};

		service.search = function (filtros)
		{
			var url = 'memoMapping/search';
			return GenericService.myPost(url, filtros);
		};
  
		return service;
	}]
);