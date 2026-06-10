using HealthAppMVC.Models;
using HealthAppWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppMVC.Services.Interface
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto> GetPatientByIdAsync(int id);
        Task RegisterPatientAsync(CreatePatientDto dto);
        Task UpdatePatientAsync(int id, CreatePatientDto dto);

        Task<IEnumerable<PatientDto>> SearchByNameAsync(string name);
        Task<int> GetAppointmentCountAsync(int patientId);
    }

}
