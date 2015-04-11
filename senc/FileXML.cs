using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace senc
{
    public class FileXML
    {
        //private BlockArray blockArray;

        public FileXML(BlockArray bA)
        {
            //blockArray = bA;
        }

        public static BlockArray saveArray(BlockArray blockArray, string path)
        {
            Type type;
            XmlSerializer writer;
            //string path;
            StreamWriter file;

            type = typeof(BlockArray);
            writer = new XmlSerializer(type);
            if (path.Substring(path.Length - 4, 4) != ".xml")
                path += ".xml";
            Console.WriteLine(path);
            file = new StreamWriter(path);

            writer.Serialize(file, blockArray);
            file.Close();
            return blockArray;
        }
        public static BlockArray loadArray(BlockArray blockArray, string path)
        {
            Type type;
            XmlSerializer writer;
            //string path;
            StreamReader reader;
            BlockArray newBlockArray;
            List<Button> buttons;

            type = typeof(BlockArray);
            writer = new XmlSerializer(type);
            reader = new StreamReader(path);
            buttons = new List<Button>();

            foreach (List<Block> list in blockArray.blocks)
                foreach (Block block in list)
                    buttons.Add(block.button);

            Block.clearCounter();
            newBlockArray = writer.Deserialize(reader) as BlockArray;
            reader.Close();

            int buttonIndex = 0;
            foreach (List<Block> list in newBlockArray.blocks)
                foreach (Block block in list)
                {
                    block.blockArray = newBlockArray;
                    block.button = buttons[buttonIndex];
                    block.changeImage(0);
                    block.rotateImage(block.rotateNumber);
                    block.setValue(block.value);
                    buttonIndex++;
                }

            return newBlockArray;           
        }
    }
}
