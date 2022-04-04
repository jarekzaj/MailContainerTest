using MailContainerTest.Data;
using NUnit.Framework;

namespace MailContainerTest.Tests
{
    [TestFixture]
    public class ContainerDataStoreFactoryTests
    {
        [Test]
        public void CreatesBackupDataStoreIfDataStoreTypeIsBackup()
        {
            var dataStore = ContainerDataStoreFactory.CreateContainerDataStore("Backup");
            Assert.IsNotNull(dataStore);
            Assert.IsInstanceOf<BackupMailContainerDataStore>(dataStore);
        }

        [TestCase("Test")]
        [TestCase("123")]
        [TestCase(null)]
        public void CreatesMailDataStoreIfDataStoreTypeIsAnythingElseButBackup(string dataStoreType)
        {
            var dataStore = ContainerDataStoreFactory.CreateContainerDataStore(dataStoreType);
            Assert.IsNotNull(dataStore);
            Assert.IsInstanceOf<MailContainerDataStore>(dataStore);
        }
    }
}
