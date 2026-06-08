using HealthAppWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebAPI.Services.Interfaces
{

    public interface IPatientService
    {
        Task<PatientDto> AddPatient(PatientDto entity);

        Task<List<PatientDto>> GetAllPatients();

        Task<PatientDto> GetById(int patientId);

        Task<PatientDto> UpdatePatient(int id, PatientDto entity);

        Task<bool> DeletePatient(int id);

        Task<bool> EmailExists(string email);

        Task<int> GetAppointmentCount(int patientId);
    }

}
