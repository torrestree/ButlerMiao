using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Misc.EA;
using Core.ViewModel.Common;
using System.Runtime.CompilerServices;
using System.Windows;

namespace HelperWpf.ViewModel
{
    public class VmMessage : ObservableObject, IVmMessage
    {
        private IExceptionAnalyzer ExceptionAnalyzer { get; set; }

        private MessageTypes messageType;
        public MessageTypes MessageType
        {
            get => messageType;
            set
            {
                SetProperty(ref messageType, value);
                IsCmdYesVisible = false;
                IsCmdNoVisible = false;
                IsCmdOkVisible = false;
                IsCmdCancelVisible = false;
                IsProgressVisible = false;
                IsIndeterminate = false;
                switch (messageType)
                {
                    case MessageTypes.Waiting:
                        IsProgressVisible = true;
                        IsIndeterminate = true;
                        break;
                    case MessageTypes.Progressing:
                        IsProgressVisible = true;
                        break;
                    case MessageTypes.OkCancel:
                        IsCmdOkVisible = true;
                        IsCmdCancelVisible = true;
                        break;
                    case MessageTypes.YesNoCancel:
                        IsCmdYesVisible = true;
                        IsCmdNoVisible = true;
                        IsCmdCancelVisible = true;
                        break;
                    default:
                        IsCmdOkVisible = true;
                        break;
                }
            }
        }

        private bool isVisible;
        public bool IsVisible
        {
            get => isVisible;
            set => SetProperty(ref isVisible, value);
        }

        private string title = string.Empty;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string message = string.Empty;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        public RelayCommand CmdOk { get; set; }

        private bool isCmdOkVisible;
        public bool IsCmdOkVisible
        {
            get => isCmdOkVisible;
            set => SetProperty(ref isCmdOkVisible, value);
        }

        public RelayCommand CmdCancel { get; set; }

        private bool isCmdCancelVisible;
        public bool IsCmdCancelVisible
        {
            get => isCmdCancelVisible;
            set => SetProperty(ref isCmdCancelVisible, value);
        }

        public RelayCommand CmdYes { get; set; }

        private bool isCmdYesVisible;
        public bool IsCmdYesVisible
        {
            get => isCmdYesVisible;
            set => SetProperty(ref isCmdYesVisible, value);
        }

        public RelayCommand CmdNo { get; set; }

        private bool isCmdNoVisible;
        public bool IsCmdNoVisible
        {
            get => isCmdNoVisible;
            set => SetProperty(ref isCmdNoVisible, value);
        }

        private bool isProgressVisible;
        public bool IsProgressVisible
        {
            get => isProgressVisible;
            set => SetProperty(ref isProgressVisible, value);
        }

        private bool isIndeterminate;
        public bool IsIndeterminate
        {
            get => isIndeterminate;
            set => SetProperty(ref isIndeterminate, value);
        }

        private float progressMax;
        public float ProgressMax
        {
            get => progressMax;
            set => SetProperty(ref progressMax, value);
        }

        private float progress;
        public float Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }

        private Awaiter<bool?> MessageAwaiter { get; set; } = null!;

        public VmMessage(IExceptionAnalyzer exceptionAnalyzer)
        {
            ExceptionAnalyzer = exceptionAnalyzer;
            CmdOk = new(() => MessageAwaiter.Continue(true));
            CmdCancel = new(() => MessageAwaiter.Continue(null));
            CmdYes = new(() => MessageAwaiter.Continue(true));
            CmdNo = new(() => MessageAwaiter.Continue(false));
        }

        public Task StartWaiting(string message, string title = "等待")
        {
            return Task.Run(() =>
            {
                Show(MessageTypes.Waiting, title, message);
            });
        }
        public Task StopWaiting()
        {
            return Task.Run(() =>
            {
                IsVisible = false;
            });
        }

        public async Task StartProgressing(string message, float progressMax, string title = "等待")
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    Progress = 0;
                    ProgressMax = progressMax;
                    Show(MessageTypes.Progressing, title, message);
                });
            });
        }
        public async Task StopProgressing()
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    IsVisible = false;
                });
            });
        }

        public async Task ShowInfo(string message, string title = "消息")
        {
            await ShowAsync(MessageTypes.Info, title, message);
        }
        public async Task ShowSuccess(string message, string title = "完成")
        {
            await ShowAsync(MessageTypes.Success, title, message);
        }
        public async Task ShowWarning(string message, string title = "警告")
        {
            await ShowAsync(MessageTypes.Warning, title, message);
        }
        public async Task ShowFailure(string message, string title = "失败")
        {
            await ShowAsync(MessageTypes.Failure, title, message);
        }
        public async Task ShowError(Exception ex, string title = "错误")
        {
            await ShowAsync(MessageTypes.Error, title, ExceptionAnalyzer.Analyze(ex));
        }

        public async Task<bool> ConfirmOkCancel(string message, string title = "确认")
        {
            return await ShowAsync(MessageTypes.OkCancel, title, message) == true;
        }
        public async Task<bool?> ConfirmYesNoCancel(string message, string title = "确认")
        {
            return await ShowAsync(MessageTypes.YesNoCancel, title, message);
        }

        private void Show(MessageTypes messageType, string title, string message)
        {
            MessageType = messageType;
            Title = title;
            Message = message;
            IsVisible = true;
        }
        private async Task<bool?> ShowAsync(MessageTypes messageType, string title, string message)
        {
            await Task.Run(() => Application.Current.Dispatcher.BeginInvoke(() => Show(messageType, title, message)));
            MessageAwaiter = new();
            bool? result = await MessageAwaiter;
            await Task.Run(() => Application.Current.Dispatcher.BeginInvoke(() => IsVisible = false));
            return result;
        }

        public enum MessageTypes
        {
            Waiting,
            Progressing,
            Info,
            Success,
            Warning,
            Failure,
            Error,
            OkCancel,
            YesNoCancel,
        }

        public class Awaiter<T> : INotifyCompletion
        {
            public T? Result { get; private set; }
            public bool IsCompleted => false;
            private Action Continuation { get; set; } = null!;

            public Awaiter<T?> GetAwaiter() => this;
            public T? GetResult() => Result;
            public void OnCompleted(Action continuation) => Continuation += continuation;
            public void Continue(T? result)
            {
                Result = result;
                Continuation?.Invoke();
            }
        }
    }
}
