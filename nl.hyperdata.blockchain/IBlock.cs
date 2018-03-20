using System;

namespace jewelzcoin.blockchain
{
    public interface IBlock
    {
        byte[] Data { get; }
        byte[] Hash { get; set; }
        int Nonce { get; set; }
        byte[] PreviousHash { get; set; }
        DateTime TimeStamp { get; }
        byte[] MinersPublicKey { get; set; }
    }
}