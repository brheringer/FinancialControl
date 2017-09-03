using System;
using System.Collections.Generic;

namespace FinancialControl.DataTransferObjects
{
	public class ResultsCentersDto 
		: CollectionDto
	{
		public string FilterCode { get; set; }
		public string FilterDescription { get; set; }
		public IList<ResultCenterDto> ResultsCenters { get; set; }
	}
}