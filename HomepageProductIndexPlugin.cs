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

        public HomepageProductIndexPlugin(ISettingService settingService, HomepageProductIndexSettings ProductIndexSettings, HomepageProductIndexTask homepageProductIndexTask,
            IWebHelper webHelper)
        {
            this._settingService = settingService;
            this._ProductIndexSettings = ProductIndexSettings;
            this._homepageProductIndexTask = homepageProductIndexTask;
            this._webHelper = webHelper;
        }

        public override string GetConfigurationPageUrl() => _webHelper.GetStoreLocation() + "Admin/MiscHomepageProductIndex/Configure";

        public override void Install()
        {
            var settings = new HomepageProductIndexSettings
            {
                QtdProductsInHome = 8
            };
            _settingService.SaveSetting(settings);

            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.HomepageProductIndex.Fields.QtdProductsInHome", "Qtd products in home");
            this.AddOrUpdatePluginLocaleResource("Plugins.Misc.HomepageProductIndex.Fields.QtdProductsInHome.Hint", "Enter number of products that will de displayed in home page.");

            _homepageProductIndexTask.InstallTask();

            base.Install();
        }

        public override void Uninstall()
        {
            _homepageProductIndexTask.UninstallTask();
            
            _settingService.DeleteSetting<HomepageProductIndexSettings>();

            this.DeletePluginLocaleResource("Plugins.Misc.HomepageProductIndex.Fields.QtdProductsInHome");
            this.DeletePluginLocaleResource("Plugins.Misc.HomepageProductIndex.Fields.QtdProductsInHome.Hint");

            base.Uninstall();
        }
    }
}
