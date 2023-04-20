using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModel
{
    public interface ITask
    {
        Question ToQuestion();
        string GetQuestionText();
        //object GetAnswer();
    }

    public abstract class TestTask: ITask
    {
        public string Name;
        public string QuestionText;

        public abstract Question ToQuestion();

        public string GetQuestionText()
        {
            return QuestionText;    
        }
    }

    public enum TaskType
    {
        Short,
        MultiChoise,
        Numerical
    }


}
