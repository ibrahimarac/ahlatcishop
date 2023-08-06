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
        [ValidationBehavior(typeof(RegisterValidator))]
        public async Task<Result<bool>> Register(RegisterVM createUserVM)
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
            //Gelen parolayı şifrele. Çünkü db de şifreli parola var.
            var hashedPassword = CipherUtil.EncryptString(_configuration["AppSettings:SecretKey"], loginVM.Password);
            //Bu kullanıcı adı ve parola ile eşleşen bir kullanıcı var mı
            var existsAccount = await _uWork.GetRepository<Account>().GetSingleByFilterAsync(x => x.Username == loginVM.Username && x.Password == hashedPassword, "Customer");
            //Kullanıcı yoksa hata fırlat.
            if(existsAccount is null)
            {
                throw new NotFoundException($"{loginVM.Username} kullanıcı adına sahip kullanıcı bulunamadı ye da parola hatalıdır.");
            }
            
            //Token expire (sona erme süresi) süresini belirle
            var expireMinute = Convert.ToInt32(_configuration["Jwt:Expire"]);
            var expireDate = DateTime.Now.AddMinutes(expireMinute);

            //Token'i üret ve return et.
            var tokenString = GenerateJwtToken(existsAccount, expireDate);

            result.Data = new TokenDto
            {
                Token = tokenString,
                ExpireDate = expireDate
            };

            return result;
        }

               
        /// <summary>
        /// Kullanıcı bilgilerini güncellemek için kullanılan servis metodu.
        /// </summary>
        /// <param name="updateUserVM"></param>
        /// <returns></returns>
        [ValidationBehavior(typeof(UpdateUserValidator))]
        public async Task<Result<bool>> UpdateUser(UpdateUserVM updateUserVM)
        {
            var result = new Result<bool>();

            var existsCustomer = await _uWork.GetRepository<Customer>().GetById(updateUserVM.Id.Value);

            _mapper.Map(updateUserVM, existsCustomer);

            _uWork.GetRepository<Customer>().Update(existsCustomer);
            result.Data = await _uWork.CommitAsync();

            return result;
        }


        private string GenerateJwtToken(Account account, DateTime expireDate)
        {
            var secretKey = _configuration["Jwt:SigningKey"];
            var issuer = _configuration["Jwt:Issuer"];
            var audiance = _configuration["Jwt:Audiance"];

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Role,account.Role.ToString()),
                new Claim(ClaimTypes.Name,account.Username),
                new Claim(ClaimTypes.Email,account.Customer.Email), //Account entity'sini Customer'a bağlayannavigation property
                new Claim(ClaimTypes.Sid,account.CustomerId.ToString())
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = audiance,
                Issuer = issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = expireDate, // Token süresi (örn: 20 dakika)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
