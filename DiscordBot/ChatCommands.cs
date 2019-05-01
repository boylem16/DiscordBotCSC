using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using System.IO;
using System.Net.NetworkInformation;


namespace DiscordBot
{

    public class ChatCommands : ModuleBase<SocketCommandContext>
    {

        List<Movie> movies = new List<Movie>();
        List<Song> songs;

        [Command("alive")]
        public async Task Echo() {
            await Context.Channel.SendMessageAsync("I am alive");
        }

        [Command("add movie")]
        public async Task addMovie([Remainder] string message)
        {
            Movie movie = new Movie();
            movie.userName = Context.User.Username;
            movie.movieName = message;
            string dir = @"C:\temp";
            string serializationFile = Path.Combine(dir, "movies.bin");
            try
            {
                using (Stream stream = File.Open(serializationFile, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    movies = (List<Movie>)bformatter.Deserialize(stream);
                }
            }
            catch(Exception e)
            {

            }

            movies.Add(movie);
            await Context.Channel.SendMessageAsync(movie.movieName + " was added");

            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, movies);
            }
        }

        [Command("remove movie")]
        public async Task removeMovie([Remainder] string message)
        {

            string dir = @"C:\temp";
            string serializationFile = Path.Combine(dir, "movies.bin");
            using (Stream stream = File.Open(serializationFile, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                movies = (List<Movie>)bformatter.Deserialize(stream);
            }
            var match = movies.FirstOrDefault(s => s.movieName == message);
            movies.Remove(match);
            if (match != null)
            {
                await Context.Channel.SendMessageAsync(match.movieName + " was removed");

            }
            else
            {
                await Context.Channel.SendMessageAsync(message + " was not found");
            }
            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, movies);
            }

        }

        [Command("list movie")]
        public async Task listMovie()
        {

            string dir = @"C:\temp";
            string serializationFile = Path.Combine(dir, "movies.bin");
            using (Stream stream = File.Open(serializationFile, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                movies = (List<Movie>)bformatter.Deserialize(stream);
            }


            await Context.Channel.SendMessageAsync("Movies: ");
            foreach (var m in movies){
                await Context.Channel.SendMessageAsync(m.movieName);
            }
        }

        [Command("ping")]
        public async Task pingComputer([Remainder] string message) {
            Ping ping = new Ping();
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingOptions options = new PingOptions();
            options.DontFragment = true;

            PingReply reply = ping.Send(message, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                await Context.Channel.SendMessageAsync("Address: " + reply.Address.ToString());
                await Context.Channel.SendMessageAsync("RoundTrip time: " + reply.RoundtripTime);
                await Context.Channel.SendMessageAsync("Time to live: " + reply.Options.Ttl);
                await Context.Channel.SendMessageAsync("Don't fragment: " + reply.Options.DontFragment);
                await Context.Channel.SendMessageAsync("Buffer size: " + reply.Buffer.Length);
            }
        }


    }
}
