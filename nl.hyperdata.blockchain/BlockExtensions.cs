using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace nl.hyperdata.blockchain
{
    public static class BlockExtensions
    {
        private static byte[] failedHash = null;

        public static byte[] GenerateHash(this IBlock block)
        {
            using (SHA512 sha = new SHA512Managed())
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                if (block.Nonce == 0 && block.MinersPublicKey.Length != 0 ) {
                    writer.Write(block.MinersPublicKey);
                }
                if (failedHash != null ) {
                    writer.Write(failedHash);
                }
                writer.Write(block.Data);
                writer.Write(block.Nonce);
                writer.Write(block.TimeStamp.ToBinary());
                writer.Write(block.PreviousHash);
                var streamArray = stream.ToArray();
                return sha.ComputeHash(streamArray);
            }
        }

        public static byte[] MineHash(this IBlock block, byte[] difficulty)
        {
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
