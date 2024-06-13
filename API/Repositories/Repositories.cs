using API.Data;
using API.Data.Interface;
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
        public User GetUser(string id);
    }
    public class UserRepository : RepositoryBase<User>, IUserRepositories
    {
        public UserRepository(DB dbContext) : base(dbContext)
        {

        }
        public User GetUser(string id)
        {
            return _dbContext.users.Find(id);
        }
    }
    #endregion

    #region "exam"
    public interface IExamRepositories : IRepository<Exam>
    {
        public List<Exam> Listall();
        public int Count(object id);
        public Exam exam(object serial, object skill);
    }
    public class ExamRepository : RepositoryBase<Exam>, IExamRepositories
    {
        public ExamRepository(DB dbContext) : base(dbContext)
        {
        }
        public Exam exam(object serial, object skill)
        {
            return _dbContext.exams.Where(x => x.Skill.Equals(skill) && x.ExamSerial.Equals(serial)).FirstOrDefault();
        }
        public List<Exam> Listall()
        {
            return _dbContext.exams.OrderBy(x => x.Part).ThenBy(x => x.Skill).ToList();
        }
        public int Count(object id)
        {
            return _dbContext.sentences.Where(x => x.exam.ExamID.Equals(id)).Count();
        }
    }
    #endregion

    #region "question"
    public interface IQuestionRepositories : IRepository<Question>
    {
        public List<Question> list(object id);
        public Sentence sentence(object id);
        public List<Question> listall();
        public Question question(object id, object serial);
    }
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepositories
    {
        public QuestionRepository(DB dbContext) : base(dbContext)
        {
        }
        public Sentence sentence(object id)
        {
            return _dbContext.sentences.Find(id);
        }
        public Question question(object id, object serial)
        {
            return _dbContext.questions.Include(x => x.sentence).Where(x => x.sentence.SentenceId.Equals(id) && x.QuestionSerial.Equals(serial)).FirstOrDefault();
        }
        public List<Question> list(object id)
        {
            return _dbContext.questions.Include(x => x.sentence).Where(x => x.sentence.SentenceId.Equals(id)).OrderBy(x => x.QuestionSerial).ToList();
        }
        public List<Question> listall()
        {
            return _dbContext.questions.Include(x => x.sentence).OrderBy(x => x.sentence.SentenceId).ThenBy(x => x.QuestionSerial).ToList();
        }
    }
    #endregion

    #region "sentencecomplete"
    public interface ISentenceCompleteRepositories : IRepository<SentenceComplete>
    {
        public SentenceComplete getbyuser(object id, object userid);
        public int? CorrectCount(object id, object user);
        public decimal? CorrectPercent(object id, object count);
        public User user(object id);
    }
    public class SentenceCompleteRepository : RepositoryBase<SentenceComplete>, ISentenceCompleteRepositories
    {
        public SentenceCompleteRepository(DB dbContext) : base(dbContext)
        {

        }
        public User user(object id)
        {
            return _dbContext.users.Find(id);
        }
        public SentenceComplete getbyuser(object id, object userid)
        {
            return _dbContext.sentenceCompletes.Where(x => x.SentenceID.Equals((string)id)
            && x.user.UserID.Equals(userid)).FirstOrDefault();
        }
        public int? CorrectCount(object id, object user)
        {
            return _dbContext.questionCompletes.Where(x => x.sen.SentenceId.Equals((string)id)
            && x.user.UserID.Equals((string)user)
            && x.IsCorrect.Equals(true)).Count();
        }
        public decimal? CorrectPercent(object id, object count)
        {
            var a = (decimal)(int)count / _dbContext.questions.Where(x => x.sentence.SentenceId.Equals((string)id)).Count();
            return a;
        }
    }
    #endregion

    #region "sentences"
    public interface ISentenceRepositories : IRepository<Sentence>
    {
        public Exam exam(object id);
        public Sentence sentence(object id, object serial);
        public List<Sentence> List(object id);
        public List<Sentence> Listall();
        public int Count(object id);
        public int? CorrectQuestion(object id, object user);
        public int? TestCorrect(object id, object user);
        public int CountQuestion(object id);
        public decimal CorrectPercent(object id, object user);
        public string? GetSkill(object id);
    }
    public class SentenceRepository : RepositoryBase<Sentence>, ISentenceRepositories
    {
        public SentenceRepository(DB dbContext) : base(dbContext)
        {

        }
        public Exam exam(object id)
        {
            return _dbContext.exams.Find(id);
        }
        public Sentence sentence(object id, object serial)
        {
            return _dbContext.sentences.Include(x => x.exam).Where(x => x.exam.ExamID.Equals(id) && x.SentenceSerial.Equals(serial)).FirstOrDefault();
        }
        public List<Sentence> List(object id)
        {
            return _dbContext.sentences.Include(x => x.exam).Where(x => x.exam.ExamID.Equals(id)).OrderBy(x => x.SentenceSerial).ToList();
        }
        public List<Sentence> Listall()
        {
            return _dbContext.sentences.Include(x => x.exam).OrderBy(x => x.exam.ExamID).ThenBy(x => x.SentenceSerial).ToList();
        }
        public int Count(object id)
        {
            return _dbContext.sentences.Where(x => x.exam.ExamID.Equals(id)).Count();
        }
        public int CountQuestion(object id)
        {
            return _dbContext.questions.Where(x => x.sentence.SentenceId.Equals(id)).Count();
        }
        public int? TestCorrect(object id, object user)
        {
            return _dbContext.sentenceCompletes.Where(x => x.sentence.exam.ExamID.Equals(id) && x.Status.Equals(true) && x.user.UserID.Equals(user)).Count();
        }
        public decimal CorrectPercent(object id, object user)
        {
            int? sentence = _dbContext.sentenceCompletes.Where(x => x.SentenceID.Equals(id)
            && x.user.UserID.Equals(user)).Select(x => x.CorrectQuestion).FirstOrDefault();
            if (sentence == null)
            {
                return (decimal)0.0;
            }
            return (decimal)sentence / CountQuestion(id);
        }
        public int? CorrectQuestion(object id, object user)
        {
            return _dbContext.sentenceCompletes.Where(x => x.SentenceID.Equals(id)
            && x.user.UserID.Equals(user)).Select(x => x.CorrectQuestion).FirstOrDefault();
        }
        public string? GetSkill(object id)
        {
            return _dbContext.sentences.Where(x => x.exam.ExamID.Equals(id)).Select(x => x.exam.Skill).FirstOrDefault();
        }
    }
    #endregion

    #region "questioncomplete"
    public interface IQuestionCompleteRepositories : IRepository<QuestionComplete>
    {
        public QuestionComplete getbyuser(object id, object userid);
        public List<QuestionComplete> list(object id, object userid);
        public User user(object id);
        public Sentence sentence(object id);
    }
    public class QuestionCompleteRepository : RepositoryBase<QuestionComplete>, IQuestionCompleteRepositories
    {
        public QuestionCompleteRepository(DB dbContext) : base(dbContext)
        {

        }
        public User user(object id)
        {
            return _dbContext.users.Find(id);
        }
        public Sentence sentence(object id)
        {
            return _dbContext.sentences.Find(id);
        }
        public QuestionComplete getbyuser(object id, object userid)
        {
            return _dbContext.questionCompletes.Where(x => x.QuestionID.Equals(id) && x.user.UserID.Equals(userid)).FirstOrDefault();
        }
        public List<QuestionComplete> list(object id, object userid)
        {
            return _dbContext.questionCompletes.Include(x => x.sen).Where(x => x.user.UserID.Equals(userid) && x.sen.SentenceId.Equals(id)).OrderBy(x => x.QuestionSerial).ToList();
        }
    }
    #endregion

    #region "vocabulary"
    public interface IVocabularyRepositories : IRepository<Vocabulary>
    {
        public List<Vocabulary> list(object id, object userid);
        public User user(object id);
        public Topic topic(object id);
    }
    public class VocabularyRepository : RepositoryBase<Vocabulary>, IVocabularyRepositories
    {
        public VocabularyRepository(DB dbContext) : base(dbContext)
        {
             
        }
        public List<Vocabulary> list(object id, object userid)
        {
            return _dbContext.vocabularies.Include(x => x.topic).Where(x => x.user.UserID.Equals(userid) && x.topic.TopicId.Equals(id)).ToList();
        }
        public User user(object id)
        {
            return _dbContext.users.Find(id);
        }
        public Topic topic(object id)
        {
            return _dbContext.topic.Find(id);
        }
    }
    #endregion
    #region "Topic"
    public interface ITopicRepositories : IRepository<Topic>
    {
        public List<Topic> list(object userid);
    }
    public class TopicRepository : RepositoryBase<Topic>, ITopicRepositories
    {
        public TopicRepository(DB dbContext) : base(dbContext)
        {

        }
        public List<Topic> list(object userid)
        {
            return _dbContext.topic.Where(x => x.user.UserID.Equals(userid)).ToList();
        }
    }
    #endregion
}


