namespace Pekanum;

public class PurchaseListPage : ContentPage
{
    private readonly PurchaseService _purchaseService; // Ñåðâèñ äëÿ ðàáîòû ñ áàçîé äàííûõ
    private readonly CollectionView _collectionView;   // Êîëëåêöèÿ äëÿ îáíîâëåíèÿ èíòåðôåéñà
    private List<Purchase> _purchases;                // Ñïèñîê ïîêóïîê

    public PurchaseListPage()
    {
        // Ïîëó÷àåì ñåðâèñ èç êîíòåéíåðà
        _purchaseService = App.ServiceProvider.GetRequiredService<PurchaseService>();

        // Çàãðóæàåì äàííûå èç áàçû
        _purchases = _purchaseService.GetPurchases();

        // Ñîçäàíèå CollectionView äëÿ îòîáðàæåíèÿ ñïèñêà
        _collectionView = new()
        {
            ItemsSource = _purchases,
            ItemTemplate = new DataTemplate(() =>
            {
                var bind = new Binding(".");
                // Ãîðèçîíòàëüíûé StackLayout äëÿ ñòðîêè
                StackLayout rowLayout = new()
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 10,
                    Padding = new Thickness(5),
                };

                // Ìåòêà ñ èíôîðìàöèåé î ïîêóïêå
                Label infoLabel = new();
                infoLabel.SetBinding(Label.TextProperty, bind);

                // Êíîïêè äåéñòâèé
                StackLayout actionsLayout = new()
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 5,
                    Children =
                    {
                        new Button
                        {
                            Text = "Èçìåíèòü",
                            CommandParameter = bind,
                            Command = new Command<Purchase>(EditPurchase)
                        },
                        new Button
                        {
                            Text = "Óäàëèòü",
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
        // Ëîãèêà èçìåíåíèÿ çàïèñè
        DisplayAlert("Èçìåíåíèå", $"Èçìåíèòü: {purchase.Name}", "ÎÊ");
    }

    private async void DeletePurchase(Purchase purchase)
    {
        bool confirm = await DisplayAlert("Ïîäòâåðæäåíèå", $"Óäàëèòü {purchase.Name}?", "Äà", "Íåò");
        if (confirm)
        {
            // Óäàëÿåì èç áàçû äàííûõ
            _purchaseService.DeletePurchase(purchase.Id);

            // Îáíîâëÿåì ñïèñîê è èíòåðôåéñ
            _purchases.Remove(purchase);
            _collectionView.ItemsSource = null; // Îáíóëÿåì èñòî÷íèê, ÷òîáû ñáðîñèòü ïðèâÿçêó
            _collectionView.ItemsSource = _purchases; // Ïðèâÿçûâàåì îáíîâëåííûé ñïèñîê
        }
    }
}