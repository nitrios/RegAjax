using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RegAjax.Data;
using RegAjax.Data.Entities;

namespace RegAjax.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _context;

        public QuestionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Question> GetAsync(long id, CancellationToken cancel)
        {
            return await _context.Questions
                .Include(q => q.Variants)
                .FirstOrDefaultAsync(q => q.Id == id, cancel);
        }

        public async Task<List<Question>> GetAsync(CancellationToken cancel)
        {
            return await _context.Questions
                .Include(q => q.Variants)
                .OrderBy(q => q.Id)
                .Take(100)
                .ToListAsync(cancel);
        }
    }
}