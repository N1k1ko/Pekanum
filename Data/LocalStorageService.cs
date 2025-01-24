using System.Text.Json;

namespace Pekanum;

public class LocalStorageService
{
    public LocalStorageService()
    {
        if (!Preferences.ContainsKey("Categories"))
            SaveList("Categories", ["Новая категория", "Продукты", "Одежда", "Техника", "Развлечения"]);
        if (!Preferences.ContainsKey("Names"))
            SaveList("Names", ["Новое наименование", "Хлеб", "Молоко", "Телефон", "Куртка"]);
    }

    public void SaveList(string key, List<string> list)
    {
        var json = JsonSerializer.Serialize(list);
        Preferences.Set(key, json);
    }

    public List<string> LoadList(string key)
    {
        var json = Preferences.Get(key, string.Empty);
        return JsonSerializer.Deserialize<List<string>>(json);
    }
}
