using CovidVaccinationSystem.Model;
using CovidVaccinationSystem.UnitConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace CovidVaccinationSystem.Controllers
{   
    [ApiController]
    [Route("api/vaccinationrecord")]  // attribute based routing
    public class VaccinationRecordController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public VaccinationRecordController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region get all resource from server
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var records = await _unitOfWork.VaccinationRecord.All();
            return Ok(records);
        }
        #endregion

        #region get specific resource from server
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVaccinationRecord(int id)
        {
            var record = await _unitOfWork.VaccinationRecord.GetById(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }
        #endregion

        #region add new resource to server
        [HttpPost]
        public async Task<IActionResult> CreateVaccinationRecord(VaccinationRecord vaccinationRecord)
        {

            await _unitOfWork.VaccinationRecord.Add(vaccinationRecord);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("CreateVaccinationRecord", new { vaccinationRecord.VaccinationId }, vaccinationRecord);
        }
        #endregion

        #region update existing resource on server
        [HttpPut]
        public async Task<IActionResult> UpdateVaccinationRecord(VaccinationRecord vaccinationRecord)
        {

            await _unitOfWork.VaccinationRecord.Update(vaccinationRecord);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("UpdateVaccinationRecord", new { vaccinationRecord.VaccinationId }, vaccinationRecord);

        }
        #endregion

        #region delete existing resource from server
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccinationRecord(int id)
        {
            var record = await _unitOfWork.VaccinationRecord.GetById(id);

            if (record == null)
                return BadRequest();

            await _unitOfWork.VaccinationRecord.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(record);
        }
        #endregion
    }
}
