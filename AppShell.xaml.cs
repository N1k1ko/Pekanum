namespace Pekanum;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AddPurchasePage), typeof(AddPurchasePage));
        Routing.RegisterRoute(nameof(PurchaseListPage), typeof(PurchaseListPage));
        Routing.RegisterRoute(nameof(StatsPage), typeof(StatsPage));
    }
}
