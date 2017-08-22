angular.module('entriesBoard')
.component('entriesBoard',
{
	templateUrl: 'financial-control/frontend/entries-board/entries-board.template.html',
	controller: ['$routeParams', '$scope', 'EntryService', 'EntryTemplateService',
	function EntriesBoardController($routeParams, $scope, EntryService, EntryTemplateService)
	{
		var alias = this;
		this.status = '';
		//TODO talvez parametrizar: this.cardColumns = 2;

        //$scope.entriesOnBoard; //lancamentos abertos no board;
		//os lancamentos tem que ficar no escopo da aplicacao para poder ser alterado na pesquisa e mantidos durante a navegacao
		//por exemplo, usuario entra na tela de pesquisa, pesquisa e abre um lancamento no board (navega para o board)
		//depois ele cria mais dois lancamentos novos no board (entao sao 3 lancamentos no board)
		//entao o usuario volta pra tela de pesquisa, pesquisa e abre outro lancamento no board (navega para o board)
		//nesse momento, o board deve exibir os 4 lancamentos
		//um lancamento so sai do board quando usuario fecha o lancamento
		//TODO talvez tenha que tomar cuidado para os lancamentos de um usuario nao ficarem no escopo para outro usuario ver ao relogar com outro usuario

        if (!$scope.entriesOnBoard)
        	$scope.entriesOnBoard = [];
        $scope.templates = [];

        loadEntry($routeParams.id);
        loadTemplates();

        function loadEntry(id)
        {
        	if (id > 0)
        	{
        		EntryService.load(id).then(
					function (dto)
					{
						if (dto.error)
						{
							alias.status = dto.error;
						}
						else
						{
							alias.add(dto);
							alias.status = 'load ok';
						}
					});
        	}
        }

        function loadTemplates()
        {
        	EntryTemplateService.search(null).then(
				function (dto)
				{
					if (dto.error)
					{
						alias.status = 'templates: ' + dto.error;
					}
					else
					{
						$scope.templates = dto.Templates;
						alias.status = 'templates ok';
					}
				});
        }

        this.closeAll = function()
        {
			//TODO e se tiver algum nao salvo?
        	$scope.entriesOnBoard = [];
        }

        this.saveAll = function()
        {
        	for (var i = 0; i < $scope.entriesOnBoard.lenght; i++)
        		save($scope.entriesOnBoard[i]);
        }

        function save(entry)
        {
        	EntryService.save(entry).then(
				function (dto)
				{
					if (dto.error)
					{
						alias.status = dto.error;
					}
					else
					{
						//TODO refresh //$scope.model = dto; //refresh
						alias.status = "save ok @ " + new Date();
					}
				});
        }

        this.addNewCard = function()
        {
			//TODO talvez refatorar para outro lugar para ser reusavel
        	var entry = {
        		AutoId: 0,
        		Date: new Date(),
        		Value: 0,
        		Memo: '',
        		Account: { AutoId: 0, Presentation: '' },
        		Center: { AutoId: 0, Presentation: '' }, 
        		Version: 0
        	}
        	alias.add(entry);
        }

        this.addNewCardUsingTemplate = function(template)
        {
        	var entry = {
        		AutoId: 0,
        		Date: new Date(),
        		Value: template.Value,
        		Memo: template.Memo,
        		Account: template.Account, //TODO clone?
        		Center: template.Center, //TODO clone?
        		Version: 0
        	}
        	alias.add(entry);
        }

        this.add = function (entry)
        {
        	$scope.entriesOnBoard.push(entry);
        }

        this.closeCard = function (cardIndex)
        {
        	if (cardIndex >= 0 && cardIndex <= $scope.entriesOnBoard.length)
        	{
        		$scope.entriesOnBoard.splice(cardIndex, 1);

				//correct new indexes
        		for (var i = 0; i < $scope.entriesOnBoard.length; i++)
        			$scope.entriesOnBoard[i].CardIndex = 0;
        	}
        }
	}]
});
