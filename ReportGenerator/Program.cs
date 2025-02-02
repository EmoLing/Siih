using DB.Models.Departments;
using DB.Models.Equipment;
using DB.Models.Users;
using NPOI.XWPF.UserModel;
using ReportGenerator;
using ReportGenerator.Strategy;
using ReportModel;

string filePath = @"D:\DiplomDaniel\act_1.3.docx";

using var document = new XWPFDocument(File.OpenRead(filePath));

var actInfo = GenerateAct1_3Data();
actInfo.Initialize();

var report = ReportFactory.CreateReport(actInfo);
FillTablesFromReport(report, document);

Console.WriteLine("Введите наименование файла");
var newName = Console.ReadLine();

document.SaveAs($"{Path.GetTempPath()}\\{newName}.docx");


static Act_1_3Info GenerateAct1_3Data()
{

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

    var errorPts = new List<(Hardware, string, string)>
    {
        (hardware1, "test1", "test2")
    };

    return new Act_1_3Info(complex, errorPts);
}

static void FillTablesFromReport(IReport report, XWPFDocument document)
{
    var tablesInfo = report.GetTablesInfo();

    foreach (var tableInfo in tablesInfo)
    {
        var table = document.FindTableByName(tableInfo.TableName);

        if (table is null)
            continue;

        if (table.NumberOfRows < 2)
            continue;

        int startRowIndex = tableInfo.StartIndexRow;

        foreach (var rowInfo in tableInfo.RowsInfo)
        {
            var row = table.GetRow(startRowIndex);

            if (row is null)
            {
                row = table.CreateRow();
                row.AddNewTableCell(); // При создании строки создает на одну ячейку меньше, нада добавлять
            }

            report.FillTableRow(row, rowInfo);

            startRowIndex++;
        }
    }
}
