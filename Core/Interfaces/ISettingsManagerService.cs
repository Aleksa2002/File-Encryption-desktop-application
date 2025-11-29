using ZastitaInformacija.Core.Settings;

namespace ZastitaInformacija.Core.Interfaces
{
    internal interface ISettingsManagerService
    {
        void Save(AppSettings settings);
        AppSettings Load();
    }
}