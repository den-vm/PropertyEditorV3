﻿using PropertyEditor.Managers;
using PropertyEditor.Models;
using PropertyEditor.Models.Enums;
using System;
using System.Windows.Forms;

namespace PropertyEditor
{
    public partial class EditView : Form
    {
        public Objects obj;
        public int nation;
        public EditView(Objects obj, int nation)
        {
            InitializeComponent();
            this.obj = obj;
            this.nation = nation;
        }

        private void EditView_Load(object sender, EventArgs e)
        {
            Text = string.Format("Edit - {0}", obj.Keys.Name);
            cbType.DataSource = Enum.GetValues(typeof(Models.Enums.ValueType));
            txtValue.Text = obj.Keys.Nations.Count > 0 
                ? obj.Keys.Nations[obj.Keys.Type == 9 ? nation : 0].ToString() 
                : obj.Keys.Name;
            cbType.Text = obj.Keys.IsFolder 
                ? Models.Enums.ValueType.STRING.ToString() 
                : ((Models.Enums.ValueType)obj.Keys.ValueType).ToString();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            btSave.Enabled = false;
            var lastValue = obj.Keys.Nations.Count > 0 
                ? obj.Keys.Nations[obj.Keys.Type == 9 ? nation : 0].ToString()
                : obj.Keys.Name;
            if (lastValue.Equals(txtValue.Text))
            {
                Close();
                return;
            }
            obj.Keys.ValueType = obj.Keys.IsFolder 
                ? obj.Keys.ValueType 
                : cbType.SelectedIndex; //change new value type
            var oldSize = obj.Size;
            if(obj.Keys.ValueType == 2) //STRING UNICODE MULTIPLIES FOR 2
            {
                ulong diference = (ulong)((lastValue.ToString().Length * 2) - (txtValue.Text.ToString().Length * 2));
                if (diference < 0)
                {
                    obj.Size += diference;
                }
                else if (diference > 0)
                {
                    obj.Size -= diference;
                }
            }
            else
            {
                ulong diference = (ulong)((lastValue.ToString().Length) - (txtValue.Text.ToString().Length));
                if (diference < 0)
                {
                    obj.Size += diference;
                }
                else if (diference > 0)
                {
                    obj.Size -= diference;
                }
            }
            Console.WriteLine("Old Size:{0} New Size:{1}", oldSize, obj.Size);
            if (obj.Keys.IsFolder)
                obj.Keys.Name = txtValue.Text;
            else 
                obj.Keys.Nations[obj.Keys.Type == 9 ? nation : 0] = txtValue.Text; // change new value

            if (!ObjectsManager._editSaved.ContainsKey(obj.Id))
            {
                ObjectsManager._editSaved.Add(obj.Id, obj);
            }
            else
            {
                ObjectsManager._editSaved[obj.Id] = obj;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            var objValue = obj.Keys.Nations.Count > 0
                ? obj.Keys.Nations[obj.Keys.Type == 9 ? nation : 0].ToString()
                : obj.Keys.Name;
            if (txtValue.Text == objValue)
            {
                btSave.Enabled = false;
                return;
            }
            btSave.Enabled = true;
        }
    }
}
