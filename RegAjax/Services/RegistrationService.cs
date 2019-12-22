using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RegAjax.Data;
using RegAjax.Data.Entities;

namespace RegAjax.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ApplicationDbContext _context;

        public RegistrationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Registration>> GetAsync(CancellationToken cancel)
        {
            return await _context.Registrations
                .OrderBy(q => q.Id)
                .Take(100)
                .ToListAsync(cancel);
        }

        public async Task<List<Registration>> GetAsync(long variantId, CancellationToken cancel)
        {
            return await _context.Registrations
                .Include(r => r.Answers)
                .Where(q => q.Answers.Select(a => a.VariantId == variantId).Any())
                .OrderBy(q => q.Id)
                .Take(100)
                .ToListAsync(cancel);
        }

        public async Task<long> Save(Registration registration, CancellationToken cancel)
        {
            await _context.Registrations.AddAsync(registration, cancel);
            await _context.SaveChangesAsync(cancel);

            return registration.Id;
        }
    }
}