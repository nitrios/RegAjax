using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RegAjax.Data.Entities;

namespace RegAjax.Services
{
    public interface IRegistrationService
    {
        Task<List<Question>> GetAsync(CancellationToken cancel);
    }
}