using HealthAppWebAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebAPI.Services.Interfaces
{

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPatientService
    {
        Task<List<PatientDto>> GetAllPatientsAsync();

        Task<PatientDto> GetPatientByIdAsync(int id);

        Task RegisterPatientAsync(CreatePatientDto dto);

        Task UpdatePatientAsync(int id, CreatePatientDto dto);
    }

}
