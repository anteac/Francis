using Francis.Services.Clients;
using Francis.Telegram.Contexts;
using Francis.Telegram.Extensions;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.MessageAnswers
{
    public class StartAnswer : TelegramAnswer
    {
        private readonly IPlexService _plexService;

        public override bool Public => true;

        public override bool CanProcess => Context.Command == "/start" && Context.Parameters.Length > 0;


        public StartAnswer(MessageAnswerContext context, IPlexService plexService) : base(context)
        {
            _plexService = plexService;
        }


        public override async Task Execute()
        {
            var pinId = Context.Parameters[0].Split(new[] { '-' })[0];
            var clientId = Context.Parameters[0].Split(new[] { '-' })[1];

            var pin = await _plexService.GetPin(pinId, clientId);
            var plexUser = await _plexService.GetMe(pin.AuthToken);
            var ombiUsers = await Context.Ombi.GetUsers();
            var ombiUser = ombiUsers.First(x => x.UserName == plexUser.User.Username || x.EmailAddress == plexUser.User.Email);

            Context.User.PlexToken = pin.AuthToken;
            Context.User.UserName = ombiUser.UserName;
            Context.User.OmbiId = ombiUser.Id;

            if (Context.Message.Chat.Id != Context.Options.Value.AdminChat)
            {
                await Context.Bot.SendMessage(Context.Options.Value.AdminChat, $"{Context.User.UserName} has just successfully authenticated.");
            }
            await Context.Bot.SendMessage(Context.Message.Chat, $"Hello {Context.User.UserName}! 😃\nNow that I know you, type /help to start!");

            Context.Logger.LogInformation($"Telegram user '{Context.Message.From.Username}' ({Context.Message.From.FirstName} {Context.Message.From.LastName}) successfully authenticated as '{Context.User.UserName}'");
        }
    }
}
