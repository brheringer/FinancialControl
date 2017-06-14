angular
.module('core')
.factory('dateTimeHelper', function ()
{
	var service = {};

	service.deserializeIncomingSerializedDate = function (serializedDate) {
		if (serializedDate == null || serializedDate == '')
			return null; //TODO mesmo?

		//TODO undefined, null, '', is string
		//TODO if starts with \/Date(
		var d = new Date(parseInt(serializedDate.substr(6)));
		return d;
	}

	service.serializeOutcomingDate = function (date) {
		if (date == null)
			return null;

		//TODO isso quebra a abstracao, mas se fizer do lado servidor tambem quebra
		//algumas pessoas defendem que as linhas comentadas abaixo sao o jeito certo de fazer, mas nao deu certo
		//var epochTicks = 621355968000000000; // the number of .net ticks at the unix epoch
		//var ticksPerMillisecond = 10000; // there are 10000 .net ticks per millisecond
		//var ticks = epochTicks + (date.getTime() * ticksPerMillisecond);
		return '\/Date(' + date.getTime() + '-0000)\/';
	}

	service.clone = function (obj) {
		//credito: http://stackoverflow.com/questions/728360/how-do-i-correctly-clone-a-javascript-object
		var copy;

		// Handle string, number, boolean, and null or undefined
		if (null == obj || "object" != typeof obj) return obj;

		if (obj instanceof Date) {
			copy = new Date();
			copy.setTime(obj.getTime());
			return copy;
		}

		if (obj instanceof Array) {
			copy = [];
			for (var i = 0, len = obj.length; i < len; i++) {
				copy[i] = service.clone(obj[i]);
			}
			return copy;
		}

		if (obj instanceof Object) {
			copy = {};
			for (var attr in obj) {
				if (obj.hasOwnProperty(attr)) copy[attr] = service.clone(obj[attr]);
			}
			return copy;
		}

		throw new Error("Unable to copy obj! Its type isn't supported.");
	}

	return service;
});