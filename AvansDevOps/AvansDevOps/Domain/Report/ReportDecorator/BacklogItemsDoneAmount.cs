using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.Report.ReportDecorator
{
    public class BacklogItemsDoneAmount : SprintReportDecorator
    {
        public BacklogItemsDoneAmount(SprintReport sprintReport)
        {
            _sprintReport = sprintReport;
        }

        public override StringBuilder GetReport()
        {
            return _sprintReport.GetReport().AppendLine("All backlog items were done this sprint");
        }
    }
}
