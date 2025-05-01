using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ValidationRules
{
    public class PatientValidator:AbstractValidator<Patient>
    {
        public PatientValidator()
        {
                RuleFor(x=>x.FirstName ).NotEmpty().WithMessage("Ad Soyad boş geçilemez");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Ad Soyad boş geçilemez");
            RuleFor(x => x.TcNo).NotEmpty().WithMessage("Bu alan boş geçilemez");
            RuleFor(x => x.TcNo).MinimumLength(11).WithMessage("Vatandaşlık numarası 11 haneli");
            RuleFor(x => x.TcNo).MaximumLength(11).WithMessage("Vatandaşlık numarası 11 haneli");
            RuleFor(x => x.Password).MinimumLength(5).WithMessage("Şifrre en az 5 karakter içermeli");




        }
    }
}
