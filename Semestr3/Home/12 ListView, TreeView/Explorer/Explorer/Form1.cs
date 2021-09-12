using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            LoadDisk();
        }

        void LoadDisk()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    ListViewItem listViewItem = new ListViewItem
                    {
                        Text = d.Name,
                        ImageIndex = 0,
                        Tag = d.Name
                    };

                    listView1.Items.Add(listViewItem);

                    //Console.WriteLine("Drive {0}", ;
                    //Console.WriteLine("  File type: {0}", d.DriveType);

                    //Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    //Console.WriteLine("  File system: {0}", d.DriveFormat);
                    //Console.WriteLine("  Available space to current user:{0, 15} bytes", d.AvailableFreeSpace);

                    //Console.WriteLine("  Total available space:          {0, 15} bytes", d.TotalFreeSpace);

                    //Console.WriteLine("  Total size of drive:            {0, 15} bytes ", d.TotalSize);
                }
            }
        }

        void OpenPath(string path)
        {
            listView1.Items.Clear();

            var directoryInfo = new DirectoryInfo(path);

            foreach (var directory in directoryInfo.GetDirectories())
            {
                listView1.Items.Add(GetListViewItem(directory));
            }
            foreach (var fileinfo in directoryInfo.GetFiles())
            {
                listView1.Items.Add(GetListViewItem(fileinfo));
            }
        }

        ListViewItem GetListViewItem(DirectoryInfo directory)
        {
            ListViewItem listViewItem = new ListViewItem
            {
                Text = directory.Name,
                ImageIndex = 1,
                Tag = directory.FullName
            };
            listViewItem.SubItems.Add(directory.CreationTime.ToString());

            listView1.Items.Add(listViewItem);

            return listViewItem;
        }
        ListViewItem GetListViewItem(FileInfo fileInfo)
        {
            ListViewItem listViewItem = new ListViewItem
            {
                Text = fileInfo.Name,
                Tag = fileInfo.FullName
            };

            int i = iconImageList.Images.IndexOfKey(fileInfo.Extension.Remove(0, 1) + ".ico");
            if (i == -1)
            {
                listViewItem.ImageIndex = 2;
            }
            else
            {
                listViewItem.ImageIndex = i;
            }
            listView1.Items.Add(listViewItem);

            return listViewItem;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenPath((sender as ListView).SelectedItems[0].Tag.ToString());
        }
    }
}
