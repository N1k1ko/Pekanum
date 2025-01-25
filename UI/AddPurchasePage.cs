namespace Pekanum;

public class AddPurchasePage : ContentPage
{
    private readonly PurchaseService _purchaseService;

    public AddPurchasePage()
    {
        _purchaseService = App.ServiceProvider.GetRequiredService<PurchaseService>();

        Picker namePicker = new()
        {
            Title = "�������� ��������",
            ItemsSource = _purchaseService.Names
        };
        Entry nameEntry = new() { Placeholder = "�������� ��������", IsVisible = false };


        Entry priceEntry = new()
        {
            Placeholder = "����",
            Keyboard = Keyboard.Numeric
        };

        Picker categoryPicker = new()
        {
            Title = "�������� ���������",
            ItemsSource = _purchaseService.Categories
        };
        Entry categoryEntry = new() { Placeholder = "���������", IsVisible = false };

        DatePicker datePicker = new()
        {
            Date = DateTime.Now,
            Format = "d MMM yyyy"
        };

        Button saveButton = new() { Text = "���������" };

        saveButton.Clicked += async (sender, e) =>
        {
            string selectedName = namePicker.SelectedItem?.ToString() == "����� ������������"
                ? nameEntry.Text
                : namePicker.SelectedItem?.ToString();
            string selectedCategory = categoryPicker.SelectedItem?.ToString() == "����� ���������"
                ? categoryEntry.Text
                : categoryPicker.SelectedItem?.ToString();

            Purchase purchase = new()
            {
                Name = selectedName,
                Price = decimal.TryParse(priceEntry.Text, out var price) ? price : 0,
                Category = selectedCategory,
                Date = datePicker.Date
            };
            try
            {
                _purchaseService.AddPurchase(purchase);
                await Navigation.PopAsync();
            }
            catch (ArgumentException ex)
            {
                await DisplayAlert("������", ex.Message, "OK");
            }
        };
        namePicker.SelectedIndexChanged += (sender, e) =>
        {
            if (namePicker.SelectedItem?.ToString() == "����� ������������")
                nameEntry.IsVisible = true;
            else
                nameEntry.IsVisible = false;
        };
        categoryPicker.SelectedIndexChanged += (sender, e) =>
        {
            if (categoryPicker.SelectedItem?.ToString() == "����� ���������")
                categoryEntry.IsVisible = true;
            else
                categoryEntry.IsVisible = false;
        };

        StackLayout layout = new()
        {
            Children =
            {
                namePicker,
                nameEntry,
                priceEntry,
                categoryPicker,
                categoryEntry,
                datePicker,
                saveButton
            },
            Padding = new Thickness(20),
            Spacing = 10
        };

        Content = layout;
    }
}