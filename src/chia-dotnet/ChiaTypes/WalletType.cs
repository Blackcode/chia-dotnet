﻿namespace chia.dotnet
{
    /// <summary>
    /// Wallet Types
    /// </summary>
    public enum WalletType : byte
    {
        STANDARD_WALLET = 0,
        RATE_LIMITED = 1,
        ATOMIC_SWAP = 2,
        AUTHORIZED_PAYEE = 3,
        MULTI_SIG = 4,
        CUSTODY = 5,
        CAT = 6,
        RECOVERABLE = 7,
        DISTRIBUTED_ID = 8,
        POOLING_WALLET = 9,
        NFT = 10
    }
}
