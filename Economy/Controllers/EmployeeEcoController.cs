using Economy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Economy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeEcoController : ControllerBase
    {
        /*
        public int CalculateSickDaysTotal()
        {
            return SickLeaves.Sum(s => (s.EndDate.HasValue ? (s.EndDate.Value - s.StartDate).Days + 1 : (DateTime.Now - s.StartDate).Days + 1));
        }
        */
    }
}
