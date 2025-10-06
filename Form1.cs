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
                gameEngine = new GameEngine(10, 5);

                this.Text = "GADE6112 - Project One - Hero Game";
                this.Size = new Size(500, 500);

                Label lblTitle = new Label
                {
                    Text = "Simple Hero Game",
                    Font = new Font("Consolas", 16, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(30, 10)
                };

                Controls.Add(lblTitle);

                // GroupBox for movement buttons
                GroupBox movementGroup = new GroupBox
                {
                    Text = "Move",
                    Location = new Point(150, 320),
                    Size = new Size(180, 120)
                };

                // Movement Buttons
                Font buttonFont = new Font("Consolas", 12, FontStyle.Bold);
                Button btnUp = new Button
                {
                    Text = "↑",
                    Font = buttonFont,
                    Location = new Point(70, 25),
                    Size = new Size(60, 40),
                    TabIndex = 0
                };
                Button btnDown = new Button
                {
                    Text = "↓",
                    Font = buttonFont,
                    Location = new Point(70, 75),
                    Size = new Size(60, 40),
                    TabIndex = 1
                };
                Button btnLeft = new Button
                {
                    Text = "←",
                    Font = buttonFont,
                    Location = new Point(10, 75),
                    Size = new Size(60, 40),
                    TabIndex = 2
                };
                Button btnRight = new Button
                {
                    Text = "→",
                    Font = buttonFont,
                    Location = new Point(130, 75),
                    Size = new Size(60, 40),
                    TabIndex = 3
                };
                // Click events
                btnUp.Click += (s, e) => MoveHero(Direction.DirectionType.up);
                btnDown.Click += (s, e) => MoveHero(Direction.DirectionType.down);
                btnLeft.Click += (s, e) => MoveHero(Direction.DirectionType.left);
                btnRight.Click += (s, e) => MoveHero(Direction.DirectionType.right);

                movementGroup.Controls.Add(btnUp);
                movementGroup.Controls.Add(btnDown);
                movementGroup.Controls.Add(btnLeft);
                movementGroup.Controls.Add(btnRight);
                Controls.Add(movementGroup);

                this.Load += Form1_Load;
                UpdateDisplay();
            }
            private void MoveHero(Direction.DirectionType direction)
            {
                if (gameEngine.TriggerMovement(direction))
                {
                    UpdateDisplay();
                }
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

            private void lbldisplay_Click(object sender, EventArgs e)
            {

            }
        }
}
