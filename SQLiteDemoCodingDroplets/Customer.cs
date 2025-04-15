using SQLite;

namespace SQLiteDemoCodingDroplets
{
    [Table("customer")]
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
