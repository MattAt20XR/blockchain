using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace nl.hyperdata.blockchain
{
    public class BlockChain : IEnumerable<IBlock>
    {
        private List<IBlock> blocks = new List<IBlock>();

        public BlockChain(IBlock genesis)
        {
            Blocks.Add(genesis);
        }

        public void Add(IBlock block)
        {
             Blocks.Add(block);
        }

        public int Count => Blocks.Count;
        public IBlock this[int index] { get => Blocks[index]; set => Blocks[index] = value; }
        public List<IBlock> Blocks { get => blocks; set => blocks = value; }

        public IEnumerator<IBlock> GetEnumerator()
        {
            return Blocks.GetEnumerator();
        }

        public IBlock LastOrDefault() 
        {
            return blocks.LastOrDefault();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Blocks.GetEnumerator();
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
