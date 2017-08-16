angular
.module('entryTemplateService')
.factory('EntryTemplateService', ['GenericService',
	function (GenericService) 
	{
		var service = {};

		service.delete = function (id)
		{
			var url = 'entryTemplate';
			return GenericService.myDelete(url, id);
		};
		 
		service.load = function (id)
		{
			var url = 'entryTemplate';
			return GenericService.myGet(url, id);
		};

		service.save = function (tipo)
		{
			var url = 'entryTemplate/update';
			return GenericService.myPost(url, tipo);
		};

		service.search = function (filtros)
		{
			var url = 'entryTemplate/search';
			return GenericService.myPost(url, filtros);
		};
  
		return service;
	}]
);