angular
.module('mainMenu')
.component('mainMenu',
{
	templateUrl: 'main-menu/main-menu.template.html',
	controller: [function MainMenuController()
	{
		function createMenu(baseName, presentation)
		{
			return {
				'icon': 'res/icon/android/' + baseName + '_24px.svg',
				'link': '#!/' + baseName,
				'presentation': presentation
			}
		}

		this.normalizedMenus = [
			createMenu('accounts', 'Accounts'),
			createMenu('resultsCenters', 'Results Centers'),
			createMenu('entries', 'Entries'),
			createMenu('entriesTemplates', 'Entries Templates'),
			createMenu('memosMappings', 'Memos Mappings'),
			createMenu('importing', 'Import Entries'),
			createMenu('exporting', 'Export Entries'),
			createMenu('accountsTotalizationsReport', 'Accounts Totalizations Report')
		];

		var originatorEv;

		this.openMenu = function ($mdOpenMenu, ev) {
			originatorEv = ev;
			$mdOpenMenu(ev);
		};

	}]
});
