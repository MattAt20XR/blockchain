using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace nl.hyperdata.blockchain
{
    public static class BlockExtensions
    {
        static  byte[] failedHash;

        public static byte[] GenerateHash(this IBlock block)
        {
            using (SHA512 sha = new SHA512Managed())
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                if (block.Nonce == 0 ) {
                    // First hash for the block. MinersPublicKey, Data, & Timestamp
                    // Are only added to the bite[] the first time a hash is
                    // calculated.
                    writer.Write(block.MinersPublicKey);
                    writer.Write(block.Data);
                    writer.Write(block.TimeStamp.ToBinary());
                    writer.Write(block.PreviousHash);
                 } else {
                    writer.Write(failedHash);                   
                }
                // Nouce isn't actually needed. 
                writer.Write(block.Nonce);

                var streamArray = stream.ToArray();
                return sha.ComputeHash(streamArray);
            }
        }

        public static byte[] MineHash(this IBlock block, byte[] difficulty)
        {
            failedHash = null;
            if(difficulty == null)
            {
                throw new ArgumentNullException(nameof(difficulty));
            }

            byte[] hash = new byte[0];
            int d = difficulty.Length;

            while (!hash.Take(d).SequenceEqual(difficulty) && block.Nonce <= int.MaxValue)
            {
                block.Nonce++;
                hash = block.GenerateHash();
                // cache the hash encase it fails validation.
                failedHash = hash;
            }
            return hash;
        }

        public static bool HasValidHash(this IBlock block)
        {
            var curr = block.GenerateHash();
            return block.Hash.SequenceEqual(curr);
        }

        public static bool HasValidPreviousHash(this IBlock block, IBlock previousblock)
        {
            if (previousblock == null)
            {
                throw new ArgumentException("previousblock is null", "previousblock");
            }
            var prev = previousblock.GenerateHash();
            return previousblock.HasValidHash() && block.PreviousHash.SequenceEqual(prev);
        }


    }
}
