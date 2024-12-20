namespace Pekanum;

public class PurchaseListPage : ContentPage
{
    public PurchaseListPage()
    {
        // Получаем данные из базы
        var dbService = DependencyService.Get<DatabaseService>();
        List<Purchase> purchases = dbService.GetPurchases();

        // Создание ListView для отображения списка
        ListView listView = new()
        {
            ItemsSource = purchases,
            ItemTemplate = new DataTemplate(() =>
            {
                Label nameLabel = new();
                nameLabel.SetBinding(Label.TextProperty, "Name");

                Label priceLabel = new();
                priceLabel.SetBinding(Label.TextProperty, "Price");

                StackLayout layout = new()
                {
                    Children = { nameLabel, priceLabel },
                    Orientation = StackOrientation.Horizontal
                };

                return new ViewCell { View = layout };
            })
        };

        // Размещение элементов в StackLayout
        StackLayout layout = new()
        {
            Children = { listView },
            Padding = new Thickness(20),
            Spacing = 10
        };

        // Установка содержимого страницы
        Content = layout;
    }
}
