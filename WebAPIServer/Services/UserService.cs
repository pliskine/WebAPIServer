using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIServer.Services
{
    public class UserService : IUserService
    {
        //infos en BDD
        public List<User> Users { get; }
        public List<RefreshTokens> RefreshTokens { get; }
        //fin infos en BDD

        private readonly IConfiguration configuration;

        public UserService(IConfiguration configuration)
        {
            this.configuration = configuration;
            Users = new List<User>();
            RefreshTokens = new List<RefreshTokens>();

            Users.Add(new User
            {
                Username = "toto",
                Password = "titi",
                Country = "fr",
                Id = 1,
                Locked = false,
                Role = "admin"
            });
        }

        public AuthenticateResponse Authenticate(string username, string password)
        {
            //identifier le user
            var usr = Users.SingleOrDefault(u => u.Username == username && u.Password == password);

            //si non identifié, renvoie null
            if (usr == null)
                return null;

            //créer le token
            string tokenString = GetNewJwt(usr);

            //création de refresh token
            var refreshTkn = new Guid().ToString();

            RefreshTokens.Add(new RefreshTokens
            {
                Id = 0,
                TokenValue = refreshTkn,
                UserId = usr.Id,
                ValidBefore = DateTime.Now.AddDays(7)
            });


            return new AuthenticateResponse { Jwt = tokenString, RefreshToken = refreshTkn };
        }

        private string GetNewJwt(User usr)
        {
            //1- les claims
            var claims = new List<Claim>
            {
                new Claim("username", usr.Username),
                new Claim("country", usr.Country),
                new Claim("role", usr.Role)
            };
            //string secretKey = configuration["secretKey"]; //"aaaaaBBBBBcccccEEEEEfffffGGGGG12";
            string secretKey = configuration.GetValue<string>("secretKey"); //"aaaaaBBBBBcccccEEEEEfffffGGGGG12";
            byte[] byteSecret = Encoding.UTF8.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(byteSecret);

            var algorithme = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithme);


            //jwt security Token
            var tokenObject = new JwtSecurityToken(
                issuer: "CciCampus",
                audience: "clients",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signingCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return tokenString;
        } 
        

        public AuthenticateResponse RenewToken(string username, string refreshToken)
        {

            //verif du refreshToken correspondant au user

            //si pas de correspondance, renvoie null

            //créer le token

            throw new NotImplementedException();

        }
    }
}
