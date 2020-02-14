using Francis.Database.Entities;
using Francis.Models;
using Francis.Models.Notification;
using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Francis.Telegram.Answers.CallbackAnswers
{
    public abstract class NextSearchAnswer : TelegramAnswer
    {
        public NextSearchAnswer(CallbackAnswerContext context) : base(context)
        { }


        public override async Task Execute()
        {
            var progression = Context.Progression as RequestProgression ?? throw new InvalidOperationException("Unknown progress status");

            var item = (await GetItems()).FirstOrDefault(x => !progression.ExcludedIds.Contains(x.Id));
            if (item == null)
            {
                progression.Status = RequestStatus.Error;
                await Context.Bot.EditMessage(Context.Message, "I'm sorry, I could not find anything that matches your search... Are you sure you typed the name correctly?");
                Context.Logger.LogError($"User '{Context.User.UserName}' could not find any suitable media that matches '{progression.Search}'.");
                return;
            }

            progression.ExcludedIds.Add(item.Id);

            await Context.Bot.EditImage(Context.Message, item.Image, "Is this what you are looking for?", item, new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Exactly!", $"/chose_{item.Type} {progression.Id} {item.Id}"),
                    InlineKeyboardButton.WithCallbackData("Next...", $"/next_{item.Type} {progression.Id}"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Cancel request", $"/cancel {progression.Id}"),
                }
            }));

            Context.Logger.LogInformation($"User '{Context.User.UserName}' continued searching with '{progression.Search}'. Result found: {item.Title} ({item.Type} - {item.Year})");
        }


        protected abstract Task<RequestItem[]> GetItems();
    }
}
