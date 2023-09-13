using System.Collections.Generic;

namespace JetFileBrowser.AdvancedContextService {
    public interface IContextProvider {
        void GetContext(List<IContextEntry> list);
    }
}