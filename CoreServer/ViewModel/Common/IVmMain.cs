namespace CoreServer.ViewModel.Common
{
    public interface IVmMain
    {
        AppStates AppState { get; set; }

        public enum AppStates
        {
            SignIn,
            Working,
        }
    }

    public abstract class VmMain : IVmMain
    {
        private IVmMain.AppStates appState;
        public IVmMain.AppStates AppState
        {
            get => appState;
            set
            {
                if (appState != value)
                {
                    appState = value;
                    AppStateSetter(appState);
                }
            }
        }

        protected abstract void AppStateSetter(IVmMain.AppStates appState);
    }
}
