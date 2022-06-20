using FluentValidation;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Validators
{
    public class AdvancePaymentValidator: AbstractValidator<AdvancePayment>
    {
        public AdvancePaymentValidator()
        {
            RuleFor(x => x.Description).Matches(@"(?!^\d+$)^.+$").WithMessage("Lütfen geçerli bir açıklama giriniz.").WithMessage("Lütfen açıklama giriniz.");
            RuleFor(x => x.Amount.ToString()).Matches(@"^[0-9]*$").WithMessage("Lütfen geçerli bir tutar giriniz.").NotEmpty().WithMessage("Lütfen tutar giriniz.");

        }
    }
}
