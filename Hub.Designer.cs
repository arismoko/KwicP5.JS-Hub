
namespace KwicFrontend
{
    partial class hub
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
            this.createBTN = new System.Windows.Forms.Button();
            this.projectTitle = new System.Windows.Forms.TextBox();
            this.projectList = new System.Windows.Forms.ListView();
            this.loadBTN = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createBTN
            // 
            this.createBTN.Location = new System.Drawing.Point(390, 55);
            this.createBTN.Name = "createBTN";
            this.createBTN.Size = new System.Drawing.Size(86, 23);
            this.createBTN.TabIndex = 0;
            this.createBTN.Text = "Create Project";
            this.createBTN.UseVisualStyleBackColor = true;
            this.createBTN.Click += new System.EventHandler(this.createBTN_Click);
            // 
            // projectTitle
            // 
            this.projectTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.projectTitle.Location = new System.Drawing.Point(284, 55);
            this.projectTitle.MaxLength = 256;
            this.projectTitle.Name = "projectTitle";
            this.projectTitle.Size = new System.Drawing.Size(100, 13);
            this.projectTitle.TabIndex = 1;
            this.projectTitle.Text = "newProject";
            this.projectTitle.WordWrap = false;
            this.projectTitle.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // projectList
            // 
            this.projectList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.projectList.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.projectList.HideSelection = false;
            this.projectList.HotTracking = true;
            this.projectList.HoverSelection = true;
            this.projectList.Location = new System.Drawing.Point(149, 84);
            this.projectList.MultiSelect = false;
            this.projectList.Name = "projectList";
            this.projectList.Size = new System.Drawing.Size(427, 361);
            this.projectList.TabIndex = 2;
            this.projectList.UseCompatibleStateImageBehavior = false;
            this.projectList.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // loadBTN
            // 
            this.loadBTN.Location = new System.Drawing.Point(490, 451);
            this.loadBTN.Name = "loadBTN";
            this.loadBTN.Size = new System.Drawing.Size(86, 23);
            this.loadBTN.TabIndex = 3;
            this.loadBTN.Text = "Load Project";
            this.loadBTN.UseVisualStyleBackColor = true;
            this.loadBTN.Click += new System.EventHandler(this.loadBTN_click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(149, 451);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Delete Project";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.deleteBTN_Click);
            // 
            // hub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(737, 533);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.loadBTN);
            this.Controls.Add(this.projectList);
            this.Controls.Add(this.projectTitle);
            this.Controls.Add(this.createBTN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "hub";
            this.Text = "KwicP5 Hub";
            this.Load += new System.EventHandler(this.hubLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createBTN;
        private System.Windows.Forms.TextBox projectTitle;
        private System.Windows.Forms.ListView projectList;
        private System.Windows.Forms.Button loadBTN;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button1;
    }
}

