using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Explorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateRootListView();
            CreateRootTreeView();
        }

        void CreateRootTreeView()
        {
            treeView1.Nodes.Clear();

            foreach (DriveInfo d in GetDrivers())
            {
                TreeNode treeNode = new TreeNode
                {
                    Text = d.Name,
                    ImageIndex = 0,
                    SelectedImageIndex = 0
                };
                treeView1.Nodes.Add(treeNode);
                CreateTreeViewNode(treeNode, 1);
            }
        }
        void CreateTreeViewNode(TreeNode node, int depth)
        {
            node.Nodes.Clear();
            try
            {
                foreach (DirectoryInfo d in (new DirectoryInfo(node.FullPath)).GetDirectories())
                {
                    TreeNode treeNode = new TreeNode
                    {
                        Text = d.Name,
                        ImageIndex = 1
                    };
                    node.Nodes.Add(treeNode);

                    if (depth > -1)
                    { CreateTreeViewNode(treeNode, depth - 1); }
                }
            }
            catch { }
        }
        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            CreateTreeViewNode(e.Node, 1);
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OpenPath((sender as TreeView).SelectedNode.FullPath, true);
        }

        void CreateRootListView()
        {
            listView1.Items.Clear();
            foreach (DriveInfo d in GetDrivers())
            {
                ListViewItem listViewItem = new ListViewItem
                {
                    Text = d.Name,
                    ImageIndex = 0,
                    Tag = d.Name
                };
                listViewItem.SubItems.Add((d.TotalFreeSpace / 1024 / 1024 / 1024).ToString() + " Gb");
                listViewItem.SubItems.Add(d.DriveFormat.ToString());

                listView1.Items.Add(listViewItem);
            }

            History.SetHistory("root");

            toolStripStatusLabel2.Text = listView1.Items.Count.ToString();
            comboBox2.Text = "Root";
        }


        void OpenPath(string path, bool history)
        {
            listView1.Items.Clear();

            if (history)
            { History.SetHistory(path); }

            toolStripStatusLabel2.Text = "0";

            var directoryInfo = new DirectoryInfo(path);
            try
            {
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    listView1.Items.Add(GetListViewItem(directory));
                }
                foreach (var fileinfo in directoryInfo.GetFiles())
                {
                    listView1.Items.Add(GetListViewItem(fileinfo));
                }
                toolStripStatusLabel2.Text = listView1.Items.Count.ToString();

                comboBox2.Text = path;
            }
            catch { }
        }
        ListViewItem GetListViewItem(DirectoryInfo directory)
        {
            ListViewItem listViewItem = new ListViewItem
            {
                Text = directory.Name,
                ImageIndex = 1,
                Tag = directory.FullName
            };
            return listViewItem;
        }
        ListViewItem GetListViewItem(FileInfo fileInfo)
        {
            ListViewItem listViewItem = new ListViewItem
            {
                Text = fileInfo.Name,
                Tag = fileInfo.FullName
            };
            listViewItem.SubItems.Add((fileInfo.Length / 1024).ToString() + " Kb");
            listViewItem.SubItems.Add(fileInfo.Extension);

            int i = -1;
            string ext = fileInfo.Extension;
            if (!string.IsNullOrWhiteSpace(ext))
                i = iconImageList.Images.IndexOfKey(ext.Remove(0, 1) + ".ico");

            if (i == -1)
            {
                listViewItem.ImageIndex = 2;
            }
            else
            {
                listViewItem.ImageIndex = i;
            }

            return listViewItem;
        }



        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;

            if (listView1.SelectedItems[0].ImageIndex > 1)
            {
                Process.Start(listView1.SelectedItems[0].Tag.ToString());
            }
            else
            {
                OpenPath(listView1.SelectedItems[0].Tag.ToString(), true);
            }
        }

        IEnumerable<DriveInfo> GetDrivers()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            return allDrives.Where(x => x.IsReady == true);
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = sender as ComboBox;
            listView1.View = (View)Enum.Parse(typeof(View), item.Text);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;

            var o = listView1.SelectedItems[0];
            LabelInfo.Text = o.Text;
        }

        private void listView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                PresBackspase();
            }
            else if(e.KeyChar == 46)
            {
                removeToolStripMenuItem_Click(sender, null);
            }
        }

        void PresBackspase()
        {
            string res = History.GetHistory();

            if (res == "root")
            {
                CreateRootListView();
            }
            else
            {
                OpenPath(res, false);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (listView1.SelectedItems[0].ImageIndex == 1)
                {
                    OpenPath(listView1.SelectedItems[0].Tag.ToString(), true);
                }
                else
                {
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        Process.Start(item.Tag.ToString());
                    }
                }

            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var result = MessageBox.Show($"Delete {listView1.SelectedItems.Count} files?", "Delete file",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        foreach (ListViewItem item in listView1.SelectedItems)
                        {
                            if (item.ImageIndex == 1)
                            {
                                if (Directory.Exists(item.Tag.ToString()))
                                {
                                    Directory.Delete(item.Tag.ToString(), true);
                                    listView1.Items.Remove(item);
                                }
                            }
                            else
                            {
                                if (System.IO.File.Exists(item.Tag.ToString()))
                                {
                                    System.IO.File.Delete(item.Tag.ToString());
                                    listView1.Items.Remove(item);
                                }
                            }
                        }
                    }
                    catch(Exception  ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(e.Label))
                {
                    e.CancelEdit = true;
                    return;
                }

                var item = listView1.Items[e.Item]; 
                string path = Path.GetDirectoryName(item.Tag.ToString());

                if (item.ImageIndex == 1)
                {
                    if (!Directory.Exists(item.Tag.ToString()))
                    { return; }
                    
                    Directory.Move(item.Tag.ToString(), Path.Combine(path, e.Label));
                }
                else
                {
                    if(!System.IO.File.Exists(item.Tag.ToString()))
                    {return;}

                    System.IO.File.Move(item.Tag.ToString(), Path.Combine(path, e.Label));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView1_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (listView1.Items[e.Item].Text.EndsWith(@":\"))
                e.CancelEdit = true;
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
          //  comboBox2.DataSource = History.GetAllHistory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PresBackspase();
        }
    }
}