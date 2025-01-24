namespace Pekanum;

public class PurchaseListPage : ContentPage
{
    public PurchaseListPage()
    {
        // �������� ������ �� ����
        var dbService = DependencyService.Get<DatabaseService>();
        List<Purchase> purchases = dbService.GetPurchases();

        // �������� ListView ��� ����������� ������
        ListView listView = new()
        {
            ItemsSource = purchases,
            ItemTemplate = new DataTemplate(() =>
            { 
                Label nameLabel = new();
                nameLabel.SetBinding(Label.TextProperty, "Name");

                Label priceLabel = new();
                priceLabel.SetBinding(Label.TextProperty, "Price");

                Label categoryLabel = new();
                categoryLabel.SetBinding(Label.TextProperty, "Category");

                Label dateLabel = new();
                dateLabel.SetBinding(Label.TextProperty, "Date");

                StackLayout layout = new()
                {
                    Children = { nameLabel, priceLabel, categoryLabel, dateLabel },
                    Orientation = StackOrientation.Vertical
                };

                return new ViewCell { View = layout };
            })
        };

        // ���������� ��������� � StackLayout
        StackLayout layout = new()
        {
            Children = { listView },
            Padding = new Thickness(20),
            Spacing = 10
        };

        // ��������� ����������� ��������
        Content = layout;
    }
}
