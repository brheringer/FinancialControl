angular
.module('entryService')
.factory('EntryService', ['GenericService',
	function (GenericService) 
	{
		var service = {};

		service.delete = function (id)
		{
			var url = 'entry';
			return GenericService.myDelete(url, id);
		};
		 
		service.load = function (id)
		{
			var url = 'entry';
			return GenericService.myGet(url, id);
		};

		service.save = function (tipo)
		{
			var url = 'entry/update';
			return GenericService.myPost(url, tipo);
		};

		service.search = function (filtros)
		{
			var url = 'entry/search';
			return GenericService.myPost(url, filtros);
		};
  
		return service;
	}]
);