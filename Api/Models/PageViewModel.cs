using System.Collections.Generic;
using Api.Controllers;

namespace Api.Models
{
    public class PageViewModel
    {
        public BotViewModel CurrentBot;
        public IEnumerable<BotViewModel> Bots;
        public IEnumerable<TextMessageAnswer> TextMessages { get; set; }
        public IEnumerable<InlineKey> InlineKeys { get; set; }
        public IEnumerable<InlineUrlKey> InlineUrlKeys { get; set; }
        public IEnumerable<Interview> Interviews {get;set;}
        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<InterviewAnswerViewModel> InterviewAnswers { get; set; }
    }
}