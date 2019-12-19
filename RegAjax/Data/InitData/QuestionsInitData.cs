using System.Collections.Generic;
using RegAjax.Data.Entities;

namespace RegAjax.Data.InitData
{
    public static class QuestionsInitData
    {
        public static IEnumerable<Question> Get()
        {
            return new List<Question>()
            {
                new Question()
                {
                    Id = 1,
                    Name = "Какой цвет вам больше нравится?"
                },
                
                new Question()
                {
                    Id = 2,
                    Name = "Какой напиток вы предпочитаете"
                }
            };
        }
    }
}