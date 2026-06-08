using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebAPI.Repositories.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHealthRecordRepository
    {
        Task<HealthRecord> AddAsync(HealthRecord record);

        Task<List<HealthRecord>> GetAllAsync();

        Task<HealthRecord> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task UpdateAsync(HealthRecord record);
    }
}
