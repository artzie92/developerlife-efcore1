namespace EfCore.Rest.Entities;

public class PersonEntity
{
    public int PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    
    public int? AddressId { get; set; }
    public virtual AddressEntity? Address { get; set; }
}