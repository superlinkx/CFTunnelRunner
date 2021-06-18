using System;
using System.Configuration;

namespace CloudflareTunnelRunner
{
    public class UserSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string Domain
        {
            get => (string)this[nameof(Domain)];
            set => this[nameof(Domain)] = value;
        }
        [UserScopedSetting()]
        [DefaultSettingValue("localhost")]
        public string Endpoint
        {
            get => (string)this[nameof(Endpoint)];
            set => this[nameof(Endpoint)] = value;
        }
        [UserScopedSetting()]
        [DefaultSettingValue("2244")]
        public string Port
        {
            get => ((string)this[nameof(Port)]);
            set => this[nameof(Port)] = value;
        }
    }
}
