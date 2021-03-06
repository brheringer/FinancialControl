﻿angular
.module('financialControlApp')
.config(['$locationProvider', '$routeProvider', '$mdIconProvider', '$sceDelegateProvider',
	function config($locationProvider, $routeProvider, $mdIconProvider, $sceDelegateProvider)
	{
		$locationProvider.hashPrefix('!');

		$routeProvider
		.when('/portal', { template: '<portal></portal>' })
		.when('/accounts', { template: '<accounts-list></accounts-list>' })
		.when('/account', { template: '<account-detail></account-detail>' })
		.when('/account/:id', { template: '<account-detail></account-detail>' })
		.when('/resultsCenters', { template: '<results-centers-list></results-centers-list>' })
		.when('/resultCenter', { template: '<result-center-detail></result-center-detail>' })
		.when('/resultCenter/:id', { template: '<result-center-detail></result-center-detail>' })
		.when('/entries', { template: '<entries-list></entries-list>' })
		.when('/entriesBoard', { template: '<entries-board></entries-board>' })
		.when('/entriesBoard/:id', { template: '<entries-board></entries-board>' })
		.when('/entriesTemplates', { template: '<entries-templates-list></entries-templates-list>' })
		.when('/entryTemplate', { template: '<entry-template-detail></entry-template-detail>' })
		.when('/entryTemplate/:id', { template: '<entry-template-detail></entry-template-detail>' })
		.when('/memosMappings', { template: '<memos-mappings-list></memos-mappings-list>' })
		.when('/memoMapping', { template: '<memo-mapping-detail></memo-mapping-detail>' })
		.when('/memoMapping/:id', { template: '<memo-mapping-detail></memo-mapping-detail>' })
		.when('/importing', { template: '<importing></importing>' })
		.when('/accountsTotalizationsReport', { template: '<accounts-totalizations-report></accounts-totalizations-report>' })
		.otherwise('/portal');

		//TODO review
		$sceDelegateProvider
		.resourceUrlWhitelist([
			'self',
			'**'
		]);

		$mdIconProvider
		.icon('iconAccount', 'res/icon/android/accounts_24px.svg', 24)
		.icon('iconCancel', 'res/icon/android/cancel_24px.svg', 24)
		.icon('iconClose', 'res/icon/android/close_24px.svg', 24)
		.icon('iconDate', 'res/icon/android/date_24px.svg', 24)
		.icon('iconDateTimePicker', 'res/icon/android/datetimepicker_24px.svg', 24)
		.icon('iconDelete', 'res/icon/android/delete_24px.svg', 24)
		.icon('iconDuplicate', 'res/icon/android/duplicate_24px.svg', 24)
		.icon('iconEdit', 'res/icon/android/edit_24px.svg', 24)
		.icon('iconEntry', 'res/icon/android/entry_24px.svg', 24)
		.icon('iconEntryTemplate', 'res/icon/android/entry_24px.svg', 24) //TODO mudar imagem
		.icon('iconExpand', 'res/icon/android/search_24px.svg', 24)
		.icon('iconHide', 'res/icon/android/hide_24px.svg', 24)
		.icon('iconMemo', 'res/icon/android/memo_24px.svg', 24)
		.icon('iconMemoMapping', 'res/icon/android/memo_24px.svg', 24) //TODO mudar imagem
		.icon('iconMenu', 'res/icon/android/menu_24px.svg', 24)
		.icon('iconMoney', 'res/icon/android/money_24px.svg', 24)
		.icon('iconNew', 'res/icon/android/new_24px.svg', 24)
		.icon('iconPagePrevious', 'res/icon/android/pagePrevious_24px.svg', 24)
		.icon('iconPageNext', 'res/icon/android/pageNext_24px.svg', 24)
		.icon('iconResultCenter', 'res/icon/android/resultsCenters_24px.svg', 24)
		.icon('iconReport', 'res/icon/android/memo_24px.svg', 24) //TODO mudar imagem
		.icon('iconSave', 'res/icon/android/save_24px.svg', 24)
		.icon('iconSearch', 'res/icon/android/search_24px.svg', 24)
		.icon('iconShow', 'res/icon/android/show_24px.svg', 24)
		.icon('iconTimePicker', 'res/icon/android/timepicker_24px.svg', 24);
	}
]);