angular
.module('core')
.filter('booleanPresentationX', function () {
	return function (booleanInput) {
		return booleanInput === true ? 'X' : '';
	}
})
.filter('booleanPresentation', function () {
	return function (booleanInput) {
		return booleanInput === true ? 'YES' : 'NO'; //TODO internationalization
	}
})
.filter('dateTimePresentation', function ($filter) {
	var angularDateFilter = $filter('date');
	return function (value) {
		var d = value;
		if (typeof value === 'string' || value instanceof String)
			if (value.indexOf('\/Date(') == 0) //coisa do aspnet
				d = new Date(parseInt(value.substr(6)));
		return angularDateFilter(d, 'dd/MM/yyyy HH:mm');
	}
})
.filter('datePresentation', function ($filter)
{
	var angularDateFilter = $filter('date');
	return function (value)
	{
		var d = value;
		if (typeof value === 'string' || value instanceof String)
			if (value.indexOf('\/Date(') == 0) //coisa do aspnet
				d = new Date(parseInt(value.substr(6)));
		return angularDateFilter(d, 'dd/MM/yyyy');
	}
})
.filter('entityReferencePresentation', function () {
	return function (dto) {
		return dto ? dto.Presentation : '';
	}
});