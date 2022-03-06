using HRIS.Models;
using HRIS.ViewModel;

namespace HRIS.IProviders
{
    public interface IHrProvider
    {
        ReturnData<string> SaveWorkFlowDocument(WfDocVm wfDoc);
        ReturnData<dynamic> GetEntitledLeave(EntiledLeaveVm entiledLeave);
        ReturnData<dynamic> CalculateLeaveDays(LeaveApplication application);
        ReturnData<dynamic> SaveWorkFlowRoute(WorkFlowRoute route, bool isEdit);
        ReturnData<dynamic> SaveWorkFlowApprover(WorkFlowApprover approver, bool isEdit);
        ReturnData<dynamic> SaveDocuments(WorkFlowDocument document, bool isEdit);
    }
}
