describe('result-center-detail tests suit', function ()
{
	beforeEach(module('resultCenterDetail'));

	describe('result-center-detail controller when services work', function ()
	{
		var $httpBackend, $componentController, $routeParams;

		beforeEach(inject(function (_$componentController_, _$httpBackend_, _$routeParams_, AppConfig)
		{
			var center = {
				AutoId: 1,
				Code: '1',
				Description: 'Car',
				User: 'user',
				Presentation: 'Car',
				Version: 0,
				Response: { HasException: false, Exception: '' }
			}

			var centerAfterUpdate = {
				AutoId: 1,
				Code: '1',
				Description: 'Car',
				User: 'user',
				Presentation: 'Car',
				Version: 1,
				Response: { HasException: false, Exception: '' }
			}

			AppConfig.serverBaseUrl = 'http://test/api/';

			$httpBackend = _$httpBackend_;
			$httpBackend.whenGET('http://test/api/resultCenter/1').respond(center);
			$httpBackend.whenPOST('http://test/api/resultCenter/update').respond(centerAfterUpdate);
			$httpBackend.whenDELETE('http://test/api/resultCenter/1').respond(center);
			$routeParams = _$routeParams_;
			$componentController = _$componentController_;
		}));

		it("should load a center by id in the route params", function ()
		{
			$routeParams.id = 1;
			var ctrl = $componentController('resultCenterDetail');
			$httpBackend.flush();

			expect(ctrl.status).toBe('load ok');
			expect(ctrl.model).not.toBe(null);
			expect(ctrl.model.AutoId).toBe(1);
			expect(ctrl.model.Code).toBe('1');
			expect(ctrl.model.Description).toBe('Car');
			expect(ctrl.model.Version).toBe(0);
			expect(ctrl.isPersistent()).toBe(true);
		});

		it('should start empty, if an id is not given', function ()
		{
			$routeParams.id = 0;
			var ctrl = $componentController('resultCenterDetail');
			//$httpBackend.flush();

			expect(ctrl.status).toBe('nothing...');
			expectToResetModel(ctrl.model);
			expect(ctrl.isPersistent()).toBe(false);
		});

		it('should update an entity successfully, if everything is all right', function ()
		{
			$routeParams.id = 1;
			var ctrl = $componentController('resultCenterDetail');
			$httpBackend.flush();
			ctrl.save();
			$httpBackend.flush();
			expect(ctrl.model.Version).toBe(1);
			expect(ctrl.status).toContain('save ok');
			expect(ctrl.isPersistent()).toBe(true);
		});

		it('should reset model on new', function ()
		{
			$routeParams.id = 1;
			var ctrl = $componentController('resultCenterDetail');
			$httpBackend.flush();
			ctrl.new();

			expect(ctrl.status).toBe('new ok');
			expectToResetModel(ctrl.model);
			expect(ctrl.isPersistent()).toBe(false);
		});

		it('should save a new entity successfully, if everything is all right', function ()
		{
			$routeParams.id = 0;
			var ctrl = $componentController('resultCenterDetail');
			//ctrl.model.Codigo = '1'
			ctrl.save();
			$httpBackend.flush();
			expect(ctrl.model.Version).toBe(1);
			expect(ctrl.status).toContain('save ok');
			expect(ctrl.isPersistent()).toBe(true);
		});

		it('should complain about missing required fields on save', function ()
		{
		});

		it('should reset model on delete', function ()
		{
			$routeParams.id = 1;
			var ctrl = $componentController('resultCenterDetail');
			$httpBackend.flush();
			ctrl.delete();
			$httpBackend.flush();

			expect(ctrl.status).toBe('delete ok');
			expectToResetModel(ctrl.model);
			expect(ctrl.isPersistent()).toBe(false);
		});
	});

	describe('result-center-detail controller when services answer with errors messages', function ()
	{
		var $httpBackend, $componentController, $routeParams;

		beforeEach(inject(function (_$componentController_, _$httpBackend_, _$routeParams_, AppConfig)
		{
			var center = {
				AutoId: 0,
				Code: '',
				Description: '',
				User: '',
				Presentation: '',
				Version: '0',
				Response: { HasException: true, Exception: 'error... whatever...' }
			}

			AppConfig.serverBaseUrl = 'http://test/api/';

			$httpBackend = _$httpBackend_;
			$httpBackend.whenGET('http://test/api/resultCenter/2').respond(center);
			$httpBackend.whenPOST('http://test/api/resultCenter/update').respond(center);
			$httpBackend.whenDELETE('http://test/api/resultCenter').respond(center);
			$routeParams = _$routeParams_;
			$componentController = _$componentController_;
		}));

		it('should show error on load', function ()
		{
			$routeParams.id = 2;
			var ctrl = $componentController('resultCenterDetail');
			$httpBackend.flush();

			expect(ctrl.status).toBe('error... whatever...');
			expectToResetModel(ctrl.model);
			expect(ctrl.isPersistent()).toBe(false);
		});

		it('should show error on save and reset model', function ()
		{
			$routeParams.id = 2;
			var ctrl = $componentController('resultCenterDetail');
			$httpBackend.flush();
			ctrl.save();
			$httpBackend.flush();

			expect(ctrl.status).toBe('error... whatever...');
			expectToResetModel(ctrl.model);
			expect(ctrl.isPersistent()).toBe(false);
		});

		it('should show error on delete and reset model', function ()
		{
			$routeParams.id = 0;
			var ctrl = $componentController('resultCenterDetail');
			ctrl.delete();
			$httpBackend.flush();

			expect(ctrl.status).toBe('error... whatever...');
			expectToResetModel(ctrl.model);
			expect(ctrl.isPersistent()).toBe(false);
		});
	});

	function expectToResetModel(model)
	{
		expect(model).not.toBe(null);
		expect(model.AutoId).toBe(0);
		expect(model.Code).toBe('');
		expect(model.Description).toBe('');
	}

});