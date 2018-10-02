using NUnit.Framework;
using FileManager;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace FileManager.Test {
    [TestFixture]
    public class ConsoleFileManagerTests {
        private string _lastMessageFromViewer;
        private FileManager _consoleFileManager;
        
        public Mock<IMessageViewer> FakeMessageViewer { get; private set; }
        public Mock<Component> FakeComponent { get; private set;}
        public Mock<IDirectory> FakeDirectory { get; private set; }

        public ConsoleFileManagerTests() {
            createAFakeMessageViewer();

            createAFakeComponent() ;

            createAFakeDirectory();

            _consoleFileManager = new ConsoleFileManager(FakeDirectory.Object, FakeMessageViewer.Object);
        }

        private void createAFakeMessageViewer() {
            FakeMessageViewer = new Mock<IMessageViewer>();

            FakeMessageViewer.Setup (method => method.DisplayMessage(It.IsAny<string>()))
                                    .Callback<string>((message) => _lastMessageFromViewer = message);
        }

        private void createAFakeComponent() {
            FakeComponent = new Mock<Component>();

            FakeComponent.Setup (method => method.Add(It.IsAny<Component>()))
                                .Returns(false);
            FakeComponent.Setup (method => method.Remove(It.IsAny<Component>()))
                                .Returns(false);
            
            List<Component> fakeEmptyList = new List<Component>();
            FakeComponent.Setup (method => method.GetContents())
                                .Returns(fakeEmptyList);
        }

        private void createAFakeDirectory() {
            FakeDirectory = new Mock<IDirectory>();

            FakeDirectory.Setup (method => method.Up())
                                .Returns(false);
            FakeDirectory.Setup (method => method.Down(It.IsAny<string>()))
                                .Returns(false);
            
            FakeDirectory.Setup (method => method.GetCurrentComponent())
                                .Returns(FakeComponent.Object);
        }

        [SetUp]
        public void Setup() {
            List<Component> fakeEmptyList = new List<Component>();
            FakeComponent.Setup (method => method.GetContents())
                                .Returns(fakeEmptyList);
        }

        [Test]
        public void AddNewFolder_ThereIsAlreadyAComponentWithTheSameName_GiveAnErrorMessage() {
            //Arrange
            Component folderWithTheSameName = new Folder("SameName");

            //Act           
            _consoleFileManager.AddNewFolderToTheCurrentDirectory(folderWithTheSameName.Name);

            //Assert
            string expectedMessage = "A component with this name already exists";
            string actualMessage = _lastMessageFromViewer;

            Assert.AreEqual(expectedMessage, actualMessage);

        }

        [Test]
        public void AddNewFile_ThereIsAlreadyAComponentWithTheSameName_GiveAnErrorMessage() {
            //Arrange
            Component fileWithTheSameName = new File("SameName");

            //Act           
            _consoleFileManager.AddNewFolderToTheCurrentDirectory(fileWithTheSameName.Name);

            //Assert
            string expectedMessage = "A component with this name already exists";
            string actualMessage = _lastMessageFromViewer;

            Assert.AreEqual(expectedMessage, actualMessage);

        }

        [Test]
        public void RemoveComponent_ThereIsNoComponentWithTheSameName_GiveAnErrorMessage() {
            //Arrange

            //Act           
            _consoleFileManager.RemoveAComponentFromTheCurrentDirectory("nameOfNotExistComponent");

            //Assert
            string expectedMessage = "The removable component does not exist";
            string actualMessage = _lastMessageFromViewer;

            Assert.AreEqual(expectedMessage, actualMessage);

        }

        [Test]
        public void RiseToTheUpperLevel_WeAreInRoot_GiveAnErrorMessage() {
            //Arrange

            //Act           
            _consoleFileManager.RiseToTheUpperLevel();

            //Assert
            string expectedMessage = "It is impossible to rise up, because you are in the root";
            string actualMessage = _lastMessageFromViewer;

            Assert.AreEqual(expectedMessage, actualMessage);

        }

        [Test]
        public void GoDownToTheLowerLevel_ComponentDoesNotExist_GiveAnErrorMessage() {
            //Arrange

            //Act           
            _consoleFileManager.GoDownToTheLowerLevel("nameOfNotExistComponent");

            //Assert
            string expectedMessage = "The component nameOfNotExistComponent does not exist in the current directory";
            string actualMessage = _lastMessageFromViewer;

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void GoDownToTheLowerLevel_ComponentExistButIsNotAComposite_GiveAnErrorMessage() {
            //Arrange
            List<Component> fakeListWithNonCompositeComponent = new List<Component>();
            fakeListWithNonCompositeComponent.Add(new File("nameOfNonCompositeComponent"));

            FakeComponent.Setup (method => method.GetContents())
                                .Returns(fakeListWithNonCompositeComponent);

            //Act           
            _consoleFileManager.GoDownToTheLowerLevel("nameOfNonCompositeComponent");

            //Assert
            string expectedMessage = "The component nameOfNonCompositeComponent can not be descended because it is not a composite";
            string actualMessage = _lastMessageFromViewer;

            Assert.AreEqual(expectedMessage, actualMessage);
        }

    }
}