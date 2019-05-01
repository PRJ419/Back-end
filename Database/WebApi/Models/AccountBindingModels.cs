using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace WebApi.Models
{
        public class RegisterBindingModel
        {
            [Required]
            [Display(Name = "Email")]
            [MaxLength(150)]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

           //[Required]
           //public DateTime Birthday { get; set; }
            
           [Required]
           [MaxLength(50)]
           public string Username { get; set; }

           public string Role { get; set; }
           /// <summary>
           /// Property for getting and setting the birthday of a customer
           /// </summary>
           [Required]
           public DateTime DateOfBirth { get; set; }
           /// <summary>
           /// Property for getting and setting the name of a customer
           /// </summary>
           [Required]
           [MaxLength(150)]
           public string Name { get; set; }
           /// <summary>
           /// Property for getting and setting the favorite bar of a customer
           /// </summary>
           [MaxLength(150)]
           public string FavoriteBar { get; set; }

           /// <summary>
           /// Property for getting and setting the favorite drink of a customer
           /// </summary>
           [MaxLength(50)]
           public string FavoriteDrink { get; set; }


    }
        public class AdminRegisterBindingModel
        {
            [Required]
            [Display(Name = "Email")]
            [MaxLength(150)]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            //[Required]
            //public DateTime Birthday { get; set; }

            [Required]
            [MaxLength(50)]
            public string Username { get; set; }

            public string Role { get; set; }
            /// <summary>
            /// Property for getting and setting the birthday of a customer
            /// </summary>
            [Required]
            public DateTime DateOfBirth { get; set; }
            /// <summary>
            /// Property for getting and setting the name of a customer
            /// </summary>
            [Required]
            [MaxLength(150)]
            public string Name { get; set; }
            /// <summary>
            /// Property for getting and setting the favorite bar of a customer
            /// </summary>
            [MaxLength(150)]
            public string FavoriteBar { get; set; }

            /// <summary>
            /// Property for getting and setting the favorite drink of a customer
            /// </summary>
            [MaxLength(50)]
            public string FavoriteDrink { get; set; }


        }
    public class BarRepRegisterBindingModel
        {
            [Required]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }


            [Required]
            public string Username { get; set; }

            public string Role { get; set; }
            /// <summary>
            /// Property for the name of the representative. This is required and has a max length of 150.
            /// </summary>
            [Required]
            [MaxLength(150)]
            public string Name { get; set; }


            /// <summary>
            /// Property for the foreign key to the bars bar name. Has a max length of 150 just like in bar.
            /// </summary>
            [MaxLength(150)]
            public string BarName { get; set; }

            [Required]
            [MaxLength(255)]
            public string Address { get; set; }

            [Required]
            public int AgeLimit { get; set; }

            [MaxLength(255)]
            public string Educations { get; set; }

            [MaxLength(500)]
            public string ShortDescription { get; set; }

            [MaxLength(2500)]
            public string LongDescription { get; set; }

            //[MaxLength(8)]
            [Required]
            public int CVR { get; set; }

            //[MaxLength(10)]
            public int PhoneNumber { get; set; }

            [Range(0.0, 5.0)]
            public double AvgRating { get; set; }

            public string Image { get; set; }


    }
    public class ChangePasswordBindingModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class LoginBindingModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }

        }
}   
