using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RegAjax.Data.Entities;

namespace RegAjax.Services
{
    public interface IQuestionService
    {
        Task<List<Question>> GetAsync(CancellationToken cancel);
    }
}