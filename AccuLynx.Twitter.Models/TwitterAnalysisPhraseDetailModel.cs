using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace AccuLynx.Twitter.Models
{
    [Table("TwitterAnalysisPhraseDetail", Schema = "dbo")]
    public class TwitterAnalysisPhraseDetailModel
    {
        [Key]
        public int AnalysisPhraseDetailId { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string ScreenName { get; set; }

        [Required]
        public DateTime TweetCreatedAt { get; set; }

        [Required]
        public string TweetText { get; set; }

        [Required]
        public bool TweetTextTruncated { get; set; }

        [Required]
        public int RetweetCount { get; set; }

        [Required]
        public int FavoriteCount { get; set; }

        [ForeignKey("Phrase")]
        public int PhraseId { get; set; }

        [ScriptIgnore]
        public TwitterAnalysisPhraseModel Phrase { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
