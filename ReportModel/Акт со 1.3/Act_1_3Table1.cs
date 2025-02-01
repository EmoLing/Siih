using DB.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportModel.Акт_со_1._3;

public class Act_1_3Table1(ComplexHardware complexHardware)
{
    public string NameComplexhardware => complexHardware.Name;

    public List<Act_1_3Table1Row> Rows { get; } = [];

    public void Initialize() => complexHardware.Hardwares.ForEach(hardware =>
    {
        var software = hardware.Softwares.FirstOrDefault();

        var row = new Act_1_3Table1Row()
        {
            Name = hardware.Name,
            SerialNumber = hardware.SerialNumber,
            Article = hardware.Article,
            DateCreate = hardware.DateCreate.ToShortDateString(),
            InstallPlace = complexHardware.User.Cabinet.Name,
            User = complexHardware.User.ToString(),
        };

        if (software is not null)
        {
            row.SoftwareName = hardware.Softwares.FirstOrDefault()?.Name;
            row.SoftwareVersion = hardware.Softwares.FirstOrDefault()?.Version;
        }

        Rows.Add(row);
    });
}
