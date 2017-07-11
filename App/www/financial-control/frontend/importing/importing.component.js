angular
.module('importing')
.component('importing',
{
	templateUrl: 'financial-control/frontend/importing/importing.template.html',
	controller: ['ImportingService', 'AppConfig', function ImportingController(ImportingService, AppConfig)
	{
		this.model = {
			URL: AppConfig.serverBaseUrl + 'importing',
			FilePath: '',
			Processed: false,
			Success: false,
			Details: [] //on error: array of messages; on success: array with 1 message
		};
		var alias = this;

		this.import = function()
		{
			var myFile = document.getElementById('fieldFilePath'); //"file:///D:/Aplicacoes/FinancialControl/App/www/index.html"
			var data = readTextFile(myFile.value);
			ImportingService.import(data).then(
				function (dto)
				{
					alias.model.Processed = true;

					if (dto.error) //this is more generic than !Success
					{
						alias.model.Success = false;
						alias.model.Details = [dto.error];
					}
					else
					{
						if (dto.Success)
						{
							var msg = dto.quantityOfImportedEntries + ' entries imported successfully ' + '@' + dto.timestamp;
							alias.model.Details = [msg];
							alias.model.Success = true;
						}
						else
						{
							alias.model.Details = dto.ErrorsMessages;
							alias.model.Success = false;
						}
					}
				});
		}

		function readTextFile(file)
		{
			var rawFile = new XMLHttpRequest();
			rawFile.open("GET", file, false);
			rawFile.onreadystatechange = function ()
			{
				if (rawFile.readyState === 4)
				{
					if (rawFile.status === 200 || rawFile.status == 0)
					{
						var allText = rawFile.responseText;
						alert(allText);
					}
				}
			}
			rawFile.send(null);
		}

	}]
});
