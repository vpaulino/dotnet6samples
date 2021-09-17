public class PersonRepository : IPersonRepository
{
    private ILogger<Person> logger;
    List<Person> persons = new List<Person>();
    public PersonRepository(ILogger<Person> logger)
    {
        this.logger = logger;
        
        persons.Add(new Person("vitor"));
        persons.Add(new Person("Miguel"));


    }

    public IAsyncEnumerable<Person> GetAllAsync(int limit, int offset, CancellationToken token)
    {
        IAsyncEnumerable<Person> persons = GetAllFromSource();
        return persons;
    }

    private async IAsyncEnumerable<Person> GetAllFromSource()
    {
        for (int i = 0; i < persons.Count; i++) 
        {
            await Task.Delay(10);
            yield return persons[i];
        }    
    }

    public Task SaveAsync(Person person, CancellationToken token)
    {
        this.logger.LogInformation("PersonRepository.SaveAsync Called");
        return Task.CompletedTask;
    }
}

