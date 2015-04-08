using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senc
{
    public class Circuit
    {
        public List<Node> nodes;
        public List<List<Node>> nodeToJoin;
        private BlockArray blockArray;

        public Circuit(BlockArray a)
        {
            blockArray = a;
            nodes = new List<Node>();
            nodeToJoin = new List<List<Node>>();
            makeCircuit();
        }

        public void makeCircuit()
        {
            Block source = blockArray.sources[blockArray.sources.Count - 1];          // TODO zapętlić aż znajdzie obwód
            nodes.Add(new Node(source, (byte)((source.way & Block.up) + (source.way & Block.left)), this));           // down or right
        }
        public void addToJoinList(Node first, Node second)
        {
            List<Node> firstExistedList;
            List<Node> secondExistedList;
            firstExistedList = nodeToJoin.Find(list => list.Exists(node => node.id == first.id));
            secondExistedList = nodeToJoin.Find(list => list.Exists(node => node.id == second.id));

            if (firstExistedList == null && secondExistedList == null)
            {
                nodeToJoin[nodeToJoin.Count] = new List<Node>();
                nodeToJoin[nodeToJoin.Count - 1].Add(first);
                nodeToJoin[nodeToJoin.Count - 1].Add(second);
            }
            else if (firstExistedList != null && secondExistedList != null)
                firstExistedList.AddRange(secondExistedList);
            else if (firstExistedList == null)
                secondExistedList.Add(first);
            else
                firstExistedList.Add(second);
        }
        public void checkBranches()         // TODO sprawdzić
        {
            foreach (Node node in nodes)
                foreach (Branch branch in node.branches)
                    if (!branch.check)
                        if (branch.input == branch.output)
                        {
                            if (!branch.blocks.Exists(block => (block.imageNumber == 8 || block.imageNumber == 9)))
                            {
                                branch.input.branches.Remove(branch);
                                branch.input.branches.Remove(branch);

                            }
                        }
                        else
                            branch.check = true;
        }
    }
}
