using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Localization;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using PropertyEditor.Managers;
using PropertyEditor.Models;
using PropertyEditor.Models.Enums;
using static System.Windows.Forms.ListViewItem;
using ValueType = PropertyEditor.Models.Enums.ValueType;

namespace PropertyEditor
{
    public partial class PropertyEditorView : Form
    {
        public bool _encryptedPef, isEnvSet;
        private readonly OpenFileDialog ofdPefSelector = new OpenFileDialog();
        private List<TreeNode> oldNodes = new List<TreeNode>();

        public PropertyEditorView()
        {
            InitializeComponent();
        }

        private void PropertyEditor_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Waiting for load file...");
            lbNation.Text = "Lang client: " + Settings.Nation;
            Console.WriteLine("Nation Selected: {0} idx: {1}", Settings.Nation, (int)Settings.Nation);
            ActiveControl = treePefFolders;
        }


        private void PropertyEditorView_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                detailComponentPef.Columns[0].Width = 120;
                detailComponentPef.Columns[1].Width = 200;
                detailComponentPef.Columns[2].Width = 130;
                detailComponentPef.Columns[3].Width = 150;
                detailComponentPef.Columns[4].Width = 400;
            }

            if (WindowState == FormWindowState.Normal)
            {
                detailComponentPef.Columns[0].Width = 98;
                detailComponentPef.Columns[1].Width = 113;
                detailComponentPef.Columns[2].Width = 124;
                detailComponentPef.Columns[3].Width = 107;
                detailComponentPef.Columns[4].Width = 121;
            }
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdPefSelector.ShowDialog() == DialogResult.OK)
            {
                FullClear(); //reset all infos

                var fileNameSplited = ofdPefSelector.FileName.Substring(ofdPefSelector.FileName.LastIndexOf("\\") + 1);

                isEnvSet = fileNameSplited.Contains("EnvSet");

                var dir = string.Format("{0}/Profiles/", Application.StartupPath);

                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                foreach (var paths in Directory.GetFiles(dir))
                {
                    var fileInfo = new FileInfo(paths);
                    if (fileInfo.Name.Split('.')[0] == fileNameSplited.Split('.')[0])
                    {
                        var buff = File.ReadAllBytes(paths);
                        var editsCount = BitConverter.ToInt32(buff, 0);
                        var nat = (Nation)BitConverter.ToInt32(buff, 4);

                        var sb = new StringBuilder();
                        sb.AppendLine(
                            $"{editsCount} old edits were found for this file in the client version {nat.ToString().ToUpper()}.\n");
                        sb.AppendLine("Load?");
                        sb.AppendLine();
                        sb.AppendLine("*Yes = Load.\n");
                        sb.AppendLine("*No = Do not load and keep saving the old editions.\n");
                        sb.AppendLine("*Cancel = Do not load and delete old edits.");

                        var dialog = MessageBox.Show(sb.ToString(), Application.ProductName,
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                            try
                            {
                                using (var binaryReader = new BinaryReader(new MemoryStream(buff)))
                                {
                                    ObjectsManager.LoadEdited(binaryReader);
                                    break;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Failed to load edits.", Application.ProductName, MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                break;
                            }

                        if (dialog == DialogResult.No) break;

                        if (MessageBox.Show("Do you really want to delete the old saved edits?",
                                Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            File.Delete(paths);
                            MessageBox.Show("Deleted successfully.", Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                }

                path_i3Pack.Text = ofdPefSelector.FileName;
                Console.WriteLine("Path file " + ofdPefSelector.FileName);
                Console.WriteLine("Loading file...");
                var buffer = File.ReadAllBytes(ofdPefSelector.FileName);
                var fileFormat = new byte[4];
                Array.Copy(buffer, 0, fileFormat, 0, 4);
                if (Encoding.GetEncoding(Settings.Encoding).GetString(fileFormat) != "I3R2")
                {
                    _encryptedPef = true;
                    for (var i = 0; i < buffer.Length; i += 2048)
                        BitRotate.Unshift(buffer, i, 2048, 3); //descriptografa em blocos de 2048 bytes (FIX)
                }

                lbEncrypted.Text = "Encrypted: " + _encryptedPef;
                Console.WriteLine("File encrypted: " + _encryptedPef);
                using (var reader = new BinaryReader(new MemoryStream(buffer)))
                {
                    HeaderManager.GetPefHeader(reader);
                    StringTablesManager.GetStringTables(reader, HeaderManager._header);
                    ObjectsManager.GetObjects(reader, HeaderManager._header);
                    ObjectsManager.GetObjectsValues(reader);
                    ObjectsManager.PutEdit();
                    ShowNodes();
                }
                //using (FileStream fs = new FileStream("Objects.json", FileMode.Create))
                //{
                //    using (StreamWriter sw = new StreamWriter(fs))
                //    {
                //        sw.WriteLine(JsonConvert.SerializeObject(ObjectManager._objects, Formatting.Indented));
                //    }
                //}
            }
        }

        public void ShowNodes()
        {
            pbTotal.Value = 0;
            var registryRoot = ObjectsManager.GetRegistryRoot();
            pbTotal.Maximum = ObjectsManager._objects.Count;
            treePefFolders.BeginUpdate();
            var tds = treePefFolders.Nodes.Add(registryRoot.Id.ToString(), registryRoot.Keys.Name);
            LoadFiles(registryRoot, tds);
            LoadSubDirectories(registryRoot, tds);
        }

        public void LoadFiles(Objects obj, TreeNode td)
        {
            foreach (var id in obj.Keys.Items)
                Task.Run(() =>
                {
                    var file = ObjectsManager.GetObjectById(id);
                    if (treePefFolders.InvokeRequired)
                    {
                        treePefFolders.Invoke(new Action(() =>
                        {
                            var tds = td.Nodes.Add(file.Id.ToString(), file.Keys.Name);
                            UpdateProgress();
                        }));
                    }
                    else
                    {
                        var tds = td.Nodes.Add(file.Id.ToString(), file.Keys.Name);
                        UpdateProgress();
                    }
                });
        }

        public void LoadSubDirectories(Objects obj, TreeNode td)
        {
            foreach (var id in obj.Keys.Folders)
                Task.Run(() =>
                {
                    var folder = ObjectsManager.GetObjectById(id);
                    if (treePefFolders.InvokeRequired)
                    {
                        treePefFolders.Invoke(new Action(() =>
                        {
                            var tds = td.Nodes.Add(folder.Id.ToString(), folder.Keys.Name);
                            LoadFiles(folder, tds);
                            LoadSubDirectories(folder, tds);
                            UpdateProgress();
                        }));
                    }
                    else
                    {
                        var tds = td.Nodes.Add(folder.Id.ToString(), folder.Keys.Name);
                        LoadFiles(folder, tds);
                        LoadSubDirectories(folder, tds);
                        UpdateProgress();
                    }
                });
        }

        private void UpdateProgress()
        {
            if (pbTotal.Value + 2 >= pbTotal.Maximum)
            {
                pbTotal.Value += 2;
                treePefFolders.Sort();
                treePefFolders.EndUpdate();
                treePefFolders.Enabled = true;
                detailComponentPef.Enabled = true;
                button1.Enabled = false;
                textBox1.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                pbTotal.Value = 0;
                MessageBox.Show("File uploaded successfully.", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (pbTotal.Value < pbTotal.Maximum)
            {
                pbTotal.Value++;
                var percent = (int)(pbTotal.Value / (double)pbTotal.Maximum * 100);
                //pbTotal.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));  
                //Application.DoEvents();
            }
        }

        private void tvFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Node.Name)) return;
            detailComponentPef.BeginUpdate();
            detailComponentPef.Items.Clear();
            //MessageBox.Show(e.Node.Name);
            var obj = ObjectsManager.GetObjectById(ulong.Parse(e.Node.Name));
            if (obj != null)
            {
                if (obj.Keys.IsFolder)
                {
                    var items = new List<Objects>();
                    foreach (var id in obj.Keys.Items)
                    {
                        var file = ObjectsManager.GetObjectById(id);
                        items.Add(file);
                    }

                    var ascending = items.OrderBy(x => x.Keys.Name); //list in crescent order
                    foreach (var file in ascending)
                    {
                        var objIdView = new ListViewItem();
                        objIdView.Text = file.Id.ToString();
                        var objNameView = new ListViewSubItem();
                        objNameView.Text = file.Keys.Name;
                        var objTypeView = new ListViewSubItem();
                        objTypeView.Text = file.Type.ToString();
                        var objValueTypeView = new ListViewSubItem();
                        objValueTypeView.Text = ((ValueType)file.Keys.ValueType).ToString();
                        var objValueView = new ListViewSubItem();
                        objValueView.Text = file.Keys.Type == 9
                            ? file.Keys.Nations[(int)Settings.Nation].ToString()
                            : file.Keys.Nations[0].ToString();
                        objIdView.SubItems.Add(objNameView);
                        objIdView.SubItems.Add(objTypeView);
                        objIdView.SubItems.Add(objValueTypeView);
                        objIdView.SubItems.Add(objValueView);
                        detailComponentPef.Items.Add(objIdView);
                    }
                }
                else
                {
                    var objIdView = new ListViewItem();
                    objIdView.Text = obj.Id.ToString();
                    var objNameView = new ListViewSubItem();
                    objNameView.Text = obj.Keys.Name;
                    var objTypeView = new ListViewSubItem();
                    objTypeView.Text = obj.Type.ToString();
                    var objValueTypeView = new ListViewSubItem();
                    objValueTypeView.Text = ((ValueType)obj.Keys.ValueType).ToString();
                    var objValueView = new ListViewSubItem();
                    objValueView.Text = obj.Keys.Type == 9
                        ? obj.Keys.Nations[(int)Settings.Nation].ToString()
                        : obj.Keys.Nations[0].ToString();
                    objIdView.SubItems.Add(objNameView);
                    objIdView.SubItems.Add(objTypeView);
                    objIdView.SubItems.Add(objValueTypeView);
                    objIdView.SubItems.Add(objValueView);
                    detailComponentPef.Items.Add(objIdView);
                }
            }

            detailComponentPef.EndUpdate();
        }

        public void SetProgressBarValue(long receive, long total)
        {
            try
            {
                pbTotal.Value = (int)(receive * 100 / total);
            }
            catch
            {
            }
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Changed item in folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var obj = ObjectsManager.GetObjectById(ulong.Parse(detailComponentPef.FocusedItem.Text));
            if (obj != null)
            {
                if (obj.Keys.IsFolder)
                    return;
                using (var edit = new EditView(obj, (int)Settings.Nation))
                {
                    if (edit.ShowDialog() == DialogResult.OK)
                    {
                        detailComponentPef.BeginUpdate();
                        detailComponentPef.Items.Clear();
                        UpdateTreeView(obj);
                        detailComponentPef.EndUpdate();
                    }
                }
            }
        }

        private void infosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = ObjectsManager.GetObjectById(ulong.Parse(treePefFolders.SelectedNode.Name));
            if (obj == null)
            {
                MessageBox.Show("The object could not be loaded.", Application.ProductName);
                return;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Type: " + obj.Type);
            stringBuilder.AppendLine("Id: " + obj.Id);
            stringBuilder.AppendLine("Offset: " + obj.Offset);
            stringBuilder.AppendLine("Size: " + obj.Size);
            stringBuilder.AppendLine("IsFolder: " + obj.Keys.IsFolder);
            if (obj.Keys.IsFolder)
            {
                stringBuilder.AppendLine("isRegistryRoot: " + obj.Keys.IsRegistryRoot);
                stringBuilder.AppendLine("Folders: " + obj.Keys.Folders.Count);
                stringBuilder.AppendLine("Items: " + obj.Keys.Items.Count);
            }
            else
            {
                stringBuilder.AppendLine("Nations: " + obj.Keys.NationsCount);
                stringBuilder.AppendLine("ValueType: " + (ValueType)obj.Keys.ValueType);
                if (obj.Keys.Type == 9)
                {
                    stringBuilder.AppendLine("***** [NATIONS] *****");
                    for (var i = 0; i < obj.Keys.NationsCount; i++)
                        stringBuilder.AppendLine(string.Format(" {1} Value: {2}", i, ((Nation)i).ToString(),
                            obj.Keys.Nations[i]));
                    stringBuilder.AppendLine("***** [NATIONS] *****");
                }
                else
                {
                    stringBuilder.AppendLine("Value:" + obj.Keys.Nations[0]);
                }
            }

            MessageBox.Show(stringBuilder.ToString(), string.Format("{0} - Infos", obj.Keys.Name));
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var settings = new SettingsView())
            {
                if (settings.ShowDialog() == DialogResult.OK)
                    MessageBox.Show("Settings saved successfully.", Application.ProductName);
            }
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copyright © Exploit Network 2021 and SITD 2022\nDevelopers: Coyote, PISTOLA, den-vm", Application.ProductName);
        }

        private void FullClear()
        {
            Console.WriteLine("Cleaning all old info...");
            path_i3Pack.Text = "None";
            isEnvSet = false;
            detailComponentPef.Enabled = false;
            treePefFolders.Enabled = false;
            lbEncrypted.Text = "None";
            _encryptedPef = false;
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            treePefFolders.Nodes.Clear();
            detailComponentPef.Items.Clear();
            gridControl1.DataSource = null;
            pbTotal.Value = 0;
            button1.Enabled = false;
            HeaderManager._header = new Header();
            StringTablesManager._stringTables = new List<StringTable>();
            ObjectsManager._changeOffsets = new Dictionary<ulong, ulong>();
            ObjectsManager._objects = new List<Objects>();
            ObjectsManager._editSaved = new Dictionary<ulong, Objects>();
            ObjectsManager._loadSaved = new Dictionary<ulong, Objects>();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
            var text = textBox1.Text.Trim().ToLower();
            if (text.Length > 0)
            {
                var cloneObjects = ObjectsManager._objects;
                var foundObjects = cloneObjects.FindAll(x =>
                    (x.Keys.NationsCount > 0
                        ? x.Keys.Type == 9
                            ? x.Keys.Nations[(int)Settings.Nation].ToString()
                            : x.Keys.Nations[0].ToString()
                        : "") == text);


                var getTreeNodeFoundObjects = new List<TreeNode>();
                foreach (var obj in foundObjects)
                {
                    var findNode = treePefFolders.Nodes.Find(obj.Id.ToString(), true);
                    if (findNode.Length > 0)
                        getTreeNodeFoundObjects.AddRange(findNode);
                }

                var collectionListBoxObject =
                    getTreeNodeFoundObjects.Select(x => $"{x.FullPath.Replace(@"\", " -> ")}\t[Id = {x.Name}]").ToArray();
                if (getTreeNodeFoundObjects.Count != 0)
                {
                    var dataTable = new DataTable("ResultFind");
                    dataTable.Columns.AddRange(new[]
                    {
                        new DataColumn("ID"),
                        new DataColumn("Name"), 
                        new DataColumn("Path")
                    });

                    foreach (var nodeElem in getTreeNodeFoundObjects)
                    {
                        var row = dataTable.NewRow();
                        row["ID"] = nodeElem.Name;
                        row["Name"] = nodeElem.Text;
                        row["Path"] = nodeElem.FullPath;
                        dataTable.Rows.Add(row);
                    }
                    gridControl1.DataSource = dataTable;
                }
                
                Console.WriteLine("Founded items: {0}", getTreeNodeFoundObjects.Count);
                lbFoundNodes.Text = string.Format("Search: {0} items found.", getTreeNodeFoundObjects.Count);
            }
        }

        private void LoadSubChidrenNodes(TreeNode treeNode, List<TreeNode> foundNodes, string text)
        {
            for (var i = 0; i < treeNode.Nodes.Count; i++)
            {
                var item = treeNode.Nodes[i];
                if (item.Text.ToLower().Contains(text)) foundNodes.Add(item);
                LoadSubChidrenNodes(item, foundNodes, text);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ofdPefSelector.FileName))
                return;
            var oldFileName = ofdPefSelector.FileName.Substring(ofdPefSelector.FileName.LastIndexOf("\\") + 1);
            using (var fs = new FileStream(ofdPefSelector.FileName, FileMode.Create))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    HeaderManager.WriteHeader(bw);
                    StringTablesManager.WriteStringTables(bw);
                    ObjectsManager.WriteObjects(bw);
                    ObjectsManager.WriteObjectsKeys(bw);
                    ObjectsManager.SetOffsets(bw);
                }

                if (ObjectsManager._editSaved.Count > 0)
                    using (var fileStream =
                           new FileStream(
                               string.Format("{0}/Profiles/XXXXXXXXXXX", Application.StartupPath)
                                   .Replace("XXXXXXXXXXX", $"{oldFileName.Split('.')[0]}.dat"), FileMode.Create))
                    {
                        using (var binaryWriter = new BinaryWriter(fileStream))
                        {
                            ObjectsManager.SaveEdited(binaryWriter);
                        }
                    }

                MessageBox.Show("File saved successfully.", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            if (_encryptedPef)
            {
                var buffer = File.ReadAllBytes(ofdPefSelector.FileName);
                for (var i = 0; i < buffer.Length; i += 2048)
                    BitRotate.Shift(buffer, i, 2048, 3); //descriptografa em blocos de 2048 bytes (FIX)
                using (var fs = new FileStream(ofdPefSelector.FileName, FileMode.Create))
                {
                    fs.Write(buffer, 0, buffer.Length);
                }
            }

            pbTotal.Value = 0;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfdSaveFile = new SaveFileDialog();
            var oldFileName = ofdPefSelector.FileName.Substring(ofdPefSelector.FileName.LastIndexOf("\\") + 1);
            sfdSaveFile.Filter = "|" + oldFileName;
            sfdSaveFile.FileName = oldFileName;
            if (sfdSaveFile.ShowDialog() == DialogResult.OK)
            {
                using (var fs = new FileStream(sfdSaveFile.FileName, FileMode.Create))
                {
                    using (var bw = new BinaryWriter(fs))
                    {
                        HeaderManager.WriteHeader(bw);
                        StringTablesManager.WriteStringTables(bw);
                        ObjectsManager.WriteObjects(bw);
                        ObjectsManager.WriteObjectsKeys(bw);
                        ObjectsManager.SetOffsets(bw);
                    }

                    if (ObjectsManager._editSaved.Count > 0)
                        using (var fileStream =
                               new FileStream(
                                   string.Format("{0}/Profiles/XXXXXXXXXXX", Application.StartupPath)
                                       .Replace("XXXXXXXXXXX", $"{oldFileName.Split('.')[0]}.dat"), FileMode.Create))
                        {
                            using (var binaryWriter = new BinaryWriter(fileStream))
                            {
                                ObjectsManager.SaveEdited(binaryWriter);
                            }
                        }

                    MessageBox.Show("File saved successfully.", Application.ProductName, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                if (_encryptedPef)
                {
                    var buffer = File.ReadAllBytes(sfdSaveFile.FileName);
                    for (var i = 0; i < buffer.Length; i += 2048)
                        BitRotate.Shift(buffer, i, 2048, 3); //descriptografa em blocos de 2048 bytes (FIX)
                    using (var fs = new FileStream(sfdSaveFile.FileName, FileMode.Create))
                    {
                        fs.Write(buffer, 0, buffer.Length);
                    }
                }
            }

            pbTotal.Value = 0;
        }

        private void lvDataItems_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void desbloquearScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdPefSelector.ShowDialog() == DialogResult.OK)
            {
                FullClear(); //reset all infos
                Console.WriteLine("Path file " + ofdPefSelector.FileName);
                Console.WriteLine("Loading file...");

                var buffer = File.ReadAllBytes(ofdPefSelector.FileName);
                var newBuffer = new byte[buffer.Length];

                using (var fs = new FileStream(ofdPefSelector.FileName, FileMode.Create))
                {
                    using (var bw = new BinaryWriter(fs))
                    {
                        bw.Write(newBuffer);
                    }

                    MessageBox.Show("File unlocked.", Application.ProductName, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                FullClear(); //reset all infos
            }
        }

        private void addItemPef_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In development...", Application.ProductName);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            var ea = e as DXMouseEventArgs;
            var view = sender as GridView;
            var info = view.CalcHitInfo(ea.Location);

            if (!info.InRow && !info.InRowCell) 
                return;

            var node = treePefFolders.Nodes.Find(gridView1.GetFocusedDataRow()["ID"].ToString(), true);
            if (node.Length == 1)
            {
                treePefFolders.SelectedNode = node[0];
                treePefFolders.Focus();
            }
            else
                MessageBox.Show(@"No or more than one ID value was found for the selected item", @"Error view node",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void gridControl1_DataSourceChanged(object sender, EventArgs e)
        {
            gridView1.BestFitColumns();
        }

        /// <summary>
        /// Changed folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameItemPef_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = ObjectsManager.GetObjectById(ulong.Parse(treePefFolders.SelectedNode.Name));
            if (obj != null)
            {
                if (obj.Keys.IsRegistryRoot || treePefFolders.SelectedNode.Parent.Text == @"RegistryRoot")
                    return;
                using (var edit = new EditView(obj, (int)Settings.Nation))
                {
                    if (edit.ShowDialog() == DialogResult.OK)
                    {
                        treePefFolders.BeginUpdate();
                        detailComponentPef.BeginUpdate();
                        detailComponentPef.Items.Clear();
                        treePefFolders.SelectedNode.Name = edit.obj.Id.ToString();
                        treePefFolders.SelectedNode.Text = edit.obj.Keys.Name;
                        UpdateTreeView(obj);
                        treePefFolders.EndUpdate();
                        detailComponentPef.EndUpdate();
                    }
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.Text.Length > 0)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        /// <summary>
        /// Update view in component TreeView
        /// </summary>
        /// <param name="obj">Item from Objects</param>
        private void UpdateTreeView(Objects obj)
        {
            var objNode = ObjectsManager.GetObjectById(ulong.Parse(treePefFolders.SelectedNode.Name));
            if (objNode != null)
            {
                if (objNode.Keys.IsFolder)
                {
                    var items = new List<Objects>();
                    foreach (var id in objNode.Keys.Items)
                    {
                        var file = ObjectsManager.GetObjectById(id);
                        items.Add(file);
                    }

                    var ascending = items.OrderBy(x => x.Keys.Name); //list in crescent order
                    foreach (var file in ascending)
                    {
                        var objIdView = new ListViewItem();
                        objIdView.Text = file.Id.ToString();
                        var objNameView = new ListViewSubItem();
                        objNameView.Text = file.Keys.Name;
                        var objTypeView = new ListViewSubItem();
                        objTypeView.Text = file.Type.ToString();
                        var objValueTypeView = new ListViewSubItem();
                        objValueTypeView.Text = ((ValueType)file.Keys.ValueType).ToString();
                        var objValueView = new ListViewSubItem();
                        objValueView.Text = file.Keys.Type == 9
                            ? file.Keys.Nations[(int)Settings.Nation].ToString()
                            : file.Keys.Nations[0].ToString();
                        objIdView.SubItems.Add(objNameView);
                        objIdView.SubItems.Add(objTypeView);
                        objIdView.SubItems.Add(objValueTypeView);
                        objIdView.SubItems.Add(objValueView);
                        detailComponentPef.Items.Add(objIdView);
                    }
                }
                else
                {
                    var objIdView = new ListViewItem();
                    objIdView.Text = objNode.Id.ToString();
                    var objNameView = new ListViewSubItem();
                    objNameView.Text = objNode.Keys.Name;
                    var objTypeView = new ListViewSubItem();
                    objTypeView.Text = objNode.Type.ToString();
                    var objValueTypeView = new ListViewSubItem();
                    objValueTypeView.Text = ((ValueType)objNode.Keys.ValueType).ToString();
                    var objValueView = new ListViewSubItem();
                    objValueView.Text = objNode.Keys.Type == 9
                        ? objNode.Keys.Nations[(int)Settings.Nation].ToString()
                        : objNode.Keys.Nations[0].ToString();
                    objIdView.SubItems.Add(objNameView);
                    objIdView.SubItems.Add(objTypeView);
                    objIdView.SubItems.Add(objValueTypeView);
                    objIdView.SubItems.Add(objValueView);
                    detailComponentPef.Items.Add(objIdView);
                }
            }

            MessageBox.Show(@"Success", obj.GetNameTitle(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}