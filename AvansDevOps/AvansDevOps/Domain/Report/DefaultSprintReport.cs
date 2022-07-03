using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.Report
{
    public class DefaultSprintReport : SprintReport
    {
        public DefaultSprintReport()
        {
            _report = new StringBuilder().AppendLine("Sprint report:");
        }

        public override StringBuilder GetReport()
        {
            return _report;
        }
    }
}
