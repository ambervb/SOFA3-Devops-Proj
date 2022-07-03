using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.Report.ReportDecorator
{
    public class CommitAmount : SprintReportDecorator
    {
        public CommitAmount(SprintReport sprintReport)
        {
            _sprintReport = sprintReport;
        }

        public override StringBuilder GetReport()
        {
            return _sprintReport.GetReport().AppendLine("A lot of commits have been done this sprint");
        }
    }
}
