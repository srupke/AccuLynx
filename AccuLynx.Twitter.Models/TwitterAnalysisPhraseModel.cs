using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace AccuLynx.Twitter.Models
{
    [Table("TwitterAnalysisPhrase", Schema = "dbo")]
    public class TwitterAnalysisPhraseModel
    {
        [Key]
        public int PhraseId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string PhraseText { get; set; }


        [ForeignKey("Analysis")]
        public int AnalysisId { get; set; }

        [ScriptIgnore]
        public TwitterAnalysisModel Analysis { get; set; }

        public ICollection<TwitterAnalysisPhraseDetailModel> Details { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public TwitterAnalysisPhraseModel()
        {
            Details = new HashSet<TwitterAnalysisPhraseDetailModel>();
        }
    }
}
