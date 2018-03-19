using System;
namespace nl.hyperdata.blockchain
{
    public class SubMiner : IMiner
    {
        public byte[] PublicKey { get; }

        public SubMiner(byte[] publicKey)
        {
            PublicKey = publicKey;

        }

        public byte[] MineHash(IBlock block) 
        {
            block.MinersPublicKey = PublicKey;
            return ProofOfWork.MineHash(block, PublicKey);
        }
  
    }
}
