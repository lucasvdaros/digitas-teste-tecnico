namespace Digitas.Quotes.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void Commit();
}
