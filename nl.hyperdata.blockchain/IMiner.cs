using System;
namespace nl.hyperdata.blockchain
{
    public interface IMiner
    {
        byte[] MineHash(IBlock block);
        byte[] PublicKey { get; }
    }
}
