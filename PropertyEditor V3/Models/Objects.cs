using System.Collections.Generic;
using System.Windows.Forms;

namespace PropertyEditor.Models
{
    public class Objects
    {
        public int Type { get; set; }
        public ulong Id { get; set; }
        public ulong Offset { get; set; }
        public ulong NewOffset { get; set; }
        public ulong Size { get; set; }
        public ObjectsValues Keys { get; set; }

        public string GetNameTitle()
        {
            if (Keys != null)
            {
                return Keys.Name;
            }
            return ".";
        }
    }

    public class ObjectsValues
    {
        public string Name { get; set; }
        public int Type { get; set; } // item type (Weapon == 9, ...)
        public int ValueType { get; set; } //value type (INT32, REAL32, STRING, ...)
        public int NationsCount { get; set; } // amount of times q will add the value in nations
        public List<object> Nations { get; set; } // if it is an item records its values
        public bool IsRegistryRoot { get; set; } // home folder
        public bool IsFolder { get; set; } // folder (TRN3)
        public List<ulong> Folders { get; set; } // se for uma pasta puxa todos os files de dentro da pasta
        public List<ulong> Items { get; set; } // pull all items
    }
}
