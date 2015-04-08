using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senc
{
    class ValueBox : System.Windows.Forms.TextBox
    {
        public float value()
        {
            if (this.Text.Length == 0)
                return 0;

            char lastChar = this.Text[this.Text.Length - 1];
            float toReturn = 0;

            if (lastChar >= '0' && lastChar <= '9')
            {
                this.Text = this.Text.Replace('.', ',');
                toReturn = float.Parse(this.Text);
                return toReturn;
            }
            else
            {
                float multipler = 1;

                this.Text = this.Text.Remove(this.Text.Length - 1);
                if (this.Text.Length > 0)
                {
                    this.Text = this.Text.Replace('.', ',');
                    try
                    {
                        toReturn = float.Parse(this.Text);          //, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        return 0f;
                    }
                    
                }

                switch (lastChar)
                {
                    case 'T':
                        multipler = (float)Math.Pow(10, 12);
                        break;
                    case 'G':
                        multipler = (float)Math.Pow(10, 9);
                        break;
                    case 'M':
                        multipler = (float)Math.Pow(10, 6);
                        break;
                    case 'k':
                        multipler = (float)Math.Pow(10, 3);
                        break;
                    case 'm':
                        multipler = (float)Math.Pow(10, -3);
                        break;
                    case 'u':
                        multipler = (float)Math.Pow(10, -6);
                        break;
                    case 'n':
                        multipler = (float)Math.Pow(10, -9);
                        break;
                    case 'p':
                        multipler = (float)Math.Pow(10, -12);
                        break;
                }
                return toReturn * multipler;
            }
        }
        public void hide()
        {
            this.Enabled = false;
            this.Visible = false;
            this.Text = null;
        }
        public void show (int x, int y)
        {
            int textBoxX;
            int textBoxY = (y - 20 < 0) ? 25 : y - 20;
            if ((x - (this.Width - 25) / 2) < 0)
                textBoxX = 0;
            else if ((x + this.Width) > (Form1.form.Width - 15))
                textBoxX = Form1.form.Width - this.Width - 15;
            else
                textBoxX = x - (this.Width - 25) / 2;
            this.Location = new System.Drawing.Point(textBoxX, textBoxY);
            this.Enabled = true;
            this.Visible = true;
            Form1.form.ActiveControl = this;
        }
    }
}
