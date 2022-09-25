using AutoMapper;
using WebApi.Entities;
using WebApi.Models.Cards;

namespace WebApi
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<Card, CardResponse>()
                .ReverseMap();

            CreateMap<Card, UpdateCardRequest>()
                .ReverseMap();
        }
    }
}
