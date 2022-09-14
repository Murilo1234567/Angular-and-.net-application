using FluentValidation;
using Domain.Entities;

namespace Service.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Please, enter a name for the product")
                                .NotNull().WithMessage("Please, enter a name for the product");
            
            RuleFor(c => c.Image).NotEmpty().WithMessage("Please, chose a image for the product")
                                .NotNull().WithMessage("Please, chose a image for the product");
            
            RuleFor(c => c.Description).NotEmpty().WithMessage("Please, enter a description for the product")
                                .NotNull().WithMessage("Please, enter a description for the product");
            
            RuleFor(c => c.Stock).NotEmpty().WithMessage("Please, enter a stock quantity for the product")
                                .NotNull().WithMessage("Please, enter a stock quantity for the product");
            
            RuleFor(c => c.Status).NotEmpty().WithMessage("Please, chose a status for the product")
                                .NotNull().WithMessage("Please, chose a status for the product");
            
            RuleFor(c => c.Price).NotEmpty().WithMessage("Please, enter a price for the product")
                                .NotNull().WithMessage("Please, enter a price for the product");
        }
    }
}