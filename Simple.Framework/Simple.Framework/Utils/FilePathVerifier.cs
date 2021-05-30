using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Simple.Framework
{
    public class FilePathVerifier
    {
        public static bool IsValidPath(string path, bool allowRelativePaths = false)
        {
            bool isValid = true;

            try
            {
                string fullPath = Path.GetFullPath(path);

                if (allowRelativePaths)
                {
                    isValid = Path.IsPathRooted(path);
                }
                else
                {
                    string root = Path.GetPathRoot(path);
                    isValid = string.IsNullOrEmpty(root.Trim(new char[] { '\\', '/' })) == false;
                }
            }
            catch //(Exception ex)
            {
                isValid = false;
                throw;
            }

            return isValid;
        }
    }
}
