using NUnit.Framework;
using FileManager;
using System.Linq;

namespace FileManager.Test {
    [TestFixture]
    public class SimpleComponentFactoryTests {

        [TestCase("older")]
        [TestCase("ile")]
        [TestCase("filee")]
        [TestCase("foldr")]
        public void CreateComponent_CreatingSomethingThatIsNotAComponent_ReturnsNull(string componentType) {
            //Arrange

            //Act           
            Component resultAfterCreatingFolder = SimpleComponentFactory.CreateComponent(componentType, "Folder");
            Component resultAfterCreatingFile = SimpleComponentFactory.CreateComponent(componentType, "File");

            //Assert
            Assert.AreEqual(null, resultAfterCreatingFolder);
            Assert.AreEqual(null, resultAfterCreatingFile);
        }

        [TestCase("Folder")]
        [TestCase("folder")]
        public void CreateComponent_CreatingFolder_ReturnsNewObjectOfTypeFolder(string componentType) {
            //Arrange

            //Act           
            Component resultAfterCreatingFolder1 = SimpleComponentFactory.CreateComponent(componentType, componentType);
            Component resultAfterCreatingFolder2 = SimpleComponentFactory.CreateComponent(componentType, componentType);

            //Assert
            Assert.AreEqual(resultAfterCreatingFolder1.GetType(), typeof(Folder));
            Assert.AreEqual(resultAfterCreatingFolder2.GetType(), typeof(Folder));
        }

        [TestCase("File")]
        [TestCase("file")]
        public void CreateComponent_CreatingFile_ReturnsNewObjectOfTypeFile(string componentType) {
            //Arrange

            //Act           
            Component resultAfterCreatingFile1 = SimpleComponentFactory.CreateComponent(componentType, componentType);
            Component resultAfterCreatingFile2 = SimpleComponentFactory.CreateComponent(componentType, componentType);

            //Assert
            Assert.AreEqual(typeof(File), resultAfterCreatingFile1.GetType());
            Assert.AreEqual(typeof(File), resultAfterCreatingFile2.GetType());
        }
    }
}