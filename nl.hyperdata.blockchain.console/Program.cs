using nl.hyperdata.blockchain;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace nl.hyperdata.blockchain.console
{
    class Program
    {
        private static Random random = new Random(DateTime.Now.Millisecond);
        private static String blockChainNodePublicKey = "ThisIsTheBlockChainNodesPublicKey";

        /** proof of work (https://en.wikipedia.org/wiki/Proof-of-work_system)  is set here: a hash needs 2 trailing zero bytes, increase the number of bytes to reduce the number of valid  hashes, and increse the proof of work time **/
        private static readonly byte[] difficulty = new byte[] {0x00, 0x00};

        static void Main(string[] args)
        {
            ProofOfWork.Difficulty = difficulty;
            SHA512 sha = new SHA512Managed();
            BlockChainNode node = new BlockChainNode(sha.ComputeHash(Encoding.ASCII.GetBytes(blockChainNodePublicKey)));
            node.InitBlockChain();


            /** output the generated block details **/
            Console.WriteLine(node.BlockChain.LastOrDefault().ToString());

            /** validate the chain (ie: is each block's hash valid and is the prevoius block has valid) **/
            Console.WriteLine(String.Format("Chain is valid {0}\n", node.BlockChain.IsValid()));


            // ** start mining 20 blocks in a loop **/
            for (int i = 0; i < 20; i++)
            {
                /** randomly generated dummy data **/
                var data = Enumerable.Range(0, 256).Select(x => (byte) random.Next());
                node.ProcessTransaction(data.ToArray());

                /** output the generated block details **/
                Console.WriteLine(node.BlockChain.LastOrDefault().ToString());

                /** validate the chain (ie: is each block's hash valid and is the prevoius block has valid) **/
                Console.WriteLine(String.Format("Chain is valid {0}\n", node.BlockChain.IsValid()));
            }
        }
    }
}