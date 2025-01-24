using SQLite;

namespace Pekanum;

public class DatabaseService
{
    private readonly SQLiteConnection _database;

    public DatabaseService(string dbPath)
    {
        _database = new(dbPath);
        _database.CreateTable<Purchase>();
    }

    public List<Purchase> GetPurchases() => [.. _database.Table<Purchase>()];

    public int SavePurchase(Purchase purchase) => _database.Insert(purchase);

    public int DeletePurchase(int id) => _database.Delete<Purchase>(id);
}