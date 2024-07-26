using System;

namespace LegacyApp
{
	public interface IUserService {
		bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId);
	}

	public class UserService: IUserService
    {
	    public UserService() {
			_clientRepository = new ClientRepository();
			_userCreditService = new UserCreditServiceClient();
			_userDataService = new UserDataService();
	    }

	    public UserService(IClientRepository clientRepository, IUserCreditService userCreditService, IUserDataService userDataService) {
		    _clientRepository = clientRepository;
		    _userCreditService = userCreditService;
		    _userDataService = userDataService;
	    }

	    private IClientRepository _clientRepository;
	    private readonly IUserCreditService _userCreditService;
	    private readonly IUserDataService _userDataService;

	    //Refactors
		//-Create statics in user class methods for validations
		//-Client.Name to SpecialClientCategory
		//-Create enum for SpecialClientCategory
		//-Create category multiplier concept

        public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
        {
			   if(User.IsValidFirstName(firname) || User.IsValidSurnameName(surname))
            {
                return false;
            }
				if(User.IsValidEmail(email))
            {
                return false;
            }
				if(!User.IsValidAge(dateOfBirth))
            {
                return false;
            }

            Client client = _clientRepository.GetById(clientId);

            User user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firname,
                Surname = surname
            };

				var creditLimit = _userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
				user.ApplyClientCategoryToBaseCreditLimitAndSet(creditLimit);

				if(user.DoesNotPassesCreditCheck())
            //if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

				_userDataService.AddUser(user);

            return true;
        }
    }

	public interface IUserDataService {
		void AddUser(User user);
	}

	public class UserDataService : IUserDataService {
		public void AddUser(User user) {
			UserDataAccess.AddUser(user);
		}

	}
}
