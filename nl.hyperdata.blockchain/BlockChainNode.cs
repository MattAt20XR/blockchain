using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


namespace nl.hyperdata.blockchain
{
    public class BlockChainNode : IMiner
    {
        private int numOfSubMiners = 3;
        private Random randMinerSelector = new Random(DateTime.Now.Millisecond);

        public BlockChain BlockChain { get; set; }
        public byte[] PublicKey { get; }
        private List<IMiner> miners = new List<IMiner>();


        public BlockChainNode(byte[] publicKey)
        {
            PublicKey = publicKey;

            for (int i = 0; i < numOfSubMiners; i++)
            {
                SHA512 sha = new SHA512Managed();
                byte[] minerName = (sha.ComputeHash(Encoding.ASCII.GetBytes("ThisIsTheSubMinersName" + i)));

                miners.Add(new SubMiner(minerName));
            }

            miners.Add(this);
        }

        public byte[] MineHash(IBlock block)
        {
            return ProofOfWork.MineHash(block, PublicKey);
        }

        public void InitBlockChain()
        {
            GenesisBlock genesis = new GenesisBlock();
            genesis.Hash = ProofOfWork.MineHash(genesis, PublicKey);
            genesis.MinersPublicKey = PublicKey;
            BlockChain = new BlockChain(genesis);
        }

        public void ProcessTransaction(byte[] data)
        {
            int minerIdx = randMinerSelector.Next() % miners.ToArray().Length;

            IMiner miner = miners[minerIdx];

            Block block = new Block(data);
            if (BlockChain.LastOrDefault() != null)
            {
                block.PreviousHash = BlockChain.LastOrDefault().Hash;
            }

            block.Hash = miner.MineHash(block);
            block.MinersPublicKey = miner.PublicKey;
            BlockChain.Add(block);
        }
    }
}