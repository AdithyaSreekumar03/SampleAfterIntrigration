using HealthAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppMVC.Repository.Interface
{
    public interface IHealthRecordRepository
    {
        List<HealthRecord> GetByPatientId(
            int patientId);

        HealthRecord GetById(
            int recordId);

        void Add(
            HealthRecord record);
    }
}
