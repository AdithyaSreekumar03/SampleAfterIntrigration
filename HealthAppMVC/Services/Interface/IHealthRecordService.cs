using HealthAppMVC.Models;
using System.Collections.Generic;

namespace HealthAppMVC.Services.Interface
{
    public interface IHealthRecordService
    {
        List<HealthRecord> GetPatientHistory(
            int patientId);

        HealthRecord GetRecordById(
            int recordId);

        HealthRecord AddHealthRecord(
     HealthRecord record);
    }
}