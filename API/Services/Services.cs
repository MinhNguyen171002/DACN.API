using API.DBContext;
using API.Enity;
using API.Model;
using API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.DirectoryServices.Protocols;
using System.Xml.Linq;

namespace API.Services
{
    #region "user"
    public class UserService
    {
        private DB dBContext;
        private IUserRepositories userRepositories;
        public UserService(DB dBContext)
        {

            this.dBContext = dBContext;
            this.userRepositories = new UserRepository(dBContext);
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public void insert(User user)
        {

            userRepositories.insertUser(user);
            Save();
        }
        public void update(User user)
        {
            userRepositories.updateUser(user);
            Save();
        }
        public void delete(User user)
        {
            userRepositories.deleteUser(user);
            Save();
        }
    }
    #endregion  

    #region "exam"
    public class ExamService
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IExamRepositories examRepositories;
        public ExamService(DB dBContext, UserManager<IdentityUser> userManager)
        {
            this.dBContext = dBContext;
            this.examRepositories = new ExamRepository(dBContext);
            this._userManager = userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(ExamDTO ex)
        {
            var user = await _userManager.FindByNameAsync(ex.Username);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.FirstOrDefault() == "admin")
            {
                Exam exam = examRepositories.getbyid(ex.ExamID);
                if (exam == null)
                {
                    Exam exam1 = new Exam()
                    {
                        ExamID = ex.ExamID,
                        Skill = ex.Skill,
                        ExamDescription = ex.ExamDescription,
                        ExamDuration = ex.ExamDuration,
                    };
                    examRepositories.insertExam(exam1);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Exam already exist" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> update(ExamDTO ex)
        {
            var user = await _userManager.FindByNameAsync(ex.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Exam exam = GetbyId(ex.ExamID);
                if (exam != null)
                {
                    exam.ExamDescription = ex.ExamDescription;
                    exam.ExamDuration = ex.ExamDuration;
                    exam.Skill = ex.Skill;
                    examRepositories.updateExam(exam);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Exam not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> delete(ExamDTO ex)
        {
            var user = await _userManager.FindByNameAsync(ex.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Exam exam = GetbyId(ex.ExamID);
                if (exam != null)
                {
                    examRepositories.deleteExam(exam);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Exam not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public Exam GetbyId(int id)
        {
            return examRepositories.getbyid(id);
        }
        public List<ExamDTO> List(string skill)
        {
            List<ExamDTO> examDTOs = new List<ExamDTO>();
            var exams = examRepositories.Listall(skill);
            foreach (var exam in exams)
            {
                examDTOs.Add(new ExamDTO { ExamID = exam.ExamID,
                    ExamDuration = exam.ExamDuration,
                    ExamDescription = exam.ExamDescription,
                    Skill = exam.Skill,
                    PracticeCount = examRepositories.Count(exam.ExamID) });
            }
            return examDTOs;
        }
    }
    #endregion

    #region "question"
    public class QuestionService
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IQuestionRepositories questionRepositories;
        public QuestionService(DB dBContext, UserManager<IdentityUser> _userManager)
        {
            this.dBContext = dBContext;
            this.questionRepositories = new QuestionRepository(dBContext);
            this._userManager = _userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(QuestionDTO ques)
        {
            var user = await _userManager.FindByNameAsync(ques.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Question test = questionRepositories.getbyid(ques.QuestionID);
                if (test == null)
                {
                    Question question = new Question()
                    {
                        QuestionID = ques.QuestionID,
                        QuestionContext = ques.QuestionContext,
                        Answer1 = ques.Answer1,
                        Answer2 = ques.Answer2,
                        Answer3 = ques.Answer3,
                        Answer4 = ques.Answer4,
                        CorrectAnswer = ques.CorrectAnswer,
                        Sentences = ques.SentenceID,
                    };
                    questionRepositories.insertQuestion(question);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Question already exist" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> update(QuestionDTO ques)
        {
            var user = await _userManager.FindByNameAsync(ques.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Question question = GetbyId(ques.QuestionID);
                if (question != null)
                {
                    question.Answer1 = ques.Answer1;
                    question.Answer1 = ques.Answer2;
                    question.Answer3 = ques.Answer3;
                    question.Answer4 = ques.Answer4;
                    question.QuestionContext = ques.QuestionContext;
                    question.CorrectAnswer = ques.CorrectAnswer;
                    question.Sentences = ques.SentenceID;
                    question.CorrectDescription = ques.CorrectDescription;
                    questionRepositories.updateQuestion(question);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Question not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> delete(QuestionDTO ques)
        {
            var user = await _userManager.FindByNameAsync(ques.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Question question = GetbyId(ques.QuestionID);
                if (question != null)
                {
                    questionRepositories.deleteQuestion(question);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Question not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public Question GetbyId(int id)
        {
            return questionRepositories.getbyid(id);
        }
        public List<QuestionDTO> List(int id)
        {
            List<QuestionDTO> questionDTOs = new List<QuestionDTO>();
            var questions = questionRepositories.Listall(id);
            foreach (var question in questions)
            {
                questionDTOs.Add(new QuestionDTO
                {
                    QuestionID = question.QuestionID,
                    QuestionContext = question.QuestionContext,
                    Answer1 = question.Answer1,
                    Answer2 = question.Answer2,
                    Answer3 = question.Answer3,
                    Answer4 = question.Answer4,
                    CorrectAnswer = question.CorrectAnswer,
                    SentenceID =  question.Sentences
                });
            }
            return questionDTOs;
        }
    }
    #endregion

    #region "result"
    public class ResultService
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IResultRepositories resultRepositories;
        public ResultService(DB dBContext, UserManager<IdentityUser> userManager)
        {
            this.dBContext = dBContext;
            this.resultRepositories = new ResultRepository(dBContext);
            this._userManager = userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(ResultDTO re)
        {
            var user = await _userManager.FindByNameAsync(re.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Result result = resultRepositories.getbyid(re.ResultID);
                if (result == null)
                {
                    Result result1 = new Result()
                    {
/*                        ResultID = re.ResultID,
                        PracticeID = re.PracticeID,
                        Score = re.Score,
                        UserID = user.Id,*/
                    };
                    resultRepositories.insertResult(result1);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Result already exist" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        /*public async Task<Response> update(ResultDTO re)
        {
            var user = await _userManager.FindByNameAsync(re.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin" & user.Id == re.UserID)
            {
                var result = GetbyId(re.ExamID);
                if (result != null)
                {
                    result.Score = re.Score; 
                    result.UserID = re.UserID;
                    result.ExamID = re.ExamID;
                    resultRepositories.updateResult(result);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Result not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }*/
        public async Task<Response> delete(ResultDTO re)
        {
            var user = await _userManager.FindByNameAsync(re.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var result = GetbyId(re.ResultID);
                if (result != null)
                {
                    resultRepositories.deleteResult(result);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public Result GetbyId(int id)
        {
            return resultRepositories.getbyid(id);
        }
    }
    #endregion

    #region "practice"
    public class PracticeService
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IPracticeRepositories practiceRepositories;
        public PracticeService(DB dBContext, UserManager<IdentityUser> userManager)
        {
            this.dBContext = dBContext;
            this.practiceRepositories = new PracticeRepository(dBContext);
            this._userManager = userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(PracticeDTO pra)
        {
            var user = await _userManager.FindByNameAsync(pra.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Practice practice = practiceRepositories.getbyid(pra.PracticeID);
                if (practice == null)
                {
                    Practice practice1 = new Practice()
                    {
                        PracticeID = pra.PracticeID,
                        PracticeDescription = pra.PracticeDescription,
                        Exam = pra.ExamID,
                    };
                    practiceRepositories.insertPractice(practice1);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Practice already exist" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> update(PracticeDTO pra)
        {
            var user = await _userManager.FindByNameAsync(pra.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Practice practice = GetbyId(pra.ExamID);
                if (practice != null)
                {
                    practice.PracticeDescription = pra.PracticeDescription;
                    practice.Exam = pra.ExamID;
                    practiceRepositories.updatePractice(practice);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Practice not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> delete(PracticeDTO pra)
        {
            var user = await _userManager.FindByNameAsync(pra.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var practice = GetbyId(pra.PracticeID);
                if (practice != null)
                {
                    practiceRepositories.deletePractice(practice);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Practice not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public Practice GetbyId(int id)
        {
            return practiceRepositories.getbyid(id);
        }        
        public List<PracticeDTO> List(int id)
        {
            List<PracticeDTO> practiceDTOs = new List<PracticeDTO>();
            var practices = practiceRepositories.Listall(id);
            foreach (var practice in practices)
            {
                practiceDTOs.Add(new PracticeDTO
                {
                    PracticeID = practice.PracticeID,
                    PracticeDescription = practice.PracticeDescription,
                    TestCount = practiceRepositories.Count(practice.PracticeID),
                    /*CorrectPercent = practiceRepositories.CorrectPercent(practice.PracticeID, userId)*/
                });
            }
            return practiceDTOs;
        }
    }
    #endregion

    #region "sentenceComplete"
    public class SentenceCompServices
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private ISentenceCompleteRepositories sencomRepositories;

        public SentenceCompServices(DB dBContext, UserManager<IdentityUser> userManager)
        {
            this.dBContext = dBContext;
            this.sencomRepositories = new SentenceCompleteRepository(dBContext);
            this._userManager = userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(SentenceComDTO sencom)
        {
            var user = await _userManager.FindByNameAsync(sencom.User);
            SentenceComplete sen = sencomRepositories.getbyid(sencom.SentenceID);
            if (sen == null)
            {
                SentenceComplete sen1 = new SentenceComplete()
                {
                    SentenceID = sencom.SentenceID,
                    Result = sencom.Result,
                    Totaltime = sencom.Totaltime,
                    Status = sencom.Status,
                    User = user.Id,
                };
                sencomRepositories.insertSenComplete(sen1);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Sentence already done" };
        }
        public async Task<Response> update(SentenceComDTO sencom)
        {
            SentenceComplete sen = sencomRepositories.getbyid(sencom.SentenceID);
            if (sen != null)
            {
                sen.SentenceID = sencom.SentenceID;
                sen.Result = sencom.Result;
                sen.Totaltime = sencom.Totaltime;
                sen.Status = sencom.Status;
                sen.User = sencom.User;
                sencomRepositories.updateSenComplete(sen);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Sentence not found!!!" };
        }
        public async Task<Response> delete(SentenceComDTO sencom)
        {
            SentenceComplete sen = sencomRepositories.getbyid(sencom.SentenceID);
            if (sen != null)
            {
                sencomRepositories.deleteSenComplete(sen);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Sentence not found!!!" };
        }

        /*public List<SentenceComDTO> List(int id)
        {
            List<SentenceComDTO> quescomDTOs = new List<QuestionCompleteDTO>();
            var sentences = sencomRepositories.
            foreach (var question in questions)
            {
                quescomDTOs.Add(new QuestionCompleteDTO
                {
                    QuestionID = question.QuestionID,
                    QuestionChoose = question.QuestionChoose,
                    IsCorrect = question.IsCorrect,
                    CorrectDescription = quescomRepositories.findcorrcet(question.QuestionID),
                });
            }
            return quescomDTOs;
        }*/
    }
    #endregion

    #region "sentence"
    public class SentenceService
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private ISentenceRepositories senRepositories;

        public SentenceService(DB dBContext, UserManager<IdentityUser> userManager)
        {
            this.dBContext = dBContext;
            this.senRepositories = new SentenceRepository(dBContext);
            this._userManager = userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(SentenceDTO sen)
        {
            var user = await _userManager.FindByNameAsync(sen.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Sentence sentence = senRepositories.getbyid(sen.SentenceId);
                if (sentence == null)
                {
                    Sentence sentence1 = new Sentence()
                    {
                        SentenceId = sen.SentenceId,
                        FilePath = sen.FilePath,
                        Practice = sen.PracticeId
                    };
                    senRepositories.insertSentence(sentence1);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Sentence already exist" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> update(SentenceDTO sen)
        {
            var user = await _userManager.FindByNameAsync(sen.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Sentence sentence = GetbyId(sen.SentenceId);
                if (sentence != null)
                {
                    sentence.FilePath = sen.FilePath;
                    sentence.Practice = sen.PracticeId;
                    senRepositories.updateSentence(sentence);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Sentence not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> delete(SentenceDTO sen)
        {
            var user = await _userManager.FindByNameAsync(sen.Username);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var sentence = GetbyId(sen.SentenceId);
                if (sentence != null)
                {
                    senRepositories.deleteSentence(sentence);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Practice not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public Sentence GetbyId(int id)
        {
            return senRepositories.getbyid(id);
        }
        public List<SentenceDTO> List(int id)
        {
            List<SentenceDTO> sentenceDTOs = new List<SentenceDTO>();
            var sentences = senRepositories.Listall(id);
            foreach (var sentence in sentences)
            {
                sentenceDTOs.Add(new SentenceDTO
                {
                    SentenceId = sentence.SentenceId,
                    FilePath = sentence.FilePath,
                    PracticeId = sentence.Practice,
                });
            }
            return sentenceDTOs;
        }
    }
    #endregion

    #region "questionComplete"
    public class QuestionComServices
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IQuestionCompleteRepositories quescomRepositories;

        public QuestionComServices(DB dBContext, UserManager<IdentityUser> userManager)
        {
            this.dBContext = dBContext;
            this.quescomRepositories = new QuestionCompleteRepository(dBContext);
            this._userManager = userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(QuestionCompleteDTO ques)
        {
            QuestionComplete quescom = quescomRepositories.getbyid(ques.QuestionID);
            if (quescom == null)
            {
                QuestionComplete quescom1 = new QuestionComplete()
                {
                    QuestionID = ques.QuestionID,
                    QuestionChoose = ques.QuestionChoose,
                    IsCorrect = ques.IsCorrect,
                    Sentence = ques.Sentence,
                };
                quescomRepositories.insertQuesComplete(quescom1);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Question already done" };
        }
        public async Task<Response> update(QuestionCompleteDTO ques)
        {
            QuestionComplete question = quescomRepositories.getbyid(ques.QuestionID);
            if (question != null)
            {
                question.QuestionChoose = ques.QuestionChoose;
                question.IsCorrect = ques.IsCorrect;
                quescomRepositories.updateQuesComplete(question);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Question not found!!!" };
        }
        public async Task<Response> delete(QuestionCompleteDTO ques)
        {
            QuestionComplete question = quescomRepositories.getbyid(ques.QuestionID);
            if (question != null)
            {
                quescomRepositories.deleteQuesComplete(question);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Question not found!!!" };
        }

        public int Correct(int id)
        {
            return quescomRepositories.CountCorrect(id);
        }

        public List<QuestionCompleteDTO> List(int id)
        {
            List<QuestionCompleteDTO> quescomDTOs = new List<QuestionCompleteDTO>();
            var questions = quescomRepositories.list(id);
            foreach (var question in questions)
            {
                quescomDTOs.Add(new QuestionCompleteDTO
                {
                    QuestionID = question.QuestionID,
                    QuestionChoose = question.QuestionChoose,
                    IsCorrect = question.IsCorrect,
                    CorrectDescription = quescomRepositories.findcorrcet(question.QuestionID),
                });
            }
            return quescomDTOs;
        }

    }
    #endregion
}
