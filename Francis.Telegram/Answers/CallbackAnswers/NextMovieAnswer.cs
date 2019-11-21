using Francis.Database.Entities;
using Francis.Models;
using Francis.Models.Notification;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public class NextMovieAnswer : NextSearchAnswer
    {
        internal override bool CanProcess => Command == $"/next_{RequestType.Movie}";


        public NextMovieAnswer(IServiceProvider provider) : base(provider)
        { }


        protected override async Task<RequestItem[]> GetItems()
        {
            var progression = Progression as RequestProgression ?? throw new InvalidOperationException("Uknown progress status");

            var items = await Ombi.SearchMovie(progression.Search);
            return items.Select(x => (RequestItem)x).ToArray();
        }
    }
}
