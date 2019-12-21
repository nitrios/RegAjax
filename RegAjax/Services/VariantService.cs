using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RegAjax.Data;
using RegAjax.Data.Entities;

namespace RegAjax.Services
{
    public class VariantService : IVariantService
    {
        private ApplicationDbContext _context;

        public VariantService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Variant> Get(long id, CancellationToken cancel)
        {
            return await _context.Variants
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken: cancel);
        }
    }
}