namespace Sea_Battle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Model = new model();
            Model.PlayerShips[0, 0] = CoordStatus.Ship;
            Model.PlayerShips[5, 2] = CoordStatus.Ship;
            Model.PlayerShips[5, 3] = CoordStatus.Ship;
            Model.PlayerShips[7,3] = CoordStatus.Ship;
        }

        model Model;
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Model.Shot(textBox1.Text).ToString());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
