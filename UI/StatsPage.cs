namespace Pekanum;

public class StatsPage : ContentPage
{
    public StatsPage()
    {
        var purchaseService = App.ServiceProvider.GetRequiredService<PurchaseService>();
        var stats = purchaseService.GetPurchases()
            .Where(z => (z.Date.Month == DateTime.Now.Month) && (z.Date.Year == DateTime.Now.Year))
            .GroupBy(z => z.Category)
            .Select(z => (z.Key, z.Sum(x => x.Price)))
            .OrderBy(z => z.Item2);

        TableView tableView = new()
        {
            Root = new TableRoot(DateTime.Now.Month.ToString())
            {
                new TableSection("Расходы по категориям")
            }
        };

        var ind = 0;
        foreach (var stat in stats)
        {
            TextCell tmp = new() { Text = stat.Key, Detail = stat.Item2 + "руб" };
            tableView.Root.First().Insert(ind++, tmp);
        }

        StackLayout layout = new()
        {
            Children = { tableView },
            Padding = new Thickness(20),
            Spacing = 10
        };

        Content = layout;
    }
}