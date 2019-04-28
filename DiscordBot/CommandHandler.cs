using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Reflection;

namespace DiscordBot
{
    class CommandHandler
    {
        DiscordSocketClient _client;
        CommandService _service;
        IServiceProvider _services;




        public async Task initializeAsync(DiscordSocketClient client){
            _client = client;
            _service = new CommandService();

            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s){

            var msg = s as SocketUserMessage;

            if (msg == null){
                System.Console.WriteLine("null");
                return;
            }

            var context = new SocketCommandContext(_client, msg);
            int argPos = 0;

            string text = Config.bot.cmdPrefix;

            if(msg.HasStringPrefix(Config.bot.cmdPrefix, ref argPos) || msg.HasMentionPrefix(_client.CurrentUser, ref argPos)){

                var result = await _service.ExecuteAsync(context, argPos, _services);
                if(!result.IsSuccess){
                    
                    Console.WriteLine(result.ErrorReason + " : " + msg.Content);
                }

            }


        }



    }


}
