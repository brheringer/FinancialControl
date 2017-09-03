angular
.module('accountDetail')
.component('accountDetail',
{
	templateUrl: 'financial-control/frontend/account-detail/account-detail.template.html',
	controller: ['$routeParams', 'AccountService', function AccountDetailController($routeParams, AccountService)
	{
		this.model = {};
		this.status = 'nothing...';
		var alias = this;

		resetModel = function ()
		{
			alias.model = {
				AutoId: 0,
				Code: '',
				Description: '',
				Structure: '',
				Type: '',
				User: '',
				Presentation: '',
				Version: 0
			}
		}

		resetModel();

		if ($routeParams.id > 0)
		{
			AccountService.load($routeParams.id).then(
				function (dto)
				{
					if (dto.error)
					{
						resetModel();
						alias.status = dto.error;
					}
					else
					{
						alias.model = dto;
						alias.status = 'load ok';
					}
				});
		}

		this.isPersistent = function ()
		{
			return alias.model.AutoId > 0;
		}

		this.new = function()
		{
			resetModel();
			alias.status = 'new ok';
		}

		this.save = function()
		{
			AccountService.save(alias.model).then(
				function (dto)
				{
					if (dto.error)
					{
						alias.status = dto.error;
					}
					else
					{
						alias.model = dto; //refresh
						alias.status = "save ok @ " + new Date();
					}
				});
		}

		this.delete = function ()
		{
			AccountService.delete(alias.model.AutoId).then(
				function (dto)
				{
					if (dto.error)
					{
						alias.status = dto.error;
					}
					else
					{
						resetModel();
						alias.status = "delete ok";
					}
				});
		}
	}]
});
