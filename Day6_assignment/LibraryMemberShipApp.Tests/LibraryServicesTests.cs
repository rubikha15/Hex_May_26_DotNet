using LibraryMembershipApp.Interfaces;
using LibraryMembershipApp.Models;
using LibraryMembershipApp.Services;
using Moq;
using NUnit.Framework;
using System.Timers;

namespace LibraryMembershipApp.Tests
{
    [TestFixture]
    public class LibraryServiceTests
    {
        private Mock<IMemberRepository> _memberRepositoryMock;
        private Mock<IBookRepository> _bookRepositoryMock;
        private Mock<INotificationService> _notificationServiceMock;
        private LibraryService _libraryService;

        [SetUp]
        public void Setup()
        {
            _memberRepositoryMock = new Mock<IMemberRepository>();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _notificationServiceMock = new Mock<INotificationService>();

            _libraryService = new LibraryService(
                _memberRepositoryMock.Object,
                _bookRepositoryMock.Object,
                _notificationServiceMock.Object);
        }

        [Test]
        public void BorrowBook_WhenAllConditionsAreValid_ShouldReturnSuccessMessage()
        {
            // Arrange
            var member = new Member
            {
                MemberId = 1,
                MemberName = "Rubikha",
                Email = "rubikha@mail.com",
                IsActive = true,
                BorrowedBookCount = 1,
                IsPremiumMember = false
            };

            var book = new Book
            {
                BookId = 101,
                BookTitle = "C# Basics",
                AuthorName = "John",
                IsAvailable = true
            };

            _memberRepositoryMock.Setup(x => x.GetMemberById(1)).Returns(member);
            _bookRepositoryMock.Setup(x => x.GetBookById(101)).Returns(book);

            // Act
            string result = _libraryService.BorrowBook(1, 101);

            // Assert
            Assert.That(result, Is.EqualTo("Book borrowed successfully"));
            _bookRepositoryMock.Verify(x => x.MarkBookAsBorrowed(101), Times.Once);
            _memberRepositoryMock.Verify(x => x.UpdateBorrowedBookCount(1), Times.Once);
            _notificationServiceMock.Verify(x => x.SendBorrowNotification("rubikha@mail.com", "C# Basics"), Times.Once);
        }

        [Test]
        public void BorrowBook_WhenMemberDoesNotExist_ShouldReturnMemberNotFound()
        {
            // Arrange
            _memberRepositoryMock.Setup(x => x.GetMemberById(1)).Returns((Member?)null);

            // Act
            string result = _libraryService.BorrowBook(1, 101);

            // Assert
            Assert.That(result, Is.EqualTo("Member not found"));
            _bookRepositoryMock.Verify(x => x.GetBookById(It.IsAny<int>()), Times.Never);
            VerifyFailureMethodsNeverCalled();
        }

        [Test]
        public void BorrowBook_WhenMemberIsInactive_ShouldReturnMemberIsNotActive()
        {
            // Arrange
            var member = new Member
            {
                MemberId = 1,
                IsActive = false
            };

            _memberRepositoryMock.Setup(x => x.GetMemberById(1)).Returns(member);

            // Act
            string result = _libraryService.BorrowBook(1, 101);

            // Assert
            Assert.That(result, Is.EqualTo("Member is not active"));
            _bookRepositoryMock.Verify(x => x.GetBookById(It.IsAny<int>()), Times.Never);
            VerifyFailureMethodsNeverCalled();
        }

        [Test]
        public void BorrowBook_WhenBookDoesNotExist_ShouldReturnBookNotFound()
        {
            // Arrange
            var member = new Member
            {
                MemberId = 1,
                IsActive = true,
                BorrowedBookCount = 1
            };

            _memberRepositoryMock.Setup(x => x.GetMemberById(1)).Returns(member);
            _bookRepositoryMock.Setup(x => x.GetBookById(101)).Returns((Book?)null);

            // Act
            string result = _libraryService.BorrowBook(1, 101);

            // Assert
            Assert.That(result, Is.EqualTo("Book not found"));
            VerifyFailureMethodsNeverCalled();
        }

        [Test]
        public void BorrowBook_WhenBookIsNotAvailable_ShouldReturnBookIsNotAvailable()
        {
            // Arrange
            var member = new Member
            {
                MemberId = 1,
                IsActive = true,
                BorrowedBookCount = 1
            };

            var book = new Book
            {
                BookId = 101,
                BookTitle = "C# Basics",
                IsAvailable = false
            };

            _memberRepositoryMock.Setup(x => x.GetMemberById(1)).Returns(member);
            _bookRepositoryMock.Setup(x => x.GetBookById(101)).Returns(book);

            // Act
            string result = _libraryService.BorrowBook(1, 101);

            // Assert
            Assert.That(result, Is.EqualTo("Book is not available"));
            VerifyFailureMethodsNeverCalled();
        }

        [Test]
        public void BorrowBook_WhenBorrowingLimitReached_ShouldReturnBorrowingLimitReached()
        {
            // Arrange
            var member = new Member
            {
                MemberId = 1,
                IsActive = true,
                BorrowedBookCount = 3,
                IsPremiumMember = false
            };

            var book = new Book
            {
                BookId = 101,
                BookTitle = "C# Basics",
                IsAvailable = true
            };

            _memberRepositoryMock.Setup(x => x.GetMemberById(1)).Returns(member);
            _bookRepositoryMock.Setup(x => x.GetBookById(101)).Returns(book);

            // Act
            string result = _libraryService.BorrowBook(1, 101);

            // Assert
            Assert.That(result, Is.EqualTo("Borrowing limit reached"));
            VerifyFailureMethodsNeverCalled();
        }

        [Test]
        public void BorrowBook_WhenMemberIdIsInvalid_ShouldReturnInvalidMemberId()
        {
            // Arrange

            // Act
            string result = _libraryService.BorrowBook(0, 101);

            // Assert
            Assert.That(result, Is.EqualTo("Invalid member id"));
            _memberRepositoryMock.Verify(x => x.GetMemberById(It.IsAny<int>()), Times.Never);
            _bookRepositoryMock.Verify(x => x.GetBookById(It.IsAny<int>()), Times.Never);
            VerifyFailureMethodsNeverCalled();
        }

        [Test]
        public void BorrowBook_WhenBookIdIsInvalid_ShouldReturnInvalidBookId()
        {
            // Arrange

            // Act
            string result = _libraryService.BorrowBook(1, 0);

            // Assert
            Assert.That(result, Is.EqualTo("Invalid book id"));
            _memberRepositoryMock.Verify(x => x.GetMemberById(It.IsAny<int>()), Times.Never);
            _bookRepositoryMock.Verify(x => x.GetBookById(It.IsAny<int>()), Times.Never);
            VerifyFailureMethodsNeverCalled();
        }

        [Test]
        public void BorrowBook_WhenNormalMemberHasThreeBooks_ShouldReturnBorrowingLimitReached()
        {
            // Arrange
            var member = new Member
            {
                MemberId = 1,
                IsActive = true,
                BorrowedBookCount = 3,
                IsPremiumMember = false
            };

            var book = new Book
            {
                BookId = 101,
                BookTitle = "C# Basics",
                IsAvailable = true
            };

            _memberRepositoryMock.Setup(x => x.GetMemberById(1)).Returns(member);
            _bookRepositoryMock.Setup(x => x.GetBookById(101)).Returns(book);

            // Act
            string result = _libraryService.BorrowBook(1, 101);

            // Assert
            Assert.That(result, Is.EqualTo("Borrowing limit reached"));
            VerifyFailureMethodsNeverCalled();
        }

        [Test]
        public void BorrowBook_WhenPremiumMemberHasThreeBooks_ShouldAllowBorrowing()
        {
            // Arrange
            var member = new Member
            {
                MemberId = 1,
                Email = "premium@mail.com",
                IsActive = true,
                BorrowedBookCount = 3,
                IsPremiumMember = true
            };

            var book = new Book
            {
                BookId = 101,
                BookTitle = "Advanced C#",
                IsAvailable = true
            };

            _memberRepositoryMock.Setup(x => x.GetMemberById(1)).Returns(member);
            _bookRepositoryMock.Setup(x => x.GetBookById(101)).Returns(book);

            // Act
            string result = _libraryService.BorrowBook(1, 101);

            // Assert
            Assert.That(result, Is.EqualTo("Book borrowed successfully"));
            _bookRepositoryMock.Verify(x => x.MarkBookAsBorrowed(101), Times.Once);
            _memberRepositoryMock.Verify(x => x.UpdateBorrowedBookCount(1), Times.Once);
            _notificationServiceMock.Verify(x => x.SendBorrowNotification("premium@mail.com", "Advanced C#"), Times.Once);
        }

        [Test]
        public void BorrowBook_WhenPremiumMemberHasFiveBooks_ShouldReturnBorrowingLimitReached()
        {
            // Arrange
            var member = new Member
            {
                MemberId = 1,
                IsActive = true,
                BorrowedBookCount = 5,
                IsPremiumMember = true
            };

            var book = new Book
            {
                BookId = 101,
                BookTitle = "Advanced C#",
                IsAvailable = true
            };

            _memberRepositoryMock.Setup(x => x.GetMemberById(1)).Returns(member);
            _bookRepositoryMock.Setup(x => x.GetBookById(101)).Returns(book);

            // Act
            string result = _libraryService.BorrowBook(1, 101);

            // Assert
            Assert.That(result, Is.EqualTo("Borrowing limit reached"));
            VerifyFailureMethodsNeverCalled();
        }

        [Test]
        public void BorrowBook_WhenBookBorrowedSuccessfully_ShouldSendNotificationWithCorrectValues()
        {
            // Arrange
            var member = new Member
            {
                MemberId = 1,
                Email = "member@mail.com",
                IsActive = true,
                BorrowedBookCount = 1,
                IsPremiumMember = false
            };

            var book = new Book
            {
                BookId = 101,
                BookTitle = "NUnit Testing",
                IsAvailable = true
            };

            _memberRepositoryMock.Setup(x => x.GetMemberById(1)).Returns(member);
            _bookRepositoryMock.Setup(x => x.GetBookById(101)).Returns(book);

            // Act
            string result = _libraryService.BorrowBook(1, 101);

            // Assert
            Assert.That(result, Is.EqualTo("Book borrowed successfully"));
            _notificationServiceMock.Verify(
                x => x.SendBorrowNotification("member@mail.com", "NUnit Testing"),
                Times.Once);
        }

        private void VerifyFailureMethodsNeverCalled()
        {
            _bookRepositoryMock.Verify(x => x.MarkBookAsBorrowed(It.IsAny<int>()), Times.Never);
            _memberRepositoryMock.Verify(x => x.UpdateBorrowedBookCount(It.IsAny<int>()), Times.Never);
            _notificationServiceMock.Verify(
                x => x.SendBorrowNotification(It.IsAny<string>(), It.IsAny<string>()),
                Times.Never);
        }
    }
}