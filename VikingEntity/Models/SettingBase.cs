using JsonBase;

namespace VikingEntity.Models;

public class SettingBase : JsonBase<Setting>, ISettingBase
{
    public string? Get(string p_key)
    {
        return this.FirstOrDefault(p_x => p_x.Name?.ToLower() == p_key.ToLower())?.Value;
    }
    
    public void Set(string p_key, string p_value)
    {
        var setting = this.FirstOrDefault(p_x => p_x.Name?.ToLower() == p_key.ToLower());
        if (setting == null)
        {
            setting = new Setting {Name = p_key};
            this.Add(setting);
        }
        setting.Value = p_value;
        this.Commit();
    }
}