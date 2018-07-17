using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;

namespace NopBrasil.Plugin.Misc.HomepageProductIndex.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Misc.HomepageProductIndex.Fields.QtdProductsInHome")]
        public int QtdProductsInHome { get; set; }
    }
}