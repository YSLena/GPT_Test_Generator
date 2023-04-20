using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModel
{
    internal class MultichoiceTask: TestTask 
    {
        public List<string> CorrectAnswers = new List<string>();
        public List<string> IncorrectAnswers = new List<string>();

        public override Question ToQuestion()
        {
            MultichoiceQuestion quest = new MultichoiceQuestion();
            quest.name.text = Name;
            quest.questiontext.text = QuestionText;
            quest.single = (CorrectAnswers.Count == 1);

            MultichoiсeAnswer answer;

            foreach (string ans in CorrectAnswers)
            {
                answer = new MultichoiсeAnswer();
                answer.text = ans;
                answer.fraction = (double)100 / CorrectAnswers.Count;
                quest.answers.Add(answer);
            }

            foreach (string ans in IncorrectAnswers)
            {
                answer = new MultichoiсeAnswer();
                answer.text = ans;
                if (CorrectAnswers.Count == 1)
                    answer.fraction = 0;
                else
                    answer.fraction = (double)-100 / (CorrectAnswers.Count + IncorrectAnswers.Count);
                quest.answers.Add(answer);
            }

            return quest;
        }

    }
}
