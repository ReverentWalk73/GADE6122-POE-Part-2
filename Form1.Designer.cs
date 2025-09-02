namespace GADE6122_POE_Part_1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitialzeComponent()
        {
            this.lbldisplay = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // LabelDisplay

            this.lbldisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbldisplay.Font = new System.Drawing.Font("Consolas", 10F);
            this.lbldisplay.Location = new System.Drawing.Point(12, 12);
            this.lbldisplay.Name = "labelDisplay";
            this.lbldisplay.Size = new System.Drawing.Size(400, 400);
            this.lbldisplay.TabIndex = 0;
            this.lbldisplay.TextAlign = System.Drawing.ContentAlignment.TopLeft;

            // Form1

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 424);
            this.Controls.Add(this.lbldisplay);
            this.Name = "Form1";
            this.Text = "Hero Game";
            this.ResumeLayout(false);
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbldisplay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbldisplay
            // 
            this.lbldisplay.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldisplay.Location = new System.Drawing.Point(48, 31);
            this.lbldisplay.Name = "lbldisplay";
            this.lbldisplay.Size = new System.Drawing.Size(523, 384);
            this.lbldisplay.TabIndex = 0;
            this.lbldisplay.Text = "Display";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbldisplay);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbldisplay;
    }
}

