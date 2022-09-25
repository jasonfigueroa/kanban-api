namespace WebApi.Entities;

using WebApi.Enums;

public class Card
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public CardStatus Status { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
