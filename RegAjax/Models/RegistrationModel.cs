using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegAjax.Models
{
    public class RegistrationModel
    {
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        
        [Display(Name = "Вопросы")]
        public List<QuestionModel> Questions { get; set; }
    }
}