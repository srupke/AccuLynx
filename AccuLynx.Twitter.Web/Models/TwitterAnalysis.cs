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

        [Required]
        public string WordOrPhrase1 { get; set; }

        [Required]
        public string WordOrPhrase2 { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}