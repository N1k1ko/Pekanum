namespace Pekanum;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Добавление маршрутов для навигации
        Routing.RegisterRoute(nameof(AddPurchasePage), typeof(AddPurchasePage));
        Routing.RegisterRoute(nameof(PurchaseListPage), typeof(PurchaseListPage));
        Routing.RegisterRoute(nameof(StatsPage), typeof(StatsPage));
    }
}
