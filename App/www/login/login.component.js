angular
.module('login')
.component('login',
{
    templateUrl: 'login/login.template.html',
    controller: ['UserService', 'Session', '$mdDialog',
		function LoginController(UserService, Session, $mdDialog)
		{
			this.result = '';
			this.userName = '';
    		this.showLoginFields = true;
    		var alias = this;

    		this.refresh = function(message)
    		{
    			var sessionInfo = Session.get();
    			if (sessionInfo.hasData)
    			{
    				alias.userName = sessionInfo.userName;
    				alias.showLoginFields = false;
    				alias.result = sessionInfo.sessionId; //temp
				}
    			else
    			{
    				alias.userName = '';
    				alias.showLoginFields = true;
    				alias.result = message; //temp
				}
    		}

    		this.refresh('');

    		this.doLogin = function (credentials)
    		{
    			UserService.login(credentials).then(
					function (message)
					{
						alias.refresh(message);
					},
					function (response)
					{
						alias.refresh(response);
					});
    		}

    		this.doLogOff = function ()
    		{
    			//TODO review
    			Session.clear();
    			alias.refresh('logoff');
    		}

    		this.register = function (credentials)
    		{
    			UserService.register(credentials).then(
					function (message)
					{
						alias.refresh(message);
					},
					function (response)
					{
						alias.refresh(response);
						//alias.refresh('sem resposta remota');
					});
    		}
		}
    ]
});