using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senc
{
    public class Branch
    {
        public int id;
        public Node input;
        public Node output;
        public bool check;
        public List<Block> blocks;
        private float current;
        private Circuit circuit;
        private static int counter;
        
        public Branch(Node node, Block firstBlock, byte wayFrom, Circuit circuit)
        {
            id = ++counter;
            input = node;
            blocks = new List<Block>();
            blocks.Add(firstBlock);
            this.circuit = circuit;
            makeBranch(firstBlock, wayFrom);
        }

        private void makeOneWayBranch(Block actualBlock, byte wayFrom, byte targetWay)
        {
            if (actualBlock.checkWay(targetWay))
            {
                Block neighbour = actualBlock.neighbourBlock(Block.up);
                if (neighbour != null)
                    if (neighbour.checkWay(Block.rotateByte(targetWay, 2)))
                    {
                        if (neighbour.imageNumber <= 2)
                            this.makeBranch(neighbour, Block.down);
                        else if (neighbour.imageNumber <= 4)
                        {
                            int i = circuit.nodes.FindIndex(n => n.blockID == actualBlock.neighbourBlock(Block.left).id);
                            if (i == -1)
                                this.output = new Node(neighbour, Block.down, this.circuit);
                            else
                            {
                                this.output = circuit.nodes[i];
                                circuit.nodes[i].branches.Add(this);
                                checkBranch();
                            }
                        }
                        else
                        {
                            blocks.Add(neighbour);
                            this.makeBranch(neighbour, Block.down);
                        }
                    }
            }
        }
        protected void makeBranch(Block actualBlock, byte wayFrom)
        {

            byte tmpWay = actualBlock.way;
            actualBlock.way -= wayFrom;

            makeOneWayBranch(actualBlock, wayFrom, Block.left);
            makeOneWayBranch(actualBlock, wayFrom, Block.up);
            makeOneWayBranch(actualBlock, wayFrom, Block.right);
            makeOneWayBranch(actualBlock, wayFrom, Block.down);
            actualBlock.way = tmpWay;
        }
        private void checkBranch()
        {
            foreach (Block block in blocks)
                block.check = true;
        }
    }
}