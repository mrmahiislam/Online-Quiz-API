using OnlineQuiz.Application;
using System.Web.Http;
using System;

namespace OnlineQuiz.Controllers
{
    [RoutePrefix("api/options")]
    public class OptionsController : ApiController
    {
        private readonly OptionService _service = new OptionService();

        [HttpGet, Route("by-question/{questionId:guid}")]
        public IHttpActionResult GetByQuestion(Guid questionId)
        {
            var data = _service.GetByQuestion(questionId);
            return Ok(data);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create([FromBody] OptionCreateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var opt = _service.Create(model.QuestionId, model.Text, model.IsCorrect);
            return Ok(opt);
        }

        [HttpDelete, Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            var result = _service.Delete(id);
            if (!result) return NotFound();
            return Ok("Option deleted");
        }
    }

    public class OptionCreateModel
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
