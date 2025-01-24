namespace Pekanum;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public App()
    {
        InitializeComponent();

        // Настройка DI
        var services = new ServiceCollection();
        ConfigureServices(services);

        // Разрешаем зависимость
        ServiceProvider = services.BuildServiceProvider();

        MainPage = new AppShell();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Путь для долгого хранения бд
        //string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Pekanum.db");
        // Получаем путь к кэшу для Android
        string dbPath = Path.Combine(FileSystem.CacheDirectory, "Pekanum.db");
        // Регистрируем зависимости
        services.AddSingleton<DatabaseService>(provider => new DatabaseService(dbPath));

        services.AddSingleton<PurchaseService>(provider => new PurchaseService(ServiceProvider.GetRequiredService<DatabaseService>()));

        services.AddSingleton<LocalStorageService>(provider => new LocalStorageService());
    }
}
