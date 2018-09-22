using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager {

    public class UserWrapper {

        private IDirectory _currentDirectory;
        private IMessageViewer _messageViewer;

        public UserWrapper(IDirectory currentDirectory, IMessageViewer messageViewer) {
            _currentDirectory = currentDirectory;
            _messageViewer = messageViewer;
        }

        public void ShowTheNameOfTheCurrentFolder() {
            Component currentFolder = _currentDirectory.GetCurrentComponent();

            Console.WriteLine(currentFolder.Name);
        }

        public void ShowThePathOfTheCurrentDirectory() {
            string pathOfTheCurrentDirectory = _currentDirectory.GetPathOfTheCurrentDirectory();

            Console.WriteLine(pathOfTheCurrentDirectory);
        }

        public void ShowTheContentsOfTheCurrentDirectory() {
            Component currentFolder = _currentDirectory.GetCurrentComponent();

            foreach (var component in currentFolder.GetContents()) {
                component.ShowName();
            }
        }

        public void RiseToTheUpperLevel() {
            bool result = _currentDirectory.Up();

            if (!result) _messageViewer.DisplayMessage("It is impossible to rise up, because you are in the root");
        }

        public void GoDownToTheLowerLevel(string nameOfDesiredComponentForGoDown) {
            bool resultAfterGoDown = _currentDirectory.Down(nameOfDesiredComponentForGoDown);

            if (resultAfterGoDown) return;

            if (!isTheCurrentFolderContainAComponent(nameOfDesiredComponentForGoDown)) {
                _messageViewer.DisplayMessage($"The component {nameOfDesiredComponentForGoDown} does not exist in the current directory");
                return;
            }

            if (!getComponentFromCurrentFolder(nameOfDesiredComponentForGoDown).IsThisACompositeComponent()) {
                _messageViewer.DisplayMessage($"The component {nameOfDesiredComponentForGoDown} can not be descended because it is not a composite");
                return;
            }

            
        }

        public void AddNewFolderToTheCurrentDirectory(string nameOfNewFolder) {
            Component newFolder = new Folder(nameOfNewFolder);

            Component currentFolder = _currentDirectory.GetCurrentComponent();

            bool resultAfterAddingNewFolder = currentFolder.Add(newFolder);

            if (!resultAfterAddingNewFolder) {
                _messageViewer.DisplayMessage("A component with this name already exists");
                return;
            }

            _messageViewer.DisplayMessage($"New folder {newFolder.Name} successfully added");
        }

        public void AddNewFileToTheCurrentDirectory(string nameOfNewFile) {
            Component newFile = new File(nameOfNewFile);

            Component currentFolder = _currentDirectory.GetCurrentComponent();

            bool resultAfterAddingNewFile = currentFolder.Add(newFile);

            if (!resultAfterAddingNewFile) {
                _messageViewer.DisplayMessage("A component with this name already exists");
                return;
            }

            _messageViewer.DisplayMessage($"New file {newFile.Name} successfully added");
        }

        public void RemoveAComponentFromTheCurrentDirectory(string nameOfTheRemovableComponent) {
            Component removableComponent = findTheRemovableComponentInTheCurrentFolder(nameOfTheRemovableComponent);

            List<Component> componentsOfCurrentFolder = getTheContentsOfTheCurrentFolder();

            bool resultAfterRemovingFile = componentsOfCurrentFolder.Remove(removableComponent);
            
            if (!resultAfterRemovingFile) {
                _messageViewer.DisplayMessage("The removable component does not exist");
                return;
            }

            _messageViewer.DisplayMessage($"Component {removableComponent.Name} successfully removed");
        }

        private Component findTheRemovableComponentInTheCurrentFolder(string nameOfTheRemovableComponent) {
            List<Component> componentsOfCurrentFolder = getTheContentsOfTheCurrentFolder();

            Component removableComponent = componentsOfCurrentFolder
                                            .SingleOrDefault(component => component.Name == nameOfTheRemovableComponent);

            return removableComponent;
        }

        private List<Component> getTheContentsOfTheCurrentFolder() {
            Component currentFolder = _currentDirectory.GetCurrentComponent();

            List<Component> componentsOfCurrentFolder = currentFolder.GetContents();

            return componentsOfCurrentFolder;
        }

        private bool isTheCurrentFolderContainAComponent(string componentName) {
            Component currentFolder = _currentDirectory.GetCurrentComponent();

            return currentFolder.GetContents().Any(component => component.Name == componentName);
        }

        private Component getComponentFromCurrentFolder(string componentName) {
            Component currentFolder = _currentDirectory.GetCurrentComponent();

            return currentFolder.GetContents().SingleOrDefault(component => component.Name == componentName);
        }

    }
}
