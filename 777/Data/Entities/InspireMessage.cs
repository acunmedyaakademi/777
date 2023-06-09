using _777.Data.Entities.Common;

namespace _777.Data.Entities
{
    public class InspireMessage : BaseClass
    {
        public string Message { get; set; }

        public  UserApp user { get; set; }

        public int UserId{ get; set; }
    }
}
