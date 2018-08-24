using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace NopBrasil.Plugin.Misc.HomepageProductIndex.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Misc.HomepageProductIndex.Fields.QtdProductsInHome")]
        public int QtdProductsInHome { get; set; }
    }
}