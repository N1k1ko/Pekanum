using Microsoft.Extensions.DependencyInjection;

namespace Pekanum;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        Button addButton = new() { Text = "Добавить покупку" };
        addButton.Clicked += async (sender, args) =>
            await Shell.Current.GoToAsync(nameof(AddPurchasePage));

        Button viewButton = new() { Text = "Список покупок" };
        viewButton.Clicked += async (sender, args) =>
            await Shell.Current.GoToAsync(nameof(PurchaseListPage));

        Button statButton = new() { Text = "Статистика" };
        statButton.Clicked += async (sender, args) =>
            await Shell.Current.GoToAsync(nameof(StatsPage)); ;

        // StackLayout для вертикального расположения элементов
        StackLayout layout = new()
        {
            Children = { addButton, viewButton, statButton },
            Padding = new Thickness(20),
            Spacing = 10
        };

        // Установка главного содержимого страницы
        Content = layout;
    }
}
