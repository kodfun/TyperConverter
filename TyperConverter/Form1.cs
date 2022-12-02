namespace TyperConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            txtOutput.Text = "^j::" + Environment.NewLine;
            foreach (string line in txtInput.Text.Split("\r\n"))
            {
                foreach (var c in line)
                {
                    ReplaceAndSend(c.ToString());
                }
                ReplaceAndSend(Environment.NewLine);
                //txtOutput.AppendText($"Send !+f{Environment.NewLine}");
            }
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\calistir.ahk", txtOutput.Text);
        }

        private void ReplaceAndSend(string s)
        {
            string cmd = "SendRaw";
            Dictionary<string, string> dic = new() 
            {
                { "\r\n", "Enter" },
                { "\n", "Enter" },
            };

            if (dic.ContainsKey(s))
            {
                s = "{" + dic[s] + "}";
            }
            else
            {
                s = "{U+" + Convert.ToString((int)s[0], 16).PadLeft(4, '0') + "}";
            }

            txtOutput.AppendText($"Send {s}{Environment.NewLine}");
            txtOutput.AppendText($"Sleep {10}{Environment.NewLine}");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + txtFileName.Text;
            File.WriteAllText(path, txtOutput.Text);
        }
    }
}