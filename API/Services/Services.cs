using API.DBContext;
using API.Enity;
using API.Model.DTO;
using API.Model.GetDTO;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.Collections.Immutable;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            userRepositories.Add(user);
            Save();
        }
        public void update(User user)
        {
            userRepositories.Update(user);
            Save();
        }
        public void delete(User user)
        {
            userRepositories.Delete(user);
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
        private IMapper _mapper;
        public ExamService(DB dBContext, UserManager<IdentityUser> userManager,IMapper _mapper)
        {
            this.dBContext = dBContext;
            this.examRepositories = new ExamRepository(dBContext);
            this._userManager = userManager;
            this._mapper = _mapper;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(ExamDTO ex)
        {
            var user = await _userManager.FindByNameAsync(ex.UserName);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.FirstOrDefault() == "admin")
            {
                Exam exam = examRepositories.GetById(ex.ExamID);
                if (exam == null)
                {
                    Exam exam1 = _mapper.Map<Exam>(ex);
                    examRepositories.Add(exam1);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Exam already exist" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> update(ExamDTO ex)
        {
            var user = await _userManager.FindByNameAsync(ex.UserName);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Exam exam = examRepositories.GetById(ex.ExamID);
                if (exam != null)
                {
                    exam.ExamDescription = ex.ExamDescription;
                    exam.ExamDuration = ex.ExamDuration;
                    exam.Skill = ex.Skill;
                    examRepositories.Update(exam);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Exam not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> delete(ExamDTO ex)
        {
            var user = await _userManager.FindByNameAsync(ex.UserName);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Exam exam = examRepositories.GetById(ex.ExamID);
                if (exam != null)
                {
                    examRepositories.Delete(exam);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Exam not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public List<ExamGDTO> List(string skill)
        {
            List<ExamGDTO> examDTOs = new List<ExamGDTO>();
            var exams = examRepositories.Listall(skill);
            foreach (var exam in exams)
            {
                examDTOs.Add(new ExamGDTO { 
                    ExamID = exam.ExamID,
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
        private IMapper _mapper;
        public QuestionService(DB dBContext, UserManager<IdentityUser> _userManager, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.questionRepositories = new QuestionRepository(dBContext);
            this._userManager = _userManager;
            _mapper = mapper;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(QuestionFDTO ques)
        {
            var user = await _userManager.FindByNameAsync(ques.UserName);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                string filePath = ques.FilePath.ToString();
                try
                {
                    string[] lines = File.ReadAllLines(filePath,Encoding.UTF8);

                    List<Question> questions = new List<Question>(lines.Length);
                    foreach (string line in lines)
                    {
                        string[] quesDTO = line.Split('/');
                        questions.Add(new Question()
                        {
                            QuestionID = Guid.NewGuid().ToString(),
                            QuestionSerial = int.Parse(quesDTO[0]),
                            QuestionContext = quesDTO[1],
                            Answer1 = quesDTO[2],
                            Answer2 = quesDTO[3],
                            Answer3 = quesDTO[4],
                            Answer4 = quesDTO[5],
                            CorrectAnswer = quesDTO[6],
                            Sentences = ques.SentenceID,
                        });
                    }
                    questionRepositories.AddRange(questions);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                catch (Exception ex)
                {
                    return new Response { Status = false, Message = "An error occurred: " + ex.Message };
                }
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> update(QuestionDTO ques)
        {
            var user = await _userManager.FindByNameAsync(ques.UserName);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Question question = questionRepositories.getbyid(ques.QuestionID);
                if (question != null)
                {
                    question.QuestionSerial = ques.QuestionSerial;
                    question.Answer1 = ques.Answer1;
                    question.Answer1 = ques.Answer2;
                    question.Answer3 = ques.Answer3;
                    question.Answer4 = ques.Answer4;
                    question.QuestionContext = ques.QuestionContext;
                    question.CorrectAnswer = ques.CorrectAnswer;
                    question.Sentences = ques.SentenceID;
                    questionRepositories.Update(question);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Question not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> delete(QuestionDTO ques)
        {
            var user = await _userManager.FindByNameAsync(ques.UserName);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var questions = questionRepositories.listall(ques.SentenceID);
                if (questions != null)
                {
                    questionRepositories.DeleteRange(questions);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Question not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public List<QuestionGDTO> List(string id)
        {
            List<QuestionGDTO> questionDTOs = new List<QuestionGDTO>();
            var questions = questionRepositories.listall(id);
            foreach (var question in questions)
            {
                questionDTOs.Add(new QuestionGDTO
                {
                    QuestionID = question.QuestionID,
                    QuestionSerial = question.QuestionSerial,
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

    #region "sentenceComplete"
    public class SentenceCompServices
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private ISentenceCompleteRepositories sencomRepositories;
        private IMapper _mapper;

        public SentenceCompServices(DB dBContext, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.sencomRepositories = new SentenceCompleteRepository(dBContext);
            this._userManager = userManager;
            _mapper = mapper;  
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(SentenceComDTO sencom)
        {
            var user = await _userManager.FindByNameAsync(sencom.User);
            SentenceComplete sen = sencomRepositories.getbyuser(sencom.SentenceID,user.Id);
            if (sen == null && user != null)
            {
                SentenceComplete sen1 = _mapper.Map<SentenceComplete>(sencom);
                sen1.CorrectQuestion = sencomRepositories.CorrectCount(sencom.SentenceID, user.Id);
                sencomRepositories.Add(sen1);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Sentence already done" };
        }
        public async Task<Response> delete(SentenceComDTO sencom)
        {
            var user = await _userManager.FindByNameAsync(sencom.User);
            SentenceComplete sen = sencomRepositories.getbyuser(sencom.SentenceID,user.Id);
            if (sen != null && user != null)
            {
                sencomRepositories.Delete(sen);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Sentence not found!!!" };
        }
        public async Task<SentenceComGDTO> getSenCom(string id, string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            SentenceComplete sentence = sencomRepositories.getbyuser(id,user.Id);
            if(sentence == null)
            {
                return new  SentenceComGDTO
                {
                    SentenceID = "0",
                    Status = false,
                    Totaltime = TimeSpan.Parse("00:00:00"),
                    CorrectQuestion = 0,
                    CorrectPercent = 0,
                };
            }
            return new SentenceComGDTO
            {
                SentenceID = id,
                Status = sentence.Status,
                Totaltime = sentence.Totaltime,
                CorrectQuestion = sentence.CorrectQuestion,
                CorrectPercent = (int?)(sencomRepositories.CorrectPercent(id,sentence.CorrectQuestion)*100),
            };
        }
    }
    #endregion

    #region "sentence"
    public class SentenceService
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private ISentenceRepositories senRepositories;
        private IMapper _mapper;

        public SentenceService(DB dBContext, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.senRepositories = new SentenceRepository(dBContext);
            this._userManager = userManager;
            _mapper = mapper;
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
                Sentence sentence = _mapper.Map<Sentence>(sen);
                sentence.SentenceId = Guid.NewGuid().ToString();
                var IsVaild = senRepositories.GetById(sentence.SentenceId);
                if (IsVaild!=null)
                {
                    senRepositories.Add(sentence);
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
                Sentence sentence = senRepositories.getbyid(sen.SentenceId);
                if (sentence != null)
                {
                    sentence.Exam = sen.ExamId;
                    sentence.SentenceSerial = sen.SentenceSerial;
                    sentence.Description = sen.Description;
                    senRepositories.Update(sentence);
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
                var sentence = senRepositories.GetById(sen.SentenceId);
                if (sentence != null)
                {
                    senRepositories.Delete(sentence);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Practice not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public List<UserSentenceDTO> List(int id, string user)
        {
            List<UserSentenceDTO> sentenceDTOs = new List<UserSentenceDTO>();
            var sentences = senRepositories.Listall(id);
            foreach (var sentence in sentences)
            {
                sentenceDTOs.Add(new UserSentenceDTO
                {
                    SentenceSerial = sentence.SentenceSerial,
                    SentenceId = sentence.SentenceId,                    
                    Description = sentence.Description,
                    QuestionCount = senRepositories.CountQuestion(sentence.SentenceId),
                    CorrectPercent = (int)(senRepositories.CorrectPercent(sentence.SentenceId, user) * 100),
                    CorrectQuestion = senRepositories.CorrectQuestion(sentence.SentenceId, user),
                });
            }
            return sentenceDTOs;
        }
        public PracticeDTO getPractice(int id, string user)
        {
            PracticeDTO practiceDTO = new PracticeDTO()
            {
                TestCount = senRepositories.Count(id),
                TestCorrect = senRepositories.TestCorrect(id, user),
            };
            return practiceDTO;
        }
    }
    #endregion

    #region "questionComplete"
    public class QuestionComServices
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IQuestionCompleteRepositories quescomRepositories;
        private IMapper _mapper;

        public QuestionComServices(DB dBContext, UserManager<IdentityUser> userManager,IMapper mapper)
        {
            this.dBContext = dBContext;
            this.quescomRepositories = new QuestionCompleteRepository(dBContext);
            this._userManager = userManager;
            _mapper = mapper;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(QuestionComDTO ques)
        {
            var user = await _userManager.FindByNameAsync(ques.User);
            QuestionComplete quescom = quescomRepositories.getbyuser(ques.QuestionSerial,user.Id);
            if (quescom == null)
            {
                QuestionComplete quescom1 = _mapper.Map<QuestionComplete>(ques);
                quescomRepositories.Add(quescom1);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            await update(ques);
            return new Response { Status = true, Message = "Success" };
        }
        public async Task<Response> update(QuestionComDTO ques)
        {
            var user = await _userManager.FindByNameAsync(ques.User);
            QuestionComplete quescom = quescomRepositories.getbyuser(ques.QuestionSerial, user.Id);
            if (quescom != null)
            {
                quescom.QuestionChoose = ques.QuestionChoose;
                quescom.IsCorrect = ques.IsCorrect;
                quescomRepositories.Update(quescom);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Question not found!!!" };
        }       
        public async Task<Response> deletes(QuestionComDTO ques)
        {
            var user = await _userManager.FindByNameAsync(ques.User);
            var questions = quescomRepositories.list(ques.Sentence,user.Id);
            if (user != null && questions !=null)
            {
                quescomRepositories.DeleteRange(questions);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Question not found!!!" };
        }

        public List<QuestionComGDTO> List(string id,string name)
        {
            List<QuestionComGDTO> quescomDTOs = new List<QuestionComGDTO>();
            var questions = quescomRepositories.list(id,name);
            foreach (var question in questions)
            {
                quescomDTOs.Add(new QuestionComGDTO
                {
                    QuestionSerial = question.QuestionSerial,
                    QuestionChoose = question.QuestionChoose,
                    IsCorrect = question.IsCorrect,
                    //CorrectDescription = quescomRepositories.getdescription(question.QuestionID),
                    CorrectAnswer = quescomRepositories.getcorrectanswer(question.QuestionID),
                });
            }
            return quescomDTOs;
        }

    }
    #endregion

    #region"file"
    public class FileService
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IFileRepositories fileRepositories;
        public FileService(DB dBContext, UserManager<IdentityUser> userManager)
        {
            this.dBContext = dBContext;
            this.fileRepositories = new FileRepository(dBContext);
            this._userManager = userManager;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> insert(byte[] data,string filename, string id,string name,string filetype)
        {
            var user = await _userManager.FindByNameAsync(name);
            var roles = await _userManager.GetRolesAsync(user);
            QuestionFile qfile = fileRepositories.getbyname(filename);
            if (qfile == null && roles.FirstOrDefault() == "admin")
            {
                QuestionFile file1 = new QuestionFile()
                {
                    Id = Guid.NewGuid().ToString(),
                    FileName = filename,
                    FileData = data,
                    Question = id,
                    FileType = filetype,
                };
                fileRepositories.Add(file1);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "File already exist!!!" };
        }
        public async Task<Response> delete(FileDTO file)
        {
            var user = await _userManager.FindByNameAsync(file.userName);
            var roles = await _userManager.GetRolesAsync(user);
            QuestionFile file1 = fileRepositories.getbyname(file.fileName);
            if (file1 != null && roles.FirstOrDefault() == "admin")
            {
                fileRepositories.Delete(file1);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "File not found!!!" };
        }
    }
    #endregion
}
