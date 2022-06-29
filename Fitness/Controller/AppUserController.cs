using Fitness.Authorization;
using Fitness.Controller.Contracts;
using Fitness.Data.access;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controller
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private Database _data;
        private AppUserAuthentication _appUserAuthentication;
        public AppUserController(
            Database data,            
            AppUserAuthentication appUserAuthentication)
        {
            _data = data;
            _appUserAuthentication = appUserAuthentication;
        }

        [HttpPost]
        [Route("login")]
        public string Login(LoginContract login)
        {
            // TODO invalidate old token
            var appUser = _data.AppUser.GetByEmail(login.Email);
            // Validate
            if (!_appUserAuthentication.Login(login.Email, login.Password, appUser))
            {
                throw new System.Exception( "Invalid email or password");
            }

            return _appUserAuthentication.GetJWT(appUser.Id).ToString();
        }

        //[HttpPost]
        //[Route("logout")]
        //public string Logout()
        //{            
        //    return "success";
        //}

        [HttpPost]
        [Route("register")]
        public string Register(RegisterContract registerContract)
        {
            if (!Util.IsValidEmail(registerContract.Email))
            {
                throw new System.Exception("invalid email format");
            }

            // create AppUser
            var newAppUser = registerContract.ToUser();
            var passwordResult = _appUserAuthentication.CreatePasswordHash(newAppUser.Password);
            newAppUser.Password = passwordResult.PasswordHash;
            newAppUser.Salt = passwordResult.Salt;
            var appuserid = _data.AppUser.InsertAppUser(newAppUser);
            return _appUserAuthentication.GetJWT(appuserid);
        }

        [HttpPost]
        [Route("resetpass")]
        public string ResetPassword()
        {            
            // send new password            
            return "not implemented";
        }
    }
}
