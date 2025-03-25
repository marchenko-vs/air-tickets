using AirTickets.Utils;
using AirTickets.Data;
using AirTickets.Models;
using AirTickets.Repositories;
using System.Security.Cryptography;
using AirTickets.Exceptions;
using AutoMapper;
using AirTickets.BlModels;

namespace AirTickets.Services
{
    public class UserService
    {
        private readonly IUserRepository<User> _db;
        private readonly MapperConfiguration _cfg;
        private readonly Mapper _mapper;

        public UserService(BlitzFlugContext context)
        {
            _db = new UserRepository(context);
            _cfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlUser, User>();
                cfg.CreateMap<User, BlUser>();
            });
            _mapper = new Mapper(_cfg);
        }

        private bool Verify(string email, string password)
        {
            string savedPasswordHash = _db.ReadByEmail(email).Password;

            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash1 = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash1[i])
                {
                    return false;
                }
            }

            return true;
        }

        public BlUser Register(BlUser user)
        {
            if (user == null)
            {
                throw new Exception();
            }
            else if (user.Email.Length < 3)
            {
                throw new Exception();
            }
            else if (user.Password.Length < 6)
            {
                throw new Exception();
            }

            user.Role = "клиент";
            user.RegDate = DateTime.Now;
            user.Password = Security.Encrypt(user.Password);

            try
            {
                var createdUser = _db.Create(_mapper.Map<User>(user));
                return _mapper.Map<BlUser>(createdUser);
            }
            catch (Exception)
            {
                throw new ExistingUserException(String.Format("Пользователь {0} уже зарегистрирован", user.Email));
            }
        }

        public BlUser Login(string email, string password)
        {
            var user = _db.ReadByEmail(email);

            if (null == user)
            {
                throw new NotExistingUserException(String.Format("Пользователь {0} не найден", email));
            }

            if (false == this.Verify(email, password))
            {
                throw new IncorrectPasswordException(String.Format("Введен неверный пароль", email));
            }

            return _mapper.Map<BlUser>(user);
        }

        public BlUser ChangeSettings(BlUser user, string newPassword)
        {
            if (user == null)
            {
                throw new Exception();
            }
            else if (user.Email.Length > 0 && user.Email.Length < 3)
            {
                throw new Exception();
            }
            else if (user.Password.Length > 0 && user.Password.Length < 6)
            {
                throw new Exception();
            }

            var existingUser = _db.Read(user.Id);

            if (existingUser == null)
            {
                throw new NotFoundException();
            }

            if (false == this.Verify(existingUser.Email, user.Password))
            {
                throw new IncorrectPasswordException(String.Format("Введен неверный пароль", existingUser.Email));
            }

            if (user.Email.Length > 0)
            {
                existingUser.Email = user.Email;
            }
            if (user.Password.Length > 0)
            {
                existingUser.Password = Security.Encrypt(newPassword);
            }
            if (user.FirstName.Length > 0)
            {
                existingUser.FirstName = user.FirstName;
            } 
            if (user.LastName.Length > 0)
            {
                existingUser.LastName = user.LastName;
            }

            try
            {
                var updatedUser = _db.Update(existingUser);
                return _mapper.Map<BlUser>(updatedUser);
            }
            catch (Exception)
            {
                throw new ExistingUserException(String.Format("Почта {0} уже используется", user.Email));
            }
        }

        public BlUser? ReadCurrent(string email)
        {
            if (email.Length < 3)
            {
                throw new Exception();
            }

            return _mapper.Map<BlUser>(_db.ReadByEmail(email));
        }

        public List<BlUser> ReadAll()
        {
            return _mapper.Map<List<BlUser>>(_db.ReadAll());
        }

        public void Delete(Int64 id)
        {
            _db.Delete(id);
        }
    }
}
