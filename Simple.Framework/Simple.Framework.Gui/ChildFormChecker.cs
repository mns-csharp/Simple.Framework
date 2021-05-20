using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Simple.Framework.Gui
{
    public class ChildFormChecker
    {
        public static bool IsOnlyOneChildExistant()
        {
            if (Application.OpenForms.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsAnyChildExistant()
        {
            if (Application.OpenForms.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsAlreadyExistant(Type childFormType)
        {
            bool alreadyExists = false;

            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == childFormType)
                {
                    form.Activate();
                    alreadyExists = true;
                }
            }

            return alreadyExists;
        }
    }
}
