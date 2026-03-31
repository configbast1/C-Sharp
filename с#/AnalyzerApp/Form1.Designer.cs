namespace TextAnalyzerApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label wordsLabel;
        private System.Windows.Forms.Label numbersLabel;
        private System.Windows.Forms.Label punctuationLabel;
        private System.Windows.Forms.Label frequentLabel;
        private System.Windows.Forms.Label avgLengthLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.wordsLabel = new System.Windows.Forms.Label();
            this.numbersLabel = new System.Windows.Forms.Label();
            this.punctuationLabel = new System.Windows.Forms.Label();
            this.frequentLabel = new System.Windows.Forms.Label();
            this.avgLengthLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // textBox
            this.textBox1.Multiline = true;
            this.textBox1.Width = 400;
            this.textBox1.Height = 150;
            this.textBox1.Top = 10;
            this.textBox1.Left = 10;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);

            // labels
            this.wordsLabel.Top = 170;
            this.wordsLabel.Left = 10;

            this.numbersLabel.Top = 200;
            this.numbersLabel.Left = 10;

            this.punctuationLabel.Top = 230;
            this.punctuationLabel.Left = 10;

            this.frequentLabel.Top = 260;
            this.frequentLabel.Left = 10;

            this.avgLengthLabel.Top = 290;
            this.avgLengthLabel.Left = 10;

            // Form
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.wordsLabel);
            this.Controls.Add(this.numbersLabel);
            this.Controls.Add(this.punctuationLabel);
            this.Controls.Add(this.frequentLabel);
            this.Controls.Add(this.avgLengthLabel);
            this.Text = "Text Analyzer";

            this.ResumeLayout(false);
        }
    }
}
