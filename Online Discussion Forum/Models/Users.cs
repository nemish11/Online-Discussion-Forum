using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

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
        [ScaffoldColumn(false)]
        public string role { get; set; }
        public virtual ICollection<Fourms> fourms { get; set; }
    }
}