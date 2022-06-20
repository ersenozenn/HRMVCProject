using FluentValidation;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Validators
{
    public class CostValidator: AbstractValidator<Cost>
    {
        public CostValidator()
        {
            RuleFor(x => x.Amount.ToString()).Matches(@"^[0-9]*$").WithMessage("Lütfen geçerli bir tutar giriniz.").NotEmpty().WithMessage("Lütfen tutar giriniz.");

        }
    }
}
