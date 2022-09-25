namespace WebApi.Controllers;

using WebApi.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Cards;
using WebApi.Services;

[Authorize]
[Route("[controller]")]
[ApiController]
public class CardsController : ControllerBase
{
    private ICardService _cardService;

    public CardsController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet]
    public IActionResult GetCards()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = _cardService.GetCards(refreshToken);
        
        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetCardById(int id)
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = _cardService.GetCardById(id, refreshToken);
        
        if (response == null)
        {
            return NotFound(new { message = $"Card id: {id} was not found" });
        }

        return Ok(response);
    }


    [HttpPost]
    public IActionResult AddCard(AddCardRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var refreshToken = Request.Cookies["refreshToken"];

        var response = _cardService.AddCard(model, refreshToken);

        return Created($"/cards/{response.Id}", response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCard(int id, UpdateCardRequest model)
    {
        var refreshToken = Request.Cookies["refreshToken"];
        
        var response = _cardService.UpdateCard(id, model, refreshToken);

        if (response == null)
        {
            return NotFound(new { message = $"Card id: {id} was not found" });
        }

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCard(int id)
    {
        var refreshToken = Request.Cookies["refreshToken"];

        var response = _cardService.DeleteCard(id, refreshToken);

        if (response == null)
        {
            return NotFound(new { message = $"Card id: {id} was not found" });
        }

        return Ok(response);
    }
}
