namespace Bloxstrap.UI.ViewModels.Settings
{
    public class BehaviourViewModel : NotifyPropertyChangedViewModel
    {
        public bool ConfirmLaunches
        {
            get => App.Settings.Prop.ConfirmLaunches;
            set => App.Settings.Prop.ConfirmLaunches = value;
        }

        public bool BackgroundUpdates
        {
            get => App.Settings.Prop.BackgroundUpdatesEnabled;
            set => App.Settings.Prop.BackgroundUpdatesEnabled = value;
        }

        public bool IsRobloxInstallationMissing => !App.IsPlayerInstalled && !App.IsStudioInstalled;

        public bool ForceRobloxReinstallation
        {
            get => App.State.Prop.ForceReinstall || IsRobloxInstallationMissing;
            set => App.State.Prop.ForceReinstall = value;
        }

        public bool UseHighProcessPriority
        {
            get => App.Settings.Prop.UseHighProcessPriority;
            set => App.Settings.Prop.UseHighProcessPriority = value;
        }

        public bool PreferDedicatedGpu
        {
            get => App.Settings.Prop.PreferDedicatedGpu;
            set => App.Settings.Prop.PreferDedicatedGpu = value;
        }
    }
}
