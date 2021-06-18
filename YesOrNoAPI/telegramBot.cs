using System;
using System.IO;
using System.Reflection.Metadata;
using Telegram.Bot;
using YesOrNoAPI;

namespace TelegramBot
{
    class TelegramBot
    {
        static void Main(string[] args)
        {
            
            TelegramBotClient bot = new TelegramBotClient("1863056024:AAHHFPGNbFpcjL5An6C3VB0Ms4nU37O7vew");

            bot.OnMessage += (s, arg) =>
            {
                Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                bot.SendTextMessageAsync(arg.Message.Chat.Id, $"You say: {arg.Message.Text} ");
                string answer = AnswerApi.Answer();
                bot.SendTextMessageAsync(arg.Message.Chat.Id, $"i say: {answer} ");
                bot.SendDocumentAsync(arg.Message.Chat.Id, "https://yesno.wtf/assets/no/0-b6d3e555af2c09094def76cf2fbddf46.gif");
            };

            bot.StartReceiving();

            Console.ReadKey();
        }
    }
}