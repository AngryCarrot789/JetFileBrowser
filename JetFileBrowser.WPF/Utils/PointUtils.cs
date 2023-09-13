using System.Numerics;
using System.Windows;

namespace JetFileBrowser.WPF.Utils {
    public static class PointUtils {
        public static Vector2 ToVec2(this Point point) => new Vector2((float) point.X, (float) point.Y);
    }
}