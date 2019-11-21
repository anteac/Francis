using System;
using Telegram.Bot.Types;

namespace Francis.Telegram.Answers
{
    public abstract class MessageAnswer : Answer<Message>
    {
        protected override long UserId => Data.Chat.Id;

        protected override string FullCommand => Data.Text;


        public MessageAnswer(IServiceProvider provider) : base(provider)
        { }
    }
}
