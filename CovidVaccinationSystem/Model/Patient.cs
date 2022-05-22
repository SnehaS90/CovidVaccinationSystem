namespace CovidVaccinationSystem.Model
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DOB { get; set; }
        public string? Gender { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string? Nationality { get; set; }
        public string? PassportNo { get; set; }
        public string? EmiratesId { get; set; }

        public ICollection<VaccinationRecord>? VaccinationRecords { get; set; }
    }
}
