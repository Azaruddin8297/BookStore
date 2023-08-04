using BookStore.User.Entity;
using BookStore.User.Interface;
using BookStore.User.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.User.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserContext _db;
        private readonly IConfiguration _config;
        public UserServices(UserContext db,IConfiguration config) 
        {
            _db = db;
            _config = config;
        }
        public UserEntity Register(UserModel newUser)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.Email = newUser.Email.ToLower();
            userEntity.Name = newUser.Name;
            userEntity.PhoneNumber= newUser.PhoneNumber;
            userEntity.Password = newUser.Password;
            _db.Users.Add(userEntity);
            _db.SaveChanges();
            return userEntity;
        }
        public string Login(string email, string password)
        {
            UserEntity user = _db.Users.FirstOrDefault(x => x.Email == email && x.Password == password);    
            if(user != null)
            {
                string token = GenerateToken(user.UserId,user.Email);
                return token;
            }
            return null;
        }
        public bool ForgetPassword(string email)
        {
            var Result = _db.Users.FirstOrDefault(x => x.Email == email);
            if (Result != null)
            {
                var Token = GenerateToken(Result.UserId, Result.Email);
                new MSMQModel().sendData2Queue(Token);
                return true;
            }
            return false;
        }
        public bool ResetPassword(string newPassword, string confirmPassword,string email)
        {
            UserEntity someUser = _db.Users.FirstOrDefault(x => x.Email == email);
            if (someUser != null && newPassword == confirmPassword)
            {
                someUser.Password = newPassword;
                _db.Users.Update(someUser);
                _db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public UserEntity GetUser(int userId)
        {
            var result = _db.Users.FirstOrDefault(x => x.UserId == userId);

            if (result != null)
            {
                result.Password = null;

                return result;
            }
            else
            {
                return null;
            }
        }

        private string GenerateToken(int userId,string email)
        {
            byte[] key = Encoding.ASCII.GetBytes(_config["JWT-Key"]);

            SecurityTokenDescriptor tokenDescripter = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim (ClaimTypes.Email, email),
                new Claim (ClaimTypes.Sid, userId.ToString()),
                  new Claim ("userId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescripter);

            return tokenHandler.WriteToken(token);
        }
    }
}
