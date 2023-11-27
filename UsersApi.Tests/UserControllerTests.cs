using Microsoft.AspNetCore.Http;
using UsersApi.Models.Validation;

namespace UsersApi.Test
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUsersService> _usersServiceMock;
        private UserController _userController;

        [SetUp]
        public void Setup()
        {
            _usersServiceMock = new Mock<IUsersService>();
            _userController = new UserController(_usersServiceMock.Object, new UserValidator());
        }

        [Test]
        public async Task Get_ReturnsListOfUsers()
        {
            // Arrange
            var users = new List<User> { GetSampleUser(), GetSampleUser2() };
            _usersServiceMock.Setup(service => service.GetAsync()).ReturnsAsync(users);

            // Act
            var result = await _userController.Get();

            // Assert
            Assert.That(result, Is.EqualTo(users));
        }

        [Test]
        public async Task Get_WithValidId_ReturnsUser()
        {
            // Arrange
            var user = GetSampleUser();
            _usersServiceMock.Setup(service => service.GetAsync(user.Id)).ReturnsAsync(user);

            // Act
            var result = await _userController.Get(user.Id);

            // Assert
            Assert.That(result.Value, Is.EqualTo(user));
        }

        [Test]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var invalidId = "invalidId";
            _usersServiceMock.Setup(service => service.GetAsync(invalidId)).ReturnsAsync(default(User));

            // Act
            var result = await _userController.Get(invalidId);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Create_WithInvalidUser_ReturnsUnprocessableEntity()
        {
            // Arrange
            var newInvalidUser = GetSampleUser(name: string.Empty);

            // Act
            var result = await _userController.Create(newInvalidUser) as ObjectResult;
            var validationFailures = result?.Value as List<FluentValidation.Results.ValidationFailure>;
            
            // Assert
            Assert.That(result?.StatusCode, Is.EqualTo(StatusCodes.Status422UnprocessableEntity));
            Assert.That(validationFailures?.Single().PropertyName, Is.EqualTo(nameof(newInvalidUser.Name)));
            _usersServiceMock.Verify(service => service.CreateAsync(newInvalidUser), Times.Never);
        }

        [Test]
        public async Task Create_WithValidUser_ReturnsCreatedAtAction()
        {
            // Arrange
            var newUser = GetSampleUser();

            // Act
            var result = await _userController.Create(newUser) as CreatedAtActionResult;

            // Assert
            Assert.That(result?.Value, Is.EqualTo(newUser));
            Assert.That(result.ActionName, Is.EqualTo(nameof(UserController.Get)));
            Assert.That(result?.RouteValues?["id"], Is.EqualTo(newUser.Id));
            _usersServiceMock.Verify(service => service.CreateAsync(newUser), Times.Once);
        }

        [Test]
        public async Task Update_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var invalidId = "invalidId";
            var updatedUser = GetSampleUser();
            _usersServiceMock.Setup(service => service.GetAsync(invalidId)).ReturnsAsync(default(User));

            // Act
            var result = await _userController.Update(invalidId, updatedUser);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
            _usersServiceMock.Verify(service => service.UpdateAsync(invalidId, updatedUser), Times.Never);
        }

        [Test]
        public async Task Update_WithInvalidUser_ReturnsUnprocessableEntity()
        {
            // Arrange
            var existingUser = GetSampleUser();
            var id = existingUser.Id;
            var updatedInvalidUser = GetSampleUser(name: string.Empty);

            _usersServiceMock.Setup(service => service.GetAsync(id)).ReturnsAsync(existingUser);

            // Act
            var result = await _userController.Update(id, updatedInvalidUser) as ObjectResult; ;
            var validationFailures = result?.Value as List<FluentValidation.Results.ValidationFailure>;

            // Assert
            Assert.That(result?.StatusCode, Is.EqualTo(StatusCodes.Status422UnprocessableEntity));
            Assert.That(validationFailures?.Single().PropertyName, Is.EqualTo(nameof(updatedInvalidUser.Name)));
            _usersServiceMock.Verify(service => service.UpdateAsync(id, updatedInvalidUser), Times.Never);
        }

        [Test]
        public async Task Update_WithValidIdAndUser_ReturnsNoContent()
        {
            // Arrange
            var existingUser = GetSampleUser();
            var id = existingUser.Id;
            var updatedUser = GetSampleUser(name: "Johnny Doe");

            _usersServiceMock.Setup(service => service.GetAsync(id)).ReturnsAsync(existingUser);

            // Act
            var result = await _userController.Update(id, updatedUser);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _usersServiceMock.Verify(service => service.UpdateAsync(id, updatedUser), Times.Once);
        }

        [Test]
        public async Task Delete_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var existingUser = GetSampleUser();
            var id = existingUser.Id;
            _usersServiceMock.Setup(service => service.GetAsync(id)).ReturnsAsync(existingUser);

            // Act
            var result = await _userController.Delete(id);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
            _usersServiceMock.Verify(service => service.RemoveAsync(id), Times.Once);
        }

        [Test]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var invalidId = "invalidId";
            _usersServiceMock.Setup(service => service.GetAsync(invalidId)).ReturnsAsync(default(User));

            // Act
            var result = await _userController.Delete(invalidId);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
            _usersServiceMock.Verify(service => service.RemoveAsync(invalidId), Times.Never);
        }

        private static User GetSampleUser(string name = "John Doe")
        {
            return new User(
                "6560ca102f78f8260a1dde90",
                name,
                "johndoe",
                "johndoe@example.com",
                "123-456-7890",
                "www.example.com",
                new Address("123 Main Street", "100", "Anytown", "12345", new Geo("40.7142", "-74.0064")),
                new Company("Acme Corporation", "We make the best products", "We are the best company in the world")
            );
        }

        private static User GetSampleUser2()
        {
            return new User(
                "6560ca102f78f8260a1dde91",
                "Jane Doe",
                "janedoe",
                "janedoe@example.com",
                "123-456-7890",
                "www.example.com",
                new Address("123 Main Street", "100", "Anytown", "12345", new Geo("40.7142", "-74.0064")),
                new Company("Acme Corporation", "We make the best products", "We are the best company in the world")
            );
        }
    }
}