using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RegAjax.Data.Entities;

namespace RegAjax.Services
{
    public interface IAnswerService
    {
        Task<List<Answer>> GetAsync(long registrationId, CancellationToken cancel);

        Task<long> Save(Answer answer, CancellationToken cancel);
    }
}