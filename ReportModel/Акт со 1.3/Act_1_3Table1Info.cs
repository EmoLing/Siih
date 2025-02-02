﻿using Common;
using DB.Models.Equipment;

namespace ReportModel.Акт_со_1._3;

public class Act_1_3Table1Info(ComplexHardware complexHardware) : TableInfo
{
    private int _startNumber = 0;

    protected override int StartRow => 4;

    public string NameComplexhardware => complexHardware.Name;

    public override string TableName => TableNames.Act_1_3Table1;

    public void Initialize() => complexHardware.Hardwares.ForEach(hardware =>
    {
        var software = hardware.Softwares.FirstOrDefault();

        var rowInfo = new Act_1_3Table1RowInfo()
        {
            Number = ++_startNumber,
            Name = hardware.Name,
            SerialNumber = hardware.SerialNumber,
            Article = hardware.Article,
            DateCreate = hardware.DateCreate.ToShortDateString(),
            InstallPlace = complexHardware.User.Cabinet.Name,
            User = complexHardware.User.ToString(),
        };

        if (software is not null)
        {
            rowInfo.SoftwareName = hardware.Softwares.FirstOrDefault()?.Name;
            rowInfo.SoftwareVersion = hardware.Softwares.FirstOrDefault()?.Version;
        }

        RowsInfo.Add(rowInfo);
    });
}
