using HRIS.Models;
using HRIS.ViewModel;

namespace HRIS.IProviders
{
    public interface IHrProvider
    {
        ReturnData<string> SaveWorkFlowDocument(WfDocVm wfDoc);
        ReturnData<dynamic> GetEntitledLeave(EntiledLeaveVm entiledLeave);
        ReturnData<dynamic> CalculateLeaveDays(LeaveApplication application);
    }
}
