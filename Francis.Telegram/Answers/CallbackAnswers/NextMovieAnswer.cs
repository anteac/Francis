using Francis.Database.Entities;
using Francis.Models;
using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class NextMovieAnswer : NextSearchAnswer
    {
        internal override bool CanProcess => Context.Command == $"/next_{RequestType.Movie}";


        public NextMovieAnswer(CallbackAnswerContext context) : base(context)
        { }


        protected override async Task<RequestItem[]> GetItems()
        {
            var progression = Context.Progression as RequestProgression ?? throw new InvalidOperationException("Unknown progress status");

            var items = await Context.Ombi.SearchMovie(progression.Search);
            return items.Select(x => (RequestItem)x).ToArray();
        }
    }
}
