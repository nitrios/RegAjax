using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegAjax.Models
{
    public class RegistrationModel
    {
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        [Phone]
        [Required]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date, ErrorMessage="Date only")]
        public DateTime BirthDate { get; set; }
        
        [Display(Name = "Вопросы")]
        public List<QuestionModel> Questions { get; set; }
    }
}