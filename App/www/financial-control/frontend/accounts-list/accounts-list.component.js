angular.module('accountsList')
.component('accountsList',
{
	templateUrl: 'financial-control/frontend/accounts-list/accounts-list.template.html',
	controller: ['AccountService',
        function AccountsListController(AccountService)
        {
        	this.model = {}
        	this.orderBy = 'Structure';
        	this.status = 'nothing...';
        	this.visibleFilters = true;
        	var alias = this;

        	resetModel = function ()
        	{
        		alias.model = {
        			FilterDescription: '',
        			FilterStructure: '',
        			Accounts: []
        		}
        	}

        	resetModel();

        	this.search = function ()
        	{
        		AccountService.search(alias.model).then(
					function (dto)
					{
						if (dto.error)
						{
							alias.status = dto.error;
						}
						else
						{
							alias.model.Contas = dto.Accounts;
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