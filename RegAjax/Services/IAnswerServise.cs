using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RegAjax.Data.Entities;

namespace RegAjax.Services
{
    public interface IAnswerService
    {
        Task<List<Answer>> GetByVariantIdAsync(long variantId, CancellationToken cancel);
        
        Task<List<Answer>> GetByRegistrationIdAsync(long registrationId, CancellationToken cancel);

        Task<long> Save(Answer answer, CancellationToken cancel);
    }
}