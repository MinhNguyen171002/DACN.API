using API.Data.Helpers;
using API.DBContext;
using API.Enity;
using API.Model.DTO;
using API.Model.GetDTO;
using API.Repositories;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.Collections;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

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
        public UserGDTO GetUser(string id)
        {
            User user = userRepositories.GetUser(id);
            if (user!=null) 
            {
                UserGDTO gDTO = new UserGDTO()
                {
                    UserID = user.UserID,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    UserPhone = user.SDT
                };
                return gDTO;
            }
            return new UserGDTO { UserEmail = null, UserName = null, UserPhone = null};
        }
        public List<UserGDTO> GetUsers()
        {
            List<User> users = userRepositories.GetList().ToList();
            if (users != null)
            {
                List<UserGDTO> gDTOs = new List<UserGDTO>();
                foreach (User user in users)
                {
                    gDTOs.Add(new UserGDTO() {
                        UserEmail = user.Email,
                        UserName = user.UserName,
                        UserPhone = user.SDT
                    });

                }
                
                return gDTOs;
            }
            return Enumerable.Empty<UserGDTO>().ToList();
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
                Exam exam = examRepositories.exam(ex.ExamSerial,ex.Skill);
                if (exam == null)
                {
                    Exam exam1 = _mapper.Map<Exam>(ex);
                    exam1.ExamID = Guid.NewGuid().ToString();
                    examRepositories.Add(exam1);
                    Save();
                    return new Response { Status = true, Message = "Thêm thành công" };
                }
                return new Response { Status = false, Message = "Nội dung đã tồn tại" };
            }
            return new Response { Status = false, Message = "Cần quyền admin"};
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
                    exam.ExamSerial = ex.ExamSerial;
                    exam.Part = ex.Part;
                    exam.ExamDescription = ex.ExamDescription;
                    exam.ExamDuration = ex.ExamDuration;
                    exam.Skill = ex.Skill;
                    examRepositories.Update(exam);
                    Save();
                    return new Response { Status = true, Message = "Cập nhật thành công" };
                }
                return new Response { Status = false, Message = "Không tìm thấy nội dung" };
            }
            return new Response { Status = false, Message = "Cần quyền admin" };
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
                    return new Response { Status = true, Message = "Xóa thành công" };
                }
                return new Response { Status = false, Message = "Không tìm thấy Nội Dung" };
            }
            return new Response { Status = false, Message = "Cần quyền admin" };
        }
        public List<ExamGDTO> List()
        {
            var exams = examRepositories.Listall();
            List<ExamGDTO> examDTOs = new List<ExamGDTO>();
            if (!exams.IsNullOrEmpty())
            {
                foreach (var exam in exams)
                {
                    examDTOs.Add(new ExamGDTO
                    {
                        ExamID = exam.ExamID,
                        ExamSerial = exam.ExamSerial,
                        Part = exam.Part,
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

        public IConfiguration configuration { get; }
        private CloudinarySettings cloudinarySetting;
        private Cloudinary _cloudinary;
        private IMapper _mapper;
        public QuestionService(DB dBContext, UserManager<IdentityUser> _userManager, IConfiguration configuration, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.questionRepositories = new QuestionRepository(dBContext);
            this._userManager = _userManager;
            this.configuration = configuration;
            this._mapper = mapper;
            cloudinarySetting = configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            Account acc = new Account
                (
                    cloudinarySetting.CloudName,
                    cloudinarySetting.ApiKey,
                    cloudinarySetting.ApiSecret
                );
            _cloudinary = new Cloudinary(acc);
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> create(QuestionDTO ques)
        {
            var user = await _userManager.FindByIdAsync(ques.UserID);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {                
                Sentence sentence = questionRepositories.sentence(ques.SentenceID);
                string ImageUrl = string.Empty, AudioUrl = string.Empty;
                if (ques.files.Any())
                {
                    foreach (IFormFile file in ques.files)
                    {
                        string type = file.ContentType;                        
                        switch (type)
                        {
                            case ("image/jpeg"):
                            case ("image/png"):
                                ImageUploadResult Imageresult = new ImageUploadResult();
                                ImageUploadParams uploadparam = new ImageUploadParams
                                {
                                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                                    Folder = "ENGL"
                                };
                                Imageresult = await _cloudinary.UploadAsync(uploadparam);
                                ImageUrl = Imageresult.Uri.ToString();
                                break;
                            case ("audio/mpeg"):
                                VideoUploadResult result = new VideoUploadResult();
                                VideoUploadParams uploadParam = new VideoUploadParams
                                {
                                    File = new FileDescription(file.Name, file.OpenReadStream()),
                                    Folder = "ENGL"
                                };
                                result = await _cloudinary.UploadAsync(uploadParam);
                                AudioUrl = result.Uri.ToString();
                                break;
                        }
                    }                       
                }
                Question question = new Question()
                {
                    QuestionID = Guid.NewGuid().ToString(),
                    sentence = sentence,
                    QuestionContext = ques.QuestionContext,
                    QuestionSerial = ques.QuestionSerial,
                    Answer1 = ques.Answer1,
                    Answer2 = ques.Answer2,
                    Answer3 = ques.Answer3,
                    Answer4 = ques.Answer4,
                    CorrectAnswer = ques.CorrectAnswer,
                    CorrectDescription = ques.CorrectDescription,
                };
                if (!string.IsNullOrEmpty(ImageUrl) || !string.IsNullOrEmpty(AudioUrl))
                {
                    question.UrlAudio = AudioUrl;
                    question.UrlImage = ImageUrl;
                }
                var IsVaild = questionRepositories.question(sentence.SentenceId,question.QuestionSerial);
                if (IsVaild == null)
                {
                    questionRepositories.Add(question);
                    Save();
                    return new Response { Status = true, Message = "Thêm thành công" };
                }
                return new Response { Status = false, Message = "Câu hỏi đã tồn tại" };
            }
            return new Response { Status = false, Message = "Cần quyền admin" };
        }
        public async Task<Response> upload(QuestionFileDTO ques)
        {
            var user = await _userManager.FindByIdAsync(ques.UserID);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                List<string> ImageUrl = new List<string>(), AudioUrl = new List<string>();
                foreach (IFormFile file in ques.files)
                {
                    string type = file.ContentType;
                    switch (type)
                    {
                        case ("image/jpeg"):
                        case ("image/png"):
                            ImageUploadResult Imageresult = new ImageUploadResult();
                            ImageUploadParams uploadparam = new ImageUploadParams
                            {
                                File = new FileDescription(file.FileName, file.OpenReadStream()),
                                Folder = "ENGL"
                            };
                            Imageresult = await _cloudinary.UploadAsync(uploadparam);
                            ImageUrl.Add(Imageresult.Uri.ToString());
                            break;                      
                        case ("audio/mpeg"):
                            VideoUploadResult result = new VideoUploadResult();
                            VideoUploadParams uploadParam = new VideoUploadParams
                            {
                                File = new FileDescription(file.Name, file.OpenReadStream()),
                                Folder = "ENGL"
                            };
                            result = await _cloudinary.UploadAsync(uploadParam);
                            AudioUrl.Add(result.Uri.ToString());
                            break;
                        case ("text/plain"):
                            Sentence sentence = questionRepositories.sentence(ques.SentenceID);
                            using (var memoryStream = new MemoryStream())
                            {
                                await file.CopyToAsync(memoryStream);
                                byte[]? fileData = memoryStream.ToArray();
                                List<string> lines = Encoding.Default.GetString(fileData).Split('\\').ToList();
                                List<Question> questions = new List<Question>();
                                for (int i = 0; i < lines.Capacity; i++)
                                {
                                    List<string> parts = lines[i].Split('/').ToList();
                                    int serial = int.Parse(parts[0]);
                                    Question question = new Question()
                                    {
                                        QuestionID = Guid.NewGuid().ToString(),
                                        QuestionSerial = serial,
                                        QuestionContext = parts[1],
                                        Answer1 = parts[2],
                                        Answer2 = parts[3],
                                        Answer3 = parts[4],
                                        Answer4 = parts[5],
                                        CorrectAnswer = parts[6],
                                        CorrectDescription = parts[7].Trim(),
                                        sentence = sentence,
                                    };
                                    if (ImageUrl.Any() || AudioUrl.Any())
                                    {
                                        question.UrlImage = ImageUrl[i];
                                        question.UrlAudio = AudioUrl[i];
                                    }
                                    questions.Add(question);
                                }
                                questionRepositories.AddRange(questions);
                                Save();
                                break;
                            }
                    }
                }
                return new Response { Status = true , Message = "Thêm thành công" };
            }
            return new Response { Status = false, Message = "Cần quyền admin" };
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
                    return new Response { Status = true, Message = "Cập nhật thành công" };
                }
                return new Response { Status = false, Message = "Câu hỏi không tồn tại" };
            }
            return new Response { Status = false, Message = "Cần quyền admin" };
        }
        public async Task<Response> deletes(BobDTO bob)
        {
            var user = await _userManager.FindByIdAsync(bob.UserId);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var questions = questionRepositories.list(bob.Id);
                if (questions != null)
                {
                    questionRepositories.DeleteRange(questions);
                    Save();
                    return new Response { Status = true, Message = "Xóa thành công" };
                }
                return new Response { Status = false, Message = "Câu hỏi không tồn tại" };
            }
            return new Response { Status = false, Message = "Cần quyền admin" };
        }
        public async Task<Response> delete(BobDTO bob)
        {
            var user = await _userManager.FindByIdAsync(bob.UserId);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "admin")
            {
                var questions = questionRepositories.GetById(bob.Id);
                if (questions != null)
                {
                    questionRepositories.Delete(questions);
                    Save();
                    return new Response { Status = true, Message = "Xóa thành công" };
                }
                return new Response { Status = false, Message = "Câu hỏi không tồn tại" };
            }
            return new Response { Status = false, Message = "Cần quyền admin" };
        }
        public List<QuestionGDTO> List(string id)
        {
            var questions = questionRepositories.list(id);
            List<QuestionGDTO> questionDTOs = new List<QuestionGDTO>();
            if (!questions.IsNullOrEmpty())
            {
                foreach (var question in questions)
                {
                    questionDTOs.Add(new QuestionGDTO
                    {
                        QuestionID = question.QuestionID,
                        QuestionSerial = question.QuestionSerial,
                        Sentence = question.sentence.Description,
                        UrlAudio = question.UrlAudio,
                        UrlImage = question.UrlImage,
                        QuestionContext = question.QuestionContext,
                        Answer1 = question.Answer1,
                        Answer2 = question.Answer2,
                        Answer3 = question.Answer3,
                        Answer4 = question.Answer4,
                        CorrectDescription = question.CorrectDescription,
                        CorrectAnswer = question.CorrectAnswer
                    });
                }
                return questionDTOs;
            }
            return Enumerable.Empty<QuestionGDTO>().ToList();
        }
        public List<QuestionGDTO> ListAll()
        {
            var questions = questionRepositories.listall();
            List<QuestionGDTO> questionDTOs = new List<QuestionGDTO>();
            if (!questions.IsNullOrEmpty())
            {
                foreach (var question in questions)
                {
                    questionDTOs.Add(new QuestionGDTO
                    {
                        QuestionID = question.QuestionID,
                        SentenceID = question.sentence.SentenceId,
                        QuestionSerial = question.QuestionSerial,
                        Sentence = question.sentence.Description,
                        UrlAudio = question.UrlAudio,
                        UrlImage = question.UrlImage,
                        QuestionContext = question.QuestionContext,
                        Answer1 = question.Answer1,
                        Answer2 = question.Answer2,
                        Answer3 = question.Answer3,
                        Answer4 = question.Answer4,
                        CorrectDescription = question.CorrectDescription,
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
                return new Response { Status = true, Message = "Nộp bài thành công" };
            }
            return new Response { Status = false, Message = "Không tìm thấy bộ đề thi" };
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
                return new Response { Status = true, Message = "Cho phép làm lại" };
            }
            return new Response { Status = false, Message = "Không tìm thấy bộ đề" };
        }
        public async Task<SentenceComGDTO> getSenCom(string id, string userid)
        {
            SentenceComplete sentence = sencomRepositories.getbyuser(id,userid);
            if (sentence == null)
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
            List<QuestionComGDTO> quescomDTOs = new List<QuestionComGDTO>();
            if (!questions.IsNullOrEmpty())
            {
                foreach (var question in questions)
                {
                    quescomDTOs.Add(new QuestionComGDTO
                    {
                        QuestionSerial = question.QuestionSerial,
/*                        QuestionCorrect = question.sen.questions.Where(x=>x.QuestionID
                        .Equals(question.QuestionID)).Select(x=>x.CorrectAnswer)
                        .FirstOrDefault(),*/
                        QuestionChoose = question.QuestionChoose,
                        IsCorrect = question.IsCorrect,
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
                    return new Response { Status = true, Message = "Thêm đề thành công" };
                }
                return new Response { Status = false, Message = "Đề đã tồn tại" };
            }
            return new Response { Status = false, Message = "Cần quyền admin" };
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
                    return new Response { Status = true, Message = "Cập nhật thành công" };
                }
                return new Response { Status = false, Message = "Không tìm thấy Đề" };
            }
            return new Response { Status = false, Message = "Cần quyền admin" };
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
                    return new Response { Status = true, Message = "Xóa thành công" };
                }
                return new Response { Status = false, Message = "Không tìm thấy Đề" };
            }
            return new Response { Status = false, Message = "Cần quyền admin" };
        }
        public List<UserSentenceDTO> List(string id, string user)
        {
            var sentences = senRepositories.List(id);
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
        public List<SentencesGDTO> ListAll()
        {
            var sentences = senRepositories.Listall();
            List<SentencesGDTO> sentenceDTOs = new List<SentencesGDTO>();
            if (!sentences.IsNullOrEmpty())
            {
                foreach (var sentence in sentences)
                {
                    sentenceDTOs.Add(new SentencesGDTO
                    {
                        Skill = sentence.exam.Skill,
                        SentenceSerial = sentence.SentenceSerial,
                        ExamDetail = sentence.exam.ExamDescription,
                        ExamId = sentence.exam.ExamID,
                        SentenceId = sentence.SentenceId,
                        Description = sentence.Description,
                        QuestionCount = senRepositories.CountQuestion(sentence.SentenceId)
                    });
                }
                return sentenceDTOs;
            }
            return Enumerable.Empty<SentencesGDTO>().ToList();
        }
        public PracticeDTO getPractice(string id, string user)
        {
            PracticeDTO practiceDTO = new PracticeDTO()
            {
                PracticeSkill = senRepositories.GetSkill(id),
                TestCount = senRepositories.Count(id),
                Tested = senRepositories.TestCorrect(id, user),
            };
            return practiceDTO;
        }
    }
    #endregion

    #region "Vocabulary"
    public class VocabularyService
    {
        private DB dBContext;
        private readonly UserManager<IdentityUser> _userManager;
        private IVocabularyRepositories vocarepositories;
        private ITopicRepositories topicRepositories;
        private IMapper _mapper;
        public VocabularyService(DB dBContext, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.vocarepositories = new VocabularyRepository(dBContext);
            this.topicRepositories = new TopicRepository(dBContext);
            this._userManager = userManager;
            _mapper = mapper;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }
        public async Task<Response> creTopic(TopicDTO topic)
        {
            User user = vocarepositories.user(topic.UserID);
            if (user != null)
            {
                Topic topic1 = _mapper.Map<Topic>(topic);
                topic1.TopicId = Guid.NewGuid().ToString();
                topic1.user = user;
                topicRepositories.Add(topic1);
                Save();
                return new Response { Status = true, Message = "Thêm thành công" };
            }
            return new Response { Status = false, Message = "Đã tồn tại" };
        }
        public async Task<Response> creVoca(VocabularyDTO vocabulary)
        {
            User user = vocarepositories.user(vocabulary.UserID);
            Topic topic = vocarepositories.topic(vocabulary.TopicId);
            if (topic != null)
            {
                Vocabulary vocabulary1 = _mapper.Map<Vocabulary>(vocabulary);
                vocabulary1.VocabularyId = Guid.NewGuid().ToString();
                vocabulary1.user = user;
                vocabulary1.topic = topic;
                vocarepositories.Add(vocabulary1);
                Save();
                return new Response { Status = true, Message = "Thêm thành công" };
            }
            return new Response { Status = false, Message = "Đã tồn tại" };
        }
        public async Task<List<VocabularyGDTO>> ListVocabulary(string id, string userid)
        {
            List<Vocabulary> vocabularys = vocarepositories.list(id, userid);
            List<VocabularyGDTO> vocabularyDTOs = new List<VocabularyGDTO>();
            if (!vocabularys.IsNullOrEmpty())
            {
                foreach (var vocabulary in vocabularys)
                {
                    vocabularyDTOs.Add(new VocabularyGDTO
                    {
                        VocabularyId = vocabulary.VocabularyId,
                        vocabulary = vocabulary.vocabulary,
                        Description = vocabulary.Description,
                        TopicId = vocabulary.topic.TopicId,
                        TopicName = vocabulary.topic.TopicName,
                    });
                }
                return vocabularyDTOs;
            }
            return Enumerable.Empty<VocabularyGDTO>().ToList();
        }
        public async Task<List<TopicGDTO>> ListTopic(string userid)
        {
            List<Topic> topics = topicRepositories.list(userid);
            List<TopicGDTO> topicDTOs = new List<TopicGDTO>();
            if (!topics.IsNullOrEmpty())
            {
                foreach (var topic in topics)
                {
                    topicDTOs.Add(new TopicGDTO
                    {
                        TopicId = topic.TopicId,
                        TopicName = topic.TopicName,
                        VocabularyCount = topic.vocabularies.Count()
                    });
                }
                return topicDTOs;
            }
            return Enumerable.Empty<TopicGDTO>().ToList();
        }
    }
    #endregion

}
