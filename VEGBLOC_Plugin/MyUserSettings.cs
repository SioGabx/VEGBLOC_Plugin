using System;
using System.Configuration;
using System.Drawing;

public class MyUserSettings : ApplicationSettingsBase
{
    [UserScopedSetting()]
    [DefaultSettingValue("white")]
    public Color BackgroundColor
    {
        get
        {
            return ((Color)this["BackgroundColor"]);
        }
        set
        {
            this["BackgroundColor"] = (Color)value;
        }
    } 
    
   
    [UserScopedSettingAttribute()]
    public String index
    {
        get { return (String)this["index"]; }
        set { this["index"] = value; }
    }

    [UserScopedSettingAttribute()]
    public String legende
    {
        get { return (String)this["legende"]; }
        set { this["legende"] = value; }
    }
}