namespace Web.Pizza.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Photo { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class LoginViewModel
    {
        /// <summary>
        /// admin@admin.com
        /// </summary>
        /// <example>admin@admin.com</example>
        public string Email { get; set; }
        /// <summary>
        /// admin@admin.com
        /// </summary>
        /// <example>123456</example>
        public string Password { get; set; }
    }
}
