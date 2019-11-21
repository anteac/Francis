using Francis.Services.Clients;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Francis.Telegram.Answers.MessageAnswers
{
    public class StartAnswer : MessageAnswer
    {
        private readonly IPlexService _plexService;

        internal override bool Public => true;

        internal override bool CanProcess => Command == "/start" && Parameters.Length > 0;


        public StartAnswer(IServiceProvider provider, IPlexService plexService) : base(provider)
        {
            _plexService = plexService;
        }


        public override async Task Execute()
        {
            var pinId = Parameters[0].Split(new[] { '-' })[0];
            var clientId = Parameters[0].Split(new[] { '-' })[1];

            var pin = await _plexService.GetPin(pinId, clientId);
            var plexUser = await _plexService.GetMe(pin.AuthToken);
            var ombiUsers = await Ombi.GetUsers();
            var ombiUser = ombiUsers.First(x => x.UserName == plexUser.User.Username || x.EmailAddress == plexUser.User.Email);

            User.PlexToken = pin.AuthToken;
            User.UserName = ombiUser.UserName;
            User.OmbiId = ombiUser.Id;

            await Bot.Client.SendTextMessageAsync(chatId: Data.Chat, text: "Successfully authenticated!");

            Logger.LogInformation($"Telegram user '{Data.From.Username}' ({Data.From.FirstName} {Data.From.LastName}) successfully authenticated as '{User.UserName}'");
        }
    }
}
