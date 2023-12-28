using CandidateCodeTest.Common.Interfaces;
using CandidateCodeTest.Services;
using Moq;
using System;
using Xunit;

namespace CandidateCodeTest
{
    // Write the following unit tests
    public class CustomerServiceTests
    {
        private CustomerService _customerService;
        private Mock<IMessageService> _messageService;
        private readonly Mock<ILogWriter> _logWriter;

        public CustomerServiceTests()
        {
            //creating mock
            _messageService = new Mock<IMessageService>();
            _logWriter = new Mock<ILogWriter>();
        }

        [Fact]
        public void Within_Time_Window_Email_Has_Been_Sent()
        {
            // Arrange
            _messageService.Setup(m => m.SendEmail());
            var startTime = new TimeSpan(0, 0, 0);
            var endTime = new TimeSpan(23, 59, 59);
            _customerService = new CustomerService(_messageService.Object, startTime, endTime, _logWriter.Object);

            // Act
            var result = _customerService.HasEmailBeenSent();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Outside_Time_Window_Email_Has_Not_Been_Sent()
        {
            // Arrange
            _messageService.Setup(m => m.SendEmail());
            var startTime = new TimeSpan();
            var endTime = new TimeSpan();
            _customerService = new CustomerService(_messageService.Object, startTime, endTime, _logWriter.Object);

            // Act
            var result = _customerService.HasEmailBeenSent();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Throw_Exception_Email_Has_Not_Been_Sent()
        {
            // Arrange
            bool result = false;
            try
            {
                var startTime = new TimeSpan(0, 0, 0);
                var endTime = new TimeSpan(23, 59, 59);
                _messageService = null;
                _customerService = new CustomerService(null, startTime, endTime, null);

                // Act
                result = _customerService.HasEmailBeenSent();
            }
            catch (Exception)
            {
                //assert
                Assert.Throws<NullReferenceException>(() => _customerService.HasEmailBeenSent());
            }
        }

        [Fact]
        public void Start_Date_Null_Email_Has_Not_Been_Sent()
        {
            // Arrange
            _messageService.Setup(m => m.SendEmail());
            var startTime = (TimeSpan?)null;
            var endTime = new TimeSpan(23, 59, 59);
            _customerService = new CustomerService(_messageService.Object, startTime, endTime, _logWriter.Object);

            // Act
            var result = _customerService.HasEmailBeenSent();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void End_Date_Null_Email_Has_Not_Been_Sent()
        {
            // Arrange
            _messageService.Setup(m => m.SendEmail());
            var startTime = new TimeSpan(0, 0, 0);
            var endTime = (TimeSpan?)null;
            _customerService = new CustomerService(_messageService.Object, startTime, endTime, _logWriter.Object);

            // Act
            var result = _customerService.HasEmailBeenSent();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Null_Params_Email_Has_Not_Been_Sent()
        {
            // Arrange
            _messageService.Setup(m => m.SendEmail());
            var startTime = (TimeSpan?)null;
            var endTime = (TimeSpan?)null;
            _customerService = new CustomerService(_messageService.Object, startTime, endTime, _logWriter.Object);

            // Act
            var result = _customerService.HasEmailBeenSent();

            // Assert
            Assert.False(result);
        }
    }
}
