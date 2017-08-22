angular.module('entryCard')
.directive('entryCard', function entryCard()
{
	return {
		restrict: 'E',
		templateUrl: 'financial-control/frontend/entry-card/entry-card.template.html',
		controller: 'EntryCardController',
		controllerAs: 'ctrl',
		scope: {
			model: '=ngModel',
			name: '@name',
			onclosecard: '=',
			onduplicatecard: '='
		}
	}
});

angular
.module('entryCard')
.controller('EntryCardController', ['$scope', 'EntryService', 'dateTimeHelper', function EntryCardController($scope, EntryService, dateTimeHelper)
{
	//TODO criar algum indicador que o card nao foi salvo, ou entao salvar sempre automaticamente, ou ambos
	//$scope.model
	//$scope.name
	var alias = this;
	this.status = 'nothing...';

	resetModel = function ()
	{ //TODO nao sei se isso vai ser usado aqui
		$scope.model = {
			AutoId: 0,
			Date: new Date(),
			Value: 0,
			Memo: '',
			Account: { AutoId: 0, Presentation: '' },
			Center: { AutoId: 0, Presentation: '' },
			Version: 0
		}
	}

	this.isPersistent = function ()
	{
		return $scope.model.AutoId > 0;
	}

	this.new = function ()
	{
		resetModel();
		alias.status = 'new ok';
	}

	this.duplicate = function ()
	{
		if ($scope.onduplicatecard)
		{
			var clone = dateTimeHelper.clone($scope.model);
			clone.AutoId = 0;
			clone.Version = 0;
			$scope.onduplicatecard(clone);
		}
	}

	this.save = function ()
	{
		EntryService.save($scope.model).then(
			function (dto)
			{
				if (dto.error)
				{
					alias.status = dto.error;
				}
				else
				{
					$scope.model = dto; //refresh
					alias.status = "save ok @ " + new Date();
				}
			});
	}

	this.delete = function ()
	{
		EntryService.delete($scope.model.AutoId).then(
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

	this.close = function ()
	{
		if ($scope.onclosecard)
			$scope.onclosecard($scope.$parent.$index);
	}
}]);