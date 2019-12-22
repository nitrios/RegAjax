using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RegAjax.Data;
using RegAjax.Data.Entities;

namespace RegAjax.Services
{
    class AnswerService : IAnswerService
    {
        private readonly ApplicationDbContext _context;

        public AnswerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Answer>> GetByVariantIdAsync(long variantId, CancellationToken cancel)
        {
            return await _context.Answers
                .Where(a => a.RegistrationId == variantId)
                .OrderBy(a => a.Id)
                .ToListAsync(cancel);
        }
        
        public async Task<List<Answer>> GetByRegistrationIdAsync(long registrationId, CancellationToken cancel)
        {
            return await _context.Answers
                .Where(a => a.RegistrationId == registrationId)
                .OrderBy(a => a.Id)
                .ToListAsync(cancel);
        }

        public async Task<long> Save(Answer answer, CancellationToken cancel)
        {
            await _context.Answers.AddAsync(answer, cancel);
            await _context.SaveChangesAsync(cancel);

            return answer.Id;
        }
    }
}