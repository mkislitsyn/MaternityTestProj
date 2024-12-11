using Maternity.Core;

namespace Maternity.Services.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatients(DateTime? birthDate, string birthDateOperator);

        Task<PatientDto> GetPatient(long id);

        Task<string> AddPatient(PatientDto patient);

        Task<string> UpdatePatient(PatientDto patient);

        Task<string> DeletePatient(long id);
    }
}
