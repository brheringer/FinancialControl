angular
.module('core')
.factory('Session', ['$cookies', function($cookies)
{
	var _session = { userName: '', sessionId: '' };

	return {
		set: function (userName, sessionId)
		{
			_session.userName = userName;
			_session.sessionId = sessionId;

			var today = new Date();
			var expired = new Date(today);
			expired.setDate(today.getDate() + 1); //Set expired date to tomorrow

			$cookies.put('userName', userName, { expires: expired });
			$cookies.put('sessionId', sessionId, { expires: expired });
		},
		get: function ()
		{
			_session.userName = $cookies.get('userName');
			_session.sessionId = $cookies.get('sessionId');
			if (_session.userName == null)
				_session.hasData = false;
			else
				_session.hasData = true;
			return _session;
		},
		clear: function ()
		{
			_session = { userName: '', sessionId: '' };
			$cookies.remove('userName');
			$cookies.remove('sessionId');
		}
	}
}]);