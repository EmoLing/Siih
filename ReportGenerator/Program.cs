using ReportGenerator;
using NPOI.XWPF.UserModel;
using ReportGenerator.Strategy;
using ReportGenerator.Strategy.Act_so_1_3;
using ReportModel;
using DB.Models.Equipment;
using DB.Models.Users;
using DB.Models.Departments;

string filePath = @"D:\DiplomDaniel\act_1.3.docx";
string tableName = "Таблица 1. Перечень ПТС для ввода в действие";

using var document = new XWPFDocument(File.OpenRead(filePath));

var table = document.FindTableByName(tableName);

if (table is null)
    return;

var columnsNames = document.GetColumnNames(table);

var complex = new ComplexHardware
{
    Name = "TestName",
    Hardwares = []
};

var hardware1 = new Hardware()
{
    Name = "hardware1",
    SerialNumber = "0012988287\r\n(410134202313173)\r\n",
    Article = "000",
    DateCreate = DateOnly.FromDateTime(DateTime.Now),
};

var software = new Software()
{
    Name = "Операционная система общего назначения \"ОСнова\",  МойОфис Стандартный"
};

var user = new User()
{
    LastName = "Пичко ",
    FirstName = "Виктор",
    Surname = "Павлович"
};

var cabinet = new Cabinet()
{
    Name = "каб 3"
};

user.Cabinet = cabinet;

hardware1.Softwares.Add(software);
complex.Hardwares.Add(hardware1);
complex.User = user;

var actInfo = new Act_1_3(complex);
actInfo.Initialize();

var report = new Act_1_3_Report(actInfo);

FillTableFromReports(table, report);

Console.WriteLine("Введите наименование файла");
var newName = Console.ReadLine();

document.SaveAs($"{Path.GetTempPath()}\\{newName}.docx");

// Универсальный метод заполнения таблицы
static void FillTableFromReports(XWPFTable table, IReport report)
{
    if (table.NumberOfRows < 2)
        return;

    int rowIndex = 4;

    foreach (var dataRow in report.GetData())
    {
        var row = table.CreateRow();
        row.AddNewTableCell(); // При создании строки создает на одну ячейку меньше, нада добавлять

        report.FillTableRow(row, dataRow);

        rowIndex++;
    }
}
