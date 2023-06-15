using _777.Data.Entities;

namespace _777.Models
{
    public class ProfileVM
    {
        public UserApp User { get; set; }
        public List<TextDetail>? Details{ get; set; }
    }
}
