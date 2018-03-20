using System;

namespace jewelzcoin.blockchain
{
    public interface IMiner
    {
        byte[] MineHash(IBlock block);
        byte[] PublicKey { get; }
    }
}