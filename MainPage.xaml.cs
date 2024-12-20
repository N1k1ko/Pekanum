namespace Pekanum;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        // Кнопка для добавления покупки
        Button addButton = new()
        {
            Text = "Добавить покупку"
        };
        addButton.Clicked += OnAddPurchaseClicked;

        // Кнопка для просмотра списка покупок
        Button viewButton = new()
        {
            Text = "Список покупок"
        };
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

    // Обработчики нажатий на кнопки
    private async void OnAddPurchaseClicked(object sender, EventArgs e)
    {
        // Переход на страницу добавления покупки
        await Navigation.PushAsync(new AddPurchasePage());
    }

    private async void OnViewPurchasesClicked(object sender, EventArgs e)
    {
        // Переход на страницу с просмотром списка покупок
        await Navigation.PushAsync(new PurchaseListPage());
    }
}
