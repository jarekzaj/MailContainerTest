using MailContainerTest.Types;
using NUnit.Framework;

namespace MailContainerTest.Tests
{
    [TestFixture]
    public class MailTypeValidatorTests
    {
        private MailTypeValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new MailTypeValidator();
        }
        
        [TestCase(MailType.StandardLetter, AllowedMailType.StandardLetter, true)]
        [TestCase(MailType.StandardLetter, AllowedMailType.LargeLetter, false)]
        [TestCase(MailType.StandardLetter, AllowedMailType.SmallParcel, false)]
        //the third scenario, as the bit flag = 3, includes the bit for 1
        //the test would work if line 48 was changed to AllowedMailType.LargeLetter, but this just highlights the issue with logic
        //unless the logic is intentional and a small parcel allows standard letters
        public void StandardLetterValidates(MailType mailType, AllowedMailType allowedMailType, bool expectedResult)
        {
            var largeLetterRequest = new MakeMailTransferRequest
            {
                MailType = mailType
            };

            var mailContainer = new MailContainer()
            {
                AllowedMailType = allowedMailType
            };

            Assert.AreEqual(expectedResult, _validator.Validate(mailContainer, largeLetterRequest));
        }

        [TestCase(MailType.LargeLetter, AllowedMailType.LargeLetter, 20, 50, true)]
        [TestCase(MailType.LargeLetter, AllowedMailType.LargeLetter, 20, 20, true)]
        [TestCase(MailType.LargeLetter, AllowedMailType.SmallParcel, 20, 50, false)] // this will fail for the same reason as above
        [TestCase(MailType.LargeLetter, AllowedMailType.StandardLetter, 20, 50, false)]
        [TestCase(MailType.LargeLetter, AllowedMailType.LargeLetter, 50, 20, false)]
        public void LargeLetterValidates(MailType mailType, AllowedMailType allowedMailType, int numberOfMailItems, int capacity, bool expectedResult)
        {
            var largeLetterRequest = new MakeMailTransferRequest
            {
                MailType = mailType,
                NumberOfMailItems = numberOfMailItems
            };

            var mailContainer = new MailContainer()
            {
                AllowedMailType = allowedMailType,
                Capacity = capacity
            };

            Assert.AreEqual(expectedResult, _validator.Validate(mailContainer, largeLetterRequest));
        }

        [TestCase(MailType.SmallParcel, AllowedMailType.SmallParcel, MailContainerStatus.Operational, true)]
        [TestCase(MailType.SmallParcel, AllowedMailType.LargeLetter, MailContainerStatus.Operational, false)]
        [TestCase(MailType.SmallParcel, AllowedMailType.StandardLetter, MailContainerStatus.Operational, false)]
        [TestCase(MailType.SmallParcel, AllowedMailType.SmallParcel, MailContainerStatus.OutOfService, false)]
        [TestCase(MailType.SmallParcel, AllowedMailType.SmallParcel, MailContainerStatus.NoTransfersIn, false)]

        public void SmallParcelValidates(MailType mailType, AllowedMailType allowedMailType, MailContainerStatus status, bool expectedResult)
        {
            var smallParcelRequest = new MakeMailTransferRequest
            {
                MailType = mailType
            };

            var mailContainer = new MailContainer
            {
                AllowedMailType = allowedMailType,
                Status = status
            };

            Assert.AreEqual(expectedResult, _validator.Validate(mailContainer, smallParcelRequest));
        }
    }
}
