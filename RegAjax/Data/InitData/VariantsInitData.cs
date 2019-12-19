using System.Collections.Generic;
using RegAjax.Data.Entities;

namespace RegAjax.Data.InitData
{
    public static class VariantsInitData
    {
        public static IEnumerable<Variant> Get()
        {
            return new List<Variant>()
            {
                new Variant()
                {
                    Id = 1,
                    Name = "Синий",
                    QuestionId = 1
                },

                new Variant()
                {
                    Id = 2,
                    Name = "Желтый",
                    QuestionId = 1
                },

                new Variant()
                {
                    Id = 3,
                    Name = "Красный",
                    QuestionId = 1
                },
                
                new Variant()
                {
                    Id = 4,
                    Name = "Чай",
                    QuestionId = 2
                },
                
                new Variant()
                {
                    Id = 5,
                    Name = "Кофе",
                    QuestionId = 2
                },
                
                new Variant()
                {
                    Id = 6,
                    Name = "Сок",
                    QuestionId = 2
                },
                
                new Variant()
                {
                    Id = 7,
                    Name = "Вода",
                    QuestionId = 2
                }
            };
        }
    }
}