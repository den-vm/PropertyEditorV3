﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace PropertyEditor
{
    static class Program
    {
        public static PropertyEditorView _propertyEditor;
        /// <summary>
        /// Main entry point to the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Utils.LoadConsoleHeader();
            Utils.LoadSettings();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _propertyEditor = new PropertyEditorView();
            Application.Run(_propertyEditor);
        }
    }
}
