using API.Enity;
using API.Model.DTO;
using AutoMapper;

namespace API.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ExamDTO, Exam>();
            CreateMap<SentenceDTO, Sentence>();
            CreateMap<QuestionDTO, Question>();
            CreateMap<QuestionDTO, QuestionComplete>();
            CreateMap<SentenceComDTO, SentenceComplete>();
            CreateMap<FileDTO, QuestionFile>();
        }              
    }
}
