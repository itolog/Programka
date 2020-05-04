using System;
using System.Collections.Generic;
using System.Text;

namespace Programka
{
    class TaskData
    { 

        public Guid ID => Guid.NewGuid();
        public string FilePath { get; set; }
       public string FileCommand { get; set; }

         
        public TaskData(string filePath, string fileCommand)
        { 
            FilePath = filePath;
            FileCommand = fileCommand;
        }
    }
}
