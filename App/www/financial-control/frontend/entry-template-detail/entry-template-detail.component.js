angular
.module('entryTemplateDetail')
.component('entryTemplateDetail',
{
	templateUrl: 'financial-control/frontend/entry-template-detail/entry-template-detail.template.html',
	controller: ['$routeParams', 'EntryTemplateService', function EntryTemplateDetailController($routeParams, EntryTemplateService)
	{
		this.model = {};
		this.status = 'nothing...';
		var alias = this;

		resetModel = function ()
		{
			alias.model = {
				AutoId: 0,
				Title: '',
				Value: 0,
				Memo: '',
				Account: { AutoId: 0, Presentation: '' },
				Center: { AutoId: 0, Presentation: '' },
				User: '',
				Presentation: '',
				Version: 0
			}
		}

		resetModel();

		if ($routeParams.id > 0)
		{
			EntryTemplateService.load($routeParams.id).then(
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
			EntryTemplateService.save(alias.model).then(
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
			EntryTemplateService.delete(alias.model.AutoId).then(
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
