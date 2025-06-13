using AutoMapper;
using iLoveIbadah.Website.Contracts;
using iLoveIbadah.Website.Models;
using iLoveIbadah.Website.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace iLoveIbadah.Website.Services
{
    public class AuthenticationService : BaseHttpService, Contracts.IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private JwtSecurityTokenHandler _tokenHandler;

        public AuthenticationService(IClient client, ILocalStorageService localStorage, IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(client, localStorage)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._mapper = mapper;
            this._tokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthRequest authenticationRequest = new() { Email = email, PasswordHash = password };
                var authenticationResponse = await _client.LoginAsync(authenticationRequest);
                Console.WriteLine(authenticationResponse);
                if (authenticationResponse.Token != string.Empty)
                {
                    //Get Claims from token and Build auth user object
                    var tokenContent = _tokenHandler.ReadJwtToken(authenticationResponse.Token);
                    if (tokenContent == null)
                    {
                        Console.WriteLine("Failed to parse JWT token");
                        return false;
                    }

                    var claims = ParseClaims(tokenContent);
                    if (claims == null || !claims.Any())
                    {
                        Console.WriteLine("No claims found in token");
                        return false;
                    }
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                    _localStorage.SetStorageValue("token", authenticationResponse.Token);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        private List<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = new List<Claim>();

            try
            {
                // Get claim value safely - never pass null to Claim constructor
                var nameValue = tokenContent.Claims?.FirstOrDefault(x => x.Type == "name")?.Value;
                if (!string.IsNullOrEmpty(nameValue))
                {
                    claims.Add(new Claim(ClaimTypes.Name, nameValue));
                }

                var emailValue = tokenContent.Claims?.FirstOrDefault(x => x.Type == "email")?.Value;
                if (!string.IsNullOrEmpty(emailValue))
                {
                    claims.Add(new Claim(ClaimTypes.Email, emailValue));
                }

                var idValue = tokenContent.Claims?.FirstOrDefault(x => x.Type == "sub" || x.Type == "id")?.Value;
                if (!string.IsNullOrEmpty(idValue))
                {
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, idValue));
                }

                // Add other claims as needed...

                return claims;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ParseClaims error: {ex.Message}");
                return new List<Claim>();
            }
        }
        public async Task<bool> Register(RegisterVM registration)
        {

            RegistrationRequest registrationRequest = _mapper.Map<RegistrationRequest>(registration);
            var response = await _client.RegisterAsync(registrationRequest);
            if (response.Id  > 0)
            {
                await Authenticate(registration.Email, registration.PasswordHash);
                return true;
            }
            return false;
        }

        public async Task Logout()
        {
            _localStorage.ClearStorage(new List<string> { "token" });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        //private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        //{
        //    var claims = tokenContent.Claims.ToList();
        //    claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        //    //claims.Add(new Claim(ClaimTypes.NameIdentifier, tokenContent.Id));
        //    return claims;
        //}
    }
}
