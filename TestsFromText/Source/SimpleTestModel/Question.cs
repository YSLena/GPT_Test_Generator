using System.Xml.Linq;
using System.Collections.Generic;

namespace TaskModel
{
    public abstract class Question
    {
        public string type;
        public Name name = new Name();
        public Format questiontext = new Format();
        public Format generalfeedback = new Format();
        public double defaultgrade = 1.0000000;
        public double penalty = 0.3333333;
        public int hidden = 0;
        public int idnumber = 0;

        public abstract XElement ToXElement();
    }

    public static class QuestionDocBuilder
    {
        public static XDocument QuestionsToXmlDoc(List<Question> questions)
        {
            XDocument doc = new XDocument(
                new XElement("quiz"));

            foreach (Question question in questions)
            {
                doc.Element("quiz").Add(question.ToXElement());
            }

            return doc;
        }

        public static XDocument TasksToXmlDoc(List<ITask> tasks)
        {
            XDocument doc = new XDocument(
                new XElement("quiz"));

            foreach (ITask task in tasks)
            {
                doc.Element("quiz").Add(task.ToQuestion().ToXElement());
            }

            return doc;
        }

    }

    public class Format
    {
        public string format = "html";
        public string text;

        public Format() { }

        public Format(string atext)
        { text = atext; }

        public XElement ToXElement(string formatname)
        {
            return
                new XElement(formatname,
                    new XAttribute("format", format),
                    text == null ? new XElement("text", null) : new  XElement("text", new XCData("<p>" + text + "</p>"))
                );
        }
    }

    public class Name
    {
        public string text = "";

        public XElement ToXElement()
        {
            return
                new XElement("name",
                    new XElement("text", text)
                );
        }
    }

    public class Answer
    {
        public string format = "moodle_auto_format";
        public double fraction = 100;
        public string text = "";
        public Format feedback = new Format();

        public virtual XElement ToXElement()
        {
            return
                new XElement("answer",
                    new XAttribute("format", format),
                    new XAttribute("fraction", fraction.ToString("#.#####").Replace(',','.')),
                    new XElement("text", text),
                    feedback.ToXElement("feedback")
                );
        }
    }


}