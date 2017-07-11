describe('import tests suit', function ()
{
	beforeEach(module('importing'));

	describe('importing controller when services work', function ()
	{
		var $httpBackend, $componentController;

		beforeEach(inject(function (_$componentController_, _$httpBackend_, AppConfig)
		{
			AppConfig.serverBaseUrl = 'http://test/api/';

			var responseDto = {
				Success: true,
				TimeStamp: new Date(2017, 7, 3, 23, 59, 59),
				QuantityOfImportedEntries: 127,
				ErrorsMessages: null,
				Response: { HasException: false, Exception: '' }
			};

			$httpBackend = _$httpBackend_;
			$httpBackend.whenPOST('http://test/api/importing/').respond(responseDto);
			$componentController = _$componentController_;
		}));

		it("should import stuff successfully and show result", function ()
		{
			var ctrl = $componentController('importing');
			ctrl.import();
			$httpBackend.flush();

			expect(ctrl.model).not.toBe(null);
			expect(ctrl.model.FilePath).toBe('');
			expect(ctrl.model.Success).toBe(true);
			expect(ctrl.model.Details).not.toBe(null);
			//TODO
		});
	});

	describe('importing controller when services answer with errors messages', function ()
	{
		var $httpBackend, $componentController;

		beforeEach(inject(function (_$componentController_, _$httpBackend_, AppConfig)
		{
			AppConfig.serverBaseUrl = 'http://test/api/';

			var responseDto = {
				Success: false,
				TimeStamp: new Date(2017, 7, 3, 23, 59, 59),
				QuantityOfImportedEntries: 0,
				ErrorsMessages: ['error 1', 'error 2'],
				Response: { HasException: false, Exception: '' }
			};

			$httpBackend = _$httpBackend_;
			$httpBackend.whenPOST('http://test/api/importing/').respond(responseDto);
			$componentController = _$componentController_;
		}));

		it('should show error on importing', function ()
		{
			var ctrl = $componentController('importing');
			ctrl.import();
			$httpBackend.flush();

			expect(ctrl.model).not.toBe(null);
			expect(ctrl.model.FilePath).toBe('');
			expect(ctrl.model.Success).toBe(false);
			expect(ctrl.model.Details).not.toBe(null);
			//TODO
		});

	});

});