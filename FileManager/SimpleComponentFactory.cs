using System;
using System.Collections.Generic;

namespace FileManager {
    public class SimpleComponentFactory {
        public static Component CreateComponent (string componentType, string componentName) {
            Component desiredComponent = null;
            
            if (componentType == "Folder" || componentType == "folder") desiredComponent = new Folder(componentName);
            else if (componentType == "File" || componentType == "file") desiredComponent = new File(componentName);

            return desiredComponent;
        }
    }
}
