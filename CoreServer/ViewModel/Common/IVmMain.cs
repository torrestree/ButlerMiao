namespace CoreServer.ViewModel.Common
{
    public interface IVmMain
    {
        void SwitchAppState(AppStates appState);

        public enum AppStates
        {
            SignIn,
            Working,
        }
    }
}
