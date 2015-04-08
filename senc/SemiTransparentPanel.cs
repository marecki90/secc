using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace senc
{
    class SemiTransparentPanel : Panel
    {
        public SemiTransparentPanel()
        {
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 0, 0, 0)), this.ClientRectangle);
        }

        public void fitTo(Object obj)
        {
            Location = new Point(0, 0);
            Form1.form.Controls.Add(this);
            Parent = obj as Control;
            Size = Parent.Size;
            BringToFront();
        }
    }
}
