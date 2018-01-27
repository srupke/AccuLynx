using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccuLynx.Twitter.Models
{
    [Table("TwitterAnalysis", Schema ="dbo")]
    public class TwitterAnalysisModel
    {
        [Key]
        public int AnalysisId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime AnalyzedOn { get; set; }


        public ICollection<TwitterAnalysisPhraseModel> Phrases { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public TwitterAnalysisModel()
        {
            Phrases = new HashSet<TwitterAnalysisPhraseModel>();
        }

    }
}
