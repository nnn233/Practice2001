namespace Practice2001
{
    public partial class Form2 : Form
    {
        public Form2(string login)
        {
            InitializeComponent();
            label1.Text = login;
            FormClosed += Form2_FormClosed;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
