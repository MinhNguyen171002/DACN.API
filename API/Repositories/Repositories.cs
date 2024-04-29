using API.Data;
using API.DBContext;
using API.Enity;
using API.Model;
using static System.Net.Mime.MediaTypeNames;

namespace API.Repositories
{
    #region "user"
    public interface IUserRepositories : IRepository<User>
    {
        public void insertUser(User user);
        public void updateUser(User user);
        public void deleteUser(User user);

    }
    public class UserRepository : RepositoryBase<User>, IUserRepositories
    {
        public UserRepository(DB dbContext) : base(dbContext)
        {

        }
        public void insertUser(User user)
        {
            _dbContext.users.Add(user);
        }
        public void deleteUser(User user)
        {
            _dbContext.users.Remove(user);
        }

        public void updateUser(User user)
        {
            _dbContext.users.Attach(user);
            _dbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
    #endregion

    #region "exam"
    public interface IExamRepositories : IRepository<Exam>
    {
        public void insertExam(Exam exam);
        public void deleteExam(Exam exam);
        public void updateExam(Exam exam);
        public Exam getbyid(object id);
        public List<Exam> Listall(object id);
        public int Count(object id);
    }
    public class ExamRepository : RepositoryBase<Exam>, IExamRepositories
    {
        public ExamRepository(DB dbContext) : base(dbContext)
        {
        }
        public void insertExam(Exam exam)
        {
            _dbContext.exams.Add(exam);
        }
        public void deleteExam(Exam exam)
        {
            _dbContext.exams.Remove(exam);
        }
        public void updateExam(Exam exam)
        {
            _dbContext.exams.Attach(exam);
            _dbContext.Entry(exam).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public Exam getbyid(object id)
        {
            return _dbContext.exams.Find(id);
        }
        public List<Exam> Listall(object skill)
        {
            return _dbContext.exams.Where(x => x.Skill == (string)skill).ToList();
        }
        public int Count(object id)
        {
            return _dbContext.practices.Where(x => x.Exam.Equals((int)id)).Count();
        }
    }
    #endregion

    #region "question"
    public interface IQuestionRepositories : IRepository<Question>
    {
        public void insertQuestion(Question test);
        public void deleteQuestion(Question test);
        public void updateQuestion(Question test);
        public Question getbyid(object id);
        public List<Question> Listall(object id);
    }
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepositories
    {
        public QuestionRepository(DB dbContext) : base(dbContext)
        {
        }
        public void insertQuestion(Question test)
        {
            _dbContext.questions.Add(test);
        }
        public void deleteQuestion(Question test)
        {
            _dbContext.questions.Remove(test);
        }
        public void updateQuestion(Question test)
        {
            _dbContext.questions.Attach(test);
            _dbContext.Entry(test).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public Question getbyid(object id)
        {
            return _dbContext.questions.Find(id);
        }
        public List<Question> Listall(object id)
        {
            return _dbContext.questions.Where(x => x.Sentences.Equals((int)id)).ToList();
        }
    }
    #endregion

    #region "result"
    public interface IResultRepositories : IRepository<Result>
    {
        public void insertResult(Result result);
        public void deleteResult(Result result);
        public void updateResult(Result result);
        public Result getbyid(object id);
    }
    public class ResultRepository : RepositoryBase<Result>, IResultRepositories
    {
        public ResultRepository(DB dbContext) : base(dbContext)
        {

        }
        public void insertResult(Result result)
        {
            _dbContext.results.Add(result);
        }
        public void deleteResult(Result result)
        {
            _dbContext.results.Remove(result);
        }
        public void updateResult(Result result)
        {
            _dbContext.results.Attach(result);
            _dbContext.Entry(result).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public Result getbyid(object id)
        {
            return _dbContext.results.Find(id);
        }
    }
    #endregion

    #region "practice"

    public interface IPracticeRepositories : IRepository<Practice>
    {
        public void insertPractice(Practice practice);
        public void deletePractice(Practice practice);
        public void updatePractice(Practice practice);
        public Practice getbyid(object id);
        public List<Practice> Listall(object id);
        public int Count(object id);
        public decimal CorrectPercent(object id, object user);
        public string GetId(object user);
    }
    public class PracticeRepository : RepositoryBase<Practice>, IPracticeRepositories
    {
        public PracticeRepository(DB dbContext) : base(dbContext)
        {

        }
        public void insertPractice(Practice practice)
        {
            _dbContext.practices.Add(practice);
        }
        public void deletePractice(Practice practice)
        {
            _dbContext.practices.Remove(practice);
        }
        public void updatePractice(Practice practice)
        {
            _dbContext.practices.Attach(practice);
            _dbContext.Entry(practice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public Practice getbyid(object id)
        {
            return _dbContext.practices.Find(id);
        }
        public List<Practice> Listall(object id)
        {
            return _dbContext.practices.Where(x => x.Exam.Equals((int)id)).ToList();
        }

        public string GetId (object user)
        {
            return _dbContext.users.Where(x => x.UserName.Equals((string)user)).Select(x => x.UserID).FirstOrDefault();
        }

        public int Count(object id)
        {
            var a = _dbContext.sentences.Where(x => x.Practice.Equals((int)id)).Count();
            return a;
        }
        public decimal CorrectPercent(object id, object user)
        {
            Sentence sentence = _dbContext.sentences.Where(x => x.Practice.Equals((int)id)).FirstOrDefault();
            int correct = _dbContext.sentenceCompletes.Where(x => x.Status.Equals(true) & x.User.Equals((string)user)
            & x.SentenceID.Equals(sentence.SentenceId)).Count();
            return correct / 100;
        }
    }
    #endregion

    #region "sentencecomplete"
    public interface ISentenceCompleteRepositories : IRepository<SentenceComplete>
    {
        public void insertSenComplete(SentenceComplete sencom);
        public void deleteSenComplete(SentenceComplete sencom);
        public void updateSenComplete(SentenceComplete sencom);
        public SentenceComplete getbyid(object id);
    }
    public class SentenceCompleteRepository : RepositoryBase<SentenceComplete>, ISentenceCompleteRepositories
    {
        public SentenceCompleteRepository(DB dbContext) : base(dbContext)
        {

        }
        public void insertSenComplete(SentenceComplete sencom)
        {
            _dbContext.sentenceCompletes.Add(sencom);
        }
        public void deleteSenComplete(SentenceComplete sencom)
        {
            _dbContext.sentenceCompletes.Remove(sencom);
        }
        public void updateSenComplete(SentenceComplete sencom)
        {
            _dbContext.sentenceCompletes.Attach(sencom);
            _dbContext.Entry(sencom).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public SentenceComplete getbyid(object id)
        {
            return _dbContext.sentenceCompletes.Find(id);
        }
/*        public List<SentenceComDTO> list(object id)
        {
            return _dbContext.sentenceCompletes.Where(x=>x.).ToList();
        }*/
    }
    #endregion

    #region "sentences"
    public interface ISentenceRepositories : IRepository<Sentence>
    {
        public void insertSentence(Sentence pracom);
        public void deleteSentence(Sentence pracom);
        public void updateSentence(Sentence pracom);
        public Sentence getbyid(object id);
        public List<Sentence> Listall(object id);
    }
    public class SentenceRepository : RepositoryBase<Sentence>, ISentenceRepositories
    {
        public SentenceRepository(DB dbContext) : base(dbContext)
        {

        }
        public void insertSentence(Sentence pracom)
        {
            _dbContext.sentences.Add(pracom);
        }
        public void deleteSentence(Sentence pracom)
        {
            _dbContext.sentences.Remove(pracom);
        }
        public void updateSentence(Sentence pracom)
        {
            _dbContext.sentences.Attach(pracom);
            _dbContext.Entry(pracom).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public Sentence getbyid(object id)
        {
            return _dbContext.sentences.Find(id);
        }
        public List<Sentence> Listall(object id)
        {
            return _dbContext.sentences.Where(x => x.Practice.Equals((int)id)).ToList();
        }
    }
    #endregion

    #region "questioncomplete"
    public interface IQuestionCompleteRepositories : IRepository<QuestionComplete>
    {
        public void insertQuesComplete(QuestionComplete quescom);
        public void deleteQuesComplete(QuestionComplete quescom);
        public void updateQuesComplete(QuestionComplete quescom);
        public QuestionComplete getbyid(object id);
        public List<QuestionComplete> list(object id);
        public string findcorrcet(object id);
        public int CountCorrect(object id);
    }
    public class QuestionCompleteRepository : RepositoryBase<QuestionComplete>, IQuestionCompleteRepositories
    {
        public QuestionCompleteRepository(DB dbContext) : base(dbContext)
        {

        }
        public void insertQuesComplete(QuestionComplete quescom)
        {
            _dbContext.questionCompletes.Add(quescom);
        }
        public void deleteQuesComplete(QuestionComplete quescom)
        {
            _dbContext.questionCompletes.Remove(quescom);
        }
        public void updateQuesComplete(QuestionComplete quescom)
        {
            _dbContext.questionCompletes.Attach(quescom);
            _dbContext.Entry(quescom).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public QuestionComplete getbyid(object id)
        {
            return _dbContext.questionCompletes.Find(id);
        }
        public string findcorrcet (object id)
        {
            return _dbContext.questions.Where(x => x.QuestionID.Equals((int)id))
                .Select(x => x.CorrectDescription).FirstOrDefault();
        }
        public int CountCorrect (object id)
        {
            return _dbContext.questionCompletes.Where(x => x.Sentence.Equals((int)id) && x.IsCorrect.Equals(true)).Count();
        }
        public List<QuestionComplete> list(object id)
        {
            return _dbContext.questionCompletes.Where(x => x.Sentence.Equals((int)id)).ToList();
        }
    }
    #endregion
}
