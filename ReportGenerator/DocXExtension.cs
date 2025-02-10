using NPOI.XWPF.UserModel;

namespace ReportGenerator;

public static class XWPFDocumentExtension
{
    public static XWPFTable FindTableByName(this XWPFDocument document, string tableName)
    {
        foreach (var paragraph in document.Paragraphs)
        {
            if (!paragraph.Text.Contains(tableName))
                continue;

            int pos = document.BodyElements.IndexOf(paragraph);

            for (int i = pos + 1; i < document.BodyElements.Count; i++)
            {
                if (document.BodyElements[i] is XWPFTable table)
                    return table;
            }
        }

        return null;
    }

    public static string[] GetColumnNames(this XWPFDocument document, XWPFTable table)
    {
        if (table.NumberOfRows == 0)
            return [];

        var firstRow = table.GetRow(0);
        return firstRow.GetTableCells().Select(cell => cell.GetText()).ToArray();
    }

    public static void SaveAs(this XWPFDocument document, string filePath)
    {
        using var fs = new FileStream(filePath, FileMode.Create);
        document.Write(fs);
    }
}
