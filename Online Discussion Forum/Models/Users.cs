using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace WebApplication4.Models
{
    public class Users
    {
        [Key]
        public int UsersID { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string uname { get; set; }
        [Required]
        public string password { get; set; }
        public string role { get; set; }
        public virtual ICollection<Fourms> fourms { get; set; }
    }
}