using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Simple.Framework;

namespace Simple.Framework.Gui
{
    public partial class GenericSimpleCollectionForm<T> : BaseForm where T : ITypePoco<T>
    {
        public IBusinessLogic<T> BusinessLogic { get; set; }

        public GenericSimpleCollectionForm()
        {
            InitializeComponent();
            AddEventHandlers();

            LoadItemsToListBox();
            LoadTotal();
        }
        public GenericSimpleCollectionForm(IBusinessLogic<T> businessLogic)
        {
            InitializeComponent();
            AddEventHandlers();

            BusinessLogic = businessLogic;

            LoadItemsToListBox();
            LoadTotal();
        }

        private void LoadTotal()
        {
            IList<T> list = BusinessLogic.Get();

            if (list != null)
            {
                base.TotalToolStripStatusLabel.Text = list.Count.ToString();
            }
            else
            {
                base.TotalToolStripStatusLabel.Text = "0";
            }
        }

        private void LoadItemsToListBox()
        {
            IList<T> list = (IList<T>)BusinessLogic.Get();

            myListBox.DataSource = list;
            myListBox.DisplayMember = "Type";            
        }

        private void AddEventHandlers()
        {
            myAddButton.Click += new EventHandler(AddButton_Click);
            myRemoveButton.Click += new EventHandler(RemoveButton_Click);
        }

        void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (myTextBox.Text != "")
                {
                    T courseType = Activator.CreateInstance<T>();
                    courseType.Type = myTextBox.Text.Trim();

                    BusinessLogic.Save(courseType);

                    LoadItemsToListBox();
                    LoadTotal();

                    ClearControls();
                }
                else
                {
                    throw new Exception("Type??");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "");
            }
        }

        private void ClearControls()
        {
            myTextBox.Text = string.Empty;
        }

        void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                object obj = myListBox.SelectedItem;

                if (obj != null)
                {
                    T courseType = (T)obj;

                    BusinessLogic.Delete(courseType);

                    LoadItemsToListBox();
                    LoadTotal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "");
            }
        }
    }
}
