using System;
namespace nl.hyperdata.blockchain
{
    public class GenisisBlock : Block
    {
        public GenisisBlock() : base(new byte[] { 0x00 }, "")
        { 
        }

        public override string ToString()
        {
            return String.Format("\nGenisisBlock\nNonce: {0}\nTimeStamp: {1}\nMiners Public Key: {2}\nCurrent Block's Hash:  {3}\n\r", Nonce, TimeStamp, MinersPublicKey, BitConverter.ToString(Hash).Replace("-", ""));

        }
    }
}