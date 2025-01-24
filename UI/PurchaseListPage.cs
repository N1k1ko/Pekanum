using System.Reflection;

namespace Pekanum;

public class PurchaseListPage : ContentPage
{
    public PurchaseListPage()
    {
        // �������� ������ �� ����
        var purchaseService = App.ServiceProvider.GetRequiredService<PurchaseService>();
        List<Purchase> purchases = purchaseService.GetPurchases();

        // �������� ListView ��� ����������� ������
        ListView listView = new()
        {
            ItemsSource = purchases,
            ItemTemplate = new DataTemplate(() =>
            {
                StackLayout layout = new() { Orientation = StackOrientation.Vertical };

                // �������� ��� ��������� �������� ������ Purchase � ������� ���������
                var properties = typeof(Purchase).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // ������� Label ��� ������� ��������
                foreach (var property in properties.Skip(1))
                {
                    Label label = new();
                    label.SetBinding(Label.TextProperty, new Binding(property.Name));

                    layout.Children.Add(label);
                }

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
