namespace OneNoteExporter.Forms
{
    partial class ExportConfirmationForm
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
            textBoxConfirmExportPath = new TextBox();
            treeViewConfirmNotebooks = new TreeView();
            buttonConfirmExport = new Button();
            buttonCancelExport = new Button();
            SuspendLayout();
            // 
            // textBoxConfirmExportPath
            // 
            textBoxConfirmExportPath.Location = new Point(12, 141);
            textBoxConfirmExportPath.Name = "textBoxConfirmExportPath";
            textBoxConfirmExportPath.ReadOnly = true;
            textBoxConfirmExportPath.Size = new Size(280, 23);
            textBoxConfirmExportPath.TabIndex = 0;
            // 
            // treeViewConfirmNotebooks
            // 
            treeViewConfirmNotebooks.Location = new Point(12, 12);
            treeViewConfirmNotebooks.Name = "treeViewConfirmNotebooks";
            treeViewConfirmNotebooks.Size = new Size(280, 108);
            treeViewConfirmNotebooks.TabIndex = 1;
            // 
            // buttonConfirmExport
            // 
            buttonConfirmExport.Location = new Point(120, 196);
            buttonConfirmExport.Name = "buttonConfirmExport";
            buttonConfirmExport.Size = new Size(82, 27);
            buttonConfirmExport.TabIndex = 2;
            buttonConfirmExport.Text = "Confirm";
            buttonConfirmExport.UseVisualStyleBackColor = true;
            buttonConfirmExport.Click += ButtonConfirmExport_Click;
            // 
            // buttonCancelExport
            // 
            buttonCancelExport.Location = new Point(208, 196);
            buttonCancelExport.Name = "buttonCancelExport";
            buttonCancelExport.Size = new Size(84, 27);
            buttonCancelExport.TabIndex = 3;
            buttonCancelExport.Text = "Cancel";
            buttonCancelExport.UseVisualStyleBackColor = true;
            buttonCancelExport.Click += ButtonCancelExport_Click;
            // 
            // ExportConfirmationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(304, 235);
            Controls.Add(buttonCancelExport);
            Controls.Add(buttonConfirmExport);
            Controls.Add(treeViewConfirmNotebooks);
            Controls.Add(textBoxConfirmExportPath);
            Name = "ExportConfirmationForm";
            Text = "Confirm Export";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxConfirmExportPath;
        private TreeView treeViewConfirmNotebooks;
        private Button buttonConfirmExport;
        private Button buttonCancelExport;
    }
}