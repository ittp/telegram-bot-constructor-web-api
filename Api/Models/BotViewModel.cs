using System.Collections.Generic;

namespace Api.Models
{
	public class BotViewModel
	{
		public Bot Bot { get; set; }
		public bool Status { get; set; }
		public IEnumerable<TextMessageAnswer> TextMessages { get; set; }
		public IEnumerable<InlineKey> InlineKeys { get; set; }
		public IEnumerable<InlineUrlKey> InlineUrlKeys { get; set; }
	}
}