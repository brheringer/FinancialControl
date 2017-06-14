angular
.module('core.dateTimePicker')
.directive('dateTimePicker', function dateTimePicker() //factory
{
	var x = {
		restrict: 'E',
		templateUrl: 'core/date-time-picker/date-time-picker.template.html',
		controller: 'DateTimePickerController',
		controllerAs: 'ctrl',
		scope: {
			datetime: '=ngModel',
			required: '@required',
			name: '@name',
			config: '=', //{ 'startView': 'day', 'minView': 'minute', 'minuteStep': 15 }
			onchangeddatetime: '='
		}
	}
	return x;
});

angular
.module('core.dateTimePicker')
.controller('DateTimePickerController', ['$scope', function DateTimePickerController($scope)
{
	var alias = this;

	//$scope = isolated scope created by directive
	//TODO isRequired not working

	this.onSetTime = function (newDate, oldDate)
	{
		//$id = scope $id, that is unique for each child scope by angularjs
		$('#anchorFor_dateTimePicker_' + $scope.$id).modal('hide');

		var changed = (newDate != oldDate);
		if (changed)
			if ($scope.onchangeddatetime)
				$scope.onchangeddatetime();
	}

}]);
