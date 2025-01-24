namespace Pekanum;

public class PurchaseService
{
    private readonly DatabaseService _databaseService;
    private readonly LocalStorageService _localStorageService;
    public List<string> Categories { get; }
    public List<string> Names { get; }

    public PurchaseService(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        _localStorageService = App.ServiceProvider.GetRequiredService<LocalStorageService>();

        Categories = _localStorageService.LoadList("Categories");
        Names = _localStorageService.LoadList("Names");
    }

    public string AddPurchase(Purchase purchase)
    {
        ValidatePurchase(purchase);

        _databaseService.SavePurchase(purchase);
        if(!Categories.Contains(purchase.Category!))
        {
            Categories.Add(purchase.Category!);
            _localStorageService.SaveList("Categories", Categories);
        }
        if (!Names.Contains(purchase.Name!))
        {
            Names.Add(purchase.Name!);
            _localStorageService.SaveList("Names", Names);
        }
        return "Покупка успешно добавлена!";
    }

    private void ValidatePurchase(Purchase purchase)
    {
        if (string.IsNullOrEmpty(purchase.Name))
            throw new ArgumentException("Имя покупки не может быть пустым.");

        if (purchase.Price <= 0)
            throw new ArgumentException("Цена должна быть больше нуля.");

        if (string.IsNullOrEmpty(purchase.Category))
            throw new ArgumentException("Категория не может быть пустой.");

        if (purchase.Date > DateTime.Now)
            throw new ArgumentException("Дата не может быть в будущем.");
    }

    private bool IsPurchaseExist(Purchase purchase)
    {
        var existingPurchases = _databaseService.GetPurchases();
        return existingPurchases.Any(p => p.Name == purchase.Name && p.Category == purchase.Category);
    }

    public List<Purchase> GetPurchases() => _databaseService.GetPurchases();

    public string DeletePurchase(int id)
    {
        var purchase = _databaseService.GetPurchases().FirstOrDefault(p => p.Id == id)
            ?? throw new ArgumentException("Покупка не найдена.");
        _databaseService.DeletePurchase(id);
        return "Покупка успешно удалена.";
    }
}
