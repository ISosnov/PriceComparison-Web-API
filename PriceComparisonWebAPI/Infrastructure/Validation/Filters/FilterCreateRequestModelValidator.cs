﻿using Domain.Models.Request.Filters;
using FluentValidation;

namespace PriceComparisonWebAPI.Infrastructure.Validation.Filters
{
    public class FilterCreateRequestModelValidator : AbstractValidator<FilterCreateRequestModel>
    {
        public FilterCreateRequestModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must be less than 100 characters.");

            RuleFor(x => x.ShortTitle)
                .NotEmpty().WithMessage("ShortTitle is required.")
                .MaximumLength(50).WithMessage("ShortTitle must be less than 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must be less than 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
