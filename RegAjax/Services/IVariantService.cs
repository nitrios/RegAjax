using System.Threading;
using System.Threading.Tasks;
using RegAjax.Data.Entities;

namespace RegAjax.Services
{
    public interface IVariantService
    {
        Task<Variant> Get(long id, CancellationToken cancel);
    }
}