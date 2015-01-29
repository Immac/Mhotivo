using System.ComponentModel.DataAnnotations;

namespace Mhotivo.Models
{
    public class DisplayUserModel
    {
        public int Id { get; set; }

        [Display(Name = "Correo Elctrónico")]
        public string Email { get; set; }

        [Display(Name = "Nombre")]
        public string DisplayName { get; set; }

        [Display(Name = "Tipo de Usuario")]
        public string Role { get; set; }

        [Display(Name = "Activo")]
        public string Status { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(ErrorMessage = "Debe Ingresar Contraseña actual")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Nueva contraseña")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar la nueva contraseña")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "La nueva contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Debe Ingresar Email de Usuario")]
        [Display(Name = "Email de usuario")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recordar cuenta?")]
        public bool RememberMe { get; set; }
    }

    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Debe Ingresar Nombre")]
        [Display(Name = "Nombre")]
        public string DisplaName { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Email")]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Display(Name = "Estado")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Contraseña")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Tipo de Usuario")]
        [Display(Name = "Tipo de Usuario")]
        public int Id { get; set; }
    }

    public class UserEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Nombre")]
        [Display(Name = "Nombre")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Contraseña")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe Ingresar Tipo de Usuario")]
        [Display(Name = "Tipo de Usuario")]
        public int RoleId { get; set; }
    }
}