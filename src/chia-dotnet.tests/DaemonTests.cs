﻿
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace chia.dotnet.tests
{
    /// <summary>
    /// This class is a test harness for interation with an actual daemon instance
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    //[Ignore] // uncomment to suppress completely
    public class DaemonTests
    {
        [TestMethod]
        public async Task GetFarmerIsRunning()
        {
            using var daemon = DaemonFactory.CreateDaemonFromHardcodedLocation(ServiceNames.Daemon);

            await daemon.Connect();
            var running = await daemon.IsServiceRunning(ServiceNames.Farmer);

            Assert.IsTrue(running);
        }

        [TestMethod]
        public async Task GetHarvesterIsRunning()
        {
            using var daemon = DaemonFactory.CreateDaemonFromHardcodedLocation(ServiceNames.Daemon);

            await daemon.Connect();
            var running = await daemon.IsServiceRunning(ServiceNames.Harvester);

            Assert.IsTrue(running);
        }

        [TestMethod]
        [Ignore]
        public async Task ExitDaemon()
        {
            using var daemon = DaemonFactory.CreateDaemonFromHardcodedLocation(ServiceNames.Daemon);

            await daemon.Connect();
            await daemon.Exit();

            // if no exception the daemon was stopped successfully
        }

        [TestMethod]
        public async Task RestartFarmer()
        {
            using var daemon = DaemonFactory.CreateDaemonFromHardcodedLocation(ServiceNames.Daemon);

            await daemon.Connect();

            if (await daemon.IsServiceRunning(ServiceNames.Farmer))
            {
                await daemon.StopService(ServiceNames.Farmer);
                Assert.IsFalse(await daemon.IsServiceRunning(ServiceNames.Farmer));
            }

            await daemon.StartService(ServiceNames.Farmer);
            Assert.IsTrue(await daemon.IsServiceRunning(ServiceNames.Farmer));
        }

        [TestMethod]
        public async Task RegisterService()
        {
            using var daemon = DaemonFactory.CreateDaemonFromHardcodedLocation(ServiceNames.Daemon);

            await daemon.Connect();

            await daemon.Register();

            // no exception we were successful
        }
    }
}
