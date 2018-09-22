using System;

namespace FileManager {
    class Program {
        static void Main(string[] args) {

            CurrentDirectory currentDirectory = new CurrentDirectory();

            IMessageViewer messageViewer = new ConsoleMessageViewer();

            UserWrapper userWrapper = new UserWrapper(currentDirectory, messageViewer);

            while (true) {        
                Console.WriteLine("Select the action:");
                Console.WriteLine("1: Display the name of the current folder");
                Console.WriteLine("2: Display the current directory");
                Console.WriteLine("3: Display the contents of the current directory");
                Console.WriteLine("4: Rise up one level higher");
                Console.WriteLine("5: Go down to the specified folder");
                Console.WriteLine("6: Add a new folder to the current directory");
                Console.WriteLine("7: Add a new file to the current directory");
                Console.WriteLine("8: Remove the specified folder from the current directory");
                Console.WriteLine("Any other key: Exit from the program");

                string command = Console.ReadLine();
                string invitationToEnterAName;

                switch (command) {
                    case "1":
                        userWrapper.ShowTheNameOfTheCurrentFolder();
                        break;
                    case "2":
                        userWrapper.ShowThePathOfTheCurrentDirectory();
                        break;
                    case "3":
                        userWrapper.ShowTheContentsOfTheCurrentDirectory();
                        break;
                    case "4":
                        userWrapper.RiseToTheUpperLevel();
                        break;
                    case "5":
                        invitationToEnterAName = "Enter the name of the component you want to go to:";
                        string nameOfDesiredComponentForGoDown = getNameOfDesiredComponent(invitationToEnterAName);
                        userWrapper.GoDownToTheLowerLevel(nameOfDesiredComponentForGoDown);
                        break;
                    case "6":
                        invitationToEnterAName = "Enter the name of the new folder:";
                        string nameOfNewFolder = getNameOfDesiredComponent(invitationToEnterAName);
                        userWrapper.AddNewFolderToTheCurrentDirectory(nameOfNewFolder);
                        break;
                    case "7":
                        invitationToEnterAName = "Enter the name of the new file:";
                        string nameOfNewFile = getNameOfDesiredComponent(invitationToEnterAName);
                        userWrapper.AddNewFileToTheCurrentDirectory(nameOfNewFile);
                        break;
                    case "8":
                        invitationToEnterAName = "Enter the name of the removable component:";
                        string nameOfRemovableComponent = getNameOfDesiredComponent(invitationToEnterAName);
                        userWrapper.RemoveAComponentFromTheCurrentDirectory(nameOfRemovableComponent);
                        break;
                    default:
                        return;
                }
            }
        }

        private static string getNameOfDesiredComponent(string invitationToEnterAName) {
            Console.WriteLine(invitationToEnterAName);
            string componentName;
            componentName = Console.ReadLine();

            return componentName;
        }
    }
}
