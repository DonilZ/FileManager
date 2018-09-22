using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManager {

    public class Folder : Component {
        private List<Component> _contents;

        public Folder (string name) : base(name) {
            _contents = new List<Component>();
        }

        public override bool Add(Component newComponent) {   
            if (newComponent == null) return false;

            if (IsThereAComponentInThisFolderWithTheSameName(newComponent.Name)) return false;

            _contents.Add(newComponent);
            return true;
        }

        public override bool Remove(Component removableComponent) {
            if (removableComponent == null) return false;

            if (!IsThereAComponentInThisFolderWithTheSameName(removableComponent.Name)) return false;

            _contents.Remove(removableComponent);
            return true;
        }

        private bool IsThereAComponentInThisFolderWithTheSameName(string componentName) {
            return _contents.Any(component => component.Name == componentName);
        }

        public override List<Component> GetContents() {
            return _contents;
        }

        public override void ShowName() {
            setForegroundColorInTheConsole(ConsoleColor.DarkBlue);
            Console.WriteLine(this.Name);
            Console.ResetColor();
        }

        private void setForegroundColorInTheConsole (ConsoleColor color) {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
        }

        public override bool IsThisACompositeComponent() {
            return true;
        }

    }
}
