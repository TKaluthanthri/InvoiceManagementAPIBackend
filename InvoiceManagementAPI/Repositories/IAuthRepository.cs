using InvoiceManagementAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementAPI.Repositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterUser(UserModel userModel);
        Task<IdentityUser> FindUser(string userName, string password);
    }
}
