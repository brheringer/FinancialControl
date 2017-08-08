using NHibernate;
using System;

namespace FinancialControl.Persistence.NHPersistence
{
	public class NHibernateDAOFactory 
		: DaoFactory
	{
		private readonly ISession SessionInstance;

		public NHibernateDAOFactory(ISession session)
		{
			this.SessionInstance = session;
		}

		#region Declaração dos atributos que são DAO
		private NHResultCenterDAO _centerDAO = null;
		private NHAccountDAO _accountDAO = null;
		private NHEntryDAO _entryDAO = null;
		private NHEntryTemplateDAO _entryTemplateDAO = null;
		private NHMemoMappingDAO _memoMappingDAO = null;
		private NHUserDAO _userDAO = null;
		private NHUserSessionDAO _userSessionDAO = null;
		#endregion

		#region Getters dos DAO

		public ResultCenterDAO ResultCenterDAO
		{
			get
			{
				if (_centerDAO == null)
					_centerDAO = new NHResultCenterDAO(this.SessionInstance);
				return _centerDAO;
			}
		}

		public AccountDAO AccountDAO
		{
			get {
				if (_accountDAO == null)
					_accountDAO = new NHAccountDAO(this.SessionInstance);
				return _accountDAO; 
			}
		}

		public EntryDAO EntryDAO
		{
			get
			{
				if (_entryDAO == null)
					_entryDAO = new NHEntryDAO(this.SessionInstance);
				return _entryDAO;
			}
		}

		public EntryTemplateDAO EntryTemplateDAO
		{
			get
			{
				if (_entryTemplateDAO == null)
					_entryTemplateDAO = new NHEntryTemplateDAO(this.SessionInstance);
				return _entryTemplateDAO;
			}
		}

		public MemoMappingDAO MemoMappingDAO
		{
			get
			{
				if (_memoMappingDAO == null)
					_memoMappingDAO = new NHMemoMappingDAO(this.SessionInstance);
				return _memoMappingDAO;
			}
		}

		public UserDAO UserDAO
		{
			get
			{
				if (_userDAO == null)
					_userDAO = new NHUserDAO(this.SessionInstance);
				return _userDAO;
			}
		}

		public UserSessionDAO UserSessionDAO
		{
			get
			{
				if (_userSessionDAO == null)
					_userSessionDAO = new NHUserSessionDAO(this.SessionInstance);
				return _userSessionDAO;
			}
		}
		#endregion
	}
}
