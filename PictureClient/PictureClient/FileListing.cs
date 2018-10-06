using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureClient
{
    public partial class FileListing : Form
    {
        Dictionary<int, string> fileList = new Dictionary<int, string>();
        PictureUploadForm parent = null;
        public FileListing(PictureUploadForm parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        public void AddFile(string file, string date, int id)
        {
            fileList.Add(id, file);
            FileListBox.Items.Add(file + " : " + id);
        }

        private void getDownload(int selected)
        {
            SaveFileDialog save = new SaveFileDialog();
            int id = -1;
            string fileName = "";
            int count = 0;
            foreach (KeyValuePair<int, string> entry in fileList)
            {
                if (count == selected)
                {
                    id = entry.Key;
                    fileName = entry.Value;
                    break;
                }
                count++;
            }
            save.FileName = fileName;
            if (save.ShowDialog() == DialogResult.OK)
            {
                string savetoo = save.FileName;
                if (id > -1)
                {
                    if (parent != null)
                    {
                        parent.downloadFile(fileName, id, savetoo);
                    }
                }
            }
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selected = FileListBox.SelectedIndex;
            if(selected >= 0)
            {
                getDownload(selected);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selected = FileListBox.SelectedIndex;
            if (selected >= 0)
            {
                int id = -1;
                string fileName = "";
                int count = 0;
                foreach (KeyValuePair<int, string> entry in fileList)
                {
                    if (count == selected)
                    {
                        id = entry.Key;
                        fileName = entry.Value;
                        break;
                    }
                    count++;
                }

                if(MessageBox.Show("Are you sure you want to delete " + fileName, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    parent.deleteFile(fileName, id);
                }
            }
        }

        private void FileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = FileListBox.SelectedIndex;
            if (selected >= 0)
            {

            }
        }

        private void FileListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int selected = FileListBox.SelectedIndex;
            if (selected >= 0)
            {
                getDownload(selected);
            }
        }

        private void FileListBox_DragDrop(object sender, DragEventArgs e)
        {
            parent.PictureQueueListBox_DragDrop(sender, e);
        }

        private void FileListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
    }
}
