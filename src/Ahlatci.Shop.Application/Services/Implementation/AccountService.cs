using Ahlatci.Shop.Application.Behaviors;
using Ahlatci.Shop.Application.Exceptions;
using Ahlatci.Shop.Application.Models.Dtos.Accounts;
using Ahlatci.Shop.Application.Models.RequestModels.Accounts;
using Ahlatci.Shop.Application.Services.Abstraction;
using Ahlatci.Shop.Application.Validators.Accounts;
using Ahlatci.Shop.Application.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Domain.UWork;
using Ahlatci.Shop.Utils;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ahlatci.Shop.Application.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUnitWork _uWork;
        private readonly IConfiguration _configuration;

        public AccountService(IMapper mapper, IUnitWork uWork, IConfiguration configuration)
        {
            _mapper = mapper;
            _uWork = uWork;
            _configuration = configuration;

        }

        /// <summary>
        /// Yeni bir kullanıcı oluşturmak için kullanılan metod
        /// </summary>
        /// <param name="createUserVM"></param>
        /// <returns></returns>
        /// <exception cref="AlreadyExistsException"></exception>
        [ValidationBehavior(typeof(CreateUserValidator))]
        public async Task<Result<bool>> CreateUser(CreateUserVM createUserVM)
        {
            var result = new Result<bool>();

            //Aynı kullanıcı adı daha önce girilmiş mi.
            var usernameExists = await _uWork.GetRepository<Account>().AnyAsync(x => x.Username.Trim().ToUpper() == createUserVM.Username.Trim().ToUpper());
            if (usernameExists)
            {
                throw new AlreadyExistsException($"{createUserVM.Username} kullanıcı adı daha önce seçilmiştir. Lütfen farklı bir kullanıcı adı belirleyiniz.");
            }

            //Eposta adresi kullanılıyor mu.
            var emailExists = await _uWork.GetRepository<Customer>().AnyAsync(x => x.Email.Trim().ToUpper() == createUserVM.Email.Trim().ToUpper());
            if (emailExists)
            {
                throw new AlreadyExistsException($"{createUserVM.Email} eposta adresi kullanılmaktadır. Lütfen farklı bir kullanıcı adı belirleyiniz.");
            }

            //Gelen model Customer türüne maplandi
            var customerEntity = _mapper.Map<Customer>(createUserVM);
            //Gelen model Account türüne maplandi.
            var accountEntity = _mapper.Map<Account>(createUserVM);
            //Kullanıcının parolasını şifreleyerek kaydedelim.
            accountEntity.Password = CipherUtil
                .EncryptString(_configuration["AppSettings:SecretKey"], accountEntity.Password);

            accountEntity.Customer = customerEntity;

            _uWork.GetRepository<Customer>().Add(customerEntity);
            _uWork.GetRepository<Account>().Add(accountEntity);
            result.Data = await _uWork.CommitAsync();

            return result;
        }

        /// <summary>
        /// Gönderilen kullanıcı adı ve parola ile login işlemini gerçekleştirir.
        /// </summary>
        /// <param name="loginVM"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<Result<TokenDto>> Login(LoginVM loginVM)
        {
            var result = new Result<TokenDto>();

            var hashedPassword = CipherUtil.EncryptString(_configuration["AppSettings:SecretKey"], loginVM.Password);

            var existsUser = await _uWork.GetRepository<Account>().GetSingleByFilterAsync(x => x.Username == loginVM.Username && x.Password == hashedPassword);

            if(existsUser is null)
            {
                throw new NotFoundException($"{loginVM.Username} kullanıcı adına sahip kullanıcı bulunamadı ye da parola hatalıdır.");
            }

            var expireMinute = Convert.ToInt32(_configuration["Jwt:Expire"]);
            var expireDate = DateTime.Now.AddMinutes(expireMinute);

            //Token'i üret ve return et.
            var tokenString = GenerateJwtToken(existsUser, expireDate);

            result.Data = new TokenDto
            {
                Token = tokenString,
                ExpireDate = expireDate
            };

            return result;
        }


        private string GenerateJwtToken(Account account, DateTime expireDate)
        {
            var secretKey = _configuration["Jwt:SigningKey"];
            var issuer = _configuration["Jwt:Issuer"];
            var audiance = _configuration["Jwt:Audiance"];

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Role,((int)account.Role).ToString()),
                new Claim("Username",account.Username),
                new Claim("Email",account.Customer.Email)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience=audiance,
                Issuer=issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = expireDate, // Token süresi (örn: 20 dakika)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
