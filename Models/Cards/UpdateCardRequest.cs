namespace WebApi.Models.Cards;

using System.ComponentModel.DataAnnotations;

public class UpdateCardRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
}
