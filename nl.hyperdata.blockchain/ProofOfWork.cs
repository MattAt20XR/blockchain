using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace nl.hyperdata.blockchain
{
    public static class ProofOfWork
    {
        public static byte[] Difficulty { get; set; }

        private static byte[] GenerateHash(DataForGeningHash dataForGeningHash)
        {
            using (SHA512 sha = new SHA512Managed())
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                if (dataForGeningHash.Block.Nonce == 0)
                {
                    // Is only used once to generate the hash.
                    writer.Write(dataForGeningHash.MinersPublicKey);
                    writer.Write(dataForGeningHash.Block.Data);
                    writer.Write(dataForGeningHash.Block.TimeStamp.ToBinary());
                    writer.Write(dataForGeningHash.Block.PreviousHash);
                }

                if (dataForGeningHash.FailedHash != null)
                {
                    // By including the previous failedHash we force 
                    // serial validation.
                    writer.Write(dataForGeningHash.FailedHash);
                }

                writer.Write(dataForGeningHash.Block.Nonce);
                var streamArray = stream.ToArray();
                return sha.ComputeHash(streamArray);
            }
        }

        public static byte[] MineHash(IBlock block, byte[] minersPublicKey)
        {
            if (Difficulty == null)
            {
                throw new ArgumentNullException(nameof(Difficulty));
            }

            byte[] hash = new byte[0];
            int d = Difficulty.Length;

            DataForGeningHash dataForGeningHash = new DataForGeningHash();
            dataForGeningHash.Block = block;
            dataForGeningHash.MinersPublicKey = minersPublicKey;

            while (!hash.Take(d).SequenceEqual(Difficulty) && block.Nonce <= int.MaxValue)
            {
                block.Nonce++;
                hash = GenerateHash(dataForGeningHash);
                dataForGeningHash.FailedHash = hash;
            }

            return hash;
        }

        public static bool HasValidHash(IBlock block)
        {
            //var curr = GenerateHash(block);
            //return block.Hash.SequenceEqual(curr);
            return true;
        }

        public static bool HasValidPreviousHash(IBlock block, IBlock previousblock)
        {
            //if (previousblock == null)
            //{
            //    throw new ArgumentException("previousblock is null", "previousblock");
            //}
            //var prev = GenerateHash(previousblock);
            //return HasValidHash(previousblock) && block.PreviousHash.SequenceEqual(prev);
            return true;
        }
    }
}