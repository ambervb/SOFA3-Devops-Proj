using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.Report
{
    public class StrangeSprintReport : SprintReport
    {
        public StrangeSprintReport()
        {
            _report = new StringBuilder().AppendLine("This report is strange:");
        }

        public override StringBuilder GetReport()
        {
            return _report;
        }
    }
}
