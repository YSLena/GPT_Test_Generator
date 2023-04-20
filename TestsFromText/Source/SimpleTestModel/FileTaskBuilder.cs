using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace TaskModel
{
    /*
     */

    public class FileTaskBuilder
    {
        public string FileName = String.Empty;
        public int IncorrectAnswersCount = 3;
        public string NamePattern = String.Empty;
        public string Preambula = String.Empty; // текст перед каждым вариатнтом вопроса

        class AnswerVariant
        {
            internal string AnswerText = "";
            internal bool IsCorrect = false;

            internal AnswerVariant(string text, bool isCorrect)
            {
                AnswerText = text;
                IsCorrect = isCorrect;
            }

            internal AnswerVariant(string text)
            {
                AnswerText = text;
                IsCorrect = false;
            }
        }

        Dictionary<string, List<AnswerVariant>> QuestionsAndManyAnswers = new Dictionary<string, List<AnswerVariant>>();

        string ReadFromFile()
        {
            StreamReader sr = new StreamReader(FileName);
            //Continue to read until you reach end of file
            string str = String.Empty;
            while (!sr.EndOfStream)
            {
                str += sr.ReadLine() + "\n";
            }
            return str;
        }

        void QuestionTextToLists(string text)
        {
            QuestionsAndManyAnswers.Clear();
            string[] lines = text.Split('\n');
            string q = string.Empty;
                
            List<AnswerVariant> answs = new List<AnswerVariant>();
            int corrAnswCount = 0;
            foreach (string str in lines)
            {
                if (str.Length == 0)
                    continue;
                switch (str[0])
                {
                    case '.':
                        q = str.Substring(1);
                        break;
                    case '+':
                        answs.Add(new AnswerVariant(str.Substring(1), true));
                        corrAnswCount++;
                        break;
                    case ';':
                        QuestionsAndManyAnswers.Add(q, answs);
                        answs = new List<AnswerVariant>();
                        break;
                    case '-':
                        answs.Add(new AnswerVariant(str.Substring(1)));
                        break;
                    default:
                        answs.Add(new AnswerVariant(str));
                        break;
                }
                if (str[0] == '.')
                    q = str.Substring(1);

            }
        }

        MultichoiceTask generateOneTaskFromManyAmswers()
        {
            MultichoiceTask task = new MultichoiceTask();
            task.Name = NamePattern + DateTime.Now.Ticks.ToString();

            string qText = QuestionsAndManyAnswers.First().Key;
            List<AnswerVariant> answers = QuestionsAndManyAnswers.First().Value;
            QuestionsAndManyAnswers.Remove(qText);

            if (Preambula != "")
                qText = "<p>" + Preambula + "</p><p></p><p>" + qText + "</p>";

            task.QuestionText = qText;
            foreach (AnswerVariant answ in answers)
            {
                if (answ.IsCorrect) task.CorrectAnswers.Add(answ.AnswerText);
                else task.IncorrectAnswers.Add(answ.AnswerText);
            }

            return task;
        }



        public List<ITask> GenerateTasksFromManyAmswersFile()
        {
            return GenerateTasksFromManyAnswersText(ReadFromFile());
        }

        public List<ITask> GenerateTasksFromManyAnswersText(string questionsText)
        {
            List<ITask> tasks = new List<ITask>();
            QuestionTextToLists(questionsText);
            ITask t;

            while (QuestionsAndManyAnswers.Count > 0)
            {
                t = generateOneTaskFromManyAmswers();
                tasks.Add(t);
            }
            return tasks;
        }

        public int SaveToXmlFromManyAnswersText(string questionsText, string filePath)
        {
            List<ITask> quiz;
            quiz = GenerateTasksFromManyAnswersText(questionsText);
            QuestionSerializer.WriteToXml(quiz, filePath);
            return quiz.Count;
        }

        public string Test(string str1, string str2)
        { return str1 + " и " + str2 + "!"; }

    }
}
