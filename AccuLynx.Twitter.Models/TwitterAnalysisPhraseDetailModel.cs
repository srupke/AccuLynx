using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("Phrase")]
        public int PhraseId { get; set; }
        public TwitterAnalysisPhraseModel Phrase { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
