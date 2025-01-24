namespace Pekanum;

public class AddPurchasePage : ContentPage
{
    public AddPurchasePage()
    {
        // ���� �����
        Entry nameEntry = new() { Placeholder = "�������� ��������" };

        Entry priceEntry = new()
        {
            Placeholder = "����",
            Keyboard = Keyboard.Numeric
        };

        Entry categoryEntry = new() { Placeholder = "���������" };

        // ������� ��� ������ ����
        DatePicker datePicker = new()
        {
            Date = DateTime.Now, // �� ��������� ������� ����
            Format = "d MMM yyyy" // ������ ������������ ����
        };

        // ������ ��� ����������
        Button saveButton = new() { Text = "���������" };

        saveButton.Clicked += async (sender, e) =>
        {
            // ������ ���������� � ���� ������
            Purchase purchase = new()
            {
                Name = nameEntry.Text,
                Price = decimal.TryParse(priceEntry.Text, out var price) ? price : 0,
                Category = categoryEntry.Text,
                Date = datePicker.Date
            };
            try
            {
                // ��������� ������� � ���� ������
                var purchaseService = App.ServiceProvider.GetRequiredService<PurchaseService>();
                purchaseService.AddPurchase(purchase);
                // ������� �� ������� �����
                await Navigation.PopAsync();
            }
            catch (ArgumentException ex)
            {
                // ��������� ������ ���������
                await DisplayAlert("������", ex.Message, "OK");
            }
        };

        // ���������� ��������� � StackLayout
        StackLayout layout = new()
        {
            Children = { nameEntry, priceEntry, categoryEntry, datePicker, saveButton },
            Padding = new Thickness(20),
            Spacing = 10
        };

        // ��������� ����������� ��������
        Content = layout;
    }
}