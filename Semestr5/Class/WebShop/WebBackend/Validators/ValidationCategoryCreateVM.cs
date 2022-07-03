using FluentValidation;
using WebBackend.Model;

namespace WebBackend.Validators
{
    public class ValidationCategoryCreateVM : AbstractValidator<CategoryCreateVM>
    {
        public ValidationCategoryCreateVM()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Вкажіть назву категорії");
            RuleFor(c => c.ImageBase64)
                .NotEmpty()
                .WithMessage("Оберіть фото для категорії");
        }
    }
}
