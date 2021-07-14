﻿using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace chia.dotnet.tests
{
    /// <summary>
    /// This class is a test harness for interation with an actual daemon instance
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class FullNodeProxyTests
    {
        private static Daemon _theDaemon;
        private static FullNodeProxy _theFullNode;

        [ClassInitialize]
        public static async Task Initialize(TestContext context)
        {
            _theDaemon = DaemonFactory.CreateDaemonFromHardcodedLocation();

            await _theDaemon.Connect();
            await _theDaemon.Register();
            _theFullNode = new FullNodeProxy(_theDaemon);
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            _theDaemon?.Dispose();
        }

        [TestMethod]
        public async Task GetBlockChainState()
        {
            var state = await _theFullNode.GetBlockchainState();

            Assert.IsNotNull(state);
        }

        [TestMethod]
        public async Task GetBlock()
        {
            var block = await _theFullNode.GetBlock("0xc5d6292aaf50c3cdc3f8481a30b2e9f12babf274c0488ab24db3dd9b1dd41364");

            Assert.IsNotNull(block);
        }

        [TestMethod]
        public async Task GetBlockRecord()
        {
            var record = await _theFullNode.GetBlockRecord("0xc5d6292aaf50c3cdc3f8481a30b2e9f12babf274c0488ab24db3dd9b1dd41364");

            Assert.IsNotNull(record);
        }

        [TestMethod]
        public async Task GetBlocks()
        {
            var blocks = await _theFullNode.GetBlocks(435160, 435167, false);

            Assert.IsNotNull(blocks);
        }

        [TestMethod()]
        public async Task GetNetworkInfo()
        {
            var info = await _theFullNode.GetNetworkInfo();

            Assert.IsNotNull(info);
        }

        [TestMethod]
        public async Task GetNetworkSpace()
        {
            var space = await _theFullNode.GetNetworkSpace("0x457649e7e6dabb5660f8c3cd9e08534522361d97cb237bdfa341bce01e91c3f5", "0x1353f4d1a01d5393cb7f8f0631487774e11125f47af487426fd9cbcd24151a15");
            Assert.IsNotNull(space);

            Debug.WriteLine(space);
        }

        [TestMethod]
        public async Task Ping()
        {
            await _theFullNode.Ping();
        }

        [TestMethod]
        public async Task GetConnections()
        {
            var connections = await _theFullNode.GetConnections();
            Assert.IsNotNull(connections);
        }

        [TestMethod]
        public async Task OpenConnection()
        {
            await _theFullNode.OpenConnection("node.chia.net", 8444);
        }

        [TestMethod()]
        public async Task GetBlockRecordByHeight()
        {
            var blockRecord = await _theFullNode.GetBlockRecordByHeight(12441);
            Assert.IsNotNull(blockRecord);
        }

        [TestMethod()]
        public async Task GetBlockRecords()
        {
            var blockRecords = await _theFullNode.GetBlockRecords(12000, 12441);
            Assert.IsNotNull(blockRecords);
        }


        [TestMethod()]
        public async Task GetUnfinishedBlockHeaders()
        {
            var blockHeaders = await _theFullNode.GetUnfinishedBlockHeaders();
            Assert.IsNotNull(blockHeaders);
        }

        [TestMethod()]
        public async Task GetCoinRecordsByPuzzleHash()
        {
            var records = await _theFullNode.GetCoinRecordsByPuzzleHash("0xb5a83c005c4ee98dc807a560ea5bc361d6d3b32d2f4d75061351d1f6d2b6085f", true);
            Assert.IsNotNull(records);
        }

        [TestMethod()]
        public async Task GetCoinRecordByName()
        {
            var coinRecord = await _theFullNode.GetCoinRecordByName("0x2b83ca807d305cd142e0e91d4e7a18f8e57df0ac6b4fa403bff249d0d491c609");
            Assert.IsNotNull(coinRecord);
        }

        [TestMethod()]
        public async Task GetAdditionsAndRemovals()
        {
            var additionsAndRemovals = await _theFullNode.GetAdditionsAndRemovals("6b143b214f731021106d411c5bdce2fe03de0af5449c63830f111f25dc7d0a2b");
            Assert.IsNotNull(additionsAndRemovals);
        }

        [TestMethod()]
        public async Task GetAllMempoolItems()
        {
            var items = await _theFullNode.GetAllMempoolItems();
            Assert.IsNotNull(items);
        }

        [TestMethod()]
        public async Task GetAllMemmpoolTxIds()
        {
            var ids = await _theFullNode.GetAllMemmpoolTxIds();
            Assert.IsNotNull(ids);
        }

        [TestMethod()]
        public async Task GetMemmpooItemByTxId()
        {
            var ids = await _theFullNode.GetAllMemmpoolTxIds();
            Assert.IsNotNull(ids);
            Assert.IsTrue(ids.Count() > 0);

            var item = await _theFullNode.GetMemmpooItemByTxId((string)ids.First());
            Assert.IsNotNull(item);
        }

        [TestMethod()]
        public async Task GetRecentSignagePoint()
        {
            var sp = await _theFullNode.GetRecentSignagePoint("0x315ec5a8e0ac849915e136b81fd2d2277ae3804e2aea3911321ed293eb3595fb");
            Assert.IsNotNull(sp);
        }
    }
}
