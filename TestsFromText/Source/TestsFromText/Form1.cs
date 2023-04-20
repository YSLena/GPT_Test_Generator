using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TaskModel;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string textExample = ".Questuon\r\n+Correct Answer\r\n-Incorrect Answer\r\n;\r\n.Що це було?\r\n-Супутник НАСА\r\n+Мабуть, НЛО\r\n-Чого?\r\n-Якась дурня\r\n;\r\n.Що таке дерево ?\r\n+Рослина\r\n-Тварина\r\n-Субатомна частка\r\n+Матеріал для табуреток\r\n+Граф ієрархічної структури\r\n; ";

        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.DefaultExt = "xml";
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.Filter = "XML files(*.xml)|*.xml|All files(*.*)|*.*";
                saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
                string filename = saveFileDialog1.FileName;

                FileTaskBuilder builder = new FileTaskBuilder();

                builder.NamePattern = "Multi";

                List<ITask> quiz;

                builder.NamePattern = "MultiMulti";
                int q = builder.SaveToXmlFromManyAnswersText(textBox2.Text, filename);

                label1.Text = q + " tests has been serialized in xml";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = textExample;
        }
    }
}
