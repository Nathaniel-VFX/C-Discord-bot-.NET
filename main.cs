using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Threading.Tasks;


class Program {
  public static void Main(string[] args)
        => new Program()
        .MainAsync()
        .GetAwaiter()
        .GetResult();

        private DiscordSocketClient client;
        public async Task MainAsync()
        {
            var client = new DiscordSocketClient();
            client.MessageReceived += CommandHandler;
            client.Log += Log;

            var token = Environment.GetEnvironmentVariable("token");

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            await client.SetStatusAsync(UserStatus.Idle);
            await client.SetActivityAsync(new Game("Codes in C#", ActivityType.Watching));

            await Task.Delay(-1); // Block this task until the program is closed.
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task CommandHandler(SocketMessage message)
        {
            //variables
            string command = "";
            int lengthOfCommand = -1;

            //filtering messages begin here
            var prefix = '!'; // set the prefix
            if (!message.Content.StartsWith(prefix)) //message starts with prefix
                return Task.CompletedTask;

            if (message.Author.IsBot) // ignores all commands from bots
                return Task.CompletedTask;

            if (message.Content.Contains(' ')) 
                lengthOfCommand = message.Content.IndexOf(' ');
            else
                lengthOfCommand = message.Content.Length;

            command = message.Content.Substring(1, lengthOfCommand - 1).ToLower();

            //Commands begin here
            if (command.Equals("hello"))
            {
                message.Channel.SendMessageAsync($@"Hello {message.Author.Mention}");
            }
            else if (command.Equals("age"))
            {
                message.Channel.SendMessageAsync($@"Your account was created at {message.Author.CreatedAt.DateTime.Date}");
            }
            else if (command.Equals("info"))
            {
                message.Channel.SendMessageAsync($@"{message.Author.Mention}, This bot is code in `C#` by `Nathaniel VFX#6321`");
            }

            return Task.CompletedTask;
        }
}