using OnlineQuiz.Application;
using System.Web.Http;

namespace OnlineQuiz.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminReportsController : ApiController
    {
        private readonly AdminReportService _service = new AdminReportService();

        [HttpGet, Route("attempts")]
        public IHttpActionResult GetAllAttempts()
        {
            var data = _service.GetAllAttempts();
            return Ok(data);
        }

        [HttpGet, Route("analytics")]
        public IHttpActionResult GetQuizAnalytics()
        {
            var data = _service.GetQuizAnalytics();
            return Ok(data);
        }
    }
}
