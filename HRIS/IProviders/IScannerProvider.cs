using HRIS.ViewModel;
using System.Collections.Generic;

namespace HRIS.IProviders
{
    public interface IScannerProvider
    {
        ReturnData<List<string>> GetReaders();
        ReturnData<string> Enroll();
    }
}
