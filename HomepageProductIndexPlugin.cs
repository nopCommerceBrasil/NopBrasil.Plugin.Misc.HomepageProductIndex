using Nop.Core.Plugins;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Common;
using NopBrasil.Plugin.Misc.HomepageProductIndex.Task;
using Nop.Core;

namespace NopBrasil.Plugin.Misc.HomepageProductIndex
{
    public class HomepageProductIndexPlugin : BasePlugin, IMiscPlugin
    {
        private readonly ISettingService _settingService;
        private readonly HomepageProductIndexSettings _ProductIndexSettings;
        private readonly HomepageProductIndexTask _homepageProductIndexTask;
        private readonly IWebHelper _webHelper;
        private readonly ILocalizationService _localizationService;

        public HomepageProductIndexPlugin(ISettingService settingService, HomepageProductIndexSettings ProductIndexSettings, HomepageProductIndexTask homepageProductIndexTask,
            IWebHelper webHelper, ILocalizationService localizationService)
        {
            this._settingService = settingService;
            this._ProductIndexSettings = ProductIndexSettings;
            this._homepageProductIndexTask = homepageProductIndexTask;
            this._webHelper = webHelper;
            this._localizationService = localizationService;
        }

        public override string GetConfigurationPageUrl() => _webHelper.GetStoreLocation() + "Admin/MiscHomepageProductIndex/Configure";

        public override void Install()
        {
            var settings = new HomepageProductIndexSettings
            {
                QtdProductsInHome = 8
            };
            _settingService.SaveSetting(settings);

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Misc.HomepageProductIndex.Fields.QtdProductsInHome", "Qtd products in home");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Misc.HomepageProductIndex.Fields.QtdProductsInHome.Hint", "Enter number of products that will de displayed in home page.");

            _homepageProductIndexTask.InstallTask();

            base.Install();
        }

        public override void Uninstall()
        {
            _homepageProductIndexTask.UninstallTask();
            
            _settingService.DeleteSetting<HomepageProductIndexSettings>();

            _localizationService.DeletePluginLocaleResource("Plugins.Misc.HomepageProductIndex.Fields.QtdProductsInHome");
            _localizationService.DeletePluginLocaleResource("Plugins.Misc.HomepageProductIndex.Fields.QtdProductsInHome.Hint");

            base.Uninstall();
        }
    }
}
