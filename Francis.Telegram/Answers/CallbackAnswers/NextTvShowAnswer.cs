using Francis.Database.Entities;
using Francis.Models;
using Francis.Models.Notification;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class NextTvShowAnswer : NextSearchAnswer
    {
        internal override bool CanProcess => Command == $"/next_{RequestType.TvShow}";


        public NextTvShowAnswer(IServiceProvider provider) : base(provider)
        { }


        protected override async Task<RequestItem[]> GetItems()
        {
            var progression = Progression as RequestProgression ?? throw new InvalidOperationException("Unknown progress status");

            var items = await Ombi.SearchTv(progression.Search);
            return items.Select(x => (RequestItem)x).ToArray();
        }
    }
}
