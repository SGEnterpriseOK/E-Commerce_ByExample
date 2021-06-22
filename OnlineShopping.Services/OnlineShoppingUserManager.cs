using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using OnlineShopping.Data;
using OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Services
{
    public class OnlineShoppingUserManager : UserManager<OnlineShoppingUser>
    {
        public OnlineShoppingUserManager(IUserStore<OnlineShoppingUser> store)
            : base(store)
        {
        }

        public static OnlineShoppingUserManager Create(IdentityFactoryOptions<OnlineShoppingUserManager> options, IOwinContext context)
        {
            var manager = new OnlineShoppingUserManager(new UserStore<OnlineShoppingUser>(context.Get<OnlineShoppingContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<OnlineShoppingUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<OnlineShoppingUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<OnlineShoppingUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

           // manager.EmailService = new EmailService();
           // manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<OnlineShoppingUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
