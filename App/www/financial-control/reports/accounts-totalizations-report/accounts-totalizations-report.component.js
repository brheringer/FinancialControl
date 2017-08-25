angular.module('accountsTotalizationsReport')
.component('accountsTotalizationsReport',
{
	templateUrl: 'financial-control/reports/accounts-totalizations-report/accounts-totalizations-report.template.html',
	controller: ['AccountsTotalizationsReportService',
        function AccountsTotalizationsReportController(AccountsTotalizationsReportService)
        {
        	this.model = {}
        	this.orderBy = 'AccountStructure';
        	this.status = 'nothing...';
        	this.visibleFilters = true;
        	this.didSearch = false;
        	this.analytical = false;
        	var alias = this;

        	resetModel = function ()
        	{
        		alias.model = {
					FilterInitialDate: null,
					FilterFinalDate: null,
					Totalizations: [] //contains AccountStructure, AccountDescription, Total, AnalyticalDetails[] : Date, Value, Memo, OriginalEntry (entity reference)
        		}
        	}

        	resetModel();

        	this.generateReport = function ()
        	{
        		AccountsTotalizationsReportService.generateReport(alias.model).then(
					function (dto)
					{
						if (dto.error)
						{
							alias.status = dto.error;
						}
						else
						{
							alias.model.Totalizations = dto.AccountsTotalizations;
							alias.status = "report ok";
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