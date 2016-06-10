using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WebApi_SAP_RFC_rfc_read_table.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}