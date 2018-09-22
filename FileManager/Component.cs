using System;
using System.Collections.Generic;

namespace FileManager {

    public abstract class Component {
        public string Name { get; private set; }

        public Component (string name) {
            this.Name = name;
        }

        public Component() {
            Name = "NewComponent";
        }

        public abstract bool Add(Component newComponent);

        public abstract bool Remove(Component removableComponent);

        public abstract List<Component> GetContents();

        public abstract void ShowName();

        public abstract bool IsThisACompositeComponent();
    }
}
