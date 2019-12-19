using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegAjax.Models
{
    public class QuestionModel
    {
        public long Id { get; set; }
        
        [Display(Name = "Вопрос")]
        public string Name { get; set; }
        
        [Display(Name = "Варианты")]
        public List<VariantModel> Variants { get; set; }
    }
}