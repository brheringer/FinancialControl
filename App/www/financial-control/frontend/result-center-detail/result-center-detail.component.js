angular
.module('resultCenterDetail')
.component('resultCenterDetail',
{
	templateUrl: 'financial-control/frontend/result-center-detail/result-center-detail.template.html',
	controller: ['$routeParams', 'ResultCenterService', function ResultCenterDetailController($routeParams, ResultCenterService)
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
				User: '',
				Presentation: '',
				Version: 0
			}
		}

		resetModel();

		if ($routeParams.id > 0)
		{
			ResultCenterService.load($routeParams.id).then(
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
			ResultCenterService.save(alias.model).then(
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
			ResultCenterService.delete(alias.model.AutoId).then(
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
