using System;
using System.IO;
using System.Reflection.Metadata;
using Telegram.Bot;
using YesOrNoAPI;
using System.Threading;

namespace TelegramBot
{
    class TelegramBot
    {
        static void Main(string[] args)
        {
            
            TelegramBotClient bot = new TelegramBotClient("1863056024:AAHHFPGNbFpcjL5An6C3VB0Ms4nU37O7vew");

            bool askQuestion = false;

            bot.OnMessage += (s, arg) =>
            {
                if (!askQuestion)
                {
                    switch (arg.Message.Text)
                    {
                        case "/start":
                            Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                            bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Чуитс, бот был создан хакером под именем lemonjuice");
                            Thread.Sleep(200);
                            bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Этот бот ответит на твои вопросы(Да/Нет)");
                            Thread.Sleep(200);
                            bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Просто напиши слово <Вопрос>");
                            Thread.Sleep(200);
                            bot.SendDocumentAsync(arg.Message.Chat.Id, "https://media.giphy.com/media/SyemapFxj7TiM/giphy.gif");
                            break;
                        case "вопрос":
                            askQuestion = true;
                            Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                            bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Задай свой вопрос и облака ответят на него!!! ");
                            break;
                        case "Вопрос":
                            askQuestion = true;
                            Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                            bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Задай свой вопрос и облака ответят на него!!! ");
                            break;
                        default:
                            Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                            bot.SendTextMessageAsync(arg.Message.Chat.Id, $"You say: {arg.Message.Text} ");
                            break;
                    }
                }
                else
                {
                    askQuestion = false;
                    string answer = AnswerApi.Answer();
                    string[] parseAnswer = answer.Split(' ');
                    Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                    bot.SendTextMessageAsync(arg.Message.Chat.Id, $"облака говорят: {parseAnswer[0]} ");
                    bot.SendDocumentAsync(arg.Message.Chat.Id, parseAnswer[1]);
                    Thread.Sleep(400);
                    bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Для повторного вопроса заново введите команду <Вопрос>");
                }
            };

            bot.StartReceiving();

            Console.ReadKey();
        }
    }
}