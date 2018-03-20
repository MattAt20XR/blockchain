using System;

namespace jewelzcoin.blockchain
{
    public class Block : IBlock
    {
        public Block(byte[] data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Nonce = -1;
            PreviousHash = new byte[] {0x00};
            TimeStamp = DateTime.Now;
        }

        public byte[] Data { get; }

        public byte[] Hash { get; set; }

        public byte[] PreviousHash { get; set; }

        public int Nonce { get; set; }

        public DateTime TimeStamp { get; }

        public byte[] MinersPublicKey { get; set; }

        public override string ToString()
        {
            return String.Format(
                "Previous Block's Hash: {0}\nNonce: {1}\nTimeStamp: {2}\nMiners Public Key: {3}\nCurrent Block's Hash:  {4}\n\r",
                BitConverter.ToString(PreviousHash).Replace("-", ""),
                Nonce,
                TimeStamp,
                BitConverter.ToString(MinersPublicKey).Replace("-", ""),
                BitConverter.ToString(Hash).Replace("-", ""));
        }
    }
}