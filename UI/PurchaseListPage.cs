namespace Pekanum;

public class PurchaseListPage : ContentPage
{
    private readonly PurchaseService _purchaseService; // ������ ��� ������ � ����� ������
    private readonly CollectionView _collectionView;   // ��������� ��� ���������� ����������
    private List<Purchase> _purchases;                // ������ �������

    public PurchaseListPage()
    {
        // �������� ������ �� ����������
        _purchaseService = App.ServiceProvider.GetRequiredService<PurchaseService>();

        // ��������� ������ �� ����
        _purchases = _purchaseService.GetPurchases();

        // �������� CollectionView ��� ����������� ������
        _collectionView = new()
        {
            ItemsSource = _purchases,
            ItemTemplate = new DataTemplate(() =>
            {
                var bind = new Binding(".");
                // �������������� StackLayout ��� ������
                StackLayout rowLayout = new()
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 10,
                    Padding = new Thickness(5),
                };

                // ����� � ����������� � �������
                Label infoLabel = new();
                infoLabel.SetBinding(Label.TextProperty, bind);

                // ������ ��������
                StackLayout actionsLayout = new()
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 5,
                    Children =
                    {
                        new Button
                        {
                            Text = "��������",
                            CommandParameter = bind,
                            Command = new Command<Purchase>(EditPurchase)
                        },
                        new Button
                        {
                            Text = "�������",
                            CommandParameter = bind,
                            Command = new Command<Purchase>(DeletePurchase)
                        }
                    }
                };

                rowLayout.Children.Add(infoLabel);
                rowLayout.Children.Add(actionsLayout);

                return rowLayout;
            })
        };

        Content = _collectionView;
    }

    private void EditPurchase(Purchase purchase)
    {
        // ������ ��������� ������
        DisplayAlert("���������", $"��������: {purchase.Name}", "��");
    }

    private async void DeletePurchase(Purchase purchase)
    {
        bool confirm = await DisplayAlert("�������������", $"������� {purchase.Name}?", "��", "���");
        if (confirm)
        {
            // ������� �� ���� ������
            _purchaseService.DeletePurchase(purchase.Id);

            // ��������� ������ � ���������
            _purchases.Remove(purchase);
            _collectionView.ItemsSource = null; // �������� ��������, ����� �������� ��������
            _collectionView.ItemsSource = _purchases; // ����������� ����������� ������
        }
    }
}