    namespace WindowsFormsApplication1
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
                this.panel1 = new System.Windows.Forms.Panel();
                this.textBox2 = new System.Windows.Forms.TextBox();
                this.textBox1 = new System.Windows.Forms.TextBox();
                this.panel1.SuspendLayout();
                this.SuspendLayout();
                // 
                // panel1
                // 
                this.panel1.AutoScroll = true;
                this.panel1.Controls.Add(this.textBox2);
                this.panel1.Controls.Add(this.textBox1);
                this.panel1.Location = new System.Drawing.Point(86, 75);
                this.panel1.Name = "panel1";
                this.panel1.Size = new System.Drawing.Size(176, 70);
                this.panel1.TabIndex = 0;
                // 
                // textBox2
                // 
                this.textBox2.Location = new System.Drawing.Point(109, 17);
                this.textBox2.Name = "textBox2";
                this.textBox2.Size = new System.Drawing.Size(100, 20);
                this.textBox2.TabIndex = 1;
                this.textBox2.Click += new System.EventHandler(this.textBox2_Click);
                // 
                // textBox1
                // 
                this.textBox1.Location = new System.Drawing.Point(3, 17);
                this.textBox1.Name = "textBox1";
                this.textBox1.Size = new System.Drawing.Size(100, 20);
                this.textBox1.TabIndex = 0;
                this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
                // 
                // Form1
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(493, 271);
                this.Controls.Add(this.panel1);
                this.Name = "Form1";
                this.Text = "Form1";
                this.panel1.ResumeLayout(false);
                this.panel1.PerformLayout();
                this.ResumeLayout(false);
            }
            #endregion
            private System.Windows.Forms.Panel panel1;
            private System.Windows.Forms.TextBox textBox2;
            private System.Windows.Forms.TextBox textBox1;
        }
    }
