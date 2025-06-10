using System.Drawing.Printing;

namespace WinFormsApp.Managers;

public class PrintManager
{
    private readonly PrintDocument _printDocument;
    private readonly PrintPreviewDialog _printPreviewDialog;
    private DataGridView? _printSourceDgv;
    private string _printTitle = string.Empty;

    public PrintManager()
    {
        _printDocument = new PrintDocument();
        _printPreviewDialog = new PrintPreviewDialog();
        _printDocument.PrintPage += PrintDocument_PrintPage;
    }

    public void Print(DataGridView dgv, string title)
    {
        _printSourceDgv = dgv;
        _printTitle = title;
        _printPreviewDialog.Document = _printDocument;
        _printPreviewDialog.ShowDialog();
    }

    private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
    {
        if (_printSourceDgv == null) return;

        Font headerFont = new Font("Comic Sans MS", 12, FontStyle.Bold);
        Font cellFont = new Font("Comic Sans MS", 10);
        int x = e.MarginBounds.Left;
        int y = e.MarginBounds.Top;
        int rowHeight = cellFont.Height + 8;
        int imageSize = 40;

        e.Graphics.DrawString(_printTitle, headerFont, Brushes.Black, x, y);
        y += rowHeight + 10;

        int colX = x;
        foreach (DataGridViewColumn col in _printSourceDgv.Columns)
        {
            if (!col.Visible) continue;
            e.Graphics.DrawString(col.HeaderText, headerFont, Brushes.Black, colX, y);
            colX += (col is DataGridViewImageColumn) ? imageSize + 10 : 125;
        }
        y += rowHeight;

        foreach (DataGridViewRow row in _printSourceDgv.Rows)
        {
            if (row.IsNewRow) continue;
            colX = x;
            foreach (DataGridViewColumn col in _printSourceDgv.Columns)
            {
                if (!col.Visible) continue;

                if (col is DataGridViewImageColumn)
                {
                    var cellValue = row.Cells[col.Index].Value;
                    if (cellValue is Image img)
                    {
                        e.Graphics.DrawImage(img, colX, y, imageSize, imageSize);
                    }
                    colX += imageSize + 10;
                }
                else
                {
                    var value = row.Cells[col.Index].FormattedValue?.ToString() ?? string.Empty;
                    e.Graphics.DrawString(value, cellFont, Brushes.Black, colX, y + (imageSize - rowHeight) / 2);
                    colX += 125;
                }
            }
            y += Math.Max(rowHeight, imageSize);
        }
    }
}
