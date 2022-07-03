using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.ExportBehaviors
{
    public class TxtExport : IExportBehavior
    {
        public string PrepareExport(StringBuilder data)
        {
            return data.Append("Generated to TXT").ToString();
        }
    }
}
