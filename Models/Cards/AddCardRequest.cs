namespace WebApi.Models.Cards;

using System.ComponentModel.DataAnnotations;

public class AddCardRequest
{
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
}
