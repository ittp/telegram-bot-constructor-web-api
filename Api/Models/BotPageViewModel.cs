using System.Collections.Generic;

namespace Api.Models
{
	public class BotPageViewModel
	{
		public BotViewModel CurrentBot;
		public IEnumerable<BotViewModel> Bots;
	}
}