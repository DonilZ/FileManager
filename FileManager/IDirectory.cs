using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager {

    public interface IDirectory{
        Component GetCurrentComponent();
        string GetPathOfTheCurrentDirectory();
        bool Down(string componentName);
        bool Up();
    }
}
