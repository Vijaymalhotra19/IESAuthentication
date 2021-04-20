using JWTAuthentication_TokenBarer.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthentication_TokenBarer.Models
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        public AuthenticateService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        private List<Student> students = new List<Student>()
{
new Student{StudentId=200476776, Fname = "Vijay", Lname="Malhotra", UserName="vijay", Password ="malhotra"}
};
        public Student Authenticate(string userName, string password)
        {
            var student = students.SingleOrDefault(x => x.UserName == userName && x.Password == password);
            //return null if student not found
            if (student == null)
            {
                return null;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
new Claim(ClaimTypes.Name, student.StudentId.ToString()),
new Claim(ClaimTypes.Role, "Admin"),
new Claim(ClaimTypes.Version, "V3.1")
}),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                student.Token = tokenHandler.WriteToken(token);
                return student;
                student.Password = null;
            }
        }
    }
}
