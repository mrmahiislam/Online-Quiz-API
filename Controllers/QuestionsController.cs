using System;
using System.Collections.Generic;
using System.Linq;
using OnlineQuiz.Domain;
using OnlineQuiz.Infrastructure;

namespace OnlineQuiz.Application
{
    public class QuestionService
    {
        private readonly QuizDbContext _context;

        public QuestionService()
        {
            _context = new QuizDbContext();
        }

        public IEnumerable<Question> GetAll()
        {
            return _context.Questions
                           .OrderBy(q => q.Id)
                           .ToList();
        }

        public class QuestionCreateModel
        {
            public string Text { get; set; }
            public Guid QuizId { get; set; }
        }

        public bool Delete(Guid id)
        {
            var question = _context.Questions.Find(id);
            if (question == null)
                return false;

            _context.Questions.Remove(question);
            _context.SaveChanges();
            return true;
        }
    }
}
