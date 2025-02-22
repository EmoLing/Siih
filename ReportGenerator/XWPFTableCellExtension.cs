using NPOI.XWPF.UserModel;

namespace ReportGenerator;
public static class XWPFTableCellExtension
{
    public static void FillCellWithStyle(this XWPFTableCell cell, string text, int fontSize, ParagraphAlignment alignment, bool isBold = false, string color = null)
    {
        cell.RemoveParagraph(0);
        var paragraph = cell.AddParagraph();
        paragraph.Alignment = alignment;

        var run = paragraph.CreateRun();
        run.SetText(text);
        run.FontSize = fontSize;
        run.IsBold = isBold;

        if (!string.IsNullOrEmpty(color))
            run.SetColor(color);
    }
}
