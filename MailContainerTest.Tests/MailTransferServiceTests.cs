using FakeItEasy;
using MailContainerTest.Data;
using MailContainerTest.Services;
using MailContainerTest.Types;
using NUnit.Framework;

namespace MailContainerTest.Tests
{
    public class MailTransferServiceTests
    {
        private IContainerDataStore _fakeDataStore;
        private IMailTypeValidator _fakeMailTypeValidator;
        private MailTransferService _service;

        [SetUp]
        public void Setup()
        {
            _fakeDataStore = A.Fake<IContainerDataStore>();
            _fakeMailTypeValidator = A.Fake<IMailTypeValidator>();
            _service = new MailTransferService(_fakeDataStore, _fakeMailTypeValidator);
        }

        [Test]
        public void MakeMailTransferUpdatesMailContainerAndReturnsSuccessIfValidationPasses()
        {
            var sourceMailContainerNumber = "Test123";
            var request = new MakeMailTransferRequest()
            {
                SourceMailContainerNumber = sourceMailContainerNumber,
                NumberOfMailItems = 55
            };

            var container = new MailContainer
            {
                Capacity = 555
            };

            A.CallTo(() => _fakeDataStore.GetMailContainer(sourceMailContainerNumber)).Returns(container);
            A.CallTo(() => _fakeMailTypeValidator.Validate(container, request)).Returns(true);


            var result = _service.MakeMailTransfer(request);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(500, container.Capacity);
            A.CallTo(() => _fakeDataStore.UpdateMailContainer(container)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void MakeMailTransferDoesNotUpdateMailContainerAndReturnsUnsuccessfulResponseIfValidationFails()
        {
            var sourceMailContainerNumber = "Test123";
            var request = new MakeMailTransferRequest()
            {
                SourceMailContainerNumber = sourceMailContainerNumber,
                NumberOfMailItems = 55
            };

            var container = new MailContainer
            {
                Capacity = 555
            };

            A.CallTo(() => _fakeDataStore.GetMailContainer(sourceMailContainerNumber)).Returns(container);
            A.CallTo(() => _fakeMailTypeValidator.Validate(container, request)).Returns(false);


            var result = _service.MakeMailTransfer(request);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(555, container.Capacity);
            A.CallTo(() => _fakeDataStore.UpdateMailContainer(container)).MustNotHaveHappened();
        }
    }
}
