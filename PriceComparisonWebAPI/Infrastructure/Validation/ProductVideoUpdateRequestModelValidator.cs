﻿using Domain.Models.Request.Products;
using FluentValidation;

namespace PriceComparisonWebAPI.Infrastructure.Validation
{
    public class ProductVideoUpdateRequestModelValidator : AbstractValidator<ProductVideoUpdateRequestModel>
    {
        public ProductVideoUpdateRequestModelValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be greater than 0.");
            RuleFor(x => x.VideoUrl)
                .NotEmpty().WithMessage("VideoUrl is required.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("VideoUrl must be a valid URL address.");
        }
    }
}
