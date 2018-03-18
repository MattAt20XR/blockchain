using System;

namespace nl.hyperdata.blockchain
{
    public class Block : IBlock
    {
        public Block(byte[] data, String minersPubLey)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Nonce = -1;
            PreviousHash = new byte[] { 0x00 };
            TimeStamp = DateTime.Now;
            MinersPublicKey = minersPubLey;
        }

        public byte[] Data { get; }

        public byte[] Hash { get; set; }

        public byte[] PreviousHash { get; set; }

        public int Nonce { get; set; }

        public DateTime TimeStamp { get; }

        public String MinersPublicKey { get; set; }

        public override string ToString()
        {
            return String.Format("{0}:\n:{1}:\n:{2}:\n{3}\n\r", BitConverter.ToString(Hash).Replace("-", ""), BitConverter.ToString(PreviousHash).Replace("-", ""), Nonce, TimeStamp);
        }
    }
}
