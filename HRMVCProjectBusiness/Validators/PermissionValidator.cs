using FluentValidation;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Validators
{
    public class PermissionValidator: AbstractValidator<Permission>
    {
        public PermissionValidator()
        {
            RuleFor(x => x.AdressToGo).Matches(@"(?!^\d+$)^.+$").WithMessage("Lütfen geçerli bir adres giriniz.").NotEmpty().WithMessage("Lütfen adres giriniz.");
            RuleFor(x => x.AdressToGo).Matches(@"^\d+$").WithMessage("Lütfen geçerli bir adres giriniz.");

        }
    }
}
