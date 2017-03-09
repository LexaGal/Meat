namespace WindowsFormsApplicationGame
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
            this.Cols = new System.Windows.Forms.Label();
            this.textBoxCols = new System.Windows.Forms.TextBox();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.textBoxRows = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxMatr = new System.Windows.Forms.GroupBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.textBoxHosts = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxLoops = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Cols
            // 
            this.Cols.AutoSize = true;
            this.Cols.Location = new System.Drawing.Point(5, 40);
            this.Cols.Name = "Cols";
            this.Cols.Size = new System.Drawing.Size(47, 13);
            this.Cols.TabIndex = 0;
            this.Cols.Text = "Columns";
            // 
            // textBoxCols
            // 
            this.textBoxCols.Location = new System.Drawing.Point(54, 37);
            this.textBoxCols.Name = "textBoxCols";
            this.textBoxCols.Size = new System.Drawing.Size(40, 20);
            this.textBoxCols.TabIndex = 1;
            this.textBoxCols.Text = "5";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(6, 111);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(87, 23);
            this.buttonCreate.TabIndex = 2;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.CreateMatrix);
            // 
            // textBoxRows
            // 
            this.textBoxRows.Location = new System.Drawing.Point(54, 12);
            this.textBoxRows.Name = "textBoxRows";
            this.textBoxRows.Size = new System.Drawing.Size(41, 20);
            this.textBoxRows.TabIndex = 4;
            this.textBoxRows.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rows";
            // 
            // groupBoxMatr
            // 
            this.groupBoxMatr.AutoSize = true;
            this.groupBoxMatr.Location = new System.Drawing.Point(99, 4);
            this.groupBoxMatr.Name = "groupBoxMatr";
            this.groupBoxMatr.Size = new System.Drawing.Size(162, 181);
            this.groupBoxMatr.TabIndex = 5;
            this.groupBoxMatr.TabStop = false;
            this.groupBoxMatr.Text = "Matrix";
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(6, 137);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(87, 23);
            this.buttonUpdate.TabIndex = 6;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.UpdateMatrix);
            // 
            // textBoxHosts
            // 
            this.textBoxHosts.Location = new System.Drawing.Point(54, 62);
            this.textBoxHosts.Name = "textBoxHosts";
            this.textBoxHosts.Size = new System.Drawing.Size(41, 20);
            this.textBoxHosts.TabIndex = 8;
            this.textBoxHosts.Text = "2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Hosts";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(6, 162);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(87, 23);
            this.buttonStart.TabIndex = 9;
            this.buttonStart.Text = "Start Game";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.StartGame);
            // 
            // textBoxLoops
            // 
            this.textBoxLoops.Location = new System.Drawing.Point(54, 85);
            this.textBoxLoops.Name = "textBoxLoops";
            this.textBoxLoops.Size = new System.Drawing.Size(41, 20);
            this.textBoxLoops.TabIndex = 11;
            this.textBoxLoops.Text = "1";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(6, 88);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(36, 13);
            this.label.TabIndex = 10;
            this.label.Text = "Loops";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(273, 197);
            this.Controls.Add(this.textBoxLoops);
            this.Controls.Add(this.label);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBoxHosts);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.groupBoxMatr);
            this.Controls.Add(this.textBoxRows);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.textBoxCols);
            this.Controls.Add(this.Cols);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Cols;
        private System.Windows.Forms.TextBox textBoxCols;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.TextBox textBoxRows;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxMatr;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.TextBox textBoxHosts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxLoops;
        private System.Windows.Forms.Label label;
    }
}

