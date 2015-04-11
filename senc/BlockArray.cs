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
        //public static Form1 form;
        //private const int W = 20;
        //private const int H = 15;
        //public Block[,] blocks;
        private static BlockArray currentArray;
        public List<List<Block>> blocks;
        public List<Block> sources;
        public int height;
        public int width;

        public BlockArray(int height, int width)
        {
            clearCurrentArray();
            Block.clearCounter();

            this.height = height;
            this.width = width;
            sources = new List<Block>();
            makeBlockList();
            currentArray = this;
        }
        public BlockArray(int height, int width, List<List<Block>> blocks, List<Block> sources)
        {
            clearCurrentArray();
            this.height = height;
            this.width = width;
            makeBlockList(blocks);
            //this.blocks = blocks;
            this.sources = sources;
            currentArray = this;
        }

        // TODO zmienić konstruktor na taki z argumentami
        protected BlockArray()
        {
            //sources = new List<Block>();
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
                    blocks[j].Add(new Block());

                    blocks[j][i].button = new System.Windows.Forms.Button();
                    blocks[j][i].button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    blocks[j][i].button.Location = new System.Drawing.Point(size * i + 1, size * j + 1);
                    blocks[j][i].button.FlatAppearance.BorderSize = 0;
                    blocks[j][i].button.Margin = new System.Windows.Forms.Padding(0);
                    blocks[j][i].button.Name = "" + i + "." + j;
                    blocks[j][i].button.Size = new System.Drawing.Size(size, size);
                    blocks[j][i].button.TabStop = false;
                    blocks[j][i].button.BackColor = System.Drawing.SystemColors.Control;
                    blocks[j][i].button.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);

                    blocks[j][i].x = i;
                    blocks[j][i].y = j;
                    blocks[j][i].blockArray = this;
                    Form1.form.addMouseEvent(blocks[j][i].button);
                    Form1.form.groupBox1.Controls.Add(blocks[j][i].button);
                    

                    // DEBUG
                    blocks[j][i].button.Text = blocks[j][i].id.ToString();
                    blocks[j][i].button.ForeColor = System.Drawing.Color.Red;
                    blocks[j][i].button.Font = new System.Drawing.Font(blocks[j][i].button.Font.FontFamily, 6, blocks[j][i].button.Font.Style);// | System.Drawing.FontStyle.Bold);
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
        
        private void makeBlockList(List<List<Block>> blocks)
        {
            int size = 25;

            Form1.form.Size = new System.Drawing.Size(width * size + 50, height * size + 120);
            Form1.form.groupBoxOutside.Size = new System.Drawing.Size(width * size + 2, height * size + 40);
            Form1.form.groupBoxOutside.Location = new System.Drawing.Point(16, 25);
            Form1.form.groupBox1.Size = new System.Drawing.Size(width * size + 2, height * size + 2);

            this.blocks = blocks;
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    blocks[j].Add(new Block());

                    Form1.form.addMouseEvent(blocks[j][i].button);
                    Form1.form.groupBox1.Controls.Add(blocks[j][i].button);


                    // DEBUG
                    blocks[j][i].button.Text = blocks[j][i].id.ToString();
                    blocks[j][i].button.ForeColor = System.Drawing.Color.Red;
                    blocks[j][i].button.Font = new System.Drawing.Font(blocks[j][i].button.Font.FontFamily, 6, blocks[j][i].button.Font.Style);// | System.Drawing.FontStyle.Bold);
                }
            }
        }

        public void clearCurrentArray()
        {
            if (currentArray != null)
                if (currentArray.blocks != null)
                    foreach (List<Block> list in currentArray.blocks)
                        foreach (Block block in list)
                            block.button.Dispose();
        }
  
        public void clearTable()
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    blocks[i][j].imageNumber = 0;
                    blocks[i][j].button.Image = null;
                    blocks[i][j].rotateNumber = 0;
                    blocks[i][j].value = 0;
                    blocks[i][j].way = 0;
                    blocks[i][j].check = false;
                }
        }

        /*
        public static BlockArray newArray(int height, int width)
        {
            foreach (List<Block> list in blocks)
                foreach (Block block in list)
                    block.button.Dispose();

            Block.clearCounter();
            return new BlockArray(height, width);
        }
        */
    }
}
