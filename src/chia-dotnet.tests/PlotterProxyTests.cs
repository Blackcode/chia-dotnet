﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace chia.dotnet.tests
{
    [TestClass]
    [TestCategory("Integration")]
    public class PlotterProxyTests
    {
        private static Daemon _theDaemon;
        private static PlotterProxy _thePlotter;

        [ClassInitialize]
        public static async Task Initialize(TestContext context)
        {
            _theDaemon = DaemonFactory.CreateDaemonFromHardcodedLocation();
            _theDaemon.BroadcastMessageReceived += _theDaemon_BroadcastMessageReceived;

            await _theDaemon.Connect();
            await _theDaemon.Register();
            _thePlotter = new PlotterProxy(_theDaemon);
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            _theDaemon?.Dispose();
        }

        [TestMethod()]
        [ExpectedException(typeof(TaskCanceledException))]
        public async Task FailOnInvaliddConfig()
        {
            var config = new PlotterConfig();
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await _thePlotter.StartPlotting(config, cts.Token);
        }

        [TestMethod()]
        public async Task StartPlotting()
        {
            var config = new PlotterConfig()
            {
                Size = KValues.K25,
                OverrideK = true,
                TempDir = "/home/don/plots",
                DestinationDir = "/home/don/plots"
            };

            await _thePlotter.StartPlotting(config);

            // this seems like the only way to get the plot queue
            var q = await _theDaemon.RegisterPlotter();

            Assert.IsNotNull(q);
        }

        private static void _theDaemon_BroadcastMessageReceived(object sender, Message e)
        {

        }
    }
}
