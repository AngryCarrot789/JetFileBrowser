using System;
using System.Reflection;
using JetFileBrowser.Services;
using JetFileBrowser.Shortcuts.Dialogs;
using JetFileBrowser.Views.Dialogs.FilePicking;
using JetFileBrowser.Views.Dialogs.Message;
using JetFileBrowser.Views.Dialogs.Progression;
using JetFileBrowser.Views.Dialogs.UserInputs;

namespace JetFileBrowser {
    public static class IoC {
        private static volatile bool isAppRunning = true;

        public static SimpleIoC Instance { get; } = new SimpleIoC();

        public static bool IsAppRunning {
            get => isAppRunning;
            set => isAppRunning = value;
        }

        public static ApplicationViewModel App { get; } = new ApplicationViewModel();

        public static IShortcutManagerDialogService ShortcutManagerDialog => Provide<IShortcutManagerDialogService>();
        public static Action<string> OnShortcutModified { get; set; }
        public static Action<string> BroadcastShortcutActivity { get; set; }

        /// <summary>
        /// The application dispatcher, used to execute actions on the main thread
        /// </summary>
        public static IDispatcher Dispatcher { get; set; }

        public static IClipboardService Clipboard => Provide<IClipboardService>();
        public static IMessageDialogService MessageDialogs => Provide<IMessageDialogService>();
        public static IProgressionDialogService ProgressionDialogs => Provide<IProgressionDialogService>();
        public static IFilePickDialogService FilePicker => Provide<IFilePickDialogService>();
        public static IUserInputDialogService UserInput => Provide<IUserInputDialogService>();
        public static IExplorerService ExplorerService => Provide<IExplorerService>();
        public static IKeyboardDialogService KeyboardDialogs => Provide<IKeyboardDialogService>();
        public static IMouseDialogService MouseDialogs => Provide<IMouseDialogService>();

        public static ITranslator Translator => Provide<ITranslator>();

        public static Action<string> BroadcastShortcutChanged { get; set; }

        public static void LoadServicesFromAttributes() {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (TypeInfo typeInfo in assembly.DefinedTypes) {
                    ServiceImplementationAttribute implementationAttribute = typeInfo.GetCustomAttribute<ServiceImplementationAttribute>();
                    if (implementationAttribute != null && implementationAttribute.Type != null) {
                        object instance;
                        try {
                            instance = Activator.CreateInstance(typeInfo);
                        }
                        catch (Exception e) {
                            throw new Exception($"Failed to create implementation of {implementationAttribute.Type} as {typeInfo}", e);
                        }

                        Instance.Register(implementationAttribute.Type, instance);
                    }
                }
            }
        }

        public static T Provide<T>() => Instance.GetService<T>();
    }
}