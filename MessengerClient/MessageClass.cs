using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClient
{
    public class MessageClass
    {
        public string userName { set; get; }
        public string messageText { set; get; }
        public string timeStamp { set; get; }

        public override string ToString()
        {
            return $"{timeStamp} - {userName}: {messageText}";
        }
    }
}
