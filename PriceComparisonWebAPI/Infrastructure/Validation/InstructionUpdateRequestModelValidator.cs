﻿using Domain.Models.Request.Products;
using FluentValidation;

namespace PriceComparisonWebAPI.Infrastructure.Validation
{
    public class InstructionUpdateRequestModelValidator : AbstractValidator<InstructionUpdateRequestModel>
    {
        public InstructionUpdateRequestModelValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be greater than 0");
            RuleFor(x => x.InstructionUrl)
                .NotEmpty().WithMessage("InstructionUrl is required.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("InstructionUrl must be a valid URL address.");
        }
    }
}
