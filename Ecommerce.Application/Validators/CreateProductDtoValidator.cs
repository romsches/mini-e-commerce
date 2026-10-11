using Ecommerce.Application.DTOs;
using FluentValidation;

namespace Ecommerce.Application.Validators;

/**
 * Enterprise Create Product DTO Validator.
 * Implements robust validation rules using FluentValidation to ensure incoming 
 * product creation payloads comply with business invariants and constraints.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

/// <summary>
/// Validation rules for <see cref="CreateProductDto"/> using FluentValidation.
/// Ensures incoming product data satisfies business requirements before processing in CQRS command handlers.
/// </summary>
public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(200).WithMessage("Product name must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");
    }
}