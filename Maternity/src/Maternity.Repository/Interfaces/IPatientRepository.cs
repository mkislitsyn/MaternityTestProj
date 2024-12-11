using Maternity.Core;

namespace Maternity.Repository.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<PatientDto>> GetAllPatients(DateTime? birthDate, string birthDateOperator);

        Task<PatientDto> GetPatient(long id);

        Task<string> AddPatient(PatientDto patient);

        Task<string> UpdatePatient(PatientDto patient);

        Task<string> DeletePatient(long id);
    }
}
