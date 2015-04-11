using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace senc
{
    public class windowNew : Form
    {
        Form1 mainForm;
        Label heightLabel;
        Label widthLabel;
        TextBox heightText;
        TextBox widthText;
        Button okButton;
        Button cancelButton;

        public windowNew(Form1 mainForm) : base()
        {
            this.mainForm = mainForm;
            widthLabel = new Label();
            heightLabel = new Label();
            widthText = new TextBox();
            heightText = new TextBox();
            okButton = new Button();
            cancelButton = new Button();

            int firstColumne = 40;
            int firstline = 10;
            int secondline = 35;

            //ActiveControl = null;         

            widthLabel.Text = "WIDTH:";
            heightLabel.Text = "HEIGHT:";
            okButton.Text = "OK";
            cancelButton.Text = "Cancel";

            widthLabel.Width = 55;
            heightLabel.Width = 55;
            widthText.Width = 25;
            heightText.Width = 25;
            okButton.Width = 60;
            cancelButton.Width = 60;

            widthLabel.Location = new Point(firstColumne, firstline);
            heightLabel.Location = new Point(firstColumne, secondline);
            widthText.Location = new Point(firstColumne + widthLabel.Width, firstline - 3);
            heightText.Location = new Point(firstColumne + heightLabel.Width, secondline - 3);
            okButton.Location = new Point(15, 60);
            cancelButton.Location = new Point(okButton.Location.X + okButton.Width + 10, 60);

            Controls.Add(widthLabel);
            Controls.Add(heightLabel);
            Controls.Add(widthText);
            Controls.Add(heightText);
            Controls.Add(okButton);
            Controls.Add(cancelButton);

            cancelButton.Click += new EventHandler(closeWindowButtonClick);
            okButton.Click += new EventHandler(okButtonClick);
            widthText.KeyDown += new System.Windows.Forms.KeyEventHandler(enterClick);
            heightText.KeyDown += new System.Windows.Forms.KeyEventHandler(enterClick);

            Size = new Size(170, 130);
            Text = "New...";
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            StartPosition = FormStartPosition.CenterScreen;
            Show();
        }

        private void closeWindowButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void enterClick(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                okButtonClick(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void okButtonClick(object sender, EventArgs e)
        {
            int height;
            int width;
            
            if(int.TryParse(heightText.Text, out height) && int.TryParse(widthText.Text, out width))
            {
                if(height > 0 && width > 14)
                {
                    Form1.blockArray = new BlockArray(height, width);
                    this.Close();
                }
                else
                    MessageBox.Show("Wrong value.");
            }
            else
                MessageBox.Show("Wrong value.");
        }

    }
}
