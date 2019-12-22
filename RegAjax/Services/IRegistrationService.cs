using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RegAjax.Data.Entities;

namespace RegAjax.Services
{
    public interface IRegistrationService
    {
        Task<List<Registration>> GetAsync(CancellationToken cancel);
        
        Task<List<Registration>> GetAsync(long variantId, CancellationToken cancel);

        Task<long> Save(Registration registration, CancellationToken cancel);
    }
}