﻿using Domain.Models.Request.Products;
using FluentValidation;

namespace PriceComparisonWebAPI.Infrastructure.Validation
{
    public class ReviewCreateRequestModelValidator : AbstractValidator<ReviewCreateRequestModel>
    {
        public ReviewCreateRequestModelValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be greater than 0");
            RuleFor(x => x.ReviewUrl)
                .NotEmpty().WithMessage("ReviewUrl is required")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("ReviewUrl must be a valid URL");
        }
    }
}
