using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RegAjax.Data.Entities;

namespace RegAjax.Services
{
    public interface IQuestionService
    {
        Task<Question> GetAsync(long id, CancellationToken cancel);
        
        Task<List<Question>> GetAsync(CancellationToken cancel);
    }
}