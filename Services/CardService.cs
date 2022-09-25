namespace WebApi.Services;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using WebApi.Entities;
using WebApi.Enums;
using WebApi.Helpers;
using WebApi.Models.Cards;

public interface ICardService
{
    List<CardResponse> GetCards(string refreshToken);
    CardResponse GetCardById(int id, string refreshToken);
    CardResponse AddCard(AddCardRequest model, string refreshToken);
    CardResponse UpdateCard(int id, UpdateCardRequest update, string refreshToken);
    CardResponse DeleteCard(int id, string refreshToken);
}

public class CardService : ICardService
{
    private IUserService _userService;
    private DataContext _dataContext;
    private IMapper _mapper;

    public CardService(IUserService userService,
        DataContext context,
        IMapper mapper)
    {
        _userService = userService;
        _dataContext = context;
        _mapper = mapper;
    }

    public CardResponse AddCard(AddCardRequest model, string refreshToken)
    {
        var user = _userService.GetCurrentUser(refreshToken);

        var newCard = new Card
        {
            Title = model.Title,
            Description = model.Description,
            Status = CardStatus.Todo,
            UserId = user.Id
        };

        _dataContext.Cards.Add(newCard);
        _dataContext.SaveChanges();

        return _mapper.Map<CardResponse>(newCard);
    }

    public CardResponse DeleteCard(int id, string refreshToken)
    {
        var user = _userService.GetCurrentUser(refreshToken);

        var card = _dataContext.Cards
            .Where(c => c.Id == id && c.UserId == user.Id)
            .FirstOrDefault();

        if (card == null)
        {
            return null;
        }

        var cardEntity = _dataContext.Cards.Remove(card).Entity;
        var response = _mapper.Map<CardResponse>(cardEntity);
        
        _dataContext.SaveChanges();
        
        return response;
    }

    public CardResponse GetCardById(int id, string refreshToken)
    {
        var user = _userService.GetCurrentUser(refreshToken);

        var card = _dataContext.Cards
            .Where(c => c.Id == id && c.UserId == user.Id)
            .FirstOrDefault();

        if (card == null)
        {
            return null;
        }

        return _mapper.Map<CardResponse>(card);
    }

    public List<CardResponse> GetCards(string refreshToken)
    {
        var user = _userService.GetCurrentUser(refreshToken);

        return _dataContext.Cards
            .Where(c => c.UserId == user.Id)
            .ProjectTo<CardResponse>(_mapper.ConfigurationProvider)
            .ToList();
    }

    public CardResponse UpdateCard(int id, UpdateCardRequest update, string refreshToken)
    {
        var user = _userService.GetCurrentUser(refreshToken);

        var card = _dataContext.Cards.Where(c => c.Id == id)
            .FirstOrDefault();

        if (card == null)
        {
            return null;
        }

        _mapper.Map(update, card);

        _dataContext.SaveChanges();

        return _mapper.Map<CardResponse>(card);
    }
}
