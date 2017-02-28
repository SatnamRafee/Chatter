using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Chatter.Models
{
    public class Chat
    {
        [Key]
        public int ChatID { get; set; }

        public string ChatBody { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt| ddd, MMM d, yyyy}")]
        public DateTime PublishDate = DateTime.Now;
        
        //
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string UserId { get; internal set; }
    }
}