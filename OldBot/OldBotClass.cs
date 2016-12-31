using System;
using Discord;
using Discord.Commands;
using System.Text.RegularExpressions;
using System.Diagnostics;

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
                Regex regex = new Regex(@"(.*)(https|www|http)");
                Match match = regex.Match(e.Message.RawText);
                if (e.Message.User.Name == "Aedrand" && match.Success)
                {
                    await e.Channel.SendMessage(e.User.NicknameMention + " Old.");
                }
                if(e.Message.User.Name == "Aedrand" && e.Message.Attachments.Length > 0)
                {
                    await e.Channel.SendMessage(e.User.NicknameMention + " Old.");
                }
                Debug.WriteLine(e.Message.RawText);
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
