using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatter.Models
{
    public class ChatViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public Chat Chat { get; set; }
    }
}