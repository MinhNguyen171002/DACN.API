using API.Enity;
using API.Model.DTO;
using AutoMapper;

namespace API.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ExamDTO, Exam>().ReverseMap();
            CreateMap<SentenceDTO, Sentence>().ReverseMap();
            CreateMap<QuestionDTO, Question>().ReverseMap();
            CreateMap<QuestionComDTO, QuestionComplete>().ReverseMap();
            CreateMap<SentenceComDTO, SentenceComplete>().ReverseMap();
            CreateMap<FileDTO, QuestionFile>().ReverseMap();
        }              
    }
}
