using System.Xml.Linq;
using System.Collections.Generic;

namespace TaskModel
{
    public class MultichoiceQuestion: Question
    {
        public bool single = true;
        public bool shuffleanswers = true;
        public string answernumbering = "abc";
        public int showstandardinstruction = 1;
        public Format correctfeedback = new Format("Відповідь правильна :)");
        public Format partiallycorrectfeedback = new Format("Відповідь частково правильна");
        public Format incorrectfeedback = new Format("Відповідь неправильна :( ");
        public bool shownumcorrect;
        public List<MultichoiсeAnswer> answers = new List<MultichoiсeAnswer>();

        public MultichoiceQuestion()
        {
            type = "multichoice";
        }

        public override XElement ToXElement()
        {
            XElement doc = new XElement("question",
                    new XAttribute("type", type),
                    name.ToXElement(),
                    questiontext.ToXElement("questiontext"),
                    generalfeedback.ToXElement("generalfeedback"),
                    new XElement("defaultgrade", defaultgrade),
                    new XElement("penalty", penalty),
                    new XElement("hidden", hidden),
                    new XElement("idnumber", idnumber),
                    new XElement("single", single),
                    new XElement("shuffleanswers", shuffleanswers),
                    new XElement("answernumbering", answernumbering),
                    new XElement("showstandardinstruction", showstandardinstruction),
                    correctfeedback.ToXElement("correctfeedback"),
                    partiallycorrectfeedback.ToXElement("partiallycorrectfeedback"),
                    incorrectfeedback.ToXElement("incorrectfeedback"),
                    new XElement("shownumcorrect", shownumcorrect)
                );

            foreach (MultichoiсeAnswer answ in answers)
                doc.Add(answ.ToXElement());

            return doc;
        }

    }

    public class MultichoiсeAnswer: Answer
    {
        public MultichoiсeAnswer()
        {
            format = "html";
            fraction = 0; // 100 для правильного ответа
        }
    }
}
