using NopBrasil.Plugin.Misc.HomepageProductIndex.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Localization;

namespace NopBrasil.Plugin.Misc.HomepageProductIndex.Controllers
{
    [Area(AreaNames.Admin)]
    public class MiscHomepageProductIndexController : BasePluginController
    {
        private readonly ISettingService _settingService;
        private readonly HomepageProductIndexSettings _productIndexSettings;
        private readonly ILocalizationService _localizationService;

        public MiscHomepageProductIndexController(ISettingService settingService, HomepageProductIndexSettings ProductIndexSettings,
            ILocalizationService localizationService)
        {
            this._settingService = settingService;
            this._productIndexSettings = ProductIndexSettings;
            this._localizationService = localizationService;
        }

        public ActionResult Configure()
        {
            var model = new ConfigurationModel()
            {
                QtdProductsInHome = _productIndexSettings.QtdProductsInHome
            };
            return View("~/Plugins/Misc.HomepageProductIndex/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public ActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
            {
                return Configure();
            }
            _productIndexSettings.QtdProductsInHome = model.QtdProductsInHome;
            _settingService.SaveSetting(_productIndexSettings);
            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}