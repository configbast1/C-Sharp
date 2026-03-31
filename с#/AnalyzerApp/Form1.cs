using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace TextAnalyzerApp
{
    public partial class Form1 : Form
    {
        string text = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            text = textBox1.Text;

            new Thread(CountWords).Start();
            new Thread(CountNumbers).Start();
            new Thread(CountPunctuation).Start();
            new Thread(MostFrequentWord).Start();
            new Thread(AverageWordLength).Start();
        }

        void CountWords()
        {
            string[] words = text.Split(' ', '\n', '\r');
            int count = 0;

            for (int i = 0; i < words.Length; i++)
                if (words[i] != "") count++;

            Invoke(new Action(() =>
            {
                wordsLabel.Text = "Words: " + count;
                SaveToFile();
            }));
        }

        void CountNumbers()
        {
            string[] parts = text.Split(' ', '\n', '\r');
            int count = 0;

            for (int i = 0; i < parts.Length; i++)
            {
                int num;
                if (int.TryParse(parts[i], out num))
                    count++;
            }

            Invoke(new Action(() =>
            {
                numbersLabel.Text = "Numbers: " + count;
                SaveToFile();
            }));
        }

        void CountPunctuation()
        {
            int count = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c == '.' || c == ',' || c == '!' || c == '?' || c == ':' || c == ';')
                    count++;
            }

            Invoke(new Action(() =>
            {
                punctuationLabel.Text = "Punctuation: " + count;
                SaveToFile();
            }));
        }

        void MostFrequentWord()
        {
            string[] words = text.Split(' ', '\n', '\r');
            int maxCount = 0;
            string frequent = "";

            for (int i = 0; i < words.Length; i++)
            {
                int count = 0;

                for (int j = 0; j < words.Length; j++)
                {
                    if (words[i].ToLower() == words[j].ToLower() && words[i] != "")
                        count++;
                }

                if (count > maxCount)
                {
                    maxCount = count;
                    frequent = words[i];
                }
            }

            Invoke(new Action(() =>
            {
                frequentLabel.Text = "Frequent: " + frequent;
                SaveToFile();
            }));
        }

        void AverageWordLength()
        {
            string[] words = text.Split(' ', '\n', '\r');
            int total = 0;
            int count = 0;

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != "")
                {
                    total += words[i].Length;
                    count++;
                }
            }

            double avg = 0;
            if (count != 0)
                avg = (double)total / count;

            Invoke(new Action(() =>
            {
                avgLengthLabel.Text = "Avg length: " + avg.ToString("F2");
                SaveToFile();
            }));
        }

        void SaveToFile()
        {
            string result =
                wordsLabel.Text + "\n" +
                numbersLabel.Text + "\n" +
                punctuationLabel.Text + "\n" +
                frequentLabel.Text + "\n" +
                avgLengthLabel.Text;

            File.WriteAllText("analysis.txt", result);
        }
    }
}
