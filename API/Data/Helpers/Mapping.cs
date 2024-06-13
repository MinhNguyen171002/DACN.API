using API.Enity;
using API.Model.DTO;
using AutoMapper;

namespace API.Data.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExamDTO, Exam>().ReverseMap();
            CreateMap<QuestionDTO, Question>().ReverseMap();
            CreateMap<SentenceDTO, Sentence>().ReverseMap();
            CreateMap<VocabularyDTO, Vocabulary>().ReverseMap();
            CreateMap<TopicDTO, Topic>().ReverseMap();
            CreateMap<SentenceComDTO, SentenceComplete>().ReverseMap();
        }
    }
}
