using System;
using System.Collections.Generic;

namespace FileManager {

    public abstract class FileManager {
        protected IDirectory _currentDirectory;
        protected IMessageViewer _messageViewer;
        
        protected FileManager(IDirectory currentDirectory, IMessageViewer messageViewer) {
            _currentDirectory = currentDirectory;
            _messageViewer = messageViewer;
        }

        public abstract void ShowTheNameOfTheCurrentFolder();

        public abstract void ShowThePathOfTheCurrentDirectory();

        public abstract void ShowTheContentsOfTheCurrentDirectory();

        public abstract void RiseToTheUpperLevel();

        public abstract void GoDownToTheLowerLevel(string nameOfDesiredComponentForGoDown);

        public abstract void AddNewFolderToTheCurrentDirectory(string nameOfNewFolder);

        public abstract void AddNewFileToTheCurrentDirectory(string nameOfNewFile);

        public abstract void RemoveAComponentFromTheCurrentDirectory(string nameOfTheRemovableComponent);
    }
}
