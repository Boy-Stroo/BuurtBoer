using Mobile_App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_App
{
    class Translator : IMarkupExtension<BindingBase>
    {
        // name -> key
        public string Name { get; set; }

        // provide value for what is going to be filled in in xaml
        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            return new Binding
            {
                Mode = BindingMode.OneWay,
                // property we're binding to
                Path = $"[{Name}]",
                // binding to localizationresourcemanager, get key from that, give key back to xaml/ui to show change
                Source = LocalizationResourceManager.Instance
            };
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
}
