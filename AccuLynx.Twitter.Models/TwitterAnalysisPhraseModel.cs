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

        [NotMapped]
        public int TotalTweets
        {
            get
            {
                switch (Details)
                {
                    case null:
                        return 0;
                    default:
                        return Details.Count;
                }
            }
        }

        [NotMapped]
        public int TotalRetweetCount
        {
            get
            {
                switch (Details)
                {
                    case null:
                        return 0;
                    default:
                        int total = 0;
                        foreach (var detail in Details)
                        {
                            total += detail.RetweetCount;
                        };
                        return total;
                }
            }
        }

        [NotMapped]
        public int TotalFavoriteCount
        {
            get
            {
                switch (Details)
                {
                    case null:
                        return 0;
                    default:
                        int total = 0;
                        foreach (var detail in Details)
                        {
                            total += detail.FavoriteCount;
                        };
                        return total;
                }
            }
        }

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
