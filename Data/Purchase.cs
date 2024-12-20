using SQLite;

namespace Pekanum;

public class Purchase
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public DateTime Date { get; set; }
}