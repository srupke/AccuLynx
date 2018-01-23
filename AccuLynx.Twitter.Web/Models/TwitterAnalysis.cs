using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccuLynx.Twitter.Web.Models
{
    public class TwitterAnalysis
    {
        [Key]
        public int AnalysisId { get; set; }

        public string Description { get; set; }

    }
}