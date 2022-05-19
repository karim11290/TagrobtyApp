using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using QuizbeePlus.Commons;
using QuizbeePlus.Data;
using QuizbeePlus.Entities;
using QuizbeePlus.Entities.CustomEntities;
using QuizbeePlus.MobileContract;
using QuizbeePlus.Providers;
using QuizbeePlus.Results;
using QuizbeePlus.Services;
using QuizbeePlus.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace QuizbeePlus.Controllers
{
    [RoutePrefix("api/App")]
    public class AppController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        

        public UserManager<QuizbeeUser> AppUser { get; private set; }
        private QuizbeeUserManager _userManager;
        public AppController()
            : this(new UserManager<QuizbeeUser>(new UserStore<QuizbeeUser>(new QuizbeeContext())))
        {
        }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
        public AppController(QuizbeeUserManager userManager,
        ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public QuizbeeUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<QuizbeeUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public AppController(UserManager<QuizbeeUser> AppUser)
        {
            this.AppUser = AppUser;

        }
        #region User Identity APIS
        // POST api/App/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // POST api/App/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/App/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }
        // POST api/App/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/App/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/App/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            QuizbeeUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                   OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/App/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/App/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new QuizbeeUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/App/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // POST api/App/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new QuizbeeUser() { UserName = info.Email, Email = info.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        # endregion


        [HttpPost]
        [Route("SignUp")]
        public dynamic SignUp(RegisterRequestAPI RegisterRequestAPI)
        {
            ReponseAPI reponseAPI = new ReponseAPI();

            if (RegisterRequestAPI.APIKey == null)
            {
                reponseAPI = new ReponseAPI()
                {
                    Status = "403",
                    Error = "API key is missing.",

                };
                return reponseAPI;
            }
            if (RegisterRequestAPI.APIKey != "APIKey")
            {
                reponseAPI = new ReponseAPI()
                {
                    Status = "401",
                    Error = "Invalid API key.",

                };
                return reponseAPI;
            }
            if (RegisterRequestAPI.UserName == null)
            {
                reponseAPI = new ReponseAPI()
                {
                    Status = "400",
                    Error = "Please provide username.",

                };
                return reponseAPI;
            }
            if (RegisterRequestAPI.Password == null)
            {
                reponseAPI = new ReponseAPI()
                {
                    Status = "400",
                    Error = "Please provide password.",

                };
                return reponseAPI;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(RegisterRequestAPI.UserName))
                {
                    RegisterRequestAPI.UserName = RegisterRequestAPI.UserName.Trim();
                }
                if (!string.IsNullOrWhiteSpace(RegisterRequestAPI.Password))
                {
                    RegisterRequestAPI.Password = RegisterRequestAPI.Password.Trim();
                }
                using (var context = new QuizbeeContext())
                {
                    QuizbeeUser QuizbeeUser = context.Users.Where(a => a.UserName == RegisterRequestAPI.UserName && a.Password==RegisterRequestAPI.Password).FirstOrDefault();

                    if (QuizbeeUser != null)
                    {
                        reponseAPI = new ReponseAPI()
                        {
                            Status = "500",
                            AuthKey = "this user is already Found",

                        };
                        return reponseAPI;
                    }
                    else
                    {

                        var user = new QuizbeeUser { UserName = RegisterRequestAPI.UserName, Email = RegisterRequestAPI.Email, RegisteredOn = DateTime.Now,Password=RegisterRequestAPI.Password };
                        AppUser.Create(user, RegisterRequestAPI.Password);
                        AppUser.AddToRole(user.Id, "User");

                        reponseAPI = new ReponseAPI()
                        {
                            Status = "201",
                           

                        };
                        return reponseAPI;
                    }
                }
            }

        }

    
        [HttpPost]
        [Route("filter")]

        public dynamic filter()
        {
            //return true;
            return new string[] { "You are successfully Authenticated to Access the Service" };
        }

        [HttpGet]
        [Route("GetCategories")]
        public List<Category> GetCategories(string APIKey)
        {
            using (var context = new QuizbeeContext())
            {
                return context.Categories.ToList();
            }
        }
        [HttpPost]
        [Route("Login")]
        public ReponseAPI Login(LoginReuestAPI LoginReuestAPI)
        {
            ReponseAPI reponseAPI = new ReponseAPI();

            if (LoginReuestAPI.APIKey == null)
            {
                reponseAPI = new ReponseAPI()
                {
                    Status = "403",
                    Error = "API key is missing.",

                };
                return reponseAPI;
            }
            if (LoginReuestAPI.APIKey != "APIKey")
            {
                reponseAPI = new ReponseAPI()
                {
                    Status = "401",
                    Error = "Invalid API key.",

                };
                return reponseAPI;
            }
            if (LoginReuestAPI.UserName == null)
            {
                reponseAPI = new ReponseAPI()
                {
                    Status = "400",
                    Error = "Please provide username.",

                };
                return reponseAPI;
            }
            if (LoginReuestAPI.Password == null)
            {
                reponseAPI = new ReponseAPI()
                {
                    Status = "400",
                    Error = "Please provide password.",

                };
                return reponseAPI;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(LoginReuestAPI.UserName))
                {
                    LoginReuestAPI.UserName = LoginReuestAPI.UserName.Trim();
                }
                if (!string.IsNullOrWhiteSpace(LoginReuestAPI.Password))
                {
                    LoginReuestAPI.Password = LoginReuestAPI.Password.Trim();
                }
                using (var context = new QuizbeeContext())
                {
                    QuizbeeUser QuizbeeUser = context.Users.Where(a => a.UserName == LoginReuestAPI.UserName && a.Password == LoginReuestAPI.Password).FirstOrDefault();
                    if (QuizbeeUser != null)
                    {
                        var TokenString = UsersService.Instance.GetToken(LoginReuestAPI.UserName, LoginReuestAPI.Password);

                        

                        reponseAPI = new ReponseAPI()
                        {
                            Status = "201",
                            AuthKey = TokenString,

                        };
                        return reponseAPI;
                    }
                    else
                    {
                        reponseAPI = new ReponseAPI()
                        {
                            Status = "500",
                            Error = "Something went wrong. Please try again later.",

                        };
                        return reponseAPI;
                    }
                }
            }

        }



        [HttpPost]
        [Route("PasswordRecovery")]

        public dynamic PasswordRecovery(string Email)
        {
            Email = Email.Trim();
            var currentuser = AppUser.FindByEmail(Email);

            if (currentuser != null)
            {
                using (var context = new QuizbeeContext())
                {
                    QuizbeeUser UserObj = context.Users.Where(a => a.Email == Email).FirstOrDefault();
                    if (UserObj != null)
                    {
                        BaseLogics.PasswordForgotEmailSend(UserObj);
                        return "Password Reset Successfully. Please Check Email...!";
                    }
                }

            }
            return "Please Enter Correct Email Address.";
        }


      



        #region Helpers
        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }



        #endregion
    }

}
