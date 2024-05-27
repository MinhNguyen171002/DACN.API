using API.DBContext;
using API.Enity;
using API.Model.DTO;
using API.Model.GetDTO;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            var user = await _userManager.FindByIdAsync(ex.UserID);
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
            var user = await _userManager.FindByIdAsync(ex.UserID);
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
        public async Task<Response> delete(BobDTO bob)
        {
            var user = await _userManager.FindByIdAsync(bob.UserId);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Exam exam = examRepositories.GetById(int.Parse(bob.Id));
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
            var exams = examRepositories.Listall(skill);
            List<ExamGDTO> examDTOs = new List<ExamGDTO>(exams.Capacity);
            if (!exams.IsNullOrEmpty())
            {
                foreach (var exam in exams)
                {
                    examDTOs.Add(new ExamGDTO
                    {
                        ExamID = exam.ExamID,
                        ExamDuration = exam.ExamDuration,
                        ExamDescription = exam.ExamDescription,
                        Skill = exam.Skill,
                        PracticeCount = examRepositories.Count(exam.ExamID)
                    });
                }
                return examDTOs;
            }
            return Enumerable.Empty<ExamGDTO>().ToList();
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
        public async Task<Response> insert(QuestionFDTO ques)
        {
            var user = await _userManager.FindByIdAsync(ques.UserID);
            var roles = await _userManager.GetRolesAsync(user);
            Sentence sentence = questionRepositories.sentence(ques.SentenceID);
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
                            sentence = sentence,
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
            var user = await _userManager.FindByIdAsync(ques.UserID);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Question question = questionRepositories.GetById(ques.QuestionID);
                if (question != null)
                {
                    question.QuestionSerial = ques.QuestionSerial;
                    question.Answer1 = ques.Answer1;
                    question.Answer1 = ques.Answer2;
                    question.Answer3 = ques.Answer3;
                    question.Answer4 = ques.Answer4;
                    question.QuestionContext = ques.QuestionContext;
                    question.CorrectAnswer = ques.CorrectAnswer;
                    questionRepositories.Update(question);
                    Save();
                    return new Response { Status = true, Message = "Success" };
                }
                return new Response { Status = false, Message = "Question not found!!!" };
            }
            return new Response { Status = false, Message = "Need authencation" };
        }
        public async Task<Response> delete(BobDTO bob)
        {
            var user = await _userManager.FindByIdAsync(bob.UserId);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var questions = questionRepositories.listall(bob.Id);
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
            var questions = questionRepositories.listall(id);
            List<QuestionGDTO> questionDTOs = new List<QuestionGDTO>(questions.Count);
            if (!questions.IsNullOrEmpty())
            {
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
                        CorrectAnswer = question.CorrectAnswer
                    });
                }
                return questionDTOs;
            }
            return Enumerable.Empty<QuestionGDTO>().ToList();
        }
    }
    #endregion

    #region "sentenceComplete"
    public class SentenceCompServices
    {
        private DB dBContext;
        private ISentenceCompleteRepositories sencomRepositories;
        private IQuestionCompleteRepositories questionCompleteRepositories;
        private IMapper _mapper;

        public SentenceCompServices(DB dBContext, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.sencomRepositories = new SentenceCompleteRepository(dBContext);
            this.questionCompleteRepositories = new QuestionCompleteRepository(dBContext);
            _mapper = mapper;  
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> submit(SubmitDTO submit)
        {
            User user = sencomRepositories.user(submit.UserID);
            Sentence sen = questionCompleteRepositories.sentence(submit.SentenceID);
            if (sen != null)
            {
                List<QuestionComplete> quescom1 = new List<QuestionComplete>();
                foreach (var q in submit.questionComs)
                {
                    quescom1.Add(new QuestionComplete()
                    {
                        QuestionID = q.QuestionID,
                        QuestionSerial = q.QuestionSerial,
                        QuestionChoose = q.QuestionChoose,
                        IsCorrect = q.IsCorrect,
                        user = user,
                        sen = sen,
                    });
                }
                questionCompleteRepositories.AddRange(quescom1);
                SentenceComplete sen1 = _mapper.Map<SentenceComplete>(submit.sentenceCom);
                sen1.SentenceID = submit.SentenceID;
                sen1.user = user;
                sencomRepositories.Add(sen1);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Not found" };
        }
        public async Task<Response> delete(BobDTO bob)
        {
            List<QuestionComplete> questions = questionCompleteRepositories.list(bob.Id, bob.UserId);
            SentenceComplete sen = sencomRepositories.getbyuser(bob.Id, bob.UserId);
            if (sen != null && questions != null)
            {
                questionCompleteRepositories.DeleteRange(questions);
                sencomRepositories.Delete(sen);
                Save();
                return new Response { Status = true, Message = "Success" };
            }
            return new Response { Status = false, Message = "Not found!!!" };
        }
        public async Task<SentenceComGDTO> getSenCom(string id, string userid)
        {
            SentenceComplete sentence = sencomRepositories.getbyuser(id,userid);
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
        public async Task<List<QuestionComGDTO>> List(string id, string userid)
        {
            List<QuestionComplete> questions = questionCompleteRepositories.list(id, userid);
            List<QuestionComGDTO> quescomDTOs = new List<QuestionComGDTO>(questions.Capacity);
            if (!questions.IsNullOrEmpty())
            {
                foreach (var question in questions)
                {
                    quescomDTOs.Add(new QuestionComGDTO
                    {
                        QuestionSerial = question.QuestionSerial,
                        QuestionChoose = question.QuestionChoose,
                        IsCorrect = question.IsCorrect,
                        CorrectDescription = Encoding.UTF8.GetString(questionCompleteRepositories.getdescription(question.QuestionID)),
                        CorrectAnswer = questionCompleteRepositories.getcorrectanswer(question.QuestionID),
                    });
                }
                return quescomDTOs;
            }
            return Enumerable.Empty<QuestionComGDTO>().ToList();
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
            var user = await _userManager.FindByIdAsync(sen.UserID);
            var roles = await _userManager.GetRolesAsync(user);
            Exam exam = senRepositories.exam(sen.ExamId);
            if (roles.FirstOrDefault() == "admin")
            {
                Sentence sentence = _mapper.Map<Sentence>(sen);
                sentence.SentenceId = Guid.NewGuid().ToString();
                sentence.exam = exam;
                var IsVaild = senRepositories.GetById(sentence.SentenceId);
                if (IsVaild==null)
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
            var user = await _userManager.FindByIdAsync(sen.UserID);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                Sentence sentence = senRepositories.GetById(sen.SentenceId);
                if (sentence != null)
                {
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
        public async Task<Response> delete(BobDTO bob)
        {
            var user = await _userManager.FindByIdAsync(bob.UserId);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var sentence = senRepositories.GetById(bob.Id);
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
            var sentences = senRepositories.Listall(id);
            List<UserSentenceDTO> sentenceDTOs = new List<UserSentenceDTO>(sentences.Capacity);
            if (!sentences.IsNullOrEmpty())
            {
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
            return Enumerable.Empty<UserSentenceDTO>().ToList();
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
        public async Task<Response> insert(byte[] data,string filename, string id,string userid,string filetype)
        {
            var user = await _userManager.FindByIdAsync(userid);
            var roles = await _userManager.GetRolesAsync(user);
            QuestionFile qfile = fileRepositories.getbyname(filename);
            Question question = fileRepositories.question(id);
            if (qfile == null && roles.FirstOrDefault() == "admin")
            {
                QuestionFile file1 = new QuestionFile()
                {
                    Id = Guid.NewGuid().ToString(),
                    FileName = filename,
                    FileData = data,
                    ques = question,
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
            var user = await _userManager.FindByIdAsync(file.UserID);
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
