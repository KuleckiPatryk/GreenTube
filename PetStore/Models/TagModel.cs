namespace PetStore.Models;

public record Tag
{
    public long Id { get; set; }
    
    public string? Name { get; set; }
}
