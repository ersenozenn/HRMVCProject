using FluentValidation;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Identity).MinimumLength(11).WithName("Kimlik numarası").MaximumLength(11).NotEmpty().WithMessage("Kimlik no 11 haneli olmak zorundadır!");
            RuleFor(x => x.FirstName).MinimumLength(3).NotEmpty().WithName("Ad").WithMessage("Lütfen geçerli bir isim giriniz.");
            RuleFor(x => x.LastName).MinimumLength(2).NotEmpty().WithName("Soyadı").WithMessage("Lütfen geçerli bir soyad giriniz.");
            RuleFor(x => x.BirthDate).LessThan(DateTime.Now.AddYears(-18)).WithName("Doğum tarihi").WithMessage("Sadece 18 yaşından büyük çalışanları ekleyebilirsiniz!");
            RuleFor(s => s.Email).NotEmpty().WithName("mail").WithMessage("Mail alanını boş bırakmayınız.")
                     .EmailAddress().WithMessage("Geçerli bir mail giriniz.");
            RuleFor(s => s.Wage).NotEmpty().WithName("Maaş").WithMessage("Maaş alanını boş bırakmayınız.").GreaterThan(4800).WithMessage("Asgari ücretten düşük bir maaş veremezsiniz.").LessThanOrEqualTo(10000000).WithMessage("10.000.000 TL'den daha yüksek maaş veremezsiniz.");
                     

            //RuleFor(x => x.PhoneNumber).Matches(@"^[0-9]{10}$").WithMessage("Telefon numarasını doğru formatta giriniz").NotEmpty().WithMessage("Telefon numarası boş bırakılamaz"); //Telefon numarası formatlama deneme

        }
    }
}
