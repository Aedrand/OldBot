using System;
using Discord;
using Discord.Commands;
using System.Text.RegularExpressions;

namespace OldBot
{
    class OldBotClass
    {
        DiscordClient client;
        CommandService commands;

        public OldBotClass()
        {
            client = new DiscordClient(input =>
            {
                input.LogLevel = LogSeverity.Info;
                input.LogHandler = Log;
            });

            client.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });

            commands = client.GetService<CommandService>();

            //TODO: Add functionality to specify which user to watch. For now, only Parker.
            //commands.CreateCommand("watch")

            client.MessageReceived += async(s, e) =>
            {
                Regex regex = new Regex(@"^(https|www|http)");
                Match match = regex.Match(e.Message.Text);
                if (e.Message.User.Name == "Aedrand" && match.Success)
                {
                    await e.Channel.SendMessage(e.User.Mention + " Old.");
                }
            };

            client.ExecuteAndWait(async () =>
            {
                await client.Connect("MjY0NTc0NzI2MzM5NDI4MzYz.C0ij-Q.rsieVdG9wnTT0hMsWD3bD8WKyKU" , TokenType.Bot);
            });
        }

        private void Log(object sender , LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
