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
    public partial class GenericNormalCollectionForm<T> : Simple.Framework.Gui.BaseForm
    {
        public GenericNormalCollectionForm()
        {
            InitializeComponent();

            InitializeForm();
        }

        void InitializeForm()
        {
            Type t = typeof(T);

            Mapping mapping = OrmEngine.GetMapping(t.FullName);

            this.SuspendLayout();

            int i = 0;

            foreach (Property p in MappingDataExtractor.GetProperties(mapping))
            {
                Label label = new Label();
                label.Left = 50;
                label.Top = i + 10;
                label.Text = PropertyDataExtractor.GetColumnName(p);
                
                Control control = null;

                switch (p.TypeName)
                {
                    case "System.String":
                        control = new TextBox();
                        control.Left = label.Left + 10 + label.Width;
                        control.Top = label.Top;
                    break;

                    case "System.DateTime":
                    control = new DateTimePicker();
                    control.Left = label.Left + 10 + label.Width;
                    control.Top = label.Top;
                    break;

                    default:
                    control = new TextBox();
                    control.Left = label.Left + 10 + label.Width;
                    control.Top = label.Top;
                    break;
                }

                //this.searchPanel.Controls.Add(label);
                //this.searchPanel.Controls.Add(control);

                i = i + 25;
            }
            
            this.ResumeLayout(false);
            this.PerformLayout();

            string str = string.Empty;
        }
    }
}
