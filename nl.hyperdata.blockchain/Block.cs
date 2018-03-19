using System;

namespace nl.hyperdata.blockchain
{
    public class Block : IBlock
    {
        public Block(byte[] data, String minersPublicKey)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Nonce = -1;
            PreviousHash = new byte[] { 0x00 };
            TimeStamp = DateTime.Now;
            MinersPublicKey = minersPublicKey;
        }

        public byte[] Data { get; }

        public byte[] Hash { get; set; }

        public byte[] PreviousHash { get; set; }

        public int Nonce { get; set; }

        public DateTime TimeStamp { get; }

        public String MinersPublicKey { get; set; }

        public override string ToString()
        {
            //return String.Format("{0}:\n:{1}:\n:{2}:\n{3}\n\r", BitConverter.ToString(Hash).Replace("-", ""), BitConverter.ToString(PreviousHash).Replace("-", ""), Nonce, TimeStamp);
            return String.Format("Previous Block's Hash: {0}\nNonce: {1}\nTimeStamp: {2}\nMiners Public Key: {3}\nCurrent Block's Hash:  {4}\n\r", BitConverter.ToString(PreviousHash).Replace("-", ""), Nonce, TimeStamp, MinersPublicKey, BitConverter.ToString(Hash).Replace("-", ""));

        }
    }
}
