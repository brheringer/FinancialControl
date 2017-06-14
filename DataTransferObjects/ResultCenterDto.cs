using System;

namespace FinancialControl.DataTransferObjects
{
	public class ResultCenterDto
		: EntityDto
	{
		public string Code { get; set; }
		public string Description { get; set; }
	}
}