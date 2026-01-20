using OneNoteExporter.Controls;

namespace OneNoteExporter.Forms
{
    partial class OneNoteExporterForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            treeViewNotebooks = new FixedTreeView();
            checkBoxSelectAll = new CheckBox();
            textBoxExportPath = new TextBox();
            buttonBrowseFiles = new Button();
            folderBrowserDialog = new FolderBrowserDialog();
            buttonExport = new Button();
            labelExportFolder = new Label();
            buttonRefresh = new Button();
            SuspendLayout();
            // 
            // treeViewNotebooks
            // 
            treeViewNotebooks.CheckBoxes = true;
            treeViewNotebooks.Location = new Point(12, 37);
            treeViewNotebooks.Name = "treeViewNotebooks";
            treeViewNotebooks.Size = new Size(393, 178);
            treeViewNotebooks.TabIndex = 0;
            treeViewNotebooks.AfterCheck += TreeViewNotebooks_AfterCheck;
            // 
            // checkBoxSelectAll
            // 
            checkBoxSelectAll.AutoSize = true;
            checkBoxSelectAll.Location = new Point(21, 12);
            checkBoxSelectAll.Name = "checkBoxSelectAll";
            checkBoxSelectAll.Size = new Size(74, 19);
            checkBoxSelectAll.TabIndex = 1;
            checkBoxSelectAll.Text = "Select All";
            checkBoxSelectAll.UseVisualStyleBackColor = true;
            checkBoxSelectAll.CheckedChanged += CheckBoxSelectAll_CheckedChanged;
            // 
            // textBoxExportPath
            // 
            textBoxExportPath.Location = new Point(12, 251);
            textBoxExportPath.Name = "textBoxExportPath";
            textBoxExportPath.Size = new Size(299, 23);
            textBoxExportPath.TabIndex = 2;
            textBoxExportPath.Text = "C:\\Users\\jakef\\Documents";
            // 
            // buttonBrowseFiles
            // 
            buttonBrowseFiles.Location = new Point(317, 250);
            buttonBrowseFiles.Name = "buttonBrowseFiles";
            buttonBrowseFiles.Size = new Size(88, 23);
            buttonBrowseFiles.TabIndex = 3;
            buttonBrowseFiles.Text = "Browse";
            buttonBrowseFiles.UseVisualStyleBackColor = true;
            buttonBrowseFiles.Click += ButtonBrowseFiles_Click;
            // 
            // buttonExport
            // 
            buttonExport.Location = new Point(126, 296);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new Size(152, 39);
            buttonExport.TabIndex = 4;
            buttonExport.Text = "Export";
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += ButtonExport_Click;
            // 
            // labelExportFolder
            // 
            labelExportFolder.AutoSize = true;
            labelExportFolder.Location = new Point(12, 233);
            labelExportFolder.Name = "labelExportFolder";
            labelExportFolder.Size = new Size(76, 15);
            labelExportFolder.TabIndex = 5;
            labelExportFolder.Text = "Export Folder";
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(317, 8);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(88, 23);
            buttonRefresh.TabIndex = 6;
            buttonRefresh.Text = "Refresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += ButtonRefresh_Click;
            // 
            // OneNoteExporterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(417, 347);
            Controls.Add(buttonRefresh);
            Controls.Add(labelExportFolder);
            Controls.Add(buttonExport);
            Controls.Add(buttonBrowseFiles);
            Controls.Add(textBoxExportPath);
            Controls.Add(checkBoxSelectAll);
            Controls.Add(treeViewNotebooks);
            Name = "OneNoteExporterForm";
            Text = "OneNote Exporter";
            Load += OneNoteExporterForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FixedTreeView treeViewNotebooks;
        private CheckBox checkBoxSelectAll;
        private TextBox textBoxExportPath;
        private Button buttonBrowseFiles;
        private FolderBrowserDialog folderBrowserDialog;
        private Button buttonExport;
        private Label labelExportFolder;
        private Button buttonRefresh;
    }
}
