using NUnit.Framework;
using FileManager;
using System.Collections.Generic;
using System.Linq;

namespace FileManager.Test {
    [TestFixture]
    public class FolderTests {

        private Component _currentFolder;

        public FolderTests() {
            _currentFolder = SimpleComponentFactory.CreateComponent("Folder", "TestFolder");
        }

        [SetUp]
        public void SetUp() {
            _currentFolder = SimpleComponentFactory.CreateComponent("Folder", "TestFolder");
        }

        [Test]
        public void AddNewComponent_ThereIsNotComponentWithTheSameName_AddingANewComponentAndReturnsTrue() {
            //Arrange
            Component newFolder = SimpleComponentFactory.CreateComponent("Folder", "newFolder");
            Component newFile = SimpleComponentFactory.CreateComponent("File", "newFile");

            //Act           
            bool resultAfterAddingFolder = _currentFolder.Add(newFolder);
            bool resultAfterAddingFile = _currentFolder.Add(newFile);

            //Assert
            bool IsTheAddedFolderAppearInTheCurrentFolder = isThereComponent("newFolder");

            bool IsTheAddedFileAppearInTheCurrentFolder = isThereComponent("newFile");


            Assert.True(IsTheAddedFolderAppearInTheCurrentFolder);
            Assert.True(IsTheAddedFileAppearInTheCurrentFolder);

            Assert.True(resultAfterAddingFolder);
            Assert.True(resultAfterAddingFile);
        }

        [Test]
        public void AddNewComponent_ThereIsAComponentWithTheSameName_NotAddingANewComponentAndReturnsFalse() {
            //Arrange
            
            Component newFolder = SimpleComponentFactory.CreateComponent("Folder", "newFolder");
            Component fileWithTheSameName = SimpleComponentFactory.CreateComponent("File", "newFolder");
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
            Component newFolder = SimpleComponentFactory.CreateComponent("Folder", "newFolder");
            _currentFolder.Add(newFolder);

            //Act           
            bool resultAfterRemoving = _currentFolder.Remove(newFolder);

            //Assert
            bool IsTheRemovedComponentAppearInTheCurrentFolder = isThereComponent("newFolder");

            Assert.False(IsTheRemovedComponentAppearInTheCurrentFolder);
            Assert.True(resultAfterRemoving);
        }

        [Test]
        public void RemoveComponent_TheCurrentFolderDoesNotContainTheRemovableComponent_ReturnsFalse() {
            //Arrange
            Component removableFolder = SimpleComponentFactory.CreateComponent("Folder", "removableFolder");

            //Act           
            bool resultAfterRemoving = _currentFolder.Remove(removableFolder);

            //Assert
            Assert.False(resultAfterRemoving);
        }

        [Test]
        public void GetContents_FromNotEmptyFolder_ReturnsContentsOfThisFolder() {
            //Arrange
            Component folder = SimpleComponentFactory.CreateComponent("Folder", "folder");
            Component file = SimpleComponentFactory.CreateComponent("File", "file");

            _currentFolder.Add(folder);
            _currentFolder.Add(file);

            //Act           
            List<Component> resultAfterGettingContentsFromNotEmptyFolder = _currentFolder.GetContents();

            //Assert
            Assert.NotNull(resultAfterGettingContentsFromNotEmptyFolder);
            Assert.True(resultAfterGettingContentsFromNotEmptyFolder.Any(f => f.Name == "folder"));
            Assert.True(resultAfterGettingContentsFromNotEmptyFolder.Any(f => f.Name == "file"));
        }

        [Test]
        public void GetContents_FromEmptyFolder_ReturnsEmptyList() {
            //Arrange

            //Act           
            List<Component> resultAfterGettingContentsFromEmptyFolder = _currentFolder.GetContents();

            //Assert
            Assert.NotNull(resultAfterGettingContentsFromEmptyFolder);
            Assert.False(resultAfterGettingContentsFromEmptyFolder.Any());
        }

        [Test]
        public void IsThisACompositeComponent_IsTheFolderACompositeComponent_ReturnsTrue() {
            //Arrange

            //Act           
            bool isTheFolderACompositeComponent = _currentFolder.IsThisACompositeComponent();

            //Assert
            Assert.True(isTheFolderACompositeComponent);
        }

        private bool isThereComponent(string componentName) {
            return _currentFolder.GetContents().Any(component => component.Name == componentName);
        }
    }
}