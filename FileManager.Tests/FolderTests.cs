using NUnit.Framework;
using FileManager;
using System.Linq;

namespace FileManager.Test {
    [TestFixture]
    public class FolderTests {

        private Component _currentFolder;

        public FolderTests() {
            _currentFolder = new Folder("TestFolder");
        }

        [SetUp]
        public void SetUp() {
            _currentFolder = new Folder("TestFolder");
        }

        [Test]
        public void AddNewComponent_ThereIsNotComponentWithTheSameName_AddingANewComponentAndReturnsTrue() {
            //Arrange
            Component newFolder = createNewFolder("newFolder");
            Component newFile = createNewFile("newFile");

            //Act           
            bool resultAfterAddingFolder = _currentFolder.Add(newFolder);
            bool resultAfterAddingFile = _currentFolder.Add(newFile);

            //Assert
            bool IsTheAddedFolderAppearInTheCurrentFolder = IsThereComponent("newFolder");

            bool IsTheAddedFileAppearInTheCurrentFolder = IsThereComponent("newFile");


            Assert.True(IsTheAddedFolderAppearInTheCurrentFolder);
            Assert.True(IsTheAddedFileAppearInTheCurrentFolder);

            Assert.True(resultAfterAddingFolder);
            Assert.True(resultAfterAddingFile);
        }

        [Test]
        public void AddNewComponent_ThereIsAComponentWithTheSameName_NotAddingANewComponentAndReturnsFalse() {
            //Arrange
            
            Component newFolder = createNewFolder("newFolder");
            Component fileWithTheSameName = createNewFile("newFolder");
            _currentFolder.Add(newFolder);

            //Act           
            bool resultAfterAddingFolderWithTheSameName = _currentFolder.Add(newFolder);
            bool resultAfterAddingFileWithTheSameName = _currentFolder.Add(fileWithTheSameName);

            //Assert
            int countOfComponentsInCurrentFolderAfterAdding = _currentFolder.GetContents().Count;
            int actualNumberOfComponentsInCurrentFolderAfterAdding = 1;


            Assert.AreEqual(countOfComponentsInCurrentFolderAfterAdding, actualNumberOfComponentsInCurrentFolderAfterAdding);

            Assert.False(resultAfterAddingFolderWithTheSameName);
            Assert.False(resultAfterAddingFileWithTheSameName);
        }

        [Test]
        public void RemoveComponent_TheCurrentFolderContainsTheRemovableComponent_RemovingComponentAndReturnsTrue() {
            //Arrange
            Component newFolder = createNewFolder("newFolder");
            _currentFolder.Add(newFolder);

            //Act           
            bool resultAfterRemoving = _currentFolder.Remove(newFolder);

            //Assert
            bool IsTheRemovedComponentAppearInTheCurrentFolder = IsThereComponent("newFolder");

            Assert.False(IsTheRemovedComponentAppearInTheCurrentFolder);
            Assert.True(resultAfterRemoving);
        }

        [Test]
        public void RemoveComponent_TheCurrentFolderDoesNotContainTheRemovableComponent_ReturnsFalse() {
            //Arrange
            Component newFolder = createNewFolder("newFolder");

            //Act           
            bool resultAfterRemoving = _currentFolder.Remove(newFolder);

            //Assert
            Assert.False(resultAfterRemoving);
        }

        private bool IsThereComponent(string componentName) {
            return _currentFolder.GetContents().Any(component => component.Name == componentName);
        }

        private Folder createNewFolder(string folderName) {
            return new Folder(folderName);
        }

        private File createNewFile(string fileName) {
            return new File(fileName);
        }
    }
}