using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Common.Models
{
    public class AgentItirenaryVM
    {
        [Display(Name = "Visit Date")]
        public DateTime? VisitDate { get; set; }
        public string? AgentId { get; set; }

        [Display(Name = "Agent Name")]
        public string? AgentName { get; set; }
        public string? CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string? CustomerName { get; set; }

        [Display(Name = "Contact Person")]
        public string? ContactPerson { get; set; }

        public string? Position { get; set; }

        [Display(Name = "Sales Amount")]
        public decimal? SalesAmount { get; set; }

        public decimal? Collection { get; set; }

        [Display(Name = "Purpose of Visit")]
        public string? PurposeOfVisit { get; set; }

        [Display(Name = "Action Plan")]
        public string? ActionPlan { get; set; }

        public string? Remarks { get; set; }

        [Display(Name = "Longhitud")]
        public string? Long { get; set; }

        [Display(Name = "Latitude")]
        public string? Lat { get; set; }
    }

    public class AgentWeeklyVM
    {
        public string AgentName { get; set; } = "";
        public string Customer { get; set; } = "All";
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal SalesForWeek { get; set; }
        public decimal CollectionForWeek { get; set; }
        public IList<AgentItirenaryVM> Rows { get; set; } = new List<AgentItirenaryVM>();
    }

    public class AgentActivityRow
    {
        public DateTime VisitDate { get; set; }
        public string CustomerName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Position { get; set; }
        public string? Purpose { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal Collection { get; set; }
        public string? ActionPlan { get; set; }
        public string? Remarks { get; set; }
    }

    public class AgentMonthlyVM
    {
        public string AgentName { get; set; } = "";
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public IList<AgentItirenaryVM> Rows { get; set; } = new List<AgentItirenaryVM>();
    }
}
