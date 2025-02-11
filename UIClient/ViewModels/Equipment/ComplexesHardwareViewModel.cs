using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIClient.Services;

namespace UIClient.ViewModels.Equipment;

public class ComplexesHardwareViewModel : ViewModel
{
    public ComplexesHardwareViewModel(ApiService apiService)
        : base(apiService)
    {
    }

    protected override Task LoadDataAsync()
    {
        throw new NotImplementedException();
    }
}
