using NPOI.XWPF.UserModel;
using ReportModel;
using ReportModel.Акт_со_1._3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Strategy.Act_so_1_3;

public class Act_1_3_Report(Act_1_3 act) : IReport
{
    public static readonly string[] Table1ColumnsNames =
    {
        "№  п/п",
        "Наименование оборудования",
        "Серийный №\r\n(инвентарный №)\r\n",
        "Артикул",
        "Дата (год) выпуска",
        "Место\r\nустановки\r\n",
        // "Установленное ОПО",
        "Наименование",
        "Версия",
        "Ф.И.О. лица,         прошедшего инструктаж",
    };

    public static readonly string[] Table2ColumnsNames =
{
        "№ п/п из таблицы 1",
        "Наименование оборудования",
        "Серийный № (инвентарный №)\r\n",
        "Результаты входного контроля с описанием нарушений \r\n(состояние упаковки, результаты внешнего осмотра, комплектность)\r\n",
        "Результаты контроля функционирования с описанием нарушений",
    };

    public IEnumerable<object> GetData() => act.Table1.Rows;

    public void FillTableRow(XWPFTableRow row, object data)
    {
        if (data is not Act_1_3Table1Row dataRow)
            return;

        row.GetCell(CellTable1.Number).FillCellWithStyle("0", 12, ParagraphAlignment.CENTER);
        row.GetCell(CellTable1.Name).FillCellWithStyle(dataRow.Name, 12, ParagraphAlignment.CENTER);
        row.GetCell(CellTable1.SerialNumber).FillCellWithStyle(dataRow.SerialNumber, 12, ParagraphAlignment.CENTER);
        row.GetCell(CellTable1.Article).FillCellWithStyle(dataRow.Article, 12, ParagraphAlignment.CENTER);
        row.GetCell(CellTable1.CreateDate).FillCellWithStyle(dataRow.DateCreate, 12, ParagraphAlignment.CENTER);
        row.GetCell(CellTable1.InstallPlace).FillCellWithStyle(dataRow.InstallPlace, 12, ParagraphAlignment.CENTER);
        row.GetCell(CellTable1.SoftwareName).FillCellWithStyle(dataRow.SoftwareName, 12, ParagraphAlignment.CENTER);
        row.GetCell(CellTable1.SoftwareVersion).FillCellWithStyle(dataRow.SoftwareVersion, 12, ParagraphAlignment.CENTER);
        row.GetCell(CellTable1.User).FillCellWithStyle(dataRow.User, 12, ParagraphAlignment.CENTER);
    }
}
