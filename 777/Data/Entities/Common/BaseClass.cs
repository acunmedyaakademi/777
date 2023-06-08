namespace _777.Data.Entities.Common
{
    public class BaseClass : IBaseClass
    {

        public int MyProperty { get; set; }
        public int Id { get; set ; }
        public DateTime CreatedOn { get ; set ; }
        public DateTime UpdatedOn { get ; set ; }
        public bool IsActive { get ; set ; }
    }
}
