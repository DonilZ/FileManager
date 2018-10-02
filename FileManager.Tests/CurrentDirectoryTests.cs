using NUnit.Framework;
using FileManager;
using System.Linq;

namespace FileManager.Test {
    
    [TestFixture]
    public class CurrentDirectoryTests {

        private IDirectory _currentDirectory;

        public CurrentDirectoryTests() {
            _currentDirectory = new CurrentDirectory();
        }

        [SetUp]
        public void SetUp() {
            _currentDirectory = new CurrentDirectory();
        }

        [Test]
        public void GoDown_ComponentInWhichWeWantToGoDownDoesNotExist_NotGoDownAndReturnsFalse() {
            //Arrange

            //Act           
            bool resultOfGoDown = _currentDirectory.Down("NameOfNotExistComponent");

            //Assert
            string expectedNameOfCurrentComponent = "Root";
            string actualNameOfCurrentComponent = _currentDirectory.GetCurrentComponent().Name;

            Assert.AreEqual(expectedNameOfCurrentComponent, actualNameOfCurrentComponent);
            Assert.False(resultOfGoDown);
        }

        [Test]
        public void GoDown_ComponentInWhichWeWantToGoDownIsNotComposite_NotGoDownAndReturnsFalse() {
            //Arrange
            Component newFile = SimpleComponentFactory.CreateComponent("File", "lowerFile");
            Component rootFolder = _currentDirectory.GetCurrentComponent();
            rootFolder.Add(newFile);

            //Act           
            bool resultOfGoDown = _currentDirectory.Down("lowerFile");

            //Assert
            string expectedNameOfCurrentComponent = "Root";
            string actualNameOfCurrentComponent = _currentDirectory.GetCurrentComponent().Name;

            Assert.AreEqual(expectedNameOfCurrentComponent, actualNameOfCurrentComponent);
            Assert.False(resultOfGoDown);
        }

        [Test]
        public void GoDown_ComponentInWhichWeWantToGoDownIsComposite_GoDownIntoAComponentAndReturnsTrue() {
            //Arrange
            Component newFolder = SimpleComponentFactory.CreateComponent("Folder", "lowerFolder");
            Component rootFolder = _currentDirectory.GetCurrentComponent();
            rootFolder.Add(newFolder);

            //Act           
            bool resultOfGoDown = _currentDirectory.Down("lowerFolder");

            //Assert
            string expectedNameOfCurrentComponent = "lowerFolder";
            string actualNameOfCurrentComponent = _currentDirectory.GetCurrentComponent().Name;

            Assert.AreEqual(expectedNameOfCurrentComponent, actualNameOfCurrentComponent);
            Assert.True(resultOfGoDown);
        }

        [Test]
        public void RiseUp_WeAreInTheRoot_NotRiseUpAndReturnsFalse() {
            //Arrange

            //Act           
            bool resultOfRiseUp = _currentDirectory.Up();

            //Assert
            string expectedNameOfCurrentComponent = "Root";
            string actualNameOfCurrentComponent = _currentDirectory.GetCurrentComponent().Name;

            Assert.AreEqual(expectedNameOfCurrentComponent, actualNameOfCurrentComponent);
            Assert.False(resultOfRiseUp);
        }

        [Test]
        public void RiseUp_WeAreNotInTheRoot_RiseUpAndReturnsTrue() {
            //Arrange
            Component newFolder = SimpleComponentFactory.CreateComponent("Folder", "lowerFolder");
            Component rootFolder = _currentDirectory.GetCurrentComponent();
            rootFolder.Add(newFolder);
            _currentDirectory.Down("lowerFolder");

            //Act           
            bool resultOfRiseUp = _currentDirectory.Up();

            //Assert
            string expectedNameOfCurrentComponent = "Root";
            string actualNameOfCurrentComponent = _currentDirectory.GetCurrentComponent().Name;

            Assert.AreEqual(expectedNameOfCurrentComponent, actualNameOfCurrentComponent);
            Assert.True(resultOfRiseUp);
        }

        [Test]
        public void GetCurrentComponent_WeAreInTheRoot_ReturnNameOfRootFolder() {
            //Arrange
            Component rootFolder = _currentDirectory.GetCurrentComponent();

            //Act           
            string actualNameOfCurrentComponent = _currentDirectory.GetCurrentComponent().Name;

            //Assert
            string expectedNameOfCurrentComponent = "Root";

            Assert.AreEqual(actualNameOfCurrentComponent, expectedNameOfCurrentComponent);
        }

        [Test]
        public void GetCurrentComponent_AddNewFolderAfterRootAndGoDownThere_ReturnNameOfNewAddedFolder() {
            //Arrange
            Component newFolder = SimpleComponentFactory.CreateComponent("Folder", "newFolder");
            Component rootFolder = _currentDirectory.GetCurrentComponent();
            rootFolder.Add(newFolder);
            _currentDirectory.Down("newFolder");

            //Act           
            string actualNameOfCurrentComponent = _currentDirectory.GetCurrentComponent().Name;

            //Assert
            string expectedNameOfCurrentComponent = "newFolder";

            Assert.AreEqual(actualNameOfCurrentComponent, expectedNameOfCurrentComponent);
        }

        [Test]
        public void GetPathOfTheCurrentDirectory_WeAreInTheRoot_ReturnPathWithOnlyRoot() {
            //Arrange

            //Act           
            string actualPathOfCurrentDirectory = _currentDirectory.GetPathOfTheCurrentDirectory();

            //Assert
            string expectedPathOfCurrentDirectory = "Root/";

            Assert.AreEqual(actualPathOfCurrentDirectory, expectedPathOfCurrentDirectory);
        }


        [TestCase("FolderOnTheLowerLevel")]
        [TestCase("Root")]
        [TestCase("1234")]
        public void GetPathOfTheCurrentDirectory_WeAreNotInTheRoot_ReturnTheFullPath(string nameOfFolderOnTheLowerLevel) {
            //Arrange
            Component componentOnTheLowerLevel = SimpleComponentFactory.CreateComponent("Folder", nameOfFolderOnTheLowerLevel);
            _currentDirectory.GetCurrentComponent().Add(componentOnTheLowerLevel);

            _currentDirectory.Down(nameOfFolderOnTheLowerLevel);

            //Act           
            string actualPathOfCurrentDirectory = _currentDirectory.GetPathOfTheCurrentDirectory();

            //Assert
            string expectedPathOfCurrentDirectory = $"Root/{nameOfFolderOnTheLowerLevel}/";

            Assert.AreEqual(actualPathOfCurrentDirectory, expectedPathOfCurrentDirectory);
        }

        [Test]
        public void GetPathOfTheCurrentDirectory_AddNewFolderAfterRootAndGoDownThere_ReturnPathWithNewFolderAfterRoot() {
            //Arrange
            Component newFolder = SimpleComponentFactory.CreateComponent("Folder", "newFolder");
            Component rootFolder = _currentDirectory.GetCurrentComponent();
            rootFolder.Add(newFolder);
            _currentDirectory.Down("newFolder");

            //Act           
            string actualPathOfCurrentDirectory = _currentDirectory.GetPathOfTheCurrentDirectory();

            //Assert
            string expectedPathOfCurrentDirectory = "Root/newFolder/";

            Assert.AreEqual(actualPathOfCurrentDirectory, expectedPathOfCurrentDirectory);
        }
        
    }
}