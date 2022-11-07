using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace userDataApi.Models
{
    [Table("loginUser")]
    public class UserDetails
    {
        [Key]
        public int UserId  { get; set; }
       // public string UserName { get ; set; }
       // public string Email { get; set; }
        //public string Password { get; set; }
        public int Count { get; set; }
     
        // public DateTime DateTime { get; set ; }


        [Required(ErrorMessage = "Name required")]
        private string _UserName ;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value.Trim(); }
        }


        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value.Trim(); }
        }

        [Required(ErrorMessage = "Paswoord required")]
        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value?.Trim() ?? string.Empty; }
        }

        private DateTime _DateTime;
        public DateTime DateTime
        {
            get { return _DateTime; }
            set { _DateTime = value.ToUniversalTime(); }
        }
    }
}
