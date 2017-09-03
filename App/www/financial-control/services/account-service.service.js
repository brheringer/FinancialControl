angular
.module('accountService')
.factory('AccountService', ['GenericService',
	function (GenericService) 
	{
		var service = {};

		service.delete = function (id)
		{
			var url = 'account';
			return GenericService.myDelete(url, id);
		};
		 
		service.load = function (id)
		{
			var url = 'account';
			return GenericService.myGet(url, id);
		};

		service.save = function (tipo)
		{
			var url = 'account/update';
			return GenericService.myPost(url, tipo);
		};

		service.search = function (filtros)
		{
			var url = 'account/search';
			return GenericService.myPost(url, filtros);
		};
  
		return service;
	}]
);