using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace senc
{
    [Serializable()]
    public class Block : Button
    {
        public const byte left = 8;
        public const byte up = 4;
        public const byte right = 2;
        public const byte down = 1;
        public const int size = 25;

        [System.Xml.Serialization.XmlIgnore]
        public BlockArray blockArray;

        public int id;
        public string type;
        public float value;
        public int x;
        public int y;
        public int imageNumber;
        public int rotateNumber;
        public byte way;
        public bool check;              // TODO ogarnąć

        public static List<string>[] images;

        private static int idCounter;

        public Block(int x, int y) : base()
        {
            this.x = x;
            this.y = y;
            id = ++idCounter;
            type = "";
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            //Margin = new System.Windows.Forms.Padding(0);
            Margin = new Padding(0, 0, 0, 0);
            Size = new Size(size, size);
            BackColor = SystemColors.Control;
            Location = new Point(size * x + 1, size * y + 1);
            TabStop = false;
            Name = "" + x.ToString() + "." + y.ToString();

            // DEBUG
            Text = id.ToString();
            ForeColor = Color.Red;
            Font = new Font(Font.FontFamily, 6, Font.Style); // | System.Drawing.FontStyle.Bold);
        }

        public Block() : base()
        {
            Console.WriteLine("działaj!");
        }

        static Block()
        {
            idCounter = 0;
        }

        public static void clearCounter()
        {
            idCounter = 0;
        }

        public void setValue (float newValue)
        {
            value = newValue;
            Form1.form.setToolTip(this);
        }

        public static byte rotateByte(byte b, int value)
        {
            b = (byte)((b >> value) | (b << (4 - value)));        // bits right rotate
            b <<= 4;
            b >>= 4;
            return (byte)b;
        }
        private void setCurrentWay()
        {
            if (imageNumber == 1 || (imageNumber >= 5 && imageNumber <= 9))
                way = 10;       // 1010 -
            else if (imageNumber == 2)
                way = 9;        // 1001 -,
            else if (imageNumber == 3)
                way = 11;       // 1011 T
            else if (imageNumber == 4)
                way = 15;       // 1111 x

            way = rotateByte(way, this.rotateNumber);
        }
        private bool isBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }
        public bool checkWay(byte way)
        {
            switch(way)
            {
                case Block.left:
                    return isBitSet(this.way, 3);
                case Block.up:
                    return isBitSet(this.way, 2);
                case Block.right:
                    return isBitSet(this.way, 1);
                case Block.down:
                    return isBitSet(this.way, 0);
                default:
                    return false;
            }
        }
        public void changeImage(int number)
        {
            //if (number == 1 || number == -1)
            {
                if (imageNumber == 8 || imageNumber == 9)
                    blockArray.sources.Remove(blockArray.sources.Find(s => s.id == id));

                if (imageNumber + number > images.Length - 1)
                    imageNumber = 0;
                else if (imageNumber + number < 0)
                    imageNumber = images.Length - 1;
                else
                    imageNumber += number;

                if (imageNumber == 0)
                    this.Image = null;
                else
                    this.Image = System.Drawing.Image.FromFile(images[imageNumber][0]);

                rotateNumber = 0;
                setCurrentWay();

                if (imageNumber == 8 || imageNumber == 9)
                    blockArray.sources.Add(this);
            }
        } // zmienić na prived
        public void changeImageUp()
        {
            changeImage(1);
        }
        public void changeImageDown()
        {
            changeImage(-1);
        }
        public void rotateImageUp()
        {
            rotateImage(1);
        }
        public void rotateImageDown()
        {
            rotateImage(-1);
        }
        public void rotateImage(int number)
        {
            if (imageNumber > 0)
            {
                if (rotateNumber + number >= images[imageNumber].Count)
                    rotateNumber = 0;
                else if (rotateNumber + number < 0)
                    rotateNumber = images[imageNumber].Count - 1;
                else
                    rotateNumber += number;
                this.Image = System.Drawing.Image.FromFile(images[imageNumber][rotateNumber]);

                setCurrentWay();
            }
        }
        public static void generateImagesList()
        {
            images = new List<string>[]
                { 
                    new List<string>(),      // 0
                    new List<string>(),      // 1
                    new List<string>(),      // 2
                    new List<string>(),      // 3
                    new List<string>(),      // 4
                    new List<string>(),      // 5
                    new List<string>(),      // 6
                    new List<string>(),      // 7
                    new List<string>(),      // 8
                    new List<string>()       // 9
                };

            //images[0].Add("");

            images[1].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\wireH.gif");
            images[1].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\wireV.gif");

            images[2].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\wireL1.gif");
            images[2].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\wireL2.gif");
            images[2].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\wireL3.gif");
            images[2].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\wireL4.gif");

            images[3].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\wireT1.gif");
            images[3].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\wireT2.gif");
            images[3].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\wireT3.gif");
            images[3].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\wireT4.gif");

            images[4].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\wireX.gif");

            images[5].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\R1.gif");
            images[5].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\R2.gif");

            images[6].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\C1.gif");
            images[6].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\C2.gif");

            images[7].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\L1.gif");
            images[7].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\L2.gif");

            images[8].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\VoltageSource1.gif");
            images[8].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\VoltageSource2.gif");
            images[8].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\VoltageSource3.gif");
            images[8].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\VoltageSource4.gif");

            images[9].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\CurrentSource1.gif");
            images[9].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\CurrentSource2.gif");
            images[9].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\CurrentSource3.gif");
            images[9].Add(@"D:\!Duperele\!Dysk Google\Programowanie\senc\senc\images\CurrentSource4.gif");          //TODO dll
        }
        private Block leftBlock()
        {
            if (this.x > 0)
                return blockArray.blocks[y][x - 1];
            else
                return null;
        }
        private Block rightBlock()
        {
            if (this.x < blockArray.blocks[0].Count - 1)
                return blockArray.blocks[y][x + 1];
            else
                return null;
        }
        private Block upBlock()
        {
            if (this.y > 0)
                return blockArray.blocks[y - 1][x];
            else
                return null;
        }
        private Block downBlock()
        {
            if (this.y < blockArray.blocks.Count - 1)
                return blockArray.blocks[y + 1][x];
            else
                return null;
        }
        public Block neighbourBlock(byte way)
        {
            switch(way)
            {
                case Block.left:
                    return leftBlock();
                case Block.up:
                    return upBlock();
                case Block.right:
                    return rightBlock();
                case Block.down:
                    return downBlock();
                default:
                    return null;
            }
        }
        public void searchOneBranch(Node rootNode, byte targetWay, Circuit circuit)
        {
            if (checkWay(targetWay))
            {
                Block neighbour = neighbourBlock(targetWay);
                byte oppositeWay = Block.rotateByte(targetWay, 2);
                if (neighbour != null)
                    if (neighbour.checkWay(oppositeWay))
                        if (neighbour.imageNumber <= 4)
                        {
                            if (neighbour.imageNumber > 2)
                            {
                                if (circuit.nodes.Find(node => node.blockID == neighbour.id) == null)
                                {
                                    Node newNode = new Node(neighbour, oppositeWay, circuit);
                                    circuit.addToJoinList(rootNode, newNode);
                                    return;
                                }
                            }
                            neighbour.searchBranches(rootNode, oppositeWay, circuit);
                        }
                        else
                            rootNode.branches.Add(new Branch(rootNode, neighbour, oppositeWay, circuit));
            }
        }
        public void searchBranches(Node rootNode, byte wayFrom, Circuit circuit)
        {
            byte tmpWay = way;
            way -= wayFrom;

            searchOneBranch(rootNode, Block.left, circuit);               // TODO sprawdzić, czy na pewno nie popierdzieliłem kierunków
            searchOneBranch(rootNode, Block.up, circuit);
            searchOneBranch(rootNode, Block.right, circuit);
            searchOneBranch(rootNode, Block.down, circuit);

            way = tmpWay;
         }
    }
}
