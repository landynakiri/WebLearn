using Bank.Server.Models;
using Bank.Server.Services;
using Microsoft.AspNetCore.Identity;
using MockQueryable;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Bank.ServerTests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<UserManager<ApplicationUser>> userManagerMock;

        [SetUp]
        public void SetUp()
        {
            var userStore = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStore.Object, null, null, null, null, null, null, null, null);
        }

        [Test]
        public async Task SetRolesAsync_UserNotFound_ReturnsFalse()
        {
            // Arrange
            userManagerMock.Setup(x => x.FindByIdAsync("notfound")).ReturnsAsync((ApplicationUser)null);
            var service = new UserService(userManagerMock.Object);

            // Act
            var result = await service.SetRolesAsync("notfound", new[] { "Admin" });

            // Assert
            Assert.IsFalse(result);
            userManagerMock.Verify(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()), Times.Never);
        }

        [Test]
        public async Task SetRolesAsync_UserFound_UpdatesRolesAndReturnsTrue()
        {
            // Arrange
            var user = new ApplicationUser { Id = "user1", Email = "user1@test.com" };
            var currentRoles = new List<string> { "User" };
            var newRoles = new[] { "Admin", "Manager" };

            userManagerMock.Setup(x => x.FindByIdAsync("user1")).ReturnsAsync(user);
            userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(currentRoles);
            userManagerMock.Setup(x => x.RemoveFromRolesAsync(user, currentRoles)).ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(x => x.AddToRolesAsync(user, newRoles)).ReturnsAsync(IdentityResult.Success);

            var service = new UserService(userManagerMock.Object);

            // Act
            var result = await service.SetRolesAsync("user1", newRoles);

            // Assert
            Assert.IsTrue(result);
            userManagerMock.Verify(x => x.GetRolesAsync(user), Times.Once);
            userManagerMock.Verify(x => x.RemoveFromRolesAsync(user, currentRoles), Times.Once);
            userManagerMock.Verify(x => x.AddToRolesAsync(user, newRoles), Times.Once);
        }

        [Test]
        public async Task GetUsers_ReturnsUserListWithRoles()
        {
            // Arrange
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", UserName = "user1", Email = "user1@test.com", CreatedAt = DateTime.UtcNow.AddDays(-2), LastLogin = DateTime.UtcNow.AddDays(-1) },
                new ApplicationUser { Id = "2", UserName = "user2", Email = "user2@test.com", CreatedAt = DateTime.UtcNow.AddDays(-3), LastLogin = null }
            }.AsQueryable().BuildMock();

            userManagerMock.Setup(x => x.Users).Returns(users);

            userManagerMock.Setup(x => x.GetRolesAsync(It.Is<ApplicationUser>(u => u.Id == "1")))
                .ReturnsAsync(new List<string> { "Admin" });
            userManagerMock.Setup(x => x.GetRolesAsync(It.Is<ApplicationUser>(u => u.Id == "2")))
                .ReturnsAsync(new List<string> { "User" });

            var service = new UserService(userManagerMock.Object);

            // Act
            var result = (await service.GetUsers()).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("1", result[0].Id);
            Assert.AreEqual("Admin", result[0].Roles.First());
            Assert.AreEqual("2", result[1].Id);
            Assert.AreEqual("User", result[1].Roles.First());
        }
    }
}