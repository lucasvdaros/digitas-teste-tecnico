namespace Digitas.Quotes.Worker.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void Commit();
}
