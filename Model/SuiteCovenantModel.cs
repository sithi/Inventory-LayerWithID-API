using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SuiteCovenantModel
    {
        public string Did { get; set; }
        public string SuiteDid { get; set; }
        public string LeaseCovenantRefusal { get; set; }
        public string LeaseCovenantRelocate { get; set; }
        public string LeaseCovenantEarlyTerm { get; set; }
        public string LeaseCovenantExpansion { get; set; }

        public string LeaseCovenantExclusive { get; set; }
        public string LeaseCovenantIndemnity { get; set; }
        public string LeaseCovenantOffer { get; set; }
        public string LeaseCovenantSignage { get; set; }

        public string LeaseCovenantAudit { get; set; }
        public string LeaseCovenantContraction { get; set; }
        public string LeaseCovenantRenewal { get; set; }

        public string LeaseCovenantGrossup { get; set; }
        public string BuildingId { get; set; }
    }
}
