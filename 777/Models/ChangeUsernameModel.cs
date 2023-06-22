using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace _777.Models
{
    public class ChangeUsernameModel { 

        [Required]
      
        public string NewUsername { get; set; }

        [Required]
        
        public string ConfirmUsername { get; set; }
    }
}
