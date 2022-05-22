using CovidVaccinationSystem.Model;
using CovidVaccinationSystem.UnitConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CovidVaccinationSystem.Controllers
{
    [ApiController]
    [Route("api/patient")] // attribute based routing
    public class PatientController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //get resource all from server
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var patients = await _unitOfWork.Patient.All();
            return Ok(patients);
        }

        //get specific resource from server
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await _unitOfWork.Patient.GetById(id);

            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        //add new resource to server
        [HttpPost]
        public async Task<IActionResult> CreatePatient(Patient patient)
        {            

            await _unitOfWork.Patient.Add(patient);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("CreatePatient", new { patient.PatientId }, patient);            
        }

        //update existing resource on server
        [HttpPut]
        public async Task<IActionResult> UpdatePatient(Patient patient)
        {

            await _unitOfWork.Patient.Update(patient);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("UpdatePatient", new { patient.PatientId }, patient);
            
        }

        //delete existing resource from server
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _unitOfWork.Patient.GetById(id);

            if (patient == null)
                return BadRequest();

            await _unitOfWork.Patient.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(patient);
        }
    }
}

