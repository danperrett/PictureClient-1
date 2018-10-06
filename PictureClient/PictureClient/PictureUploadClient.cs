using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace PictureClient
{
    public partial class PictureUploadForm : Form
    {
        private string username = "";
        private string password = "";
        private string FileName = "";
        static List<string> upload_files = new List<string>();
        Thread uploadThread = null;
        static volatile bool running = false;
        delegate void uploadPictureDelegate();
        delegate void setResultLabelDelegate(string text);
        delegate void updateLabelDelegate(string text);
        static string current_project = "";
        string label = "";
        System.Windows.Forms.Timer tl;
        bool update = false;
        FileListing listing = null;

        public PictureUploadForm()
        {
            InitializeComponent();
            running = true;
            uploadThread = new Thread(UploadWork);
            uploadThread.Priority = ThreadPriority.BelowNormal;
            uploadThread.Start(this);
            tl = new System.Windows.Forms.Timer();
            tl.Tick += new EventHandler(Tl_Tick);
            tl.Interval = 700;
            tl.Enabled = true;
            tl.Start();
        }

        public static void UploadWork(object data)
        {
            PictureUploadForm form = data as PictureUploadForm;
            if (form == null)
            {
                return;
            }
            while (running)
            {
                lock(upload_files)
                {
                    if(upload_files.Count > 0)
                    {
                        Thread.Sleep(10);
                        string file = upload_files.ElementAt(0);
                        string project = form.Project;
                        form.sendPicture(file, project, form.getFilename(file));
                        upload_files.RemoveAt(0);
                        form.updatePictureQueue();
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            running = false;
        }

        override protected void OnLoad(EventArgs e)
        {
            LogonPage logon = new LogonPage();
            if (logon.ShowDialog() == DialogResult.OK)
            {
                username = logon.Username;
                password = logon.Password;
                initialiseComboBox();
            }
            else
            {
                Close();
            }
           
        }

        public string Project
        {
            get
            {
                lock(current_project)
                {
                    return current_project;
                }
            }
        }

        void Download(string savetoo, int id)
        {
            using (PictureService.PictureInterfaceClient service = new PictureService.PictureInterfaceClient())
            {
                PictureService.EncryptionData edata = service.getDownloadFileRights(username, password, false, 1, id);
                BinaryWriter bw = new BinaryWriter(new FileStream(savetoo, FileMode.Create));
                for (int n =0; n < edata.splitInto; n++)
                {
                    try
                    {
                        PictureService.FileDownloadInfo  info = service.downloadFile(username, password, id, edata.access_key_id, 1, n + 1);
                        
                        byte[] data = Convert.FromBase64String(info.data);
                        bw.Write(data);
                    }
                    catch(Exception ex)
                    {
                        
                    }
                }
                bw.Close();
            }
        }

        private void initialiseComboBox()
        { 
            using (PictureService.PictureInterfaceClient service = new PictureService.PictureInterfaceClient())
            {
                PictureService.EncryptionData edata = service.getDownloadRights(username, password, false, 1);
                PictureService.ProjectInformation[] info = service.getProjects(username, password, edata.access_key_id);
                if (info != null)
                {
                    int size = info.Length;
                    if (size > 0)
                    {
                        System.Object[] ItemObject = new System.Object[size];
                        for (int i = 0; i < size; i++)
                        {
                            ItemObject[i] = info[i].id + "-" + info[i].projectName;
                        }
                        ProjectComboBox.Items.Clear();
                        ProjectComboBox.Items.AddRange(ItemObject);
                        ProjectComboBox.Text = info[0].id + "-" + info[0].projectName;
                    }
                }
            }
        }

        void setResultLabel(string text)
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new setResultLabelDelegate(this.setResultLabel), new object[] { text });
            }
            else
            {
                ResultLabel.Text = text;
            }
        }

        void sendPicture(string file, string project, string FileName)
        {
            using (PictureService.PictureInterfaceClient service = new PictureService.PictureInterfaceClient())
            {
                int proj_id = 0;
                string[] sp = null;
                lock (label)
                {
                    label = "";
                    update = true;
                }
                try
                {
                    sp = project.Split('-');
                    proj_id = Int32.Parse(sp[0]);

                    if (proj_id > 0)
                    {
                        byte[] fileBytes = File.ReadAllBytes(file);
                        int size = fileBytes.Length;
                        String bas = Convert.ToBase64String(fileBytes);
                        int success = -1;
                        PictureService.EncryptionData edata = service.getUploadRights(username, password, false, 1, FileName, fileBytes.Length);
                        int split = size / edata.splitInto;
                        byte[] copyFile = new byte[split];
                        int index = 0;
                        for (int n = 0; n < edata.splitInto-1; n++)
                        {
                            for(int i = 0; i < split; i++)
                            {
                                copyFile[i] = fileBytes[index + i];
                            }
                            bas = Convert.ToBase64String(copyFile);

                            if (edata.useable && edata.usePutPictures)
                            {
                                success = service.putPicture(bas, FileName, username, password, sp[1], proj_id, edata.access_key_id, 1, n+1);
                            }
                            else
                            {
                                success = service.uploadFile(bas, FileName, username, password, sp[1], edata.access_key_id, 1, n+1);
                            }
                            size -= split;
                            index += split;
                        }
                        copyFile = new byte[size];
                        for (int i = 0; i < size; i++)
                        {
                            copyFile[i] = fileBytes[index + i];
                        }
                        bas = Convert.ToBase64String(copyFile);
                        if (edata.useable && edata.usePutPictures)
                        {
                            success = service.putPicture(bas, FileName, username, password, sp[1], proj_id, edata.access_key_id, 1, edata.splitInto);
                        }
                        else
                        {
                            success = service.uploadFile(bas, FileName, username, password, sp[1], edata.access_key_id, 1, edata.splitInto);
                        }

                        setResultLabel("ID: " + success);
                    }
                }
                catch (Exception ex)
                {
                    setResultLabel("Error" + ex.Message);
                }
                lock (label)
                {
                    label = "";
                    update = false;
                    updateProgressLabel(label);
                }
            }
        }

        private void Tl_Tick(object sender, EventArgs e)
        {
            if (update)
            {
                if (label.Length > 8)
                {
                    label = "";
                }
                else
                {
                    label += ".";
                }
                updateProgressLabel(label);
            }
        }

        private string getFilename(string path)
        {
            string[] ps = path.Split('\\');
            if(ps.Length > 0)
            {
                return ps.Last();
            }
            else
            {
                return "";
            }
        }
        
        private void Send_Click(object sender, EventArgs e)
        {
            string text = FileTextBox.Text;
            if (!string.IsNullOrEmpty(text))
            {
                string project = ProjectComboBox.Text;
                sendPicture(text, project, getFilename(text));
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {

            //testDownload();
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Pictures files (*.jpg)|*.jpg";
            if(file.ShowDialog() == DialogResult.OK)
            {
                FileTextBox.Text = file.FileName;
                FileName = file.SafeFileName;
            }

        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CreateTextBox.Text))
            {
                using (PictureService.PictureInterfaceClient service = new PictureService.PictureInterfaceClient())
                {
                    PictureService.EncryptionData edata = service.getDownloadRights(username, password, false, 1);
                    int ret = service.createProject(username, password, CreateTextBox.Text, edata.access_key_id, 1);
                    ResultLabel.Text = "" + ret;
                    if(ret > 0)
                    {
                        initialiseComboBox();
                    }
                }
            }
        }

        public void PictureQueueListBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int count = 0;
            lock (upload_files)
            {
                foreach (string file in files)
                {
                    string type = file.Substring(file.Length - 4).ToLower();
                    //if (type == ".jpg")
                    {
                        upload_files.Add(file);
                    }
                }
            }
            updatePictureQueue();
        }

        private void updateProgressLabel(string text)
        {
            if (this.InvokeRequired)
            {
                Invoke(new updateLabelDelegate(this.updateProgressLabel), new object[] { text });
            }
            else
            {
                progressLabel.Text = text;
            }
        }
        private void updatePictureQueue()
        {
            if (this.InvokeRequired)
            {
                Invoke(new uploadPictureDelegate(this.updatePictureQueue));
            }
            else
            {

                System.Object[] ItemObject = new System.Object[upload_files.Count];
                int count = 0;
                foreach (string file in upload_files)
                {
                    ItemObject[count] = file;
                    count++;
                }

                PictureQueueListBox.Items.Clear();
                PictureQueueListBox.Items.AddRange(ItemObject);

            }
        }

        private void PictureQueueListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void ProjectComboBox_TextChanged(object sender, EventArgs e)
        {
            current_project = ProjectComboBox.Text;
        }

        private void FilesButton_Click(object sender, EventArgs e)
        {
            if (listing == null)
            {
                listing = new FileListing(this);
                using (PictureService.PictureInterfaceClient service = new PictureService.PictureInterfaceClient())
                {
                    PictureService.EncryptionData edata = service.getDownloadRights(username, password, false, 1);
                    PictureService.FileInformation[] info = service.getFileInformation(username, password, edata.access_key_id, 1, -1);
                    foreach (PictureService.FileInformation file in info)
                    {
                        listing.AddFile(file.filename, "", file.id);
                    }
                }
                listing.FormClosed += Listing_FormClosed;
            }
            listing.Show();
        }

        private void Listing_FormClosed(object sender, FormClosedEventArgs e)
        {
            listing = null;
        }

        public void downloadFile(string filename, int id, string filelocation)
        {
            Download(filelocation, id);
        }

        public void deleteFile(string filename, int id)
        {
            using (PictureService.PictureInterfaceClient service = new PictureService.PictureInterfaceClient())
            {
                Random rand = new Random();
                int r = rand.Next();
                PictureService.EncryptionData edata = service.getDownloadRights(username, password, false, r);
                if (service.deleteFile(username, password, id, edata.access_key_id, r))
                {
                    MessageBox.Show(filename + " deleted successfully!");
                }
            }
        }
    }
}
