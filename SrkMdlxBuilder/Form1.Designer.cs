namespace SrkMdlxBuilder
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.newButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.openButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.outlineButton = new System.Windows.Forms.Button();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.mergeButton = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.listBox = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extractStripL1 = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceStripL1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.meshListBox = new System.Windows.Forms.ListBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.importFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // newButton
            // 
            this.newButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.newButton.Location = new System.Drawing.Point(6, 0);
            this.newButton.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(78, 26);
            this.newButton.TabIndex = 0;
            this.newButton.Text = "New";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.groupBox1.Size = new System.Drawing.Size(114, 176);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.newButton);
            this.flowLayoutPanel1.Controls.Add(this.openButton);
            this.flowLayoutPanel1.Controls.Add(this.saveButton);
            this.flowLayoutPanel1.Controls.Add(this.outlineButton);
            this.flowLayoutPanel1.Controls.Add(this.numericUpDown3);
            this.flowLayoutPanel1.Controls.Add(this.mergeButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 13);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(102, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(90, 150);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // openButton
            // 
            this.openButton.AllowDrop = true;
            this.openButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.openButton.Location = new System.Drawing.Point(6, 26);
            this.openButton.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(78, 26);
            this.openButton.TabIndex = 1;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            this.openButton.DragDrop += new System.Windows.Forms.DragEventHandler(this.openButton_DragDrop);
            this.openButton.DragEnter += new System.Windows.Forms.DragEventHandler(this.openButton_DragEnter);
            // 
            // saveButton
            // 
            this.saveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(6, 52);
            this.saveButton.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(78, 26);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.EnabledChanged += new System.EventHandler(this.saveButton_EnabledChanged);
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // outlineButton
            // 
            this.outlineButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.outlineButton.Enabled = false;
            this.outlineButton.Location = new System.Drawing.Point(6, 78);
            this.outlineButton.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.outlineButton.Name = "outlineButton";
            this.outlineButton.Size = new System.Drawing.Size(78, 26);
            this.outlineButton.TabIndex = 3;
            this.outlineButton.Text = "Out Line";
            this.outlineButton.UseVisualStyleBackColor = true;
            this.outlineButton.EnabledChanged += new System.EventHandler(this.saveButton_EnabledChanged);
            this.outlineButton.Click += new System.EventHandler(this.outlineButton_Click);
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.DecimalPlaces = 3;
            this.numericUpDown3.Location = new System.Drawing.Point(6, 104);
            this.numericUpDown3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(65, 20);
            this.numericUpDown3.TabIndex = 13;
            this.numericUpDown3.Value = new decimal(new int[] {
            105,
            0,
            0,
            131072});
            // 
            // mergeButton
            // 
            this.mergeButton.AllowDrop = true;
            this.mergeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mergeButton.Enabled = false;
            this.mergeButton.Location = new System.Drawing.Point(6, 124);
            this.mergeButton.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.mergeButton.Name = "mergeButton";
            this.mergeButton.Size = new System.Drawing.Size(78, 26);
            this.mergeButton.TabIndex = 14;
            this.mergeButton.Text = "Model merge";
            this.mergeButton.UseVisualStyleBackColor = true;
            this.mergeButton.Click += new System.EventHandler(this.mergeButtonClick);
            this.mergeButton.DragDrop += new System.Windows.Forms.DragEventHandler(this.mergeButton_DragDrop);
            this.mergeButton.DragEnter += new System.Windows.Forms.DragEventHandler(this.openButton_DragEnter);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 3;
            this.numericUpDown1.Location = new System.Drawing.Point(166, 39);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(65, 20);
            this.numericUpDown1.TabIndex = 10;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "M_DL000";
            this.openFileDialog.Filter = "Model Files|*.bin";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "M_DL000";
            this.saveFileDialog.Filter = "Model Files|*.bin";
            // 
            // listBox
            // 
            this.listBox.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox.FormattingEnabled = true;
            this.listBox.Items.AddRange(new object[] {
            "Skeleton definitions",
            "Skeleton",
            "Model",
            "Shadow"});
            this.listBox.Location = new System.Drawing.Point(6, 0);
            this.listBox.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(112, 108);
            this.listBox.TabIndex = 2;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            this.listBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractStripL1,
            this.replaceStripL1,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 70);
            // 
            // extractStripL1
            // 
            this.extractStripL1.Name = "extractStripL1";
            this.extractStripL1.Size = new System.Drawing.Size(124, 22);
            this.extractStripL1.Text = "Extract...";
            this.extractStripL1.Click += new System.EventHandler(this.extractStripL1_Click);
            // 
            // replaceStripL1
            // 
            this.replaceStripL1.Name = "replaceStripL1";
            this.replaceStripL1.Size = new System.Drawing.Size(124, 22);
            this.replaceStripL1.Text = "Replace...";
            this.replaceStripL1.Click += new System.EventHandler(this.replaceStripL1_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Enabled = false;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(6, 192);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.groupBox2.Size = new System.Drawing.Size(357, 130);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.listBox);
            this.flowLayoutPanel2.Controls.Add(this.meshListBox);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 13);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(254, 108);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // meshListBox
            // 
            this.meshListBox.ContextMenuStrip = this.contextMenuStrip2;
            this.meshListBox.FormattingEnabled = true;
            this.meshListBox.Location = new System.Drawing.Point(130, 0);
            this.meshListBox.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.meshListBox.Name = "meshListBox";
            this.meshListBox.Size = new System.Drawing.Size(118, 108);
            this.meshListBox.TabIndex = 3;
            this.meshListBox.SelectedIndexChanged += new System.EventHandler(this.meshListBox_SelectedIndexChanged);
            this.meshListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseDown);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 92);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem4.Text = "Delete...";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem1.Text = "Extract...";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem2.Text = "Replace...";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem3.Text = "Add...";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // importFileDialog
            // 
            this.importFileDialog.FileName = "M_DL000";
            this.importFileDialog.Filter = "Model Files|*.bin";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Controls.Add(this.checkBox10);
            this.groupBox3.Controls.Add(this.checkBox4);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numericUpDown2);
            this.groupBox3.Controls.Add(this.checkBox9);
            this.groupBox3.Controls.Add(this.checkBox8);
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.checkBox6);
            this.groupBox3.Controls.Add(this.checkBox5);
            this.groupBox3.Controls.Add(this.checkBox3);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.checkBox7);
            this.groupBox3.Location = new System.Drawing.Point(114, 6);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.groupBox3.Size = new System.Drawing.Size(243, 191);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cool shit";
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Location = new System.Drawing.Point(20, 114);
            this.checkBox10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(138, 17);
            this.checkBox10.TabIndex = 20;
            this.checkBox10.Text = "Reuse every other BAR";
            this.checkBox10.UseVisualStyleBackColor = true;
            this.checkBox10.CheckedChanged += new System.EventHandler(this.checkBox10_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(20, 99);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(79, 17);
            this.checkBox4.TabIndex = 19;
            this.checkBox4.Text = "Reuse TIM";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Max unpacked size:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Hexadecimal = true;
            this.numericUpDown2.Location = new System.Drawing.Point(115, 165);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            3840,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(65, 20);
            this.numericUpDown2.TabIndex = 11;
            this.numericUpDown2.Value = new decimal(new int[] {
            2560,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Location = new System.Drawing.Point(12, 146);
            this.checkBox9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(103, 17);
            this.checkBox9.TabIndex = 17;
            this.checkBox9.Text = "Tim tile (Repeat)";
            this.checkBox9.UseVisualStyleBackColor = true;
            this.checkBox9.CheckedChanged += new System.EventHandler(this.checkBox9_CheckedChanged);
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(12, 130);
            this.checkBox8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(167, 17);
            this.checkBox8.TabIndex = 16;
            this.checkBox8.Text = "Sort triangles per Matrix (Beta)";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.CheckedChanged += new System.EventHandler(this.checkBox8_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(20, 84);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(126, 17);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.Text = "Reuse Skeleton data";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(12, 69);
            this.checkBox6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(138, 17);
            this.checkBox6.TabIndex = 13;
            this.checkBox6.Text = "Show all available TIMs";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(12, 55);
            this.checkBox5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(122, 17);
            this.checkBox5.TabIndex = 12;
            this.checkBox5.Text = "Custom texture sizes";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(12, 41);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(96, 17);
            this.checkBox3.TabIndex = 9;
            this.checkBox3.Text = "Vertex coloring";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(12, 27);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(91, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Triangle strips";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(12, 13);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(71, 17);
            this.checkBox7.TabIndex = 11;
            this.checkBox7.Text = "Swap UV";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 328);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(326, 290);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Srk MDLX Builder 21-Jan-2022 02:27";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button saveButton;
        public System.Windows.Forms.Button openButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        public System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox meshListBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem extractStripL1;
        private System.Windows.Forms.ToolStripMenuItem replaceStripL1;
        private System.Windows.Forms.SaveFileDialog exportFileDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.OpenFileDialog importFileDialog;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        public System.Windows.Forms.CheckBox checkBox5;
        public System.Windows.Forms.CheckBox checkBox6;
        public System.Windows.Forms.CheckBox checkBox7;
        public System.Windows.Forms.CheckBox checkBox2;
        public System.Windows.Forms.CheckBox checkBox8;
        public System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox checkBox4;
        public System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.Button outlineButton;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Button mergeButton;
    }
}

