namespace Core.ViewModel.Common
{
    public interface IVmMessage
    {
        Task StartWaiting(string message, string title = "等待");
        Task StopWaiting();

        Task StartProgressing(string message, float progressMax, string title = "等待");
        Task StopProgressing();

        Task ShowInfo(string message, string title = "消息");
        Task ShowSuccess(string message, string title = "完成");
        Task ShowWarning(string message, string title = "警告");
        Task ShowFailure(string message, string title = "失败");
        Task ShowError(Exception ex, string title = "错误");

        Task<bool> ConfirmOkCancel(string message, string title = "确认");
        Task<bool?> ConfirmYesNoCancel(string message, string title = "确认");
    }
}
