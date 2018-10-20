using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models
{
    public class Fourms
    {
        [Key]
        public int FourmsID { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string text { get; set; }
        [ScaffoldColumn(false)]
        public DateTime datetime { get; set; }
        [ForeignKey("users")]
        public int UsersID { get; set;  }
        public virtual Users users { get; set; }
        public virtual ICollection<Comments> comments { get; set; }
    }
}