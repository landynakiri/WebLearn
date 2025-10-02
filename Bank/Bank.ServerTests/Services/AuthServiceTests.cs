using Bank.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Bank.Server.Services.Tests
{
    [TestFixture]
    public class AuthServiceTests
    {
        private Mock<UserManager<ApplicationUser>> userManagerMock;
        private Mock<RoleManager<IdentityRole>> roleManagerMock;
        private Mock<SignInManager<ApplicationUser>> signInManagerMock;

        [SetUp]
        public void SetUp()
        {
            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStore.Object, null, null, null, null, null, null, null, null);

            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                roleStore.Object, null, null, null, null);

            var contextAccessor = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                userManagerMock.Object,
                contextAccessor.Object,
                claimsFactory.Object,
                null, null, null, null);
        }


        [Test]
        public async Task RegisterAsync_SuccessfulRegistration_AddsUserRole()
        {
            // Arrange
            var registerRequest = new RegisterRequest { Email = "test@bank.com", Password = "Test123!" };
            var identityResultSuccess = IdentityResult.Success;

            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityResultSuccess);
            roleManagerMock.Setup(x => x.RoleExistsAsync("User")).ReturnsAsync(true);
            userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User"))
                .ReturnsAsync(identityResultSuccess);

            var authService = new AuthService(userManagerMock.Object, roleManagerMock.Object, signInManagerMock.Object);

            // Act
            var result = await authService.RegisterAsync(registerRequest);

            // Assert
            Assert.IsTrue(result.Succeeded);
            userManagerMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), "Test123!"), Times.Once);
            userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User"), Times.Once);
        }

        [Test]
        public async Task RegisterAsync_UserCreationFails_ReturnsFailure()
        {
            // Arrange
            var registerRequest = new RegisterRequest { Email = "fail@bank.com", Password = "Test123!" };
            var identityResultFail = IdentityResult.Failed(new IdentityError { Description = "Failed" });

            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityResultFail);

            var authService = new AuthService(userManagerMock.Object, roleManagerMock.Object, signInManagerMock.Object);

            // Act
            var result = await authService.RegisterAsync(registerRequest);

            // Assert
            Assert.IsFalse(result.Succeeded);
            userManagerMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), "Test123!"), Times.Once);
            userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User"), Times.Never);
        }

        [Test]
        public async Task LoginAsync_Success_ReturnsLoginResp()
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = "test@example.com", Password = "Test123!" };
            var user = new ApplicationUser { Email = loginRequest.Email, UserName = loginRequest.Email };

            signInManagerMock.Setup(x => x.PasswordSignInAsync(
                loginRequest.Email, loginRequest.Password, false, false))
                .ReturnsAsync(SignInResult.Success);

            userManagerMock.Setup(x => x.FindByEmailAsync(loginRequest.Email))
                .ReturnsAsync(user);

            userManagerMock.Setup(x => x.UpdateAsync(user))
                .ReturnsAsync(IdentityResult.Success);

            userManagerMock.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "User" });

            var service = new AuthService(userManagerMock.Object, roleManagerMock.Object, signInManagerMock.Object);

            // Act
            var result = await service.LoginAsync(loginRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Roles, Is.EquivalentTo(new[] { "User" }));
        }

        [Test]
        public async Task LoginAsync_PasswordSignInFails_ReturnsNull()
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = "fail@example.com", Password = "fail" };

            signInManagerMock.Setup(x => x.PasswordSignInAsync(
                loginRequest.Email, loginRequest.Password, false, false))
                .ReturnsAsync(SignInResult.Failed);

            var service = new AuthService(userManagerMock.Object, roleManagerMock.Object, signInManagerMock.Object);

            // Act
            var result = await service.LoginAsync(loginRequest);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task LoginAsync_UserNotFound_ReturnsNull()
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = "notfound@example.com", Password = "Test123!" };

            signInManagerMock.Setup(x => x.PasswordSignInAsync(
                loginRequest.Email, loginRequest.Password, false, false))
                .ReturnsAsync(SignInResult.Success);

            userManagerMock.Setup(x => x.FindByEmailAsync(loginRequest.Email))
                .ReturnsAsync((ApplicationUser)null);

            var service = new AuthService(userManagerMock.Object, roleManagerMock.Object, signInManagerMock.Object);

            // Act
            var result = await service.LoginAsync(loginRequest);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task LoginAsync_UpdateUserFails_ReturnsNull()
        {
            // Arrange
            var loginRequest = new LoginRequest { Email = "test@example.com", Password = "Test123!" };
            var user = new ApplicationUser { Email = loginRequest.Email, UserName = loginRequest.Email };

            signInManagerMock.Setup(x => x.PasswordSignInAsync(
                loginRequest.Email, loginRequest.Password, false, false))
                .ReturnsAsync(SignInResult.Success);

            userManagerMock.Setup(x => x.FindByEmailAsync(loginRequest.Email))
                .ReturnsAsync(user);

            userManagerMock.Setup(x => x.UpdateAsync(user))
                .ReturnsAsync(IdentityResult.Failed());

            var service = new AuthService(userManagerMock.Object, roleManagerMock.Object, signInManagerMock.Object);

            // Act
            var result = await service.LoginAsync(loginRequest);

            // Assert
            Assert.IsNull(result);
        }
    }
}