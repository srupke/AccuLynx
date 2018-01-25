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
        public string WordOrPhrase1 { get; set; }

        [Required]
        public string WordOrPhrase2 { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
