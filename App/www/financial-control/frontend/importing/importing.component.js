angular
.module('importing')
.component('importing',
{
	templateUrl: 'financial-control/frontend/importing/importing.template.html',
	controller: ['ImportingService', 'AppConfig', function ImportingController(ImportingService, AppConfig)
	{
		this.model = {
			Processed: false,
			Success: false,
			Details: [] //on error: array of messages; on success: array with 1 message
		};
		var alias = this;

		this.import = function ()
		{
			var fileList = document.getElementById('fieldFilePath').files;
			ImportingService.import(fileList[0]).then(
				function (dto)
				{
					if (dto.error)
					{
						alias.model.Success = false;
						alias.model.Details = [dto.error];
						alias.model.Processed = true;
					}
					else
					{
						if (dto.Success)
						{
							var msg = dto.quantityOfImportedEntries + ' entries imported successfully ' + '@' + dto.timestamp;
							alias.model.Details = [msg];
							alias.model.Success = true;
							alias.model.Processed = true;
						}
						else
						{
							alias.model.Details = dto.ErrorsMessages;
							alias.model.Success = false;
							alias.model.Processed = true;
						}
					}
				});
		}
	}]
});
