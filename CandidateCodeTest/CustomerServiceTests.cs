using CandidateCodeTest.Common.Interfaces;
using CandidateCodeTest.Repository;
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
        private readonly Mock<IMessageService> _messageService;
        private readonly Mock<IMessageRepository> _messageRepository;
        private readonly Mock<ILogWriter> _logWriter;

        public CustomerServiceTests()
        {
            //creating mock
            _messageService = new Mock<IMessageService>();
            _logWriter = new Mock<ILogWriter>();
            _messageRepository = new Mock<IMessageRepository>(MockBehavior.Strict);
        }

        [Fact]
        public void Within_Time_Window_Email_Has_Been_Sent()
        {
            // Arrange
            _messageService.Setup(m => m.SendEmail());
            var startTime = new TimeSpan(0, 0, 0);
            var endTime = new TimeSpan(23, 59, 59);
            _customerService = new CustomerService(_messageService.Object, _messageRepository.Object, startTime, endTime, _logWriter.Object);

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
            _customerService = new CustomerService(_messageService.Object, _messageRepository.Object, startTime, endTime, _logWriter.Object);     
            
            // Act
            var result = _customerService.HasEmailBeenSent();

            // Assert
            Assert.False(result);
        }
    }
}
