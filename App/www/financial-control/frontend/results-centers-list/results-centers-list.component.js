angular.module('resultsCentersList')
.component('resultsCentersList',
{
	templateUrl: 'financial-control/frontend/results-centers-list/results-centers-list.template.html',
	controller: ['ResultCenterService',
        function ResultsCentersListController(ResultCenterService)
        {
        	this.model = {}
        	this.orderBy = 'Description';
        	this.status = 'nothing...';
        	this.visibleFilters = true;
        	var alias = this;

        	resetModel = function ()
        	{
        		alias.model = {
        			FilterDescription: '',
        			FilterCode: '',
        			ResultsCenters: []
        		}
        	}

        	resetModel();

        	this.search = function ()
        	{
        		ResultCenterService.search(alias.model).then(
					function (dto)
					{
						if (dto.error)
						{
							alias.status = dto.error;
						}
						else
						{
							alias.model.ResultsCenters = dto.ResultsCenters; //if you set model, filters will be lost
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