namespace CovidVaccinationSystem.Model
{
    public class VaccinationRecord
    {
        public int VaccinationId { get; set; }        
        public string? VaccinationName { get; set; }
        public string? VaccinationCentre { get; set; }
        public string? Dose { get; set; }
        public DateTime VaccinationDate { get; set; }

        public int PatientId { get; set; }
        public Patient? PatientDtl { get; set; }
    }
}
