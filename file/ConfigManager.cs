using Web_Framework.http;
using Web_Framework.lib.err;
using Web_Framework.logger;

namespace Web_Framework.file;

public class ConfigManager
{
    private static ConfigManager _instance;
    private Dictionary<string, string> _config;

    private ConfigManager()
    {
        
        RefreshConfig();
    }

    public static ConfigManager GetManager()
    {
        _instance ??= new ConfigManager();
        return _instance;
    }

    public void RefreshConfig()
    {
        _config = new Dictionary<string, string>();
        string[] options = File.ReadAllLines("server.cfg");
        
        foreach (string option in options)
        {
            string[] data = option.Split(":");

            _config.Add(data[0], data[1].TrimStart());
        }
    }

    public string GetString(string key)
    {
        return _config[key];
    }

    public bool GetBool(string key)
    {
        return bool.Parse(_config[key]);
    }

    public int GetInt(string key)
    {
        return int.Parse(_config[key]);
    }

    public double GetDouble(string key)
    {
        return double.Parse(_config[key]);
    }

    public float GetFloat(string key)
    {
        return float.Parse(_config[key]);
    }

    public T GetType<T>(string key) where T : struct
    {
        if (typeof(T).IsEnum)
        {
            if (Enum.TryParse(_config[key], out T data))
            {
                return data;
            }
            else
            {
                Logger.GetLogger().Log(Logger.LogLevel.Warning, "Failed to get a config value for " + key);
                return default;
            }
        }
        else
        {
            throw new InvalidTypeEntryException("A non enum value was use and so the config data could not be extracted and parsed as a " + typeof(T).Name);
        }
    }
}