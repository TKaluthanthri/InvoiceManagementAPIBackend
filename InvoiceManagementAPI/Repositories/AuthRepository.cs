using InvoiceManagementAPI.DBOperations;
using InvoiceManagementAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InvoiceManagementAPI.Repositories
{
    public class AuthRepository : IDisposable, IAuthRepository
    {
        private DBContext _context;

        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _context = new DBContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_context));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };
                
            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }


        public void Dispose()
        {
            _context.Dispose();
            _userManager.Dispose();

        }


    }
}