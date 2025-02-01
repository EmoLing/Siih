using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator;
public static class XWPFTableCellExtension
{
    // Метод для заполнения ячейки с заданным стилем
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
