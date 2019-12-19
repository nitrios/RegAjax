using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegAjax.Data.Entities
{
    public class Variant
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [Required] public string Name { get; set; }
        
        [Required] public long QuestionId { get; set; }
        
        public virtual Question Question { get; set; }
    }
}