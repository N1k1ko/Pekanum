namespace Pekanum;

public class PurchaseService
{
    private readonly DatabaseService _databaseService;

    public PurchaseService(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    // Метод для добавления новой покупки с валидацией
    public string AddPurchase(Purchase purchase)
    {
        // Валидация данных
        ValidatePurchase(purchase);

        // Сохранение покупки в базу данных
        _databaseService.SavePurchase(purchase);
        return "Покупка успешно добавлена!";
    }

    // Метод для валидации данных покупки
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

    // Метод для проверки наличия уже существующей покупки в базе данных
    private bool IsPurchaseExist(Purchase purchase)
    {
        var existingPurchases = _databaseService.GetPurchases();
        return existingPurchases.Any(p => p.Name == purchase.Name && p.Category == purchase.Category);
    }

    // Метод для получения списка всех покупок
    public List<Purchase> GetPurchases() => _databaseService.GetPurchases();

    // Метод для удаления покупки по ID
    public string DeletePurchase(int id)
    {
        var purchase = _databaseService.GetPurchases().FirstOrDefault(p => p.Id == id)
            ?? throw new ArgumentException("Покупка не найдена.");
        _databaseService.DeletePurchase(id);
        return "Покупка успешно удалена.";
    }
}
