using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.ViewModels
{
    public class ContactViewModel
    {

        [Required] //Özelliğin zorunlu (gerekli) olduğunu bildirir
        [MinLength(5)]//minimum karakter sayısı
        public string Name { get; set; }

        [Required]
        [EmailAddress]//bize gönderilen e-postanın geçerli bir e-posta adresi olduğunu doğrulayacak
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Too Long")]//maksimum girilmesi gereken karakter sayısı
        public string Message { get; set; }

    }
}
