angular
.module('memoMappingDetail')
.component('memoMappingDetail',
{
	templateUrl: 'financial-control/frontend/memo-mapping-detail/memo-mapping-detail.template.html',
	controller: ['$routeParams', 'MemoMappingService', function MemoMappingDetailController($routeParams, MemoMappingService)
	{
		this.model = {};
		this.status = 'nothing...';
		var alias = this;

		resetModel = function ()
		{
			alias.model = {
				AutoId: 0,
				IncomingMemo: '',
				MappedMemo: '',
				MappedAccount: { AutoId: 0, Presentation: '' },
				MappedCenter: { AutoId: 0, Presentation: '' },
				User: '',
				Presentation: '',
				Version: 0
			}
		}

		resetModel();

		if ($routeParams.id > 0)
		{
			MemoMappingService.load($routeParams.id).then(
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
			MemoMappingService.save(alias.model).then(
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
			MemoMappingService.delete(alias.model.AutoId).then(
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
