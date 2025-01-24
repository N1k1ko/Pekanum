namespace Pekanum;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public App()
    {
        InitializeComponent();

        var services = new ServiceCollection();
        ConfigureServices(services);

        ServiceProvider = services.BuildServiceProvider();

        MainPage = new AppShell();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pekanum.db");
        services.AddSingleton<DatabaseService>(provider => new DatabaseService(dbPath));

        services.AddSingleton<PurchaseService>(provider => new PurchaseService(ServiceProvider.GetRequiredService<DatabaseService>()));

        services.AddSingleton<LocalStorageService>(provider => new LocalStorageService());
    }
}
