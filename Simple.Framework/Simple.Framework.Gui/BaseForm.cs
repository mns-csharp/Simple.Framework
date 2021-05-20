using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Simple.Framework.Orm;

namespace Simple.Framework.Gui
{
    public partial class BaseForm : Form
    {
        ///////////Event Mechanism///////////
        public event ItemStateChanged ItemStateChangeEvent;
        public void OnItemStateChanged()
        {
            if (ItemStateChangeEvent != null)
            {
                ItemStateChangeEvent();
            }
        }
        ///////////Event Mechanism///////////

        protected internal ToolStripStatusLabel TotalToolStripStatusLabel
        {
            get { return this.baseTotalToolStripStatusLabel; }
            set { this.baseTotalToolStripStatusLabel = value; }
        }

        protected internal ToolStripStatusLabel MessageToolStripStatusLabel
        {
            get { return this.baseMessageToolStripStatusLabel; }
            set { this.baseMessageToolStripStatusLabel = value; }
        }

        public BaseForm()
        {
            InitializeComponent();
        }

        private void BaseForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            //base.OnKeyPress(e);

            int charInt = Convert.ToInt16(e.KeyChar);

            switch (charInt)
            {
                case 27:
                    this.Close();
                    break;
            }
        }
    }
}
