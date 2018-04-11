using System.Collections.Generic;

namespace Api.Models
{
    public class PageViewModel
    {
        public BotViewModel CurrentBot;
        public IEnumerable<BotViewModel> Bots;
        public InlineKey CurrentInlineKey;
        public IEnumerable<TextMessageAnswer> TextMessages { get; set; }
        public IEnumerable<InlineKey> InlineKeys { get; set; }
        public IEnumerable<InlineUrlKey> InlineUrlKeys { get; set; }
        public IEnumerable<Interview> Interviews {get;set;}
    }
}