using NSubstitute;

namespace LegacyApp.Tests {
	public class Tests {
		[SetUp]
		public void Setup() {
		}

		[Test]
		public void Test1() {
			IClientRepository? mockClientRepository = Substitute.For<IClientRepository>();
			mockClientRepository.GetById(4)
				.Returns(new Client {
					ClientStatus = ClientStatus.Gold,
					Id = 4,
					SpecialClientCategory = SpecialClientCategory.Unknown
				});

			IUserCreditService mockUserCreditService = Substitute.For<IUserCreditService>();
			mockUserCreditService.GetCreditLimit("Bruno", "Camba", new DateTime(1993, 1, 1)).Returns(500);

			IUserDataService mockUserDataService = Substitute.For<IUserDataService>();
			mockUserDataService.AddUser(new User());


			var userService = new UserService(mockClientRepository, mockUserCreditService, mockUserDataService);
			var addResult = userService.AddUser("Bruno", "Camba", "bruno.camba@gmail.com", new DateTime(1993, 1, 1), 4);
			Console.WriteLine("Adding Bruno Camba was " + (addResult ? "successful" : "unsuccessful"));
			Assert.Pass();
		}
	}
}