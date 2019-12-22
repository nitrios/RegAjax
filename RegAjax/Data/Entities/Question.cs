using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegAjax.Data.Entities
{
    public class Question
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [Required] public string Name { get; set; }
        
        [ForeignKey("QuestionId")]
        public virtual ICollection<Variant> Variants { get; set; }
    }
}