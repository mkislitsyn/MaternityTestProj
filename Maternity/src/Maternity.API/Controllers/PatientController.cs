using Maternity.Core;
using Maternity.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Maternity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: api/Patient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients([FromQuery] string? birthDate)
        {
            //var validOperators = new HashSet<string> { "eq", "ge", "le", "gt", "lt", "ne", "sa", "eb", "ap" };
            //if (birthDate.HasValue && !string.IsNullOrEmpty(birthDateOperator))
            //{
            //    if (!validOperators.Contains(birthDateOperator.ToLower()))
            //    {
            //        return BadRequest("Invalid birthDate operator. Use eq,ne,lt,gt,ge,le,sa,eb or ap.");
            //    }
            //}


            var birthDateOperator = string.Empty;
            var birthDateValue = string.Empty;
            DateTime? birthDateTime = null;
            if (!string.IsNullOrEmpty(birthDate))
            {

                birthDateOperator = birthDate.Substring(0, 2);
                birthDateValue = birthDate.Substring(2);
                if (!DateTime.TryParse(birthDateValue, out DateTime parsedDate))
                {
                    return BadRequest("Invalid date format.");
                }
                birthDateTime = parsedDate;

                var validOperators = new HashSet<string> { "eq", "ge", "le", "gt", "lt", "ne", "sa", "eb", "ap" };

                if (!string.IsNullOrEmpty(birthDateOperator) && !validOperators.Contains(birthDateOperator.ToLower()))
                {
                    return BadRequest("Invalid birthDate operator. Use eq,ne,lt,gt,ge,le,sa,eb or ap.");
                }
            }



            var result = await _patientService.GetAllPatients(birthDateTime, birthDateOperator);
            if (result.Count() > 0)
            {
                return result.ToList();
            }
            return NotFound();
        }

        //[HttpGet("search")]
        //public async Task<ActionResult<IEnumerable<PatientDto>>> SearchPatientsByBirthDate([FromQuery] string birthDate)
        //{
        //    if (string.IsNullOrEmpty(birthDate))
        //    {
        //        return BadRequest("Date parameter is required.");
        //    }

        //    var birthDateOperator = birthDate.Substring(0, 2);
        //    var birthDateValue = birthDate.Substring(2);

        //    if (!DateTime.TryParse(birthDateValue, out DateTime parsedDate))
        //    {
        //        return BadRequest("Invalid date format.");
        //    }

        //    var validOperators = new HashSet<string> { "eq", "ge", "le", "gt", "lt", "ne", "sa", "eb", "ap" };

        //    if (!string.IsNullOrEmpty(birthDateOperator) && !validOperators.Contains(birthDateOperator.ToLower()))
        //    {
        //        return BadRequest("Invalid birthDate operator. Use eq,ne,lt,gt,ge,le,sa,eb or ap.");
        //    }

        //    var result = await _patientService.GetAllPatients(parsedDate, birthDateOperator);
        //    if (result.Count() > 0)
        //    {
        //        return result.ToList();
        //    }
        //    return NotFound();
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetPatient(long id)
        {
            var result = await _patientService.GetPatient(id);

            if (result.Id > 0)
            {
                return result;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PatientDto>> PostPatient(PatientDto patient)
        {
            var result = await _patientService.AddPatient(patient);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, PatientDto patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            patient.Id = id;
            var result = await _patientService.UpdatePatient(patient);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(long id)
        {
            var result = await _patientService.DeletePatient(id);

            return Ok(result);
        }
    }
}