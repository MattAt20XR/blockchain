using System;

namespace nl.hyperdata.blockchain
{
    public class GenesisBlock : Block
    {
        public GenesisBlock() : base(new byte[] { })
        {
        }

        public override string ToString()
        {
            return String.Format(
                "\nGenisisBlock\nNonce: {0}\nTimeStamp: {1}\nMiners Public Key: {2}\nCurrent Block's Hash:  {3}\n\r",
                Nonce,
                TimeStamp,
                BitConverter.ToString(MinersPublicKey).Replace("-", ""),
                BitConverter.ToString(Hash).Replace("-", ""));
        }
    }
}