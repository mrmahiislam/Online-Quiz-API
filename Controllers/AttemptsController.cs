using OnlineQuiz.Application;
using System;
using System.Web.Http;

namespace OnlineQuiz.Controllers
{
    [RoutePrefix("api/attempts")]
    public class AttemptsController : ApiController
    {
        private readonly AttemptService _service = new AttemptService();

        [HttpPost, Route("start")]
        public IHttpActionResult Start([FromBody] StartAttemptModel model)
        {
            var attempt = _service.StartAttempt(model.UserId, model.QuizId);
            return Ok(attempt);
        }

        [HttpPost, Route("submit")]
        public IHttpActionResult Submit([FromBody] SubmitAttemptModel model)
        {
            var result = _service.SubmitAttempt(model.AttemptId, model.Score);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet, Route("user/{userId:guid}")]
        public IHttpActionResult GetByUser(Guid userId)
        {
            var data = _service.GetByUser(userId);
            return Ok(data);
        }
    }

    public class StartAttemptModel
    {
        public Guid UserId { get; set; }
        public Guid QuizId { get; set; }
    }

    public class SubmitAttemptModel
    {
        public Guid AttemptId { get; set; }
        public int Score { get; set; }
    }
}
