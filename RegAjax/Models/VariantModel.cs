using System.ComponentModel.DataAnnotations;

namespace RegAjax.Models
{
    public class VariantModel
    {
        public long Id { get; set; }
        
        [Display(Name = "Вариант")]
        public string Name { get; set; }
    }
}