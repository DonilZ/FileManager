using NUnit.Framework;
using FileManager;
using System.Linq;

namespace FileManager.Test {
    [TestFixture]
    public class FileTests {

        private Component _currentFile;

        public FileTests() {
            _currentFile = SimpleComponentFactory.CreateComponent("File", "TestFile");
        }

        [Test]
        public void AddNewComponent_AnyAttemptToAddAComponentInFile_ReturnsFalse() {
            //Arrange
            Component newFolder = SimpleComponentFactory.CreateComponent("Folder", "newFolder");
            Component newFile = SimpleComponentFactory.CreateComponent("File", "newFile");

            //Act           
            bool resultAfterAddingFolder = _currentFile.Add(newFolder);
            bool resultAfterAddingFile = _currentFile.Add(newFile);

            //Assert
            Assert.False(resultAfterAddingFolder);
            Assert.False(resultAfterAddingFile);
        }

        
        [Test]
        public void RemoveComponent_AnyAttemptToRemoveAComponentFromFile_ReturnsFalse() {
            //Arrange
            Component removableFolder = SimpleComponentFactory.CreateComponent("Folder", "removableFolder");
            Component removableFile = SimpleComponentFactory.CreateComponent("File", "removableFile");

            //Act           
            bool resultAfterRemovingFolder = _currentFile.Remove(removableFolder);
            bool resultAfterRemovingFile = _currentFile.Remove(removableFile);

            //Assert
            Assert.False(resultAfterRemovingFolder);
            Assert.False(resultAfterRemovingFile);
        }

        [Test]
        public void GetContents_FromFile_ReturnsNull() {
            //Arrange

            //Act           
            var resultAfterGettingContents = _currentFile.GetContents();

            //Assert
            Assert.AreEqual(null, resultAfterGettingContents);
        }

        [Test]
        public void IsThisACompositeComponent_IsTheFileACompositeComponent_ReturnsFalse() {
            //Arrange

            //Act           
            bool isTheFileACompositeComponent = _currentFile.IsThisACompositeComponent();

            //Assert
            Assert.False(isTheFileACompositeComponent);
        }
    }
}