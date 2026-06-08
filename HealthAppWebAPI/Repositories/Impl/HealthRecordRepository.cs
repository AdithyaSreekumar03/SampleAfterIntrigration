using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAppWebAPI.Repositories.Impl
{
    using HealthAppWebAPI.Repositories.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly AppDbContext _context;

        public HealthRecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HealthRecord> AddAsync(HealthRecord record)
        {
            _context.HealthRecords.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<List<HealthRecord>> GetAllAsync()
        {
            return await _context.HealthRecords.ToListAsync();
        }

        public async Task<HealthRecord> GetByIdAsync(int id)
        {
            return await _context.HealthRecords
                .FirstOrDefaultAsync(r => r.RecordId == id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _context.HealthRecords.FindAsync(id);

            if (record == null)
                return false;

            _context.HealthRecords.Remove(record);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task UpdateAsync(HealthRecord record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            _context.Entry(record).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}