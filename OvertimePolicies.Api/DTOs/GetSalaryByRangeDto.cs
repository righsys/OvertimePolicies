namespace OvertimePolicies.Api.DTOs
{
    public class GetSalaryByRangeDto
    {
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int StartMonth { get; set; }
        public int EndMonth { get; set; }
    }
}
