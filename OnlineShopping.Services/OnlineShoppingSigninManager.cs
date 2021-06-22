using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using OnlineShopping.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Services
{
    public class OnlineShoppingSignInManager : SignInManager<OnlineShoppingUser, string>
    {
        public OnlineShoppingSignInManager(OnlineShoppingUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(OnlineShoppingUser user)
        {
            return user.GenerateUserIdentityAsync((OnlineShoppingUserManager)UserManager);
        }

        public static OnlineShoppingSignInManager Create(IdentityFactoryOptions<OnlineShoppingSignInManager> options, IOwinContext context)
        {
            return new OnlineShoppingSignInManager(context.GetUserManager<OnlineShoppingUserManager>(), context.Authentication);
        }
    }
}
