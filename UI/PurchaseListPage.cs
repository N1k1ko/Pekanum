using System.Reflection;

namespace Pekanum;

public class PurchaseListPage : ContentPage
{
    public PurchaseListPage()
    {
        // Получаем данные из базы
        var purchaseService = App.ServiceProvider.GetRequiredService<PurchaseService>();
        List<Purchase> purchases = purchaseService.GetPurchases();

        // Создание ListView для отображения списка
        ListView listView = new()
        {
            ItemsSource = purchases,
            ItemTemplate = new DataTemplate(() =>
            {
                StackLayout layout = new() { Orientation = StackOrientation.Vertical };

                // Получаем все публичные свойства класса Purchase с помощью рефлексии
                var properties = typeof(Purchase).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // Создаем Label для каждого свойства
                foreach (var property in properties.Skip(1))
                {
                    Label label = new();
                    label.SetBinding(Label.TextProperty, new Binding(property.Name));

                    layout.Children.Add(label);
                }

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
