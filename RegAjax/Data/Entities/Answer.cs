using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegAjax.Data.Entities
{
    public class Answer
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        public long RegistrationId { get; set; }
            
        public Registration Registration { get; set; }
        
        public long VariantId { get; set; }
        
        public virtual Variant Variant { get; set; }
    }
}