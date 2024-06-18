using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Misc;
using System.Runtime.CompilerServices;

namespace Core.ViewModel.Common
{
    public class VmMessage : ObservableObject
    {
        private IExceptionAnalyzer ExceptionAnalyzer { get; set; }

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

        private string content = string.Empty;
        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
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

                switch (messageType)
                {
                    case MessageTypes.Waiting:
                        IsProgressVisible = true;
                        IsIndeterminate = true;
                        IsCmdCancelVisible = true;
                        break;
                    case MessageTypes.Progress:
                        IsProgressVisible = true;
                        IsIndeterminate = false;
                        IsCmdCancelVisible = true;
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

        private Awaiter<bool?> MessageAwaiter { get; set; } = null!;

        public VmMessage(IExceptionAnalyzer exceptionAnalyzer)
        {
            ExceptionAnalyzer = exceptionAnalyzer;
            CmdYes = new(() => MessageAwaiter.Continue(true));
            CmdNo = new(() => MessageAwaiter.Continue(false));
            CmdOk = new(() => MessageAwaiter.Continue(true));
            CmdCancel = new(() => MessageAwaiter.Continue(null));
        }

        public async Task ShowWaiting(string title, string content)
        {
            MessageType = MessageTypes.Waiting;
            await Show(title, content);
        }
        public async Task ShowProgress(string title, string content, float progressMax)
        {
            MessageType = MessageTypes.Progress;
            ProgressMax = progressMax;
            Progress = 0;
            await Show(title, content);
        }
        public async Task ShowInfo(string title, string content)
        {
            MessageType = MessageTypes.Info;
            await Show(title, content);
        }
        public async Task ShowSuccess(string title, string content)
        {
            MessageType = MessageTypes.Success;
            await Show(title, content);
        }
        public async Task ShowWarning(string title, string content)
        {
            MessageType = MessageTypes.Warning;
            await Show(title, content);
        }
        public async Task ShowFailure(string title, string content)
        {
            MessageType = MessageTypes.Failure;
            await Show(title, content);
        }
        public async Task ShowError(string title, Exception ex)
        {
            MessageType = MessageTypes.Error;
            await Show(title, ExceptionAnalyzer.Analyze(ex));
        }
        public async Task<bool> ShowOkCancel(string title, string content)
        {
            MessageType = MessageTypes.OkCancel;
            return await Show(title, content) == true;
        }
        public async Task<bool?> ShowYesNoCancel(string title, string content)
        {
            MessageType = MessageTypes.YesNoCancel;
            return await Show(title, content);
        }
        private async Task<bool?> Show(string title, string content)
        {
            Title = title;
            Content = content;
            IsVisible = true;
            MessageAwaiter = new();
            bool? result = await MessageAwaiter;
            IsVisible = false;
            return result;
        }

        public enum MessageTypes
        {
            /// <summary>
            /// waiting
            /// </summary>
            Waiting,
            /// <summary>
            /// progress
            /// </summary>
            Progress,
            /// <summary>
            /// ok
            /// </summary>
            Info,
            /// <summary>
            /// ok
            /// </summary>
            Success,
            /// <summary>
            /// ok
            /// </summary>
            Warning,
            /// <summary>
            /// ok
            /// </summary>
            Failure,
            /// <summary>
            /// ok
            /// </summary>
            Error,
            /// <summary>
            /// ok, cancel
            /// </summary>
            OkCancel,
            /// <summary>
            /// yes, no, cancel
            /// </summary>
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
