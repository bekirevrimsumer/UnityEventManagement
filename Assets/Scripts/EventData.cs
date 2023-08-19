using System.Collections.Generic;

public class EventData
{
    private Dictionary<string, object> _data;

    public EventData()
    {
        _data = new Dictionary<string, object>();
    }

    public void AddData(string key, object value)
    {
        if (!_data.ContainsKey(key))
            _data.Add(key, value);
        else
            _data[key] = value;
    }

    public T GetData<T>(string key)
    {
        if (_data.ContainsKey(key))
            return (T)_data[key];

        return default(T);
    }

    public void Clear()
    {
        _data.Clear();
    }
}