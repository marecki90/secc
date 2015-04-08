using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace senc
{
    public class WindowPanel : Panel
    {
        public WindowPanel(int height, int width)
        {
            Size = new System.Drawing.Size(height, width);
            BorderStyle = BorderStyle.FixedSingle;
            Anchor = AnchorStyles.None;
        }

        public void addControl(System.Windows.Forms.Control control, int width, int locationX, int locationY)
        {
            control.Width = width;
            control.Location = new System.Drawing.Point(locationX, locationY);
            Controls.Add(control);
        }
        public void addControl(System.Windows.Forms.Control control, int width, int locationX, int locationY, string text)
        {
            control.Text = text;
            control.Width = width;
            control.Location = new System.Drawing.Point(locationX, locationY);
            Controls.Add(control);
        }
        public void center(Form form)
        {
            Location = new Point(
                form.ClientSize.Width / 2 - Size.Width / 2,
                form.ClientSize.Height / 2 - Size.Height / 2);
        }
    }
}
