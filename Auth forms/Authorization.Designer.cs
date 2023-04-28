namespace Kogtev_Lopushok
{
    partial class Authorization
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_Login = new System.Windows.Forms.Panel();
            this.textBox_Login = new System.Windows.Forms.TextBox();
            this.panel_Password = new System.Windows.Forms.Panel();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.label_Login = new System.Windows.Forms.Label();
            this.label_Password = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Login = new System.Windows.Forms.Button();
            this.panel_Login.SuspendLayout();
            this.panel_Password.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Login
            // 
            this.panel_Login.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Login.Controls.Add(this.textBox_Login);
            this.panel_Login.Location = new System.Drawing.Point(115, 82);
            this.panel_Login.Name = "panel_Login";
            this.panel_Login.Size = new System.Drawing.Size(350, 45);
            this.panel_Login.TabIndex = 0;
            // 
            // textBox_Login
            // 
            this.textBox_Login.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox_Login.Location = new System.Drawing.Point(3, 10);
            this.textBox_Login.Name = "textBox_Login";
            this.textBox_Login.Size = new System.Drawing.Size(342, 20);
            this.textBox_Login.TabIndex = 0;
            this.textBox_Login.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Login_KeyDown);
            // 
            // panel_Password
            // 
            this.panel_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Password.Controls.Add(this.textBox_Password);
            this.panel_Password.Location = new System.Drawing.Point(115, 126);
            this.panel_Password.Name = "panel_Password";
            this.panel_Password.Size = new System.Drawing.Size(350, 40);
            this.panel_Password.TabIndex = 1;
            // 
            // textBox_Password
            // 
            this.textBox_Password.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox_Password.Location = new System.Drawing.Point(3, 10);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.PasswordChar = '*';
            this.textBox_Password.Size = new System.Drawing.Size(342, 20);
            this.textBox_Password.TabIndex = 1;
            this.textBox_Password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Password_KeyDown);
            // 
            // label_Login
            // 
            this.label_Login.AutoSize = true;
            this.label_Login.Location = new System.Drawing.Point(120, 74);
            this.label_Login.Name = "label_Login";
            this.label_Login.Size = new System.Drawing.Size(38, 13);
            this.label_Login.TabIndex = 2;
            this.label_Login.Text = "Логин";
            // 
            // label_Password
            // 
            this.label_Password.AutoSize = true;
            this.label_Password.Location = new System.Drawing.Point(120, 118);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(45, 13);
            this.label_Password.TabIndex = 3;
            this.label_Password.Text = "Пароль";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(565, 57);
            this.label1.TabIndex = 4;
            this.label1.Text = "Авторизация";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_Login
            // 
            this.button_Login.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button_Login.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.button_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Login.Location = new System.Drawing.Point(232, 175);
            this.button_Login.Name = "button_Login";
            this.button_Login.Size = new System.Drawing.Size(115, 25);
            this.button_Login.TabIndex = 5;
            this.button_Login.Text = "Войти";
            this.button_Login.UseVisualStyleBackColor = false;
            this.button_Login.Click += new System.EventHandler(this.button_Login_Click);
            // 
            // Authorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(589, 252);
            this.Controls.Add(this.button_Login);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_Password);
            this.Controls.Add(this.label_Login);
            this.Controls.Add(this.panel_Password);
            this.Controls.Add(this.panel_Login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Authorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лопушок";
            this.panel_Login.ResumeLayout(false);
            this.panel_Login.PerformLayout();
            this.panel_Password.ResumeLayout(false);
            this.panel_Password.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_Login;
        private System.Windows.Forms.TextBox textBox_Login;
        private System.Windows.Forms.Panel panel_Password;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.Label label_Login;
        private System.Windows.Forms.Label label_Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Login;
    }
}

