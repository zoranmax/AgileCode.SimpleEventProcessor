using System.Threading.Tasks;
using NUnit.Framework;
using System.Data;

namespace AgileCode.SimpleEventProcessor.Tests
{
    [TestFixture]
    public class ServiceBrokerAsyncHelperTest
    {
        [Test]
        [Explicit]
        public async Task CanDequeue()
        {
            var connString =
                "Data Source=localhost;Initial Catalog=mydb;Integrated Security=SSPI;Pooling=True;";
            var sp = "dbo.ProcGetExportMessage";
            //var client = new ServiceBrokerAsyncHelper(connString, sp);
            //var dataTable = await client.DequeueAsync(1, 100, new CancellationToken(false));

            DataTable dataTable = new DataTable();
            await Task.Delay(100);
            Assert.That(dataTable != null);
        }
    }
}