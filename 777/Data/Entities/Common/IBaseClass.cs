namespace _777.Data.Entities.Common
{
    public interface IBaseClass
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set;}
       
    }
}
