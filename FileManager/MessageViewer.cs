using System;
using System.Collections.Generic;

namespace FileManager {

    public interface IMessageViewer {
        void DisplayMessage(string message);
    }

    public class ConsoleMessageViewer : IMessageViewer {
        public void DisplayMessage(string message) {
            setForegroundColorInTheConsole(message);

            Console.WriteLine(message);
            
            Console.ResetColor();
        }

        private void setForegroundColorInTheConsole(string message) {
            if (message.Contains("successfully") || message.Contains("Successfully")) Console.ForegroundColor = ConsoleColor.Green;
            else Console.ForegroundColor = ConsoleColor.Red;
        }
    }
}
