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
            
            TelegramBotClient bot = new TelegramBotClient(File.ReadAllText($"../../../token.txt")); /*token hide on my pc*/

            bool askQuestion = false;

            bot.OnMessage += (s, arg) =>
            {
                if (!askQuestion)
                {
                    switch (arg.Message.Text)
                    {
                        case "/start":
                            addNewUser(Convert.ToString(arg.Message.Chat.Id));
                            Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                            bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Чуитс, бот был создан хакером под именем lemonjuice");
                            Thread.Sleep(200);
                            bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Этот бот ответит на твои вопросы(Да/Нет)");
                            Thread.Sleep(200);
                            bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Просто напиши слово <Вопрос>");
                            Thread.Sleep(200);
                            bot.SendDocumentAsync(arg.Message.Chat.Id, "https://media.giphy.com/media/SajdfSNg6f8rK/giphy.gif");
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
                    Console.WriteLine(parseAnswer[0]);
                    Thread.Sleep(400);
                    bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Для повторного вопроса заново введите команду <Вопрос>");
                }
            };

            bot.StartReceiving();

            Console.ReadKey();
        }

        public static void addNewUser(string chatId)
		{
            bool alreadyInBot = false;
            string[] Ips = File.ReadAllLines($"../../../usersIP.txt");
            StreamWriter sw = new StreamWriter($"../../../usersIP.txt");
            foreach(var f in Ips){
                sw.WriteLine(f);
                if (f == chatId)
				{
                    alreadyInBot = true;
				}
			}
			if (!alreadyInBot)
			{
                Console.WriteLine($"add new IP in file {chatId}");
                sw.WriteLine(chatId);
			}
			else
			{
                Console.WriteLine("user already in BOT");
			}
            sw.Close();
        }
    }
}