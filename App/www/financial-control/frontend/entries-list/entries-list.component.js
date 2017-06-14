angular.module('entriesList')
.component('entriesList',
{
	templateUrl: 'financial-control/frontend/entries-list/entries-list.template.html',
	controller: ['EntryService',
        function EntriesListController(EntryService)
        {
        	this.model = {}
        	this.orderBy = 'Structure';
        	this.status = 'nothing...';
        	this.visibleFilters = true;
        	this.didSearch = false;
        	var alias = this;

        	resetModel = function ()
        	{
        		alias.model = {
        			FilterAccount: { AutoId: 0, Presentation: '' },
        			FilterCenter: { AutoId: 0, Presentation: '' },
					FilterInitialDate: null,
					FilterFinalDate: null,
					FilterExactDate: null,
					FilterLowerValue: 0,
					FilterHigherValue: 0,
					FilterExactValue: 0,
					FilterMemo: '',
					Entries: []
        		}
        	}

        	resetModel();

        	this.search = function ()
        	{
        		EntryService.search(alias.model).then(
					function (dto)
					{
						if (dto.error)
						{
							alias.status = dto.error;
						}
						else
						{
							alias.model.Entries = dto.Entries;
							alias.status = "search ok";
							alias.didSearch = true;
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