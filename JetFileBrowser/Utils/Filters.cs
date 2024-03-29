using JetFileBrowser.Views.Dialogs.FilePicking;

namespace JetFileBrowser.Utils {
    public static class Filters {
        public const string FrameFPXExtension = "fpx";
        public const string FrameFPXExtensionDot = "." + FrameFPXExtension;

        public static readonly string ImageTypesAndAll =
            Filter.Of().
                   AddFilter("PNG File", "png").
                   AddFilter("JPEG", "jpg", "jpeg").
                   AddFilter("Bitmap", "bmp").
                   AddAllFiles().
                   ToString();

        public static readonly string ProjectTypeAndAllFiles =
            Filter.Of().AddFilter("JetFileBrowser Project", FrameFPXExtension).AddAllFiles().ToString();

        public static readonly string VideoFormatsAndAll =
            Filter.Of().
                   AddFilter("MP4", "mp4").
                   AddFilter("MOV", "mov").
                   AddFilter("MKV", "mkv").
                   AddFilter("FLV", "flv").
                   AddAllFiles().
                   ToString();
    }
}