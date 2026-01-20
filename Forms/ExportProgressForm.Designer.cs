namespace OneNoteExporter.Forms
{
    partial class ExportProgressForm
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
            progressBarPages = new ProgressBar();
            labelNotebookCount = new Label();
            labelSectionCount = new Label();
            labelPageCount = new Label();
            buttonCancelDuringExport = new Button();
            labelCurrentNotebook = new Label();
            labelCurrentSection = new Label();
            labelCurrentPage = new Label();
            labelNotebooksText = new Label();
            labelSectionsText = new Label();
            labelPagesText = new Label();
            labelCurrentNotebookText = new Label();
            labelCurrentSectionText = new Label();
            labelCurrentPageText = new Label();
            SuspendLayout();
            // 
            // progressBarPages
            // 
            progressBarPages.Location = new Point(12, 79);
            progressBarPages.Name = "progressBarPages";
            progressBarPages.Size = new Size(561, 30);
            progressBarPages.TabIndex = 0;
            // 
            // labelNotebookCount
            // 
            labelNotebookCount.AutoSize = true;
            labelNotebookCount.Location = new Point(89, 17);
            labelNotebookCount.Name = "labelNotebookCount";
            labelNotebookCount.Size = new Size(28, 15);
            labelNotebookCount.TabIndex = 1;
            labelNotebookCount.Text = "- / -";
            // 
            // labelSectionCount
            // 
            labelSectionCount.AutoSize = true;
            labelSectionCount.Location = new Point(89, 32);
            labelSectionCount.Name = "labelSectionCount";
            labelSectionCount.Size = new Size(28, 15);
            labelSectionCount.TabIndex = 2;
            labelSectionCount.Text = "- / -";
            // 
            // labelPageCount
            // 
            labelPageCount.AutoSize = true;
            labelPageCount.Location = new Point(89, 47);
            labelPageCount.Name = "labelPageCount";
            labelPageCount.Size = new Size(28, 15);
            labelPageCount.TabIndex = 3;
            labelPageCount.Text = "- / -";
            // 
            // buttonCancelDuringExport
            // 
            buttonCancelDuringExport.Location = new Point(439, 115);
            buttonCancelDuringExport.Name = "buttonCancelDuringExport";
            buttonCancelDuringExport.Size = new Size(134, 34);
            buttonCancelDuringExport.TabIndex = 4;
            buttonCancelDuringExport.Text = "Cancel";
            buttonCancelDuringExport.UseVisualStyleBackColor = true;
            buttonCancelDuringExport.Click += ButtonCancelDuringExport_Click;
            // 
            // labelCurrentNotebook
            // 
            labelCurrentNotebook.AutoSize = true;
            labelCurrentNotebook.Location = new Point(301, 17);
            labelCurrentNotebook.Name = "labelCurrentNotebook";
            labelCurrentNotebook.Size = new Size(12, 15);
            labelCurrentNotebook.TabIndex = 5;
            labelCurrentNotebook.Text = "-";
            // 
            // labelCurrentSection
            // 
            labelCurrentSection.AutoSize = true;
            labelCurrentSection.Location = new Point(301, 31);
            labelCurrentSection.Name = "labelCurrentSection";
            labelCurrentSection.Size = new Size(12, 15);
            labelCurrentSection.TabIndex = 6;
            labelCurrentSection.Text = "-";
            // 
            // labelCurrentPage
            // 
            labelCurrentPage.AutoSize = true;
            labelCurrentPage.Location = new Point(301, 47);
            labelCurrentPage.Name = "labelCurrentPage";
            labelCurrentPage.Size = new Size(12, 15);
            labelCurrentPage.TabIndex = 7;
            labelCurrentPage.Text = "-";
            // 
            // labelNotebooksText
            // 
            labelNotebooksText.AutoSize = true;
            labelNotebooksText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelNotebooksText.Location = new Point(12, 16);
            labelNotebooksText.Name = "labelNotebooksText";
            labelNotebooksText.Size = new Size(71, 15);
            labelNotebooksText.TabIndex = 8;
            labelNotebooksText.Text = "Notebooks:";
            // 
            // labelSectionsText
            // 
            labelSectionsText.AutoSize = true;
            labelSectionsText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelSectionsText.Location = new Point(26, 32);
            labelSectionsText.Name = "labelSectionsText";
            labelSectionsText.Size = new Size(57, 15);
            labelSectionsText.TabIndex = 9;
            labelSectionsText.Text = "Sections:";
            // 
            // labelPagesText
            // 
            labelPagesText.AutoSize = true;
            labelPagesText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelPagesText.Location = new Point(41, 47);
            labelPagesText.Name = "labelPagesText";
            labelPagesText.Size = new Size(42, 15);
            labelPagesText.TabIndex = 10;
            labelPagesText.Text = "Pages:";
            // 
            // labelCurrentNotebookText
            // 
            labelCurrentNotebookText.AutoSize = true;
            labelCurrentNotebookText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelCurrentNotebookText.Location = new Point(183, 17);
            labelCurrentNotebookText.Name = "labelCurrentNotebookText";
            labelCurrentNotebookText.Size = new Size(112, 15);
            labelCurrentNotebookText.TabIndex = 11;
            labelCurrentNotebookText.Text = "Current Notebook:";
            // 
            // labelCurrentSectionText
            // 
            labelCurrentSectionText.AutoSize = true;
            labelCurrentSectionText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelCurrentSectionText.Location = new Point(197, 32);
            labelCurrentSectionText.Name = "labelCurrentSectionText";
            labelCurrentSectionText.Size = new Size(98, 15);
            labelCurrentSectionText.TabIndex = 12;
            labelCurrentSectionText.Text = "Current Section:";
            // 
            // labelCurrentPageText
            // 
            labelCurrentPageText.AutoSize = true;
            labelCurrentPageText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelCurrentPageText.Location = new Point(212, 47);
            labelCurrentPageText.Name = "labelCurrentPageText";
            labelCurrentPageText.Size = new Size(83, 15);
            labelCurrentPageText.TabIndex = 13;
            labelCurrentPageText.Text = "Current Page:";
            // 
            // ExportProgressForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(585, 161);
            Controls.Add(labelCurrentPageText);
            Controls.Add(labelCurrentSectionText);
            Controls.Add(labelCurrentNotebookText);
            Controls.Add(labelPagesText);
            Controls.Add(labelSectionsText);
            Controls.Add(labelNotebooksText);
            Controls.Add(labelCurrentPage);
            Controls.Add(labelCurrentSection);
            Controls.Add(labelCurrentNotebook);
            Controls.Add(buttonCancelDuringExport);
            Controls.Add(labelPageCount);
            Controls.Add(labelSectionCount);
            Controls.Add(labelNotebookCount);
            Controls.Add(progressBarPages);
            Name = "ExportProgressForm";
            Text = "Exporting ...";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBarPages;
        private Label labelNotebookCount;
        private Label labelSectionCount;
        private Label labelPageCount;
        private Button buttonCancelDuringExport;
        private Label labelCurrentNotebook;
        private Label labelCurrentSection;
        private Label labelCurrentPage;
        private Label labelNotebooksText;
        private Label labelSectionsText;
        private Label labelPagesText;
        private Label labelCurrentNotebookText;
        private Label labelCurrentSectionText;
        private Label labelCurrentPageText;
    }
}