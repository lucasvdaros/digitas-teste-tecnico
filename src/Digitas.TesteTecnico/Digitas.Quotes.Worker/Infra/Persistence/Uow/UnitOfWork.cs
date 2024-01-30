using Digitas.Quotes.Worker.Domain.Interfaces;

namespace Digitas.Quotes.Worker.Infra.Persistence.Uow;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext context;

    public UnitOfWork(ApplicationDbContext context)
    {
        this.context = context;
    }

    public void Commit()
    {
        try
        {
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }
}
