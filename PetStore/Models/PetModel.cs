namespace PetStore.Models;

public record PetModel
{
    public long Id { get; set; }

    public Category? Category { get; set; }

    public string? Name { get; set; }

    public string[]? PhotoUrls { get; set; }

    public Tag[]? Tags { get; set; }

    public string? Status { get; set; }

    public virtual bool Equals(PetModel? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other is null) return false;

        return Id == other.Id
               && Equals(Category, other.Category)
               && Name == other.Name
               && Status == other.Status
               && (PhotoUrls ?? []).SequenceEqual(other.PhotoUrls ?? [])
               && (Tags ?? []).SequenceEqual(other.Tags ?? []);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Category, Name, Status);
    }
}
