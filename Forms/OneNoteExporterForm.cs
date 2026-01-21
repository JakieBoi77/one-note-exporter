using Microsoft.Office.Interop.OneNote;
using System.Diagnostics;
using System.Xml;
using OneNoteApp = Microsoft.Office.Interop.OneNote.Application;

namespace OneNoteExporter.Forms
{
    public partial class OneNoteExporterForm : Form
    {
        private bool _isRefreshing = false;
        private DateTime _lastRefreshTime = DateTime.MinValue;
        private static readonly TimeSpan RefreshCooldown = TimeSpan.FromMilliseconds(750);

        public OneNoteExporterForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private static void OpenOneNote()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "onenote",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to open OneNote:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static OneNoteApp CreateFreshOneNoteApp()
        {
            Thread.Sleep(500);
            return new OneNoteApp();
        }

        private static bool IsOneNoteRunning()
        {
            return Process.GetProcessesByName("ONENOTE").Length != 0
                || Process.GetProcessesByName("ONENOTEIM").Length != 0;
        }

        private static bool HasOpenNotebooks(OneNoteApp oneNoteApp)
        {
            oneNoteApp.GetHierarchy("", HierarchyScope.hsNotebooks, out string xml);

            if (string.IsNullOrWhiteSpace(xml))
                return false;

            XmlDocument doc = new();
            doc.LoadXml(xml);

            XmlNamespaceManager ns = new(doc.NameTable);
            ns.AddNamespace("one", doc.DocumentElement!.NamespaceURI);

            XmlNodeList notebooks = doc.SelectNodes("//one:Notebook", ns)!;
            return notebooks.Count > 0;
        }

        private bool EnsureOneNoteReady()
        {
            if (!IsOneNoteRunning())
            {
                DialogResult result = MessageBox.Show(
                    "Microsoft OneNote is not running.\n\nWould you like to open OneNote?",
                    "OneNote Required",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    OpenOneNote();
                    LoadNotebooks();

                }
                return false;
            }

            OneNoteApp oneNoteApp = CreateFreshOneNoteApp();

            if (!HasOpenNotebooks(oneNoteApp))
            {
                MessageBox.Show(
                    "OneNote is open, but no notebooks are loaded.\n\n" +
                    "Please open at least one notebook to continue.",
                    "OneNote Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);
                return false;
            }

            return true;
        }

        private void LoadNotebooks()
        {
            treeViewNotebooks.Nodes.Clear();
            treeViewNotebooks.BeginUpdate();

            try
            {
                OneNoteApp oneNoteApp = CreateFreshOneNoteApp();
                treeViewNotebooks.Nodes.Clear();

                foreach (XmlNode notebook in GetNotebookNodes(oneNoteApp))
                {
                    string notebookName = notebook.Attributes?["name"]?.Value ?? "Unnamed Notebook";
                    string? notebookId = notebook.Attributes?["ID"]?.Value;
                    if (notebookId == null) continue;

                    TreeNode notebookNode = new(notebookName) { Tag = notebookId };

                    foreach (XmlNode section in GetSectionNodes(oneNoteApp, notebookId))
                    {
                        string sectionName = section.Attributes?["name"]?.Value ?? "Unnamed Section";
                        string? sectionId = section.Attributes?["ID"]?.Value;
                        if (sectionId == null) continue;

                        TreeNode sectionNode = new(sectionName) { Tag = sectionId };

                        foreach (XmlNode page in GetPageNodes(oneNoteApp, sectionId))
                        {
                            string pageName = page.Attributes?["name"]?.Value ?? "Unnamed Page";
                            string? pageId = page.Attributes?["ID"]?.Value;
                            if (pageId == null) continue;

                            TreeNode pageNode = new(pageName) { Tag = pageId };
                            sectionNode.Nodes.Add(pageNode);
                        }

                        notebookNode.Nodes.Add(sectionNode);
                    }

                    treeViewNotebooks.Nodes.Add(notebookNode);
                }

                Debug.WriteLine("Notebooks loaded successfully!");
            }
            finally
            {
                treeViewNotebooks.EndUpdate();
            }
        }

        private static XmlNodeList GetNotebookNodes(OneNoteApp oneNoteApp)
        {
            oneNoteApp.GetHierarchy("", HierarchyScope.hsNotebooks, out string xml);

            XmlDocument doc = new();
            doc.LoadXml(xml);
            XmlNamespaceManager ns = new(doc.NameTable);
            ns.AddNamespace("one", doc.DocumentElement!.NamespaceURI);

            XmlNodeList? nodes = doc.SelectNodes("//one:Notebook", ns);
            return nodes ?? doc.CreateNode(XmlNodeType.Element, "Notebook", "").ChildNodes;
        }

        private static XmlNodeList GetSectionNodes(OneNoteApp oneNoteApp, string notebookId)
        {
            oneNoteApp.GetHierarchy(notebookId, HierarchyScope.hsSections, out string xml);

            XmlDocument doc = new();
            doc.LoadXml(xml);
            XmlNamespaceManager ns = new(doc.NameTable);
            ns.AddNamespace("one", doc.DocumentElement!.NamespaceURI);

            XmlNodeList? nodes = doc.SelectNodes("//one:Section", ns);
            return nodes ?? doc.CreateNode(XmlNodeType.Element, "Section", "").ChildNodes;
        }

        private static XmlNodeList GetPageNodes(OneNoteApp oneNoteApp, string sectionId)
        {
            oneNoteApp.GetHierarchy(sectionId, HierarchyScope.hsPages, out string xml);

            XmlDocument doc = new();
            doc.LoadXml(xml);
            XmlNamespaceManager ns = new(doc.NameTable);
            ns.AddNamespace("one", doc.DocumentElement!.NamespaceURI);

            XmlNodeList? nodes = doc.SelectNodes("//one:Page", ns);
            return nodes ?? doc.CreateNode(XmlNodeType.Element, "Page", "").ChildNodes;
        }

        private async void ButtonRefresh_Click(object sender, EventArgs e)
        {
            if (_isRefreshing)
                return;

            if (DateTime.UtcNow - _lastRefreshTime < RefreshCooldown)
                return;

            _isRefreshing = true;
            _lastRefreshTime = DateTime.UtcNow;

            buttonRefresh.Enabled = false;
            treeViewNotebooks.Nodes.Clear();

            try
            {
                await Task.Delay(300);

                if (EnsureOneNoteReady())
                {
                    LoadNotebooks();
                }
            }
            finally
            {
                buttonRefresh.Enabled = true;
                _isRefreshing = false;
            }
        }

        private void OneNoteExporterForm_Load(object sender, EventArgs e)
        {
            textBoxExportPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (EnsureOneNoteReady())
            {
                LoadNotebooks();
            }
        }

        private bool _suppressCheckEvent = false;

        private void TreeViewNotebooks_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_suppressCheckEvent)
                return;

            try
            {
                _suppressCheckEvent = true;
                treeViewNotebooks.Enabled = false;
                treeViewNotebooks.BeginUpdate();

                if (e.Node == null)
                {
                    return;
                }

                SetChildNodesCheckedState(e.Node, e.Node.Checked);
                UpdateParentNodes(e.Node);
            }
            finally
            {
                treeViewNotebooks.EndUpdate();
                treeViewNotebooks.Enabled = true;
                _suppressCheckEvent = false;
            }
        }

        private static void SetChildNodesCheckedState(TreeNode node, bool isChecked)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = isChecked;
                SetChildNodesCheckedState(child, isChecked);
            }
        }

        private static void UpdateParentNodes(TreeNode node)
        {
            TreeNode parent = node.Parent;
            while (parent != null)
            {
                bool allChecked = true;

                foreach (TreeNode child in parent.Nodes)
                {
                    if (!child.Checked)
                    {
                        allChecked = false;
                    }
                }

                parent.Checked = allChecked;
                parent = parent.Parent;
            }
        }

        private void CheckBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressCheckEvent)
                return;

            try
            {
                _suppressCheckEvent = true;
                treeViewNotebooks.BeginUpdate();

                foreach (TreeNode notebook in treeViewNotebooks.Nodes)
                {
                    notebook.Checked = checkBoxSelectAll.Checked;
                    SetChildNodesCheckedState(notebook, checkBoxSelectAll.Checked);
                }
            }
            finally
            {
                treeViewNotebooks.EndUpdate();
                _suppressCheckEvent = false;
            }
        }

        private void ButtonBrowseFiles_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxExportPath.Text = folderBrowserDialog.SelectedPath;
            }

        }

        private async void ButtonExport_Click(object sender, EventArgs e)
        {
            buttonExport.Enabled = false;

            if (_isRefreshing)
            {
                MessageBox.Show(
                    "Please wait for the refresh to complete before exporting.",
                    "OneNote Exporter",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                buttonExport.Enabled = true;
                return;
            }

            if (!EnsureOneNoteReady())
            {
                buttonExport.Enabled = true;
                return;
            }

            if (!HasAnyCheckedPages())
            {
                MessageBox.Show(
                    "Please select at least one page to export.",
                    "Nothing Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                buttonExport.Enabled = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxExportPath.Text))
            {
                MessageBox.Show(
                    "Please select an export path first.",
                    "Export",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            List<TreeNode> selectedNodes = GetCheckedNotebookTree(treeViewNotebooks);
            string exportPath = textBoxExportPath.Text;

            OneNoteApp validationApp = CreateFreshOneNoteApp();

            if (HasInvalidSelectedPages(selectedNodes, validationApp, out List<string> invalidPages))
            {
                MessageBox.Show(
                    "Some selected pages belong to notebooks that are no longer open in OneNote.\n\n" +
                    "Please refresh the notebook list before exporting.\n\n" +
                    "Affected pages:\n• " + string.Join("\n• ", invalidPages.Take(10)) +
                    (invalidPages.Count > 10 ? "\n• ..." : ""),
                    "Notebooks Closed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                buttonExport.Enabled = true;
                return;
            }

            CountTotals(
                [.. selectedNodes],
                out int totalNotebooks,
                out int totalSections,
                out int totalPages);

            if (totalPages == 0)
            {
                MessageBox.Show(
                    "No pages selected for export.",
                    "Export",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                buttonExport.Enabled = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxExportPath.Text))
            {
                MessageBox.Show(
                    "Please select an export path first.",
                    "Export",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                buttonExport.Enabled = true;
                return;
            }

            using ExportConfirmationForm confirmForm = new(treeViewNotebooks, textBoxExportPath.Text);

            if (confirmForm.ShowDialog() != DialogResult.OK)
            {
                buttonExport.Enabled = true;
                return;
            }

            string exportsRootPath = Path.Combine(exportPath, "OneNote Exports");
            Directory.CreateDirectory(exportsRootPath);

            HashSet<string> createdNotebookFolders = [];

            using ExportProgressForm progressForm =
                new(totalNotebooks, totalSections, totalPages);

            progressForm.Show();

            try
            {
                await Task.Run(() =>
                {
                    OneNoteApp oneNoteApp = CreateFreshOneNoteApp();

                    int notebooksDone = 0;
                    int sectionsDone = 0;
                    int pagesDone = 0;


                    foreach (TreeNode notebookNode in selectedNodes)
                    {
                        progressForm.Token.ThrowIfCancellationRequested();

                        notebooksDone++;

                        string notebookName = SanitizeFileName(notebookNode.Text);
                        string notebookPath = Path.Combine(exportsRootPath, notebookName);
                        Directory.CreateDirectory(notebookPath);
                        createdNotebookFolders.Add(notebookPath);


                        foreach (TreeNode sectionNode in notebookNode.Nodes)
                        {
                            progressForm.Token.ThrowIfCancellationRequested();

                            sectionsDone++;

                            string sectionName = SanitizeFileName(sectionNode.Text);
                            string sectionPath = Path.Combine(notebookPath, sectionName);
                            Directory.CreateDirectory(sectionPath);

                            foreach (TreeNode pageNode in sectionNode.Nodes)
                            {
                                progressForm.Token.ThrowIfCancellationRequested();

                                pagesDone++;

                                string pageName = SanitizeFileName(pageNode.Text);
                                string pagePath = Path.Combine(sectionPath, pageName + ".pdf");
                                string? pageId = pageNode.Tag as string;

                                progressForm.UpdateProgress(
                                    notebooksDone,
                                    sectionsDone,
                                    pagesDone,
                                    notebookNode.Text,
                                    sectionNode.Text,
                                    pageNode.Text);

                                if (pageId != null)
                                {
                                    try
                                    {
                                        string xpsPath = Path.Combine(sectionPath, pageName + ".pdf");

                                        oneNoteApp.Publish(pageId, xpsPath, PublishFormat.pfPDF);
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.WriteLine(
                                            $"Failed to export page '{pageName}': {ex.Message}");
                                    }
                                }
                            }
                        }
                    }
                });

                MessageBox.Show(
                    $"Export completed successfully!\n\n" +
                    $"Notebooks: {totalNotebooks}\n" +
                    $"Sections: {totalSections}\n" +
                    $"Pages: {totalPages}\n\n" +
                    $"Export location:\n{exportsRootPath}",
                    "Export Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                foreach (string folder in createdNotebookFolders)
                {
                    try
                    {
                        if (Directory.Exists(folder))
                            Directory.Delete(folder, true);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Failed to delete notebook folder {folder}: {ex.Message}");
                    }
                }
                MessageBox.Show(
                    "Export canceled by user.",
                    "Export Canceled",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred during export:\n\n{ex.Message}",
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                progressForm.Close();
                buttonExport.Enabled = true;
            }
        }

        private static string SanitizeFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            return name;
        }

        private static void CountTotals(
            TreeNode[] notebooks,
            out int totalNotebooks,
            out int totalSections,
            out int totalPages)
        {
            totalNotebooks = 0;
            totalSections = 0;
            totalPages = 0;

            foreach (TreeNode notebook in notebooks)
            {
                totalNotebooks++;

                foreach (TreeNode section in notebook.Nodes)
                {
                    totalSections++;
                    totalPages += section.Nodes.Count;
                }
            }
        }

        private bool HasAnyCheckedPages()
        {
            foreach (TreeNode notebook in treeViewNotebooks.Nodes)
            {
                foreach (TreeNode section in notebook.Nodes)
                {
                    foreach (TreeNode page in section.Nodes)
                    {
                        if (page.Checked)
                            return true;
                    }
                }
            }
            return false;
        }

        private static List<TreeNode> GetCheckedNotebookTree(System.Windows.Forms.TreeView treeView)
        {
            List<TreeNode> result = [];

            foreach (TreeNode notebook in treeView.Nodes)
            {
                TreeNode notebookClone = new(notebook.Text) { Tag = notebook.Tag };

                foreach (TreeNode section in notebook.Nodes)
                {
                    TreeNode sectionClone = new(section.Text) { Tag = section.Tag };

                    foreach (TreeNode page in section.Nodes)
                    {
                        if (page.Checked)
                        {
                            sectionClone.Nodes.Add(
                                new TreeNode(page.Text) { Tag = page.Tag });
                        }
                    }

                    if (sectionClone.Nodes.Count > 0)
                        notebookClone.Nodes.Add(sectionClone);
                }

                if (notebookClone.Nodes.Count > 0)
                    result.Add(notebookClone);
            }

            return result;
        }
        private static HashSet<string> GetAllOpenPageIds(OneNoteApp oneNoteApp)
        {
            oneNoteApp.GetHierarchy("", HierarchyScope.hsPages, out string xml);

            HashSet<string> pageIds = [];

            if (string.IsNullOrWhiteSpace(xml))
                return pageIds;

            XmlDocument doc = new();
            doc.LoadXml(xml);

            XmlNamespaceManager ns = new(doc.NameTable);
            ns.AddNamespace("one", doc.DocumentElement!.NamespaceURI);

            XmlNodeList pages = doc.SelectNodes("//one:Page", ns)!;

            foreach (XmlNode page in pages)
            {
                string? id = page.Attributes?["ID"]?.Value;
                if (!string.IsNullOrEmpty(id))
                    pageIds.Add(id);
            }

            return pageIds;
        }

        private static bool HasInvalidSelectedPages(
            IEnumerable<TreeNode> selectedNotebooks,
            OneNoteApp oneNoteApp,
            out List<string> invalidPageNames)
        {
            invalidPageNames = [];

            HashSet<string> livePageIds = GetAllOpenPageIds(oneNoteApp);

            foreach (TreeNode notebook in selectedNotebooks)
            {
                foreach (TreeNode section in notebook.Nodes)
                {
                    foreach (TreeNode page in section.Nodes)
                    {
                        string? pageId = page.Tag as string;

                        if (pageId == null || !livePageIds.Contains(pageId))
                        {
                            invalidPageNames.Add(page.Text);
                        }
                    }
                }
            }

            return invalidPageNames.Count > 0;
        }

        private void ShowTreeViewMessage(string message)
        {
            treeViewNotebooks.BeginUpdate();
            treeViewNotebooks.Nodes.Clear();

            TreeNode messageNode = new(message)
            {
                ForeColor = SystemColors.GrayText,
                NodeFont = new Font(treeViewNotebooks.Font, FontStyle.Italic),
                Tag = null
            };

            treeViewNotebooks.Nodes.Add(messageNode);
            treeViewNotebooks.ExpandAll();

            treeViewNotebooks.EndUpdate();
        }
    }
}
