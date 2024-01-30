using Digitas.Quotes.Application.Order.Simulation.Commands;
using FluentValidation;

namespace Digitas.Quotes.Application.Order.Simulation.Validators;

public class SimulationCommandValidator : AbstractValidator<SimulationCommand>
{
    public SimulationCommandValidator()
    {
        RuleFor(c => c.Amount)
            .NotEmpty()
            .WithMessage(item => "Amount is required");

        RuleFor(c => c.Coin)
            .NotEmpty()
            .IsInEnum()
            .WithMessage(item => "Coin is required");

        RuleFor(c => c.Operation)
            .NotEmpty()
            .IsInEnum()
            .WithMessage(item => "Operation is required");
    }
}
