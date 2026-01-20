namespace OneNoteExporter.Forms
{
    public partial class ExportConfirmationForm : Form
    {
        public string ExportPath { get; private set; }
        public TreeNode[] SelectedNodes { get; private set; } = [];

        public ExportConfirmationForm(System.Windows.Forms.TreeView treeView, string exportPath)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            ExportPath = exportPath;
            textBoxConfirmExportPath.Text = ExportPath;

            foreach (TreeNode notebook in treeView.Nodes)
            {
                TreeNode? copied = CloneCheckedNodes(notebook);
                if (copied != null)
                    treeViewConfirmNotebooks.Nodes.Add(copied);
            }
        }

        private static TreeNode? CloneCheckedNodes(TreeNode node)
        {
            if (!node.Checked && node.Nodes.Cast<TreeNode>().All(c => !c.Checked))
                return null;

            TreeNode clone = new(node.Text) { Tag = node.Tag, Checked = node.Checked };
            foreach (TreeNode child in node.Nodes)
            {
                TreeNode? childClone = CloneCheckedNodes(child);
                if (childClone != null)
                    clone.Nodes.Add(childClone);
            }

            return clone;
        }

        private void ButtonConfirmExport_Click(object sender, EventArgs e)
        {
            SelectedNodes = [.. treeViewConfirmNotebooks.Nodes.Cast<TreeNode>()];
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancelExport_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
