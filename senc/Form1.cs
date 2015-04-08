﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace senc
{
    public partial class Form1 : Form
    {
        public static Form1 form;
        public static BlockArray blockArray;
        private bool projectStep;
        private Block thisBlock;
        private Circuit circuit;
        //private Point PanelMouseDownLocation;

         public Form1(int height, int width)
        {
            InitializeComponent();
            Block.generateImagesList();
            form = this;
            blockArray = new BlockArray(height, width);
            projectStep = true;
            thisBlock = null;
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            Button thisButton = sender as Button;
            int x = Convert.ToInt32(thisButton.Name.Split('.')[0]);
            int y = Convert.ToInt32(thisButton.Name.Split('.')[1]);
            thisBlock = blockArray.blocks[y][x];

            if (projectStep)
            {
                if (e.Button == MouseButtons.Left)
                    thisBlock.changeImageUp();
                else if (e.Button == MouseButtons.Right)
                    thisBlock.changeImageDown();
                else if (e.Button == MouseButtons.Middle)
                    thisBlock.rotateImageUp();

                // DEBUG
                else
                {
                    //MessageBox.Show(thisBlock.neighbourBlock(Block.left).id.ToString());
                }
            }
            else if (e.Button == MouseButtons.Left && thisBlock.imageNumber > 4)
                textBox.show(thisButton.Location.X, thisButton.Location.Y);

            if (this.ActiveControl != textBox)
                textBox.hide();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            textBox.hide();
        }

        private void btnStepClick(object sender, EventArgs e)
        {
            projectStep = !projectStep;
            btnStep.Text = projectStep ? "Input values" : "Back to drawing";
            textBox.hide();
        }

        private void textBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                textBox.hide();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                thisBlock.setValue(textBox.value());
                textBox.hide();
                e.SuppressKeyPress = true;
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (blockArray.sources.Count > 0)
                circuit = new Circuit(blockArray);
            else
                MessageBox.Show("There is no source.");


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            blockArray.clearTable();
        }

        public void PaintBorderlessGroupBox(object sender, PaintEventArgs p)
        {
            GroupBox box = sender as GroupBox;
            p.Graphics.Clear(Color.Black);
            p.Graphics.DrawString(box.Text, box.Font, Brushes.Black, 0, 0);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = sfd.FileName;
                blockArray = FileXML.saveArray(blockArray, path);
            }         
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if ( ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = ofd.FileName;
                blockArray = FileXML.loadArray(blockArray, path);
            }
        }

        private void windowNewCloseButton_Click(object sender, EventArgs e)
        {

        }

        /*
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
                PanelMouseDownLocation = e.Location;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (panel.Left + e.X - PanelMouseDownLocation.X > 0)
                {
                    if (panel.Left + e.X - PanelMouseDownLocation.X < Width - panel.Width - 16)
                        panel.Left += e.X - PanelMouseDownLocation.X;
                    else
                        panel.Left = Width - panel.Width - 16;
                }
                else
                    panel.Left = 0;

                if (panel.Top + e.Y - PanelMouseDownLocation.Y > 0)
                {
                    if (panel.Top + e.Y - PanelMouseDownLocation.Y < Height - panel.Height - 39)
                        panel.Top += e.Y - PanelMouseDownLocation.Y;
                    else
                        panel.Top = Height - panel.Height - 39;
                }
                else
                    panel.Top = 0;
            }
        }
         */

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form sizeForm = new Form();  //(160, 100);
            Label heightLabel = new Label();
            Label widthLabel = new Label();
            TextBox heightText = new TextBox();
            TextBox widthText = new TextBox();
            Button okButton = new Button();
            Button cancelButton = new Button();

            int firstColumne = 40;
            int firstline = 10;
            int secondline = 35;

            //ActiveControl = null;         


            heightLabel.Text = "HEIGHT:";
            widthLabel.Text = "WIDTH:";
            okButton.Text = "OK";
            cancelButton.Text = "Cancel";

            heightLabel.Width = 55;
            widthLabel.Width = 55;
            heightText.Width = 25;
            widthText.Width = 25;
            okButton.Width = 60;
            cancelButton.Width = 60;

            heightLabel.Location = new Point(firstColumne, firstline);
            widthLabel.Location = new Point(firstColumne, secondline);
            heightText.Location = new Point(firstColumne + heightLabel.Width, firstline - 3);
            widthText.Location = new Point(firstColumne + widthLabel.Width, secondline - 3);
            okButton.Location = new Point(15, 65);
            cancelButton.Location = new Point(okButton.Location.X + okButton.Width + 10, 65);


            //this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);


            sizeForm.Controls.Add(heightLabel);
            sizeForm.Controls.Add(widthLabel);
            sizeForm.Controls.Add(heightText);
            sizeForm.Controls.Add(widthText);
            sizeForm.Controls.Add(okButton);
            sizeForm.Controls.Add(cancelButton);



            sizeForm.Size = new Size(160, 100);
            //sizeForm.BorderStyle = BorderStyle.FixedSingle;
            sizeForm.Location = new Point(
                this.ClientSize.Width / 2 - sizeForm.Size.Width / 2,
                this.ClientSize.Height / 2 - sizeForm.Size.Height / 2);
            //sizePanel.Anchor = AnchorStyles.None;
            //sizeForm.FormBorderStyle = FormBorderStyle.None;

            sizeForm.Show();
        }
    }
    
}