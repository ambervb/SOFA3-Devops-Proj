using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain.Report
{
    public abstract class SprintReport
    {
        protected StringBuilder _report;

        public abstract StringBuilder GetReport();
    }
}
