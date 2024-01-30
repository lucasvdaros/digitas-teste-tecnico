using Digitas.Quotes.Application.Order.Simulation.Queries;
using FluentValidation;

namespace Digitas.Quotes.Application.Order.Simulation.Validators;

public class SimulationQueryValidator : AbstractValidator<SimulationQuery>
{
    public SimulationQueryValidator()
    {
        RuleFor(c => c.IdSimulation)
           .NotEmpty()
           .WithMessage(item => "IdSimulation is required");
    }
}
