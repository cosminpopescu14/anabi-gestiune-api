using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anabi.Domain.Common.Address
{
    public class AddMinimalAddress : IAddAddressMinimal
    {
        public string CountyCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
    }

        public class EmptyAddMinimalAddressValidator : AbstractValidator<IAddAddressMinimal>
        {
            public EmptyAddMinimalAddressValidator()
            {
                RuleFor(m => m.CountyCode).Empty();
                RuleFor(m => m.City).Empty();
                RuleFor(m => m.Street).Empty();
                RuleFor(m => m.Building).Empty();
                //RuleFor(m => m.Building).Empty();
                //RuleFor(m => m.Stair).Empty();
                //RuleFor(m => m.Floor).Empty();
                //RuleFor(m => m.FlatNo).Empty();
            }
        }

        public class AddMinimalAddressValidator : AbstractValidator<IAddAddressMinimal>
        {
            private readonly IDatabaseChecks checks;
            public AddMinimalAddressValidator(IDatabaseChecks checks)
            {
                this.checks = checks;
                AddRules();
            }

            public void AddRules()
            {
                RuleFor(m => m.City).NotEmpty().Length(1, 30).WithMessage("INVALID_CITY");
                RuleFor(m => m.CountyCode).NotEmpty().Length(1, 2).WithMessage("INVALID_COUNTY_CODE_MIN_1_MAX_2");
                RuleFor(m => m.Street).NotEmpty().Length(1, 100).WithMessage("INVALID_STREET_NAME");
                RuleFor(m => m.Building).MaximumLength(30).WithMessage("BUILDING_TOO_LONG_MAX_30");
                //RuleFor(m => m.Stair).MaximumLength(5).WithMessage("STAIR_TOO_LONG_MAX_5");
                //RuleFor(m => m.Floor).MaximumLength(5).WithMessage("FLOOR_TOO_LONG_MAX_5");
                //RuleFor(m => m.FlatNo).MaximumLength(5).WithMessage("FLATNO_TOO_LONG_MAX_5");

                RuleFor(m => m.CountyCode).MustAsync(checks.CountyExists).WithMessage("INVALID_COUNTY_CODE");
            }
        }
}
