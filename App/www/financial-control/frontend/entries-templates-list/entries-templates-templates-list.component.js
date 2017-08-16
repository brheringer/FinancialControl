angular.module('entriesTemplatesList')
.component('entriesTemplatesList',
{
	templateUrl: 'financial-control/frontend/entries-templates-list/entries-templates-list.template.html',
	controller: ['EntryTemplateService',
        function EntryTemplateListController(EntryTemplateService)
        {
        	this.model = {}
        	this.orderBy = 'Account';
        	this.status = 'nothing...';
        	this.visibleFilters = true;
        	var alias = this;

        	resetModel = function ()
        	{
        		alias.model = {
        			Templates: []
        		}
        	}

        	resetModel();

        	this.search = function ()
        	{
        		EntryTemplateService.search(alias.model).then(
					function (dto)
					{
						if (dto.error)
						{
							alias.status = dto.error;
						}
						else
						{
							alias.model.Templates = dto.Templates;
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