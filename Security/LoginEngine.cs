using System;
using FinancialControl.DataTransferObjects;
using FinancialControl.Model;

namespace FinancialControl.Security
{
	public class LoginEngine
	{
		public LoginEngine()
		{
		}

		public UserSession DoLogoff()
		{
			return new UserSession();
		}

		public UserSession DoLogin(User user, string password)
		{
			UserBanishedSieve(user);
			PasswordSieve(user, password);
			UserSession s = CreateUserSession(user);
			return s;
		}

		private void UserBanishedSieve(User user)
		{
			if (user == null)
				throw new ArgumentNullException();

			if (user.Banished)
			{
				throw new Exception("User banished: " + user);
			}
		}

		public void SetPasswordBySideEffect(User user, string newPassword)
		{
			HashPasswordBySideEffect(user, newPassword);
		}

		public void ChangePasswordBySideEffect(User user, string oldPassword, string newPassword)
		{
			PasswordSieve(user, oldPassword);
			HashPasswordBySideEffect(user, newPassword);
		}

		private void HashPasswordBySideEffect(User user, string newPassword)
		{
			HajaPasswordHash hasher = HajaPasswordHash.CreateForHashing();
			hasher.CreateHash(newPassword);
			user.Salt = hasher.Salt;
			user.HashedPassword = hasher.HashedPassword;
		}

		private void PasswordSieve(User user, string password)
		{
			if (!VerifyPassword(user, password))
			{
				Exception ex = new Exception("Invalid password or user.");
				throw ex;
			}
		}

		private bool VerifyPassword(User user, string password)
		{
			if (user == null)
				throw new ArgumentNullException();

			return HajaPasswordHash
				.CreateForVerification(user.Salt, user.HashedPassword)
				.Verify(password);
		}

		private UserSession CreateUserSession(User user)
		{
			UserSession s = new UserSession();
			s.SessionId = GenerateSessionId();
			s.UserLoggedIn = user;
			return s;
		}

		private string GenerateSessionId()
		{
			return Guid.NewGuid().ToString();
			//note: NewGuid is thread-safe
		}
	}
}
