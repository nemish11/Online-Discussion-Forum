using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models
{
    public class Comments
    {
        [Key]
        public int CommentsID { get; set; }
        [Required]
        public string commenttext { get; set; }
        public string uname { get; set; }
        [ForeignKey("fourms")]
        public int FourmsID { get; set; }
        public virtual Fourms fourms { get; set; }
    }
}