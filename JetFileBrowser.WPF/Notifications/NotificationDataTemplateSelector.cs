using System.Windows;
using System.Windows.Controls;
using JetFileBrowser.History;
using JetFileBrowser.Notifications.Types;

namespace JetFileBrowser.WPF.Notifications {
    public class NotificationDataTemplateSelector : DataTemplateSelector {
        public DataTemplate MessageNotificationTemplate { get; set; }
        public DataTemplate SavingProjectNotificationTemplate { get; set; }
        public DataTemplate HistoryNotificationTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            switch (item) {
                case HistoryNotification _: return this.HistoryNotificationTemplate;
                case MessageNotification _: return this.MessageNotificationTemplate;
                default: {
                    return base.SelectTemplate(item, container);
                }
            }
        }
    }
}