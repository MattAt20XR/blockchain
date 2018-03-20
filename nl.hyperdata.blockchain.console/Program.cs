using jewelzcoin.blockchain;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace jewelzcoin.blockchain.console
{
    class Program
    {
        private static Random random = new Random(DateTime.Now.Millisecond);
        private static String blockChainNodePublicKey = "ThisIsTheBlockChainNodesPublicKey";

        private static readonly byte[] difficulty = new byte[] {0x00, 0x00, 0x00};

        static void Main(string[] args)
        {
            ProofOfWork.Difficulty = difficulty;
            SHA256 sha = new SHA256Managed();
            BlockChainNode node = new BlockChainNode(sha.ComputeHash(Encoding.ASCII.GetBytes(System.Environment.MachineName + blockChainNodePublicKey)));
            node.InitBlockChain();


            /** output the generated block details **/
            Console.WriteLine(node.BlockChain.LastOrDefault().ToString());

            /** validate the chain (ie: is each block's hash valid and is the prevoius block has valid) **/
            Console.WriteLine(String.Format("Chain is valid {0}\n", node.BlockChain.IsValid()));


            // ** start mining 20 blocks in a loop **/
            while (true)
            {
                /** randomly generated dummy data **/
                var trx = Enumerable.Range(0, 256).Select(x => (byte) random.Next());
                node.ProcessTransaction(trx.ToArray());

                /** output the generated block details **/
                Console.WriteLine(node.BlockChain.LastOrDefault().ToString());

                /** validate the chain (ie: is each block's hash valid and is the prevoius block has valid) **/
                Console.WriteLine(String.Format("Chain is valid {0}\n", node.BlockChain.IsValid()));
            }
        }
    }
}