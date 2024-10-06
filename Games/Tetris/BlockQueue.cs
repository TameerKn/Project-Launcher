using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.Games.Tetris
{
    public class BlockQueue
    {
        private readonly Blocks[] blocks = new Blocks[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new SBlock(),
            new OBlock(),
            new ZBlock(),
            new TBlock(),
        };

        private readonly Random rnd = new Random();

        public Blocks NextBlock { get; private set; }

        public BlockQueue()
        {
            NextBlock = rndBlock();
        }
        private Blocks rndBlock()
        {
            return blocks[rnd.Next(blocks.Length)];
        }

        public Blocks GetAndUpdate()
        {
            Blocks block = NextBlock;

            do
            {
                NextBlock = rndBlock();
            }
            while (block.Id == NextBlock.Id);

            return block;
        }
    }
}
