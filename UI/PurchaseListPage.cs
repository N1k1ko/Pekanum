namespace Pekanum;

public class PurchaseListPage : ContentPage
{
    private readonly PurchaseService _purchaseService; // Сервис для работы с базой данных
    private readonly CollectionView _collectionView;   // Коллекция для обновления интерфейса
    private List<Purchase> _purchases;                // Список покупок

    public PurchaseListPage()
    {
        // Получаем сервис из контейнера
        _purchaseService = App.ServiceProvider.GetRequiredService<PurchaseService>();

        // Загружаем данные из базы
        _purchases = _purchaseService.GetPurchases();

        // Создание CollectionView для отображения списка
        _collectionView = new()
        {
            ItemsSource = _purchases,
            ItemTemplate = new DataTemplate(() =>
            {
                var bind = new Binding(".");
                // Горизонтальный StackLayout для строки
                StackLayout rowLayout = new()
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 10,
                    Padding = new Thickness(5),
                };

                // Метка с информацией о покупке
                Label infoLabel = new();
                infoLabel.SetBinding(Label.TextProperty, bind);

                // Кнопки действий
                StackLayout actionsLayout = new()
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 5,
                    Children =
                    {
                        new Button
                        {
                            Text = "Изменить",
                            CommandParameter = bind,
                            Command = new Command<Purchase>(EditPurchase)
                        },
                        new Button
                        {
                            Text = "Удалить",
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
        // Логика изменения записи
        DisplayAlert("Изменение", $"Изменить: {purchase.Name}", "ОК");
    }

    private async void DeletePurchase(Purchase purchase)
    {
        bool confirm = await DisplayAlert("Подтверждение", $"Удалить {purchase.Name}?", "Да", "Нет");
        if (confirm)
        {
            // Удаляем из базы данных
            _purchaseService.DeletePurchase(purchase.Id);

            // Обновляем список и интерфейс
            _purchases.Remove(purchase);
            _collectionView.ItemsSource = null; // Обнуляем источник, чтобы сбросить привязку
            _collectionView.ItemsSource = _purchases; // Привязываем обновленный список
        }
    }
}