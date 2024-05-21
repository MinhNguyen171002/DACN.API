using API.Data;
using API.DBContext;
using API.Enity;
using API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;

namespace API.Repositories
{
    #region "user"
    public interface IUserRepositories : IRepository<User>
    {

    }
    public class UserRepository : RepositoryBase<User>, IUserRepositories
    {
        public UserRepository(DB dbContext) : base(dbContext)
        {

        }
    }
    #endregion

    #region "exam"
    public interface IExamRepositories : IRepository<Exam>
    {
        public List<Exam> Listall(object skill);
        public int Count(object id);
    }
    public class ExamRepository : RepositoryBase<Exam>, IExamRepositories
    {
        public ExamRepository(DB dbContext) : base(dbContext)
        {
        }
        public List<Exam> Listall(object skill)
        {
            return _dbContext.exams.Where(x => x.Skill == (string)skill).ToList();
        }
        public int Count(object id)
        {
            return _dbContext.sentences.Where(x => x.Exam.Equals((int)id)).Count();
        }
    }
    #endregion

    #region "question"
    public interface IQuestionRepositories : IRepository<Question>
    {
        public Question getbyid(object id);
        public List<Question> listall(object id);
    }
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepositories
    {
        public QuestionRepository(DB dbContext) : base(dbContext)
        {
        }
        public Question getbyid(object id)
        {
            return _dbContext.questions.Find(id);
        }
        public List<Question> listall(object id)
        {
            return _dbContext.questions.Where(x => x.Sentences.Equals(id)).ToList();
        }
    }
    #endregion

    #region "sentencecomplete"
    public interface ISentenceCompleteRepositories : IRepository<SentenceComplete>
    {
        public SentenceComplete getbyuser(object id,object user);
        public int? CorrectCount(object id, object user);
        public decimal? CorrectPercent(object id, object count);
    }
    public class SentenceCompleteRepository : RepositoryBase<SentenceComplete>, ISentenceCompleteRepositories
    {
        public SentenceCompleteRepository(DB dbContext) : base(dbContext)
        {

        }
        public SentenceComplete getbyuser(object id,object user)
        {
            return _dbContext.sentenceCompletes.Where(x => x.SentenceID.Equals((string)id)
            && x.User.Equals((string)user)).FirstOrDefault();
        }
        public int? CorrectCount(object id, object user)
        {
            return _dbContext.questionCompletes.Where(x=>x.Sentence.Equals((string)id)
            &&x.User.Equals((string)user)
            &&x.IsCorrect.Equals(true)).Count();                
        }
        public decimal? CorrectPercent(object id,object count)
        {
            var a = (decimal)(int)count / _dbContext.questions.Where(x => x.Sentences.Equals((string)id)).Count();
            return a;
        }
    }
    #endregion

    #region "sentences"
    public interface ISentenceRepositories : IRepository<Sentence>
    {
        public Sentence getbyid(object id);
        public List<Sentence> Listall(object id);
        public int Count(object id);
        public int? CorrectQuestion(object id, object user);
        public int? TestCorrect(object id, object user);
        public int CountQuestion(object id);
        public decimal CorrectPercent(object id, object user);
    }
    public class SentenceRepository : RepositoryBase<Sentence>, ISentenceRepositories
    {
        public SentenceRepository(DB dbContext) : base(dbContext)
        {

        }
        public Sentence getbyid(object id)
        {
            return _dbContext.sentences.Find(id);
        }
        public List<Sentence> Listall(object id)
        {
            return _dbContext.sentences.Where(x => x.Exam.Equals((int)id)).OrderBy(x=>x.SentenceSerial).ToList();
        }
        public string getUserId(object user)
        {
            return _dbContext.users.Where(x => x.UserName.Equals((string)user)).Select(x => x.UserID).FirstOrDefault();
        }
        public int Count(object id)
        {
            return _dbContext.sentences.Where(x => x.Exam.Equals((int)id)).Count();
        }
        public int CountQuestion(object id)
        {
            return _dbContext.questions.Where(x => x.Sentences.Equals(id)).Count();
        }
        public int? TestCorrect(object id, object user)
        {
            string userId = getUserId((string)user);
            return _dbContext.sentenceCompletes.Where(x => x.SentenceID
            .Equals(_dbContext.sentences.Where(x => x.Exam.Equals((int)id)).Select(x => x.SentenceId).FirstOrDefault())
            && x.Status.Equals(true) && x.User.Equals(userId)).Count();
        }
        public decimal CorrectPercent(object id, object user) {
            string userId = getUserId((string)user);
            int? sentence = _dbContext.sentenceCompletes.Where(x => x.SentenceID.Equals(id)
            && x.User.Equals(userId)).Select(x => x.CorrectQuestion).FirstOrDefault();
            if (sentence == null)
            {
                return (decimal)0.0;
            }
            return (decimal)sentence / CountQuestion(id); 
        }
        public int? CorrectQuestion(object id, object user)
        {
            string userId = getUserId((string)user);
            return _dbContext.sentenceCompletes.Where(x => x.SentenceID.Equals(id)
            && x.User.Equals(userId)).Select(x => x.CorrectQuestion).FirstOrDefault();
        }
    }
    #endregion

    #region "questioncomplete"
    public interface IQuestionCompleteRepositories : IRepository<QuestionComplete>
    {
        public QuestionComplete getbyuser(object id,object user);
        public List<QuestionComplete> list(object id, object user);
        //public string getdescription(object id);
        public string getcorrectanswer(object id);
        public int CountCorrect(object id);
    }
    public class QuestionCompleteRepository : RepositoryBase<QuestionComplete>, IQuestionCompleteRepositories
    {
        public QuestionCompleteRepository(DB dbContext) : base(dbContext)
        {

        }
        public string getUserId(object user)
        {
            return _dbContext.users.Where(x => x.UserName.Equals((string)user)).Select(x => x.UserID).FirstOrDefault();
        }
        public QuestionComplete getbyuser(object id, object user)
        {
            return _dbContext.questionCompletes.Where(x=>x.QuestionID.Equals(id)&&x.User.Equals(user)).FirstOrDefault();
        }
/*        public string getdescription (object id)
        {
            return _dbContext.questions.Where(x => x.QuestionID.Equals(id))
                .Select(x => x.CorrectDescription).FirstOrDefault();
        }*/
        public string getcorrectanswer(object id)
        {
            return _dbContext.questions.Where(x => x.QuestionID.Equals(id))
                .Select(x => x.CorrectAnswer).FirstOrDefault();
        }
        public int CountCorrect (object id)
        {
            return _dbContext.questionCompletes.Where(x => x.Sentence.Equals(id) && x.IsCorrect.Equals(true)).Count();
        }
        public List<QuestionComplete> list(object id,object user)
        {
            string userID = getUserId(user);
            return _dbContext.questionCompletes.Where(x => x.Sentence.Equals(id)&&x.User.Equals(userID)).ToList();
        }
    }
    #endregion

    #region"file"
    public interface IFileRepositories : IRepository<QuestionFile>
    {
        public QuestionFile getbyname(object name);
        public void deletes(object id);
    }
    public class FileRepository : RepositoryBase<QuestionFile>, IFileRepositories
    {
        public FileRepository(DB dbContext) : base(dbContext)
        {

        }
        public void deletes(object id)
        {
            var files = _dbContext.files.Where(x=>x.Question.Equals((string)id)).ToList();
            _dbContext.files.RemoveRange(files);
        }
        public QuestionFile getbyname(object name)
        {
            return _dbContext.files.Where(x=>x.FileName.Equals((string)name)).FirstOrDefault();
        }
    }
    #endregion
}
