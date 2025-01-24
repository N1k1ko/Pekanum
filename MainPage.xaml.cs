namespace Pekanum;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        Button addButton = new() { Text = "Добавить покупку" };
        addButton.Clicked += OnAddPurchaseClicked;

        Button viewButton = new() { Text = "Список покупок" };
        viewButton.Clicked += OnViewPurchasesClicked;

        // StackLayout для вертикального расположения элементов
        StackLayout layout = new()
        {
            Children = { addButton, viewButton },
            Padding = new Thickness(20),
            Spacing = 10
        };

        // Установка главного содержимого страницы
        Content = layout;
    }

    private async void OnAddPurchaseClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(AddPurchasePage));

    private async void OnViewPurchasesClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(PurchaseListPage));
}
