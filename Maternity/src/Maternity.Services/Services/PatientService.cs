using Maternity.Core;
using Maternity.Repository.Interfaces;
using Maternity.Services.Interfaces;

namespace Maternity.Services.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public async Task<string> AddPatient(PatientDto patient)
        {
            return await patientRepository.AddPatient(patient);
        }

        public async Task<string> DeletePatient(long id)
        {
            return await patientRepository.DeletePatient(id);
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatients(DateTime? birthDate, string birthDateOperator)
        {
            return await patientRepository.GetAllPatients(birthDate, birthDateOperator);
        }

        public async Task<PatientDto> GetPatient(long id)
        {
            return await patientRepository.GetPatient(id);
        }

        public async Task<string> UpdatePatient(PatientDto patient)
        {
            return await patientRepository.UpdatePatient(patient);
        }
    }
}
