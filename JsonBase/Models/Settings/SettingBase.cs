using System.Net.Sockets;
using JsonBase;
using JsonBase.Models.Logging;
using JsonBase.Models.Settings;

namespace JsonBase.Models.Settings;

public class SettingBase : JsonBase<Setting>, ISettingBase
{
    public SettingBase() { }

    public Setting? GetSetting(string p_key)
    {
        try
        {
            var setting = this.FirstOrDefault(p_x => p_x.Name?.ToLower() == p_key.ToLower());
            setting!.Value = Convert.ToString(setting.Type switch
            {
                "System.Int32" => int.Parse(setting.Value!),
                "System.Boolean" => bool.Parse(setting.Value!),
                "System.Double" => double.Parse(setting.Value!),
                "System.String" => setting.Value,
                _ => throw new Exception("Invalid Type")
            });
            return setting;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public object? GetValue(string p_key)
    {
        try{
            var setting = this.FirstOrDefault(p_x => p_x.Name?.ToLower() == p_key.ToLower());
            if (setting == null)
            {
                throw new Exception($"Setting '{p_key}' does not exist");
            }
            return setting!.Type switch
            {
                "System.Int32" => int.Parse(setting.Value!),
                "System.Boolean" => bool.Parse(setting.Value!),
                "System.Double" => double.Parse(setting.Value!),
                "System.String" => setting.Value,
                _ => throw new Exception("Invalid Type")
            };
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public void Set(string p_key, object p_value)
    {
        try
        {
            var setting = this.FirstOrDefault(p_x => p_x.Name?.ToLower() == p_key.ToLower());
            if (setting == null)
            {
                setting = new Setting { Name = p_key };
                this.Add(setting);
            }

            setting.Value = p_value.ToString();
            this.Commit();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public void Add(string p_key, object? p_value)
    {
        try
        {
            var maxId = this.Max(p_x => p_x.Oid);
            var oid = maxId != null ? maxId + 1 : 1;

            var setting = new Setting
            {
                Oid =  oid,
                Name = p_key,
                Value = p_value!.ToString(),
                Type = p_value.GetType().ToString()
            };
            Add(setting);
            Commit();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}