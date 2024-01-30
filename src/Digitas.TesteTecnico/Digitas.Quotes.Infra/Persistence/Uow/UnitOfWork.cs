using Digitas.Quotes.Domain.Interfaces;

namespace Digitas.Quotes.Infra.Persistence.Uow;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly SimulationDbContext context;

    public UnitOfWork(SimulationDbContext context)
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
