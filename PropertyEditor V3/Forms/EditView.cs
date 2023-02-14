using PropertyEditor.Managers;
using PropertyEditor.Models;
using PropertyEditor.Models.Enums;
using System;
using System.Windows.Forms;

namespace PropertyEditor
{
    public partial class EditView : Form
    {
        public Objects Obj;
        public int Nation;
        public string ClassItem;
        public string OriginalValue;
        public EditView(Objects obj, int nation, string classItem)
        {
            InitializeComponent();
            this.Obj = obj;
            this.Nation = nation;
            this.ClassItem = classItem;
        }

        private void EditView_Load(object sender, EventArgs e)
        {
            Text = $@"Edit - {Obj.Keys.Name}";
            cbType.DataSource = Enum.GetValues(typeof(Models.Enums.ValueType));
            switch (ClassItem)
            {
                case "item":
                    txtValue.Text = Obj.Keys.Nations[Obj.Keys.Type == 9 ? Nation : 0].ToString();
                    OriginalValue = Obj.Keys.Nations[Obj.Keys.Type == 9 ? Nation : 0].ToString();
                    cbType.Text = ((Models.Enums.ValueType)Obj.Keys.ValueType).ToString();
                    break;
                case "folder":
                    txtValue.Text = Obj.Keys.Name;
                    OriginalValue = Obj.Keys.Name;
                    cbType.Text = Models.Enums.ValueType.STRING.ToString();
                    break;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            btSave.Enabled = false;
            if (OriginalValue.Equals(txtValue.Text))
            {
                Close();
                return;
            }
            Obj.Keys.ValueType = ClassItem == "item" 
                ? cbType.SelectedIndex 
                : Obj.Keys.ValueType; //change new value type
            var oldSize = Obj.Size;
            if(Obj.Keys.ValueType == 2) //STRING UNICODE MULTIPLIES FOR 2
            {
                var diference = (ulong)((OriginalValue.Length * 2) - (txtValue.Text.Length * 2));
                if (diference < 0)
                {
                    Obj.Size += diference;
                }
                else if (diference > 0)
                {
                    Obj.Size -= diference;
                }
            }
            else
            {
                var diference = (ulong)((OriginalValue.Length) - (txtValue.Text.Length));
                if (diference < 0)
                {
                    Obj.Size += diference;
                }
                else if (diference > 0)
                {
                    Obj.Size -= diference;
                }
            }
            Console.WriteLine(@"Old Size:{0} New Size:{1}", oldSize, Obj.Size);
            if (ClassItem == "item") // change new value
                Obj.Keys.Nations[Obj.Keys.Type == 9 ? Nation : 0] = txtValue.Text; 
            else
                Obj.Keys.Name = txtValue.Text;

            if (!ObjectsManager._editSaved.ContainsKey(Obj.Id))
            {
                ObjectsManager._editSaved.Add(Obj.Id, Obj);
            }
            else
            {
                ObjectsManager._editSaved[Obj.Id] = Obj;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            if (txtValue.Text == OriginalValue)
            {
                btSave.Enabled = false;
                return;
            }
            btSave.Enabled = true;
        }
    }
}
