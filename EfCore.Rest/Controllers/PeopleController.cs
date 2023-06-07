using EfCore.Rest.Database;
using EfCore.Rest.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Rest.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private readonly PeopleDbContext _db;
    private readonly ILogger<PeopleController> _logger;

    public PeopleController(PeopleDbContext db,
        ILogger<PeopleController> logger)
    {
        _db = db;
        _logger = logger;
    }

    [HttpGet(Name = "GetPeople")]
    public async Task<IEnumerable<PersonEntity>> Get()
    {
        var people = await _db.People
            .ToListAsync();

        foreach (var p in people)
        {
            Console.WriteLine(p.Address?.AddressLine1);
        }
        
        return people;
    }

    [HttpPost(Name = "AddPerson")]
    public async Task<PersonEntity> AddPerson([FromBody] AddPersonCommand command)
    {
        var personEntity = new PersonEntity()
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            PhoneNumber = command.PhoneNumber
        };

        _db.People.Add(personEntity);
        await _db.SaveChangesAsync();

        return personEntity;
    }
}

public class AddPersonCommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
}