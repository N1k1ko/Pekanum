namespace Pekanum;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        // Путь для долгого хранения бд
        //string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Pekanum.db");
        
        // Получаем путь к кэшу для Android
        string dbPath = Path.Combine(FileSystem.CacheDirectory, "Pekanum.db");
        
        // Регистрация DatabaseService в DependencyService
        DependencyService.RegisterSingleton(new DatabaseService(dbPath));

        Button addButton = new() {Text = "Добавить покупку"};
        addButton.Clicked += OnAddPurchaseClicked;

        Button viewButton = new() {Text = "Список покупок"};
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
