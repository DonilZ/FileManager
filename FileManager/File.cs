using System;
using System.Collections.Generic;

namespace FileManager {

    public class File : Component {
        public File (string name) : base(name) 
        {}

        public override bool Add(Component newComponent) {
            return false;
        }

        public override bool Remove(Component removableComponent){
            return false;
        }

        public override List<Component> GetContents() {
            return null;
        }

        public override void ShowName() {
            Console.WriteLine(this.Name);
        }

        public override bool IsThisACompositeComponent() {
            return false;
        }
    }
}
