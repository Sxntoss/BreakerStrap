using System.Windows;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Input;

using Bloxstrap.Enums.FlagPresets;

namespace Bloxstrap.UI.ViewModels.Settings
{
    public class FastFlagsViewModel : NotifyPropertyChangedViewModel
    {
        private Dictionary<string, object>? _preResetFlags;

        public event EventHandler? RequestPageReloadEvent;
        
        public event EventHandler? OpenFlagEditorEvent;

        private void OpenFastFlagEditor()
        {
            // ativa o gerenciamento de Fast Flags automaticamente ao entrar no editor,
            // já que agora não existe mais o toggle manual separado
            App.Settings.Prop.UseFastFlagManager = true;
            OpenFlagEditorEvent?.Invoke(this, EventArgs.Empty);
        }

        public ICommand OpenFastFlagEditorCommand => new RelayCommand(OpenFastFlagEditor);

        public bool UseFastFlagManager
        {
            get => App.Settings.Prop.UseFastFlagManager;
            set => App.Settings.Prop.UseFastFlagManager = value;
        }

        public IReadOnlyDictionary<MSAAMode, string?> MSAALevels => FastFlagManager.MSAAModes;

        public MSAAMode SelectedMSAALevel
        {
            get => MSAALevels.FirstOrDefault(x => x.Value == App.FastFlags.GetPreset("Rendering.MSAA")).Key;
            set => App.FastFlags.SetPreset("Rendering.MSAA", MSAALevels[value]);
        }

        public bool FixDisplayScaling
        {
            get => App.FastFlags.GetPreset("Rendering.DisableScaling") == "True";
            set => App.FastFlags.SetPreset("Rendering.DisableScaling", value ? "True" : null);
        }

        public IReadOnlyDictionary<TextureQuality, string?> TextureQualities => FastFlagManager.TextureQualityLevels;

        public TextureQuality SelectedTextureQuality
        {
            get => TextureQualities.Where(x => x.Value == App.FastFlags.GetPreset("Rendering.TextureQuality.Level")).FirstOrDefault().Key;
            set
            {
                if (value == TextureQuality.Default)
                {
                    App.FastFlags.SetPreset("Rendering.TextureQuality", null);
                }
                else
                {
                    App.FastFlags.SetPreset("Rendering.TextureQuality.OverrideEnabled", "True");
                    App.FastFlags.SetPreset("Rendering.TextureQuality.Level", TextureQualities[value]);
                }
            }
        }
        // conjunto de Fast Flags do preset "WhiteList"
        private static readonly Dictionary<string, string> WhiteListFlags = new()
        {
            { "FFlagHandleAltEnterFullscreenManually", "False" },
            { "DFFlagTextureQualityOverrideEnabled", "True" },
            { "DFIntDebugFRMQualityLevelOverride", "1" },
            { "DFIntTextureQualityOverride", "0" },
            { "FIntGrassMovementReducedMotionFactor", "0" },
            { "DFIntCSGLevelOfDetailSwitchingDistanceL34", "0" },
            { "DFIntCSGLevelOfDetailSwitchingDistanceL23", "0" },
            { "DFIntCSGLevelOfDetailSwitchingDistanceL12", "0" },
            { "DFIntCSGLevelOfDetailSwitchingDistance", "0" },
            { "FIntDebugForceMSAASamples", "0" },
            { "DFFlagDisableDPIScale", "False" },
            { "DFFlagDebugPauseVoxelizer", "True" },
            { "FFlagDebugGraphicsPreferD3D11", "True" },
            { "FIntFRMMaxGrassDistance", "0" },
            { "FIntFRMMinGrassDistance", "0" },
            { "disabled_FFlagDebugGraphicsPreferVulkan", "True" },
            { "disabled_FFlagDebugGraphicsPreferOpenGL", "True" },
            { "disabled_FFlagDebugGraphicsDisableDirect3D11", "True" },
            { "disabled_FFlagDebugSkyGray", "True" },
        };

        public bool UseWhiteList
        {
            get => WhiteListFlags.All(pair => App.FastFlags.GetValue(pair.Key) == pair.Value);
            set
            {
                foreach (var pair in WhiteListFlags)
                    App.FastFlags.SetValue(pair.Key, value ? pair.Value : null);
            }
        }

        // NOTA: verificamos a allowlist oficial de Fast Flags da Roblox (anúncio de 29/09/2025) e
        // TODAS as flags da WhiteList já estão nela - ótimo, ela continua funcionando de verdade.
        // As flags que tínhamos aqui antes (limite de FPS, desativar sombra/pós-processamento, etc.)
        // NÃO estão na allowlist oficial, então a Roblox as ignora silenciosamente - não faziam
        // mais nenhum efeito real. Removidas por honestidade. O que sobrou de "performance máxima"
        // de verdade hoje em dia são otimizações de sistema (prioridade de processo, GPU dedicada),
        // não mais Fast Flags extras.
        private static readonly Dictionary<string, string> MaxPerformanceExtraFlags = new();

        public bool UseMaxPerformanceMode
        {
            // hoje em dia, "Performance Máxima" = WhiteList inteira (que já é o máximo permitido
            // pela allowlist da Roblox) + ligar as otimizações de sistema (fora do escopo de Fast Flags)
            get => WhiteListFlags.All(pair => App.FastFlags.GetValue(pair.Key) == pair.Value)
                && App.Settings.Prop.UseHighProcessPriority
                && App.Settings.Prop.PreferDedicatedGpu;
            set
            {
                foreach (var pair in WhiteListFlags)
                    App.FastFlags.SetValue(pair.Key, value ? pair.Value : null);

                foreach (var pair in MaxPerformanceExtraFlags)
                    App.FastFlags.SetValue(pair.Key, value ? pair.Value : null);

                App.Settings.Prop.UseHighProcessPriority = value;
                App.Settings.Prop.PreferDedicatedGpu = value;

                OnPropertyChanged(nameof(UseWhiteList));
            }
        }

        public bool ResetConfiguration
        {
            get => _preResetFlags is not null;

            set
            {
                if (value)
                {
                    _preResetFlags = new(App.FastFlags.Prop);
                    App.FastFlags.Prop.Clear();
                }
                else
                {
                    App.FastFlags.Prop = _preResetFlags!;
                    _preResetFlags = null;
                }

                RequestPageReloadEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
