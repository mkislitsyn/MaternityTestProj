using Maternity.Core;
using Maternity.Repository.DbContexts;
using Maternity.Repository.Entity;
using Maternity.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace Maternity.Repository
{
    internal class PatientRepository : IPatientRepository
    {
        internal readonly MaternityContext _context;

        private readonly ILogger<PatientRepository> _logger;

        public PatientRepository(MaternityContext db, ILogger<PatientRepository> logger)
        {
            _context = db;
            _logger = logger;
        }

        public async Task<string> AddPatient(PatientDto patient)
        {
            try
            {

                var entity = new Patient();
                var name = new Name();
                entity.InjectFrom(patient);
                name.InjectFrom(patient.Name);
                entity.Name = name;
                entity.Name.Given = string.Join(",", patient.Name.Given);
                _context.Patients.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Add Patient Error");

                return $"Errors while addind new patient {ex}";
            }
            return $"patient  {patient.Name.Family} was added successfully";
        }

        public async Task<string> DeletePatient(long id)
        {
            try
            {

                var patient = await _context.Patients.Include(p => p.Name).FirstOrDefaultAsync(p => p.Id == id);
                if (patient == null)
                {
                    return $"patient dosen't Exist with current id {id}";
                }
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return $"patient id:{id} was deleted successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete Patient error");

                return $"Errors while deleting {ex}";
            }
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatients(DateTime? birthDate, string birthDateOperator)
        {
            try
            {

                IQueryable<Patient> query = _context.Patients;

                if (birthDate.HasValue && !string.IsNullOrEmpty(birthDateOperator))
                {
                    switch (birthDateOperator.ToLower())
                    {    
                        case "eq": // equals (равно)
                            query = query.Where(p => p.BirthDate == birthDate.Value);
                            break;
                        case "ne": // not equals (не равно)
                            query = query.Where(p => p.BirthDate != birthDate.Value);
                            break;
                        case "lt": // less than (меньше)
                            query = query.Where(p => p.BirthDate < birthDate.Value);
                            break;
                        case "gt": // greater than (больше)
                            query = query.Where(p => p.BirthDate > birthDate.Value);
                            break;
                        case "ge": // greater than or equal (больше или равно)
                            query = query.Where(p => p.BirthDate >= birthDate.Value);
                            break;
                        case "le": // less than or equal (меньше или равно)
                            query = query.Where(p => p.BirthDate <= birthDate.Value);
                            break;
                        case "sa": // starts after (после)
                            query = query.Where(p => p.BirthDate > birthDate.Value);
                            break;
                        case "eb": // ends before (до)
                            query = query.Where(p => p.BirthDate < birthDate.Value);
                            break;
                        case "ap": // approximately (приблизительно)
                            var lowerBound = birthDate.Value.AddYears(-1);  // Дата ± 1 year
                            var upperBound = birthDate.Value.AddYears(1);
                            query = query.Where(p => p.BirthDate >= lowerBound && p.BirthDate <= upperBound);
                            break;
                        default:
                            break;
                    }
                }

                var patients = await query.Include(n => n.Name).ToListAsync();
                var items = new List<PatientDto>();

                foreach (var item in patients)
                {
                    var value = new PatientDto();
                    var name = new NameDto();

                    value.InjectFrom(item);
                    name.InjectFrom(item.Name);

                    value.Name = name;
                    value.Name.Given = item.Name.Given.Split(',').ToList();
                    items.Add(value);
                }

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get All Patients Error");

                return new List<PatientDto>();
            }
        }

        public async Task<PatientDto> GetPatient(long id)
        {
            try
            {
                var result = new PatientDto();
                var name = new NameDto();

                var patient = await _context.Patients.Include(n => n.Name).FirstOrDefaultAsync(x => x.Id == id);
                if (patient != null)
                {
                    result.InjectFrom(patient);
                    name.InjectFrom(patient.Name);

                    result.Name = name;
                    result.Name.Given = patient.Name.Given.Split(',').ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get All Patient  Error");

                return new PatientDto();
            }
        }

        public async Task<string> UpdatePatient(PatientDto updatedPatient)
        {
            try
            {
                var patient = await _context.Patients.Include(p => p.Name).FirstOrDefaultAsync(p => p.Id == updatedPatient.Id);

                if (patient == null)
                {
                    return "patient dosen't Exist";
                }

                patient.BirthDate = updatedPatient.BirthDate;
                patient.Gender = updatedPatient.Gender;
                patient.Active = updatedPatient.Active;

                if (patient.Name != null)
                {
                    patient.Name.Family = updatedPatient.Name.Family;
                    patient.Name.Given = string.Join(",", updatedPatient.Name.Given);
                    patient.Name.Use = updatedPatient.Name.Use;
                }

                _context.Entry(patient).State = EntityState.Modified;

                if (patient.Name != null)
                {
                    _context.Entry(patient.Name).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Update Patient  Error");

                return $"Errors while updating patient {updatedPatient.Name.Family}: {ex}";
            }

            return $"patient {updatedPatient.Id} {updatedPatient.Name.Family} was updated successfully";
        }
    }
}
