using System.Windows.Forms;

namespace Platformer
{
    public class Button : System.Windows.Forms.Button
    {
        public Button()
        {
            this.SetStyle(ControlStyles.Selectable, false);
        }
    }
}