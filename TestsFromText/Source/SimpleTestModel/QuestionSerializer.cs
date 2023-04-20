using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


namespace TaskModel
{
    public static class QuestionSerializer
    {
        public static void WriteToXml(List<ITask> tasks, string FileName)
        {
            XDocument doc = QuestionDocBuilder.TasksToXmlDoc(tasks);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            XmlWriter writer = XmlWriter.Create(FileName, settings);
            doc.WriteTo(writer);
            writer.Close();
        }

        public static void WriteToTxt(List<ITask> tasks, string FileName, bool append = true)
        {
            using (StreamWriter sw = new StreamWriter(FileName, append))
            {
                foreach (ITask t in tasks)
                {
                    sw.WriteLine(t.GetQuestionText());
                }
            }
        }
    }
}
