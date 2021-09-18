
namespace Panorama_QRCode_Generator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.btnSearchApp = new System.Windows.Forms.Button();
            this.textBoxAppPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(580, 162);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(226, 55);
            this.btnGenerate.TabIndex = 21;
            this.btnGenerate.Text = "Générer QR Code";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(406, 33);
            this.label5.TabIndex = 20;
            this.label5.Text = "Mot de passe de l\'application :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(357, 33);
            this.label4.TabIndex = 19;
            this.label4.Text = "Chemin vers l\'application :";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassword.Location = new System.Drawing.Point(24, 173);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(420, 44);
            this.textBoxPassword.TabIndex = 18;
            // 
            // btnSearchApp
            // 
            this.btnSearchApp.Location = new System.Drawing.Point(977, 71);
            this.btnSearchApp.Name = "btnSearchApp";
            this.btnSearchApp.Size = new System.Drawing.Size(82, 44);
            this.btnSearchApp.TabIndex = 17;
            this.btnSearchApp.Text = "...";
            this.btnSearchApp.UseVisualStyleBackColor = true;
            this.btnSearchApp.Click += new System.EventHandler(this.btnSearchApp_Click);
            // 
            // textBoxAppPath
            // 
            this.textBoxAppPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAppPath.Location = new System.Drawing.Point(26, 71);
            this.textBoxAppPath.Name = "textBoxAppPath";
            this.textBoxAppPath.Size = new System.Drawing.Size(922, 44);
            this.textBoxAppPath.TabIndex = 16;
            this.textBoxAppPath.Text = "C:\\Users\\Public\\Documents\\Codra\\Panorama E2\\ST-Demo";
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Location = new System.Drawing.Point(24, 246);
            this.textBoxStatus.Multiline = true;
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxStatus.Size = new System.Drawing.Size(1035, 412);
            this.textBoxStatus.TabIndex = 23;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "\"PDF files\"|*.pdf";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 706);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.btnSearchApp);
            this.Controls.Add(this.textBoxAppPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button btnSearchApp;
        private System.Windows.Forms.TextBox textBoxAppPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

