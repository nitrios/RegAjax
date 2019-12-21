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
        private ApplicationDbContext _context;

        public AnswerService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Answer>> GetAsync(long registrationId, CancellationToken cancel)
        {
            return await _context.Answers
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