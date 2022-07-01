using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotWpfSB2
{
    public class MessageLog
    {
        public MessageLog(string time, string msg, string firstName, long id, string type)
        {
            Time = time;
            Msg = msg;
            FirstName = firstName;
            Id = id;
            Type = type;
        }

        public string Time { get; set; }

        public long Id { get; set; }

        public string Msg { get; set; }

        public string FirstName { get; set; }

        public string Type { get; set; }

    }
}
