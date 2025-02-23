using Common;
using Shared.DTOs.Equipment;

namespace ReportModel.Акт_со_1._3;

public class Act_1_3Table1Info(List<ComplexHardwareObject> complexesHardware) : TableInfo
{
    private int _startNumber = 0;

    protected override int StartRow => 4;

    public override string TableName => TableNames.Act_1_3Table1;

    public void Initialize()
    {
        foreach (var complexHardware in complexesHardware)
        {
            complexHardware.Hardwares.ForEach(hardware =>
            {
                var software = hardware.Softwares.FirstOrDefault();

                var rowInfo = new Act_1_3Table1RowInfo()
                {
                    Number = ++_startNumber,
                    Name = hardware.Name,
                    SerialNumber = hardware.SerialNumber,
                    Article = hardware.Article,
                    DateCreate = hardware.DateCreate.ToShortDateString(),
                    InstallPlace = complexHardware.User?.Cabinet?.Name,
                    User = complexHardware.User?.ToString(),
                };

                if (hardware.Softwares.Count == 1)
                {
                    rowInfo.SoftwareName = hardware.Softwares.FirstOrDefault()?.Name;
                    rowInfo.SoftwareVersion = hardware.Softwares.FirstOrDefault()?.Version;
                    rowInfo.HasMerge = false;

                    RowsInfo.Add(rowInfo);
                }
                else if (hardware.Softwares.Count > 1)
                {
                    for (int i = 0; i < hardware.Softwares.Count; i++)
                    {
                        if (i == 0)
                        {
                            rowInfo.IsStartMerge = true;
                            rowInfo.SoftwareName = hardware.Softwares[i].Name;
                            rowInfo.SoftwareVersion = hardware.Softwares[i].Version ?? String.Empty;
                            rowInfo.HasMerge = true;

                            RowsInfo.Add(rowInfo);

                            continue;
                        }

                        var row = new Act_1_3Table1RowInfo()
                        {
                            SoftwareName = hardware.Softwares[i].Name,
                            SoftwareVersion = hardware.Softwares[i].Version ?? String.Empty,
                            HasMerge = true
                        };

                        if (i == (hardware.Softwares.Count - 1))
                            row.IsEndMerge = true;

                        RowsInfo.Add(row);
                    }
                }
                else
                {
                    rowInfo.HasMerge = false;
                    RowsInfo.Add(rowInfo);
                }
            });
        }
    }
}
