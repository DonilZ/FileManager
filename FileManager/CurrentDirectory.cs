using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager {

    public class CurrentDirectory : IDirectory {
        private List<Component> _componentsOfCurrentDirectory;

        public CurrentDirectory() {
            _componentsOfCurrentDirectory = new List<Component>();

            Folder rootFolder = new Folder("Root");
            _componentsOfCurrentDirectory.Add(rootFolder);

        }
        
        public Component GetCurrentComponent() {
            return _componentsOfCurrentDirectory.Last();
        }

        public string GetPathOfTheCurrentDirectory() {
            string pathOfCurrentDirectory = formThePathOfCurrentDirectory();

            return pathOfCurrentDirectory;
        }

        private string formThePathOfCurrentDirectory() {
            string path = "";

            foreach(var component in _componentsOfCurrentDirectory) {
                path += $"{component.Name}/";
            }

            return path;
        }

        public bool Down (string componentName) {
            
            Component desiredComponentOnLowerLevel = pullTheDesiredComponentFromTheLowerLevel(componentName);
            
            if (desiredComponentOnLowerLevel == null) return false;

            if (!desiredComponentOnLowerLevel.IsThisACompositeComponent()) return false;

            _componentsOfCurrentDirectory.Add(desiredComponentOnLowerLevel);

            return true;
            
        }

        private Component pullTheDesiredComponentFromTheLowerLevel(string componentName) {
            Component currentComponent = _componentsOfCurrentDirectory.Last();

            List<Component> componentsOnLowerLevel = currentComponent.GetContents();

            Component desiredComponentOnLowerLevel = componentsOnLowerLevel
                                                    .SingleOrDefault(component => component.Name == componentName);
            
            return desiredComponentOnLowerLevel;
        }

        public bool Up () {
            if (AreWeInTheRoot()) return false;

            _componentsOfCurrentDirectory.Remove(_componentsOfCurrentDirectory.Last());

            return true;
        }

        private bool AreWeInTheRoot() {
            return _componentsOfCurrentDirectory.Count == 1;
        }
    }
}
