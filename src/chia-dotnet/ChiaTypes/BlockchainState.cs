﻿using System.Numerics;

namespace chia.dotnet
{
    /// <summary>
    /// The node's view of the blockchain.
    /// </summary>
    public record BlockchainState
    {
        public ulong Difficulty { get; init; }
        public bool GenesisChallengeInitiated { get; init; }
        public int MempoolSize { get; init; }
        public BlockRecord? Peak { get; init; }
        public BigInteger Space { get; init; }
        public ulong SubSlotIters { get; init; }
        public SyncState Sync { get; init; } = new();
    }
}
