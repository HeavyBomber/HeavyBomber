using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhantomDevelopmentKit.Forms
{
    public partial class MainForm : Form
    {
        public IntPtr CanvasHandle
        {
            get { return canvas.Handle; }
        }

        public MainForm()
        {
            InitializeComponent();
        }
    }
}
