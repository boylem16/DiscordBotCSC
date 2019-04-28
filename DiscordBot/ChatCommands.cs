﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordBot
{
    class ChatCommands : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task Echo() {
            await Context.Channel.SendMessageAsync("Hello World");
        }
    }
}