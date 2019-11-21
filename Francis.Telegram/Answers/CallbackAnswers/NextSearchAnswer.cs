using Francis.Database.Entities;
using Francis.Models;
using Francis.Toolbox.Extensions;
using Francis.Toolbox.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public abstract class NextSearchAnswer : CallbackAnswer
    {
        protected override bool HasProgression => true;


        public NextSearchAnswer(IServiceProvider provider) : base(provider)
        { }


        public override async Task Execute()
        {
            var progression = Progression as RequestProgression ?? throw new InvalidOperationException("Uknown progress status");

            var movies = await Ombi.SearchMovie(progression.Search);
            var tvs = await Ombi.SearchTv(progression.Search);

            var item = (await GetItems()).First(x => !progression.ExcludedIds.Contains(x.Id));

            progression.ExcludedIds.Add(item.Id);
            Context.Entry(progression).State = EntityState.Modified;

            //TODO: Cleanup this mess
            var defaultPoster = Assembly.GetExecutingAssembly().GetResource("Assets.default_poster.png");
            var image = await WebResource.Exists(item.Image) ? new InputMedia(item.Image) : new InputMedia(defaultPoster, "default_poster");
            var caption = item.AsString("Is this what you are looking for?");
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                    InlineKeyboardButton.WithCallbackData("Exactly!", $"/chose_{item.Type} {item.Id}"),
                    InlineKeyboardButton.WithCallbackData("Next...", $"/next_{item.Type} {progression.Id}"),
            });

            if (progression.ExcludedIds.Count() == 1)
            {
                await Bot.Client.DeleteMessageAsync(Data.Message.Chat, Data.Message.MessageId);
                await Bot.Client.SendPhotoAsync(Data.Message.Chat, image, caption, replyMarkup: keyboard);
            }
            else
            {
                await Bot.Client.EditMessageMediaAsync(
                    chatId: Data.Message.Chat,
                    messageId: Data.Message.MessageId,
                    media: new InputMediaPhoto(image) { Caption = item.AsString("Is this what you are looking for?") },
                    replyMarkup: keyboard
                );
            }

            Logger.LogInformation($"User '{User.UserName}' continued searching with '{progression.Search}'. Result found: {item.Title} ({item.Type} - {item.Year})");
        }


        protected abstract Task<RequestItem[]> GetItems();
    }
}
