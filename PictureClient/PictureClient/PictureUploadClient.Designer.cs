namespace PictureClient
{
    partial class PictureUploadForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FileTextBox = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PictureQueueListBox = new System.Windows.Forms.ListBox();
            this.CreateButton = new System.Windows.Forms.Button();
            this.CreateTextBox = new System.Windows.Forms.TextBox();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.ProjectComboBox = new System.Windows.Forms.ComboBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileTextBox
            // 
            this.FileTextBox.Location = new System.Drawing.Point(7, 100);
            this.FileTextBox.Name = "FileTextBox";
            this.FileTextBox.Size = new System.Drawing.Size(209, 20);
            this.FileTextBox.TabIndex = 0;
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(7, 130);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(75, 23);
            this.Send.TabIndex = 1;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PictureQueueListBox);
            this.groupBox1.Controls.Add(this.CreateButton);
            this.groupBox1.Controls.Add(this.CreateTextBox);
            this.groupBox1.Controls.Add(this.ResultLabel);
            this.groupBox1.Controls.Add(this.ProjectComboBox);
            this.groupBox1.Controls.Add(this.BrowseButton);
            this.groupBox1.Controls.Add(this.Send);
            this.groupBox1.Controls.Add(this.FileTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 307);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // PictureQueueListBox
            // 
            this.PictureQueueListBox.AllowDrop = true;
            this.PictureQueueListBox.FormattingEnabled = true;
            this.PictureQueueListBox.Location = new System.Drawing.Point(7, 159);
            this.PictureQueueListBox.Name = "PictureQueueListBox";
            this.PictureQueueListBox.Size = new System.Drawing.Size(289, 147);
            this.PictureQueueListBox.TabIndex = 7;
            this.PictureQueueListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.PictureQueueListBox_DragDrop);
            this.PictureQueueListBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.PictureQueueListBox_DragEnter);
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(221, 62);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateButton.TabIndex = 6;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // CreateTextBox
            // 
            this.CreateTextBox.Location = new System.Drawing.Point(7, 62);
            this.CreateTextBox.Name = "CreateTextBox";
            this.CreateTextBox.Size = new System.Drawing.Size(208, 20);
            this.CreateTextBox.TabIndex = 5;
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Location = new System.Drawing.Point(99, 135);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(0, 13);
            this.ResultLabel.TabIndex = 4;
            // 
            // ProjectComboBox
            // 
            this.ProjectComboBox.FormattingEnabled = true;
            this.ProjectComboBox.Location = new System.Drawing.Point(7, 20);
            this.ProjectComboBox.Name = "ProjectComboBox";
            this.ProjectComboBox.Size = new System.Drawing.Size(290, 21);
            this.ProjectComboBox.TabIndex = 3;
            this.ProjectComboBox.TextChanged += new System.EventHandler(this.ProjectComboBox_TextChanged);
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(221, 98);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseButton.TabIndex = 2;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // PictureUploadForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 331);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PictureUploadForm";
            this.Text = "Picture Upload";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox FileTextBox;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox ProjectComboBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.TextBox CreateTextBox;
        private System.Windows.Forms.ListBox PictureQueueListBox;
    }
}

