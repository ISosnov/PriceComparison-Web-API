﻿using Domain.Models.Request.Products;
using FluentValidation;

namespace PriceComparisonWebAPI.Infrastructure.Validation.Product
{
    public class ReviewUpdateRequestModelValidator : AbstractValidator<ReviewUpdateRequestModel>
    {
        public ReviewUpdateRequestModelValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Review Id must be greater than 0");
            
            RuleFor(x => x.BaseProductId)
                .GreaterThan(0).WithMessage("BaseProductId must be greater than 0");

            RuleFor(x => x.ReviewUrl)
                .NotEmpty().WithMessage("ReviewUrl is required")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("ReviewUrl must be a valid URL");
        }
    }
}
