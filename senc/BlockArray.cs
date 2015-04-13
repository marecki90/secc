using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senc
{
    [Serializable()]
    public class BlockArray
    {
        public int height;
        public int width;

        private static BlockArray currentArray;

        //[System.Xml.Serialization.XmlIgnore]
        public List<List<Block>> blocks;

        [System.Xml.Serialization.XmlIgnore]
        public List<Block> sources;
        

        public BlockArray(int height, int width)
        {
            clearCurrentArray();
            Block.clearCounter();

            this.height = height;
            this.width = width;
            sources = new List<Block>();
            makeBlockList();
            currentArray = this;
            Console.WriteLine("k z argumentami");
        }

        // TODO zmienić konstruktor na taki z argumentami
        protected BlockArray()
        {
            //sources = new List<Block>();
            Console.WriteLine("k BEZ argumentów");
        }

        private void makeBlockList()
        {
            int size = 25;

            Form1.form.Size = new System.Drawing.Size(width * size + 50, height * size + 120);
            Form1.form.groupBoxOutside.Size = new System.Drawing.Size(width * size + 2, height * size + 40);
            Form1.form.groupBoxOutside.Location = new System.Drawing.Point(16, 25);
            Form1.form.groupBox1.Size = new System.Drawing.Size(width * size + 2, height * size + 2);

            blocks = new List<List<Block>>();
            for (int j = 0; j < height; j++)
            {
                blocks.Add(new List<Block>());
                for (int i = 0; i < width; i++)
                {
                    blocks[j].Add(new Block(i, j));
                    blocks[j][i].blockArray = this;
                    Form1.form.addMouseEvent(blocks[j][i]);
                    Form1.form.groupBox1.Controls.Add(blocks[j][i]);
                }
            }
            // DEBUG
            ////blocks[5][5].changeImage(2);
            ////blocks[5][5].rotateImage();
            ////blocks[5][5].rotateImage();
            ////blocks[5][5].rotateImage();

            ////blocks[6][5].changeImage(2);
            ////blocks[6][5].rotateImage();
            ////blocks[6][5].rotateImage();

            ////blocks[6][7].changeImage(2);
            ////blocks[6][7].rotateImage();

            ////blocks[5][7].changeImage(2);

            ////blocks[5][6].changeImage(8);
            ////this.sources.Add(blocks[5][6]);

            ////blocks[6][6].changeImage(5);
        }    

        public void clearCurrentArray()
        {
            if (currentArray != null)
                if (currentArray.blocks != null)
                    foreach (List<Block> list in currentArray.blocks)
                        foreach (Block block in list)
                            block.Dispose();
        }
  
        public void clearTable()
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    blocks[i][j].imageNumber = 0;
                    blocks[i][j].Image = null;
                    blocks[i][j].rotateNumber = 0;
                    blocks[i][j].value = 0;
                    blocks[i][j].way = 0;
                    blocks[i][j].check = false;
                }
        }
    }
}
