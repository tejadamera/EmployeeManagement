namespace EmployeeManagement.DTOs
{
    public class EmployeeQueryParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search {  get; set; }
        public string? Department { get; set; }
    }
}
