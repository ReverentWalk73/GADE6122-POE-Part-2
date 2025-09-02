using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GADE6122_POE_Part_1.Form1;

namespace GADE6122_POE_Part_1
{
    public partial class Form1 : Form
    {
        private GameEngine gameEngine;
        public Form1()
        {
            InitializeComponent();
            gameEngine = new GameEngine(10);
        }

        private void UpdateDisplay()
        {
            if (lbldisplay == null || gameEngine == null)
                return;
            try

            {
                lbldisplay.Text = gameEngine.ToString();
            }
            catch (Exception ex)
            {
                lbldisplay.Text = $"Error displaying game state: {ex.Message}";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
    }
}
