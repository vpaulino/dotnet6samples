public interface IPersonRepository
{
    IAsyncEnumerable<Person> GetAllAsync(int limit, int offset, CancellationToken token);

    /**
* 
* */
    Task SaveAsync(Person person, CancellationToken token);
}

