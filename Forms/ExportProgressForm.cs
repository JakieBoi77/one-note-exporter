namespace OneNoteExporter.Forms
{
    public partial class ExportProgressForm : Form
    {
        private readonly int _totalNotebooks;
        private readonly int _totalSections;
        private readonly int _totalPages;

        private readonly CancellationTokenSource _cts = new();
        public CancellationToken Token => _cts.Token;

        public ExportProgressForm(int totalNotebooks, int totalSections, int totalPages)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            _totalNotebooks = totalNotebooks;
            _totalSections = totalSections;
            _totalPages = totalPages;

            progressBarPages.Minimum = 0;
            progressBarPages.Maximum = totalPages;
            progressBarPages.Value = 0;

            labelNotebookCount.Text = $"0 / {_totalNotebooks}";
            labelSectionCount.Text = $"0 / {_totalSections}";
            labelPageCount.Text = $"0 / {_totalPages}";
        }

        public void UpdateProgress(
            int notebooksDone,
            int sectionsDone,
            int pagesDone,
            string currentNotebook,
            string currentSection,
            string currentPage)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                    UpdateProgress(
                        notebooksDone,
                        sectionsDone,
                        pagesDone,
                        currentNotebook,
                        currentSection,
                        currentPage)));
                return;
            }

            labelNotebookCount.Text = $"{notebooksDone} / {_totalNotebooks}";
            labelSectionCount.Text = $"{sectionsDone} / {_totalSections}";
            labelPageCount.Text = $"{pagesDone} / {_totalPages}";

            labelCurrentNotebook.Text = $"{currentNotebook}";
            labelCurrentSection.Text = $"{currentSection}";
            labelCurrentPage.Text = $"{currentPage}";

            progressBarPages.Value = Math.Min(pagesDone, _totalPages);
        }

        private void ButtonCancelDuringExport_Click(object sender, EventArgs e)
        {
            buttonCancelDuringExport.Enabled = false;
            buttonCancelDuringExport.Text = "Canceling...";
            _cts.Cancel();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }
    }
}
