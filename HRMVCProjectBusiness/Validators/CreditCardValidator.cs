using FluentValidation;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Validators
{
    public class CreditCardValidator:AbstractValidator<CreditCard>
    {
        public CreditCardValidator()
        {
            RuleFor(x => x.CardNumber.ToString()).Matches("^4[0-9]{12}(?:[0-9]{3})?$").WithMessage("Lütfen geçerli bir kart numarası giriniz.(Sadece Visa kartlar kabul edilmektedir.)").NotEmpty().WithMessage("Lütfen kart numarası giriniz.");
            RuleFor(x => x.ExpirationDate).GreaterThan(DateTime.Now).WithName("Kartınızın son kullanma tarihi").WithMessage("Tarih geçmiş!");
            RuleFor(x => x.CardBalance.ToString()).Matches(@"^[0-9]*$").WithMessage("Lütfen geçerli bir tutar giriniz.").NotEmpty().WithMessage("Lütfen tutar giriniz.").MaximumLength(9).WithMessage("100.000.000 TL'den fazla bakiyeler kabul edilmemektedir.").MinimumLength(4).WithMessage("1.000 TL'den düşük bakiyeli kart giremezsiniz.");

        }
    }
}
