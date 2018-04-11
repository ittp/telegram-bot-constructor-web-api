using System.Collections.Generic;

namespace Api.Models
{
	public class PageViewModel
	{
		public BotViewModel CurrentBot;
		public IEnumerable<BotViewModel> Bots;
	}
}