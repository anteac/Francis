using Francis.Database.Entities;
using Francis.Models;
using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class NextMovieAnswer : NextSearchAnswer
    {
        public override bool CanProcess => Context.Command == $"/next_{RequestType.Movie}";


        public NextMovieAnswer(CallbackAnswerContext<RequestProgression> context) : base(context)
        { }


        protected override async Task<RequestItem[]> GetItems()
        {
            var items = await Context.Ombi.SearchMovie(Context.Progression.Search);
            return items.Select(x => (RequestItem)x).ToArray();
        }
    }
}
