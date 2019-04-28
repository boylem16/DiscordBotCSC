using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    [Serializable]
    public class BaseModel
    {
        public string userName = "";
        
    }


    [Serializable]
    public class Movie : BaseModel
    {
        public string genre = "";
        public string movieName = "";
        public string desc = "";
    }


    [Serializable]
    public class Song : BaseModel
    {
        public string link;
    }
}
