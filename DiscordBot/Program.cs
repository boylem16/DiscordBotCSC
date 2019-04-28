﻿using System;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBotCSC260
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;


        static void Main(string[] args) => new Program().StartAsync().GetAwaiter().GetResult();

        

        public async Task StartAsync(){
            if(Config.bot.token == "" || Config.bot.token == "null" ){
                return;
            }
            
            _client = new DiscordSocketClient(new DiscordSocketConfig {
                LogLevel = LogSeverity.Debug
            });

            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            _handler = new CommandHandler();
            await _handler.initializeAsync(_client);
            await Task.Delay(-1);


        }

        private async Task Log(LogMessage msg){
            Console.WriteLine(msg.Message);
        }

    }
}