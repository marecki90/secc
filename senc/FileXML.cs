using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace senc
{
    public class FileXML
    {
        public FileXML()
        {
        }

        public static void saveArray(string path)
        {
            Type type;
            XmlSerializer writer;
            StreamWriter file;

            type = typeof(BlockArray);
            writer = new XmlSerializer(type);
            if (path.Substring(path.Length - 4, 4) != ".xml")
                path += ".xml";
            //Console.WriteLine(path);
            file = new StreamWriter(path);

            writer.Serialize(file, Form1.blockArray);
            file.Close();
        }
        
        public static BlockArray loadArray(string path)
        {
            Type type;
            XmlSerializer writer;
            StreamReader reader;
            BlockArray newBlockArray;
            List<Button> buttons;
            XmlDocument xml = new XmlDocument();
            XmlNodeList xmlWidth;
            XmlNodeList xmlHeight;
            int width;
            int height; 

            xml.Load(path);
            xmlWidth = xml.GetElementsByTagName("width");
            xmlHeight = xml.GetElementsByTagName("height");
            width = Convert.ToInt32(xmlWidth[0].InnerXml);
            height = Convert.ToInt32(xmlHeight[0].InnerXml);

            type = typeof(BlockArray);
            writer = new XmlSerializer(type);
            reader = new StreamReader(path);
            buttons = new List<Button>();

            
            Form1.blockArray = new BlockArray(height, width);
            /*
            foreach (List<Block> list in Form1.blockArray.blocks)
                foreach (Block block in list)
                    buttons.Add(block.button);
            */
            

            Block.clearCounter();
            newBlockArray = writer.Deserialize(reader) as BlockArray;
            reader.Close();
            
            //int buttonIndex = 0;
            foreach (List<Block> list in newBlockArray.blocks)
                foreach (Block block in list)
                {
                    block.blockArray = newBlockArray;
                    //block.button = buttons[buttonIndex];
                    block.changeImage(0);
                    block.rotateImage(block.rotateNumber);
                    block.setValue(block.value);
                    //buttonIndex++;
                }
            
            return newBlockArray;           
        }
    }
}
