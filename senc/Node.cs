using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senc
{
    public class Node : Block
    {
        private float potential;
        //private Block source;

        public List<Branch> branches;
        public Circuit circuit;
        public int blockID;
        public bool isSource;

        public static int nodeCounter;

        public Node(Block block, byte wayFrom, Circuit circuit)
        {
            inheritBlock(block);
            //this.blockArray = block.blockArray;
            id = ++nodeCounter;
            potential = 0;
            branches = new List<Branch>();
            this.circuit = circuit;
            if (block.imageNumber == 8 || block.imageNumber == 9)
                isSource = true;
            block.searchBranches(this, wayFrom, circuit);
        }

        public void inheritBlock(Block block)
        {
            x = block.x;
            y = block.y;
            way = block.way;
            blockID = block.id;
        }
    }
}
