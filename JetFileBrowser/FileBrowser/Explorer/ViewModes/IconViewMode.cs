using JetFileBrowser.Utils;

namespace JetFileBrowser.FileBrowser.Explorer.ViewModes {
    public class IconViewMode : BaseViewModel, IExplorerViewMode {
        public static IconViewMode SmallIcons => new IconViewMode(40);
        public static IconViewMode MediumIcons => new IconViewMode(80);
        public static IconViewMode LargeIcons => new IconViewMode(150);

        private int width;

        public string Id => "Icons";

        /// <summary>
        /// The absolute width (in pixels, for now) that the icons take up. The height may vary
        /// </summary>
        public int Width {
            get => this.width;
            set => this.RaisePropertyChanged(ref this.width, Maths.Clamp(value, 1, 4));
        }

        public IconViewMode() : this(80) {
        }

        public IconViewMode(int width) {
            this.width = width;
        }
    }
}