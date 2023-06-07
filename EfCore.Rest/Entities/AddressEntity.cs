namespace EfCore.Rest.Entities;

public class AddressEntity
{
    public int Id { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }

    public virtual List<PersonEntity> People { get; set; }
}