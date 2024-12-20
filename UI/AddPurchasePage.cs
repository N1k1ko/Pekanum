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
                Date = DateTime.Now
            };

            // ��������� ������� � ���� ������
            var dbService = DependencyService.Get<DatabaseService>();
            dbService.SavePurchase(purchase);

            // ������� �� ������� �����
            await Navigation.PopAsync();
        };

        // ���������� ��������� � StackLayout
        StackLayout layout = new()
        {
            Children = { nameEntry, priceEntry, categoryEntry, saveButton },
            Padding = new Thickness(20),
            Spacing = 10
        };

        // ��������� ����������� ��������
        Content = layout;
    }
}