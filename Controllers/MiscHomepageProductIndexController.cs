using System.Web.Mvc;
using NopBrasil.Plugin.Misc.HomepageProductIndex.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Controllers;
using Nop.Web.Controllers;

namespace NopBrasil.Plugin.Misc.HomepageProductIndex.Controllers
{
    public class MiscHomepageProductIndexController : BasePublicController
    {
        private readonly ISettingService _settingService;
        private readonly HomepageProductIndexSettings _productIndexSettings;

        public MiscHomepageProductIndexController(ISettingService settingService,
            HomepageProductIndexSettings ProductIndexSettings)
        {
            this._settingService = settingService;
            this._productIndexSettings = ProductIndexSettings;
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new ConfigurationModel()
            {
                QtdProductsInHome = _productIndexSettings.QtdProductsInHome
            };
            return View("~/Plugins/Misc.HomepageProductIndex/Views/MiscHomepageProductIndex/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
            {
                return Configure();
            }
            _productIndexSettings.QtdProductsInHome = model.QtdProductsInHome;
            _settingService.SaveSetting(_productIndexSettings);
            return Configure();
        }
    }
}