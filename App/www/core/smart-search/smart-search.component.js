angular
.module('core.smartSearch')
.directive('smartSearch', function smartSearch() //factory
{
	return {
		restrict: 'E',
		templateUrl: 'core/smart-search/smart-search.template.html',
		controller: 'SmartSearchController',
		controllerAs: 'ctrl',
		scope: {
			entityInfo: '=ngModel',
			isRequired: '@isRequired',
			requireMatch: '@requireMatch',
			minLength: '@minLength',
			name: '@name',
			targetService: '@targetService',
			smartonchanged: '='
		}
	}
});

angular
.module('core.smartSearch')
.controller('SmartSearchController', ['$scope', 'GenericService', function SmartSearch2Controller($scope, GenericService) {
	var self = this;
	self.simulateQuery = false;
	self.isDisabled = false;
	self.visible = false;
	self.searchText = null;
	self.selectedItem = null;
	self.labelName = ($scope.name) ? $scope.name : " "; // If you defined a floating label in your component, it has to be at least an empty string or it will not render.

	self.smartSearchServiceUrl = $scope.targetService + '/SmartSearch'; //TODO

	if ($scope.entityInfo == null)
		$scope.entityInfo = { 'AutoId': 0, 'EntryKey': '', 'Presentation': '' };

	//self.lastSmartEntryForCacheImprovement = '';
	//self.lastGrabbedItems = [];
	//self.status = '';

	self.search = function (smartEntry) {
		if (smartEntry == $scope.entityInfo.Presentation) {
			return createFakeListForSelection();
		}
		else if (!isSearchNecessary(smartEntry)) {
			return searchInCache(smartEntry);
		}
		else {
			return searchReally(smartEntry);
		}
	}

	function createFakeListForSelection() {
		var item = {
			'AutoId': $scope.entityInfo.AutoId,
			'EntryKey': $scope.entityInfo.EntryKey,
			'Presentation': $scope.entityInfo.Presentation
		};
		var list = [item];
		return list;
	}

	function searchInCache(smartEntry) {
		//var theList = self.lastGrabbedItems;
		//var filteredData = smartEntry ? theList.filter(createFilterFor(smartEntry)) : theList;
		//return filteredData;
	}

	function searchReally(smartEntry) {
		if (smartEntry === '')
			return [];

		//self.lastSmartEntryForCacheImprovement = smartEntry;

		var promise = GenericService.myGet(self.smartSearchServiceUrl, smartEntry)
			.then(function (data) {
				if (data.error) {
					//TODO show(data.error); ?
					//self.status = data.error;
					return [];
				}
				else {
					var theList = data.References;
					//self.lastGrabbedItems = theList;
					var filteredData = smartEntry ? theList.filter(createFilterFor(smartEntry)) : theList, promise;
					return filteredData;
				}
			});
		return promise;
	}

	function isSearchNecessary(smartEntry) {
		//var precisa = false;
		//if (self.lastSmartEntryForCacheImprovement == '')
		//	precisa = true;
		//else if (smartEntry)
		//	precisa = !startsWith(smartEntry, self.lastSmartEntryForCacheImprovement);
		//return precisa;
		return true;
	}

	function startsWith(longer, shorter) {
		return (angular.lowercase(longer).lastIndexOf(angular.lowercase(shorter), 0) === 0);
	}

	function createFilterFor(smartEntry) {
		var lowercaseQuery = angular.lowercase(smartEntry);

		return function filterFn(item) {
			return (angular.lowercase(item.Presentation).indexOf(lowercaseQuery) >= 0);
		};
	}

	self.selectedItemChange = function (item) {
		if ($scope.entityInfo == null)
			$scope.entityInfo = { 'AutoId': 0, 'EntryKey': '', 'Presentation': '' };

		var changed = false;
		if (item) {
			if ($scope.entityInfo == null || $scope.entityInfo.AutoId != item.AutoId)
				changed = true;

			$scope.entityInfo.AutoId = item.AutoId;
			$scope.entityInfo.EntryKey = item.EntryKey;
			$scope.entityInfo.Presentation = item.Presentation;
		}
		else {
			if ($scope.entityInfo.AutoId != 0)
				changed = true;

			$scope.entityInfo.AutoId = 0;
			$scope.entityInfo.EntryKey = '';
			$scope.entityInfo.Presentation = '';
		}

		if (changed)
			if ($scope.smartonchanged)
				$scope.smartonchanged();
	}

	self.searchTextChange = function searchTextChange(text) {
	}

	function updateSelectedItem() {
		//quando o item muda externamente, tem que "selecioná-lo"
		//TODO problema: quando seleciona um item, ta chamando aqui tambem
		if ($scope.entityInfo.AutoId > 0) {
			$scope.searchText = $scope.entityInfo.Presentation;
		}
		else {
			$scope.searchText = '';
		}
		console.log('smart-search.component: updateSelectedItem');
	}

	updateSelectedItem();

	$scope.$watch('entityInfo.AutoId', updateSelectedItem, true);
}]);
