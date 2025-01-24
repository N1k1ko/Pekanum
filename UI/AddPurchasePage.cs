namespace Pekanum;

public class AddPurchasePage : ContentPage
{
    public AddPurchasePage()
    {
        // Поля ввода
        Entry nameEntry = new() { Placeholder = "Название продукта" };

        Entry priceEntry = new()
        {
            Placeholder = "Цена",
            Keyboard = Keyboard.Numeric
        };

        Entry categoryEntry = new() { Placeholder = "Категория" };

        // Кнопка для сохранения
        Button saveButton = new() { Text = "Сохранить" };

        saveButton.Clicked += async (sender, e) =>
        {
            // Логика сохранения в базу данных
            Purchase purchase = new()
            {
                Name = nameEntry.Text,
                Price = decimal.TryParse(priceEntry.Text, out var price) ? price : 0,
                Category = categoryEntry.Text,
                Date = DateTime.Now
            };
            try
            {
                // Сохраняем покупку в базу данных
                var purchaseService = App.ServiceProvider.GetRequiredService<PurchaseService>();
                purchaseService.AddPurchase(purchase);
                // Переход на главный экран
                await Navigation.PopAsync();
            }
            catch (ArgumentException ex)
            {
                // Обработка ошибок валидации
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        };

        // Размещение элементов в StackLayout
        StackLayout layout = new()
        {
            Children = { nameEntry, priceEntry, categoryEntry, saveButton },
            Padding = new Thickness(20),
            Spacing = 10
        };

        // Установка содержимого страницы
        Content = layout;
    }
}