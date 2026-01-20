namespace OneNoteExporter.Controls
{
    // fix from https://stackoverflow.com/questions/14647216/treeview-ignore-double-click-only-at-checkbox
    public class FixedTreeView : TreeView
    {
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x203) // detect double-click
            {
                var localPos = PointToClient(Cursor.Position);
                var hitTestInfo = HitTest(localPos);
                if (hitTestInfo.Location == TreeViewHitTestLocations.StateImage)
                    m.Result = IntPtr.Zero; // ignore double-click on checkbox
                else
                    base.WndProc(ref m);
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}
