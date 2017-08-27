using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace NopBrasil.Plugin.Misc.HomepageProductIndex.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Misc.HomepageProductIndex.Fields.QtdProductsInHome")]
        [AllowHtml]
        public int QtdProductsInHome { get; set; }
    }
}