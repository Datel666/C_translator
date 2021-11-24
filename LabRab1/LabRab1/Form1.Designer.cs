namespace LabRab1
{
    partial class Compiler
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
            this.butOpen = new System.Windows.Forms.Button();
            this.butWork = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.richTB = new System.Windows.Forms.RichTextBox();
            this.DGW = new System.Windows.Forms.DataGridView();
            this.Lexeme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DGW2 = new System.Windows.Forms.DataGridView();
            this.Type_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.DGW6 = new System.Windows.Forms.DataGridView();
            this.SpecialWords = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGW5 = new System.Windows.Forms.DataGridView();
            this.Classification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGW4 = new System.Windows.Forms.DataGridView();
            this.Literals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGW3 = new System.Windows.Forms.DataGridView();
            this.Variable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lbMatrix = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGW)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGW2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGW6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGW5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGW4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGW3)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // butOpen
            // 
            this.butOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butOpen.Location = new System.Drawing.Point(810, 12);
            this.butOpen.Name = "butOpen";
            this.butOpen.Size = new System.Drawing.Size(75, 23);
            this.butOpen.TabIndex = 0;
            this.butOpen.Text = "Открыть";
            this.butOpen.UseVisualStyleBackColor = true;
            this.butOpen.Click += new System.EventHandler(this.button_Open);
            // 
            // butWork
            // 
            this.butWork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butWork.Location = new System.Drawing.Point(810, 42);
            this.butWork.Name = "butWork";
            this.butWork.Size = new System.Drawing.Size(75, 23);
            this.butWork.TabIndex = 1;
            this.butWork.Text = "Выполнить";
            this.butWork.UseVisualStyleBackColor = true;
            this.butWork.Click += new System.EventHandler(this.button_Work);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // richTB
            // 
            this.richTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTB.Location = new System.Drawing.Point(2, 14);
            this.richTB.Name = "richTB";
            this.richTB.Size = new System.Drawing.Size(166, 540);
            this.richTB.TabIndex = 2;
            this.richTB.Text = "";
            // 
            // DGW
            // 
            this.DGW.AllowUserToAddRows = false;
            this.DGW.AllowUserToDeleteRows = false;
            this.DGW.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Lexeme,
            this.Type});
            this.DGW.Location = new System.Drawing.Point(15, 6);
            this.DGW.Name = "DGW";
            this.DGW.ReadOnly = true;
            this.DGW.Size = new System.Drawing.Size(247, 504);
            this.DGW.TabIndex = 4;
            // 
            // Lexeme
            // 
            this.Lexeme.HeaderText = "Лексема";
            this.Lexeme.Name = "Lexeme";
            this.Lexeme.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.HeaderText = "Тип";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(174, 14);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(630, 542);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGW);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(622, 516);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Таблица 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DGW2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(622, 516);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Таблица 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DGW2
            // 
            this.DGW2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DGW2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGW2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type_2});
            this.DGW2.Location = new System.Drawing.Point(6, 3);
            this.DGW2.Name = "DGW2";
            this.DGW2.Size = new System.Drawing.Size(246, 507);
            this.DGW2.TabIndex = 0;
            // 
            // Type_2
            // 
            this.Type_2.HeaderText = "Тип/Номер";
            this.Type_2.Name = "Type_2";
            this.Type_2.Width = 200;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.DGW6);
            this.tabPage3.Controls.Add(this.DGW5);
            this.tabPage3.Controls.Add(this.DGW4);
            this.tabPage3.Controls.Add(this.DGW3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(622, 516);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Таблица 3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // DGW6
            // 
            this.DGW6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DGW6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGW6.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SpecialWords});
            this.DGW6.Location = new System.Drawing.Point(3, 6);
            this.DGW6.Name = "DGW6";
            this.DGW6.Size = new System.Drawing.Size(150, 503);
            this.DGW6.TabIndex = 3;
            // 
            // SpecialWords
            // 
            this.SpecialWords.HeaderText = "Ключевые слова";
            this.SpecialWords.Name = "SpecialWords";
            // 
            // DGW5
            // 
            this.DGW5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DGW5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGW5.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Classification});
            this.DGW5.Location = new System.Drawing.Point(159, 6);
            this.DGW5.Name = "DGW5";
            this.DGW5.Size = new System.Drawing.Size(154, 503);
            this.DGW5.TabIndex = 2;
            // 
            // Classification
            // 
            this.Classification.HeaderText = "Разделители";
            this.Classification.Name = "Classification";
            // 
            // DGW4
            // 
            this.DGW4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DGW4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGW4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Literals});
            this.DGW4.Location = new System.Drawing.Point(469, 6);
            this.DGW4.Name = "DGW4";
            this.DGW4.Size = new System.Drawing.Size(147, 503);
            this.DGW4.TabIndex = 1;
            // 
            // Literals
            // 
            this.Literals.HeaderText = "Литералы";
            this.Literals.Name = "Literals";
            // 
            // DGW3
            // 
            this.DGW3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DGW3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGW3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Variable});
            this.DGW3.Location = new System.Drawing.Point(319, 6);
            this.DGW3.Name = "DGW3";
            this.DGW3.Size = new System.Drawing.Size(145, 503);
            this.DGW3.TabIndex = 0;
            // 
            // Variable
            // 
            this.Variable.HeaderText = "Идентификаторы";
            this.Variable.Name = "Variable";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lbMatrix);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(622, 516);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Матрица";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lbMatrix
            // 
            this.lbMatrix.FormattingEnabled = true;
            this.lbMatrix.Location = new System.Drawing.Point(3, 6);
            this.lbMatrix.Name = "lbMatrix";
            this.lbMatrix.Size = new System.Drawing.Size(265, 498);
            this.lbMatrix.TabIndex = 0;
            // 
            // Compiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 566);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.richTB);
            this.Controls.Add(this.butWork);
            this.Controls.Add(this.butOpen);
            this.Name = "Compiler";
            this.Text = "Compiler";
            ((System.ComponentModel.ISupportInitialize)(this.DGW)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGW2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGW6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGW5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGW4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGW3)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butOpen;
        private System.Windows.Forms.Button butWork;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox richTB;
        private System.Windows.Forms.DataGridView DGW;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView DGW2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView DGW3;
        private System.Windows.Forms.DataGridView DGW4;
        private System.Windows.Forms.DataGridView DGW5;
        private System.Windows.Forms.DataGridView DGW6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lexeme;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpecialWords;
        private System.Windows.Forms.DataGridViewTextBoxColumn Classification;
        private System.Windows.Forms.DataGridViewTextBoxColumn Literals;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variable;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListBox lbMatrix;
    }
}

