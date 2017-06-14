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
		private NHResultCenterDAO _centroDAO = null;
		private NHAccountDAO _contaDAO = null;
		private NHEntryDAO _lancamentoDAO = null;
		private NHUserDAO _userDAO = null;
		private NHUserSessionDAO _userSessionDAO = null;
		#endregion

		#region Getters dos DAO

		public ResultCenterDAO ResultCenterDAO
		{
			get
			{
				if (_centroDAO == null)
					_centroDAO = new NHResultCenterDAO(this.SessionInstance);
				return _centroDAO;
			}
		}

		public AccountDAO AccountDAO
		{
			get {
				if (_contaDAO == null)
					_contaDAO = new NHAccountDAO(this.SessionInstance);
				return _contaDAO; 
			}
		}

		public EntryDAO EntryDAO
		{
			get
			{
				if (_lancamentoDAO == null)
					_lancamentoDAO = new NHEntryDAO(this.SessionInstance);
				return _lancamentoDAO;
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
