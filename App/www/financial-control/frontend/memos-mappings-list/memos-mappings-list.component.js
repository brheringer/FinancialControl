angular.module('memosMappingsList')
.component('memosMappingsList',
{
	templateUrl: 'financial-control/frontend/memos-mappings-list/memos-mappings-list.template.html',
	controller: ['MemoMappingService',
        function AccountsListController(MemoMappingService)
        {
        	this.model = {}
        	this.orderBy = 'IncomingMemo';
        	this.status = 'nothing...';
        	this.visibleFilters = true;
        	var alias = this;

        	resetModel = function ()
        	{
        		alias.model = {
        			Mappings: []
        		}
        	}

        	resetModel();

        	this.search = function ()
        	{
        		MemoMappingService.search(alias.model).then(
					function (dto)
					{
						if (dto.error)
						{
							alias.status = dto.error;
						}
						else
						{
							alias.model.Mappings = dto.Mappings;
							alias.status = "search ok";
						}
					});
        	}

        	this.hideFilters = function ()
        	{
        		this.visibleFilters = false;
        	}

        	this.showFilters = function ()
        	{
        		this.visibleFilters = true;
        	}
        }]
});