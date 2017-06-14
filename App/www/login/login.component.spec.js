describe('login', function ()
{
	beforeEach(module('login'));

	describe('LoginController', function ()
	{
		var ctrl;
		var $httpBackend; //We configure "fake" responses to server requests by calling methods on a service called $httpBackend

		// The injector ignores leading and trailing underscores here (i.e. _$httpBackend_).
		// This allows us to inject a service and assign it to a variable with the same name
		// as the service while avoiding a name conflict.
		beforeEach(inject(function ($componentController, _$httpBackend_)
		{
			ctrl = $componentController('login');

			var fakeResponse = {
				"_resposta": { "ExceptionDataMember": null, "ExceptionIdDataMember": 0, "WarningsDataMember": [] },
				"IsAdmin": false, "UserName": "sa", "UserSessionId": "d51a4cea-7a5f-4eef-aa08-f256c2416c82"
			};

			$httpBackend = _$httpBackend_;
			$httpBackend.expectPOST('http://localhost:60691/UserService.svc/Login')
						.respond(fakeResponse);
		}));

		it('should start with no userName and show the login fields', inject(function ($componentController)
		{
			expect(ctrl.userName).toBe('');
			expect(ctrl.showLoginFields).toBe(true);
		}));

		it('should has userName and hide the login fields after successful login', inject(function ($componentController) 
		{
			//TODO esse teste tem problemas com os cookies - se usuario estiver logado, deveria ter username e ocultar os campos
			var credentials = { UserName: 'sa', NewPassword: '******' };
			ctrl.doLogin(credentials);
			$httpBackend.flush();
			expect(ctrl.userName).toBe('sa');
			expect(ctrl.showLoginFields).toBe(false);
		}));

		it('should has no userName and show the login fields after logoff', inject(function ($componentController)
		{
			var credentials = { UserName: 'sa', NewPassword: '******' };
			ctrl.doLogin(credentials);

			$httpBackend.flush();

			ctrl.doLogOff(credentials);

			expect(ctrl.userName).toBe('');
			expect(ctrl.showLoginFields).toBe(true);
		}));

		it('should has no userName and show the login fields after unsuccessful login', inject(function ($componentController)
		{
			//TODO nao sei como alterar a resposta do $httpbackend
			//var credentials = { UserName: 'sa', NewPassword: '******' };
			//ctrl.doLogin(credentials);
			//$httpBackend.flush();
			//expect(ctrl.userName).toBe('');
			//expect(ctrl.showLoginFields).toBe(true);
		}));
	});
});