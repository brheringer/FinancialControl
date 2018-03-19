angular
.module('common')
.filter('accountLevelFilter', function()
{
	return function (items, itemPropertyName, levelFilter)
	{
		if (!levelFilter || levelFilter == 0 || levelFilter == '' || !itemPropertyName || itemPropertyName == '')
			return items;

		var filtered = []
		for (var i = 0; i < items.length; i++)
		{
			if (items[i][itemPropertyName] <= levelFilter)
			{
				filtered.push(items[i]);
			}
		}
		return filtered;
	};
});