using SQLite;

namespace Pekanum;

public class Purchase
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Category { get; set; }
    public DateTime Date { get; set; }

    public override string ToString() =>
        $"{Name}\n" +
        $"{Price} руб\n" +
        $"{Category}\n" +
        $"{Date:dd.MM.yyyy}";
}