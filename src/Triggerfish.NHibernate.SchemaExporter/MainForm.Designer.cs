namespace Triggerfish.NHibernate.SchemaExporter
{
	partial class MainForm
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
			this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.txtParameter = new System.Windows.Forms.TextBox();
			this.lblParameters = new System.Windows.Forms.Label();
			this.cboParameters = new System.Windows.Forms.ComboBox();
			this.cmdExport = new System.Windows.Forms.Button();
			this.cboConfigurations = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.txtAssembly = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtScript = new System.Windows.Forms.RichTextBox();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dlgFileOpen
			// 
			this.dlgFileOpen.AddExtension = false;
			this.dlgFileOpen.Filter = "Dll files|*.dll|All files|*.*";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.txtParameter);
			this.splitContainer1.Panel1.Controls.Add(this.lblParameters);
			this.splitContainer1.Panel1.Controls.Add(this.cboParameters);
			this.splitContainer1.Panel1.Controls.Add(this.cmdExport);
			this.splitContainer1.Panel1.Controls.Add(this.cboConfigurations);
			this.splitContainer1.Panel1.Controls.Add(this.label3);
			this.splitContainer1.Panel1.Controls.Add(this.cmdBrowse);
			this.splitContainer1.Panel1.Controls.Add(this.txtAssembly);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.txtScript);
			this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(7);
			this.splitContainer1.Size = new System.Drawing.Size(691, 517);
			this.splitContainer1.SplitterDistance = 110;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 9;
			// 
			// txtParameter
			// 
			this.txtParameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtParameter.Location = new System.Drawing.Point(514, 46);
			this.txtParameter.Name = "txtParameter";
			this.txtParameter.Size = new System.Drawing.Size(165, 22);
			this.txtParameter.TabIndex = 18;
			this.txtParameter.Visible = false;
			this.txtParameter.Leave += new System.EventHandler(this.txtParamater_Leave);
			// 
			// lblParameters
			// 
			this.lblParameters.AutoSize = true;
			this.lblParameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblParameters.Location = new System.Drawing.Point(295, 49);
			this.lblParameters.Name = "lblParameters";
			this.lblParameters.Size = new System.Drawing.Size(81, 16);
			this.lblParameters.TabIndex = 17;
			this.lblParameters.Text = "Parameters:";
			this.lblParameters.Visible = false;
			// 
			// cboParameters
			// 
			this.cboParameters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboParameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboParameters.FormattingEnabled = true;
			this.cboParameters.Location = new System.Drawing.Point(382, 46);
			this.cboParameters.Name = "cboParameters";
			this.cboParameters.Size = new System.Drawing.Size(126, 24);
			this.cboParameters.TabIndex = 16;
			this.cboParameters.Visible = false;
			this.cboParameters.SelectedIndexChanged += new System.EventHandler(this.cboParameters_SelectedIndexChanged);
			// 
			// cmdExport
			// 
			this.cmdExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdExport.Location = new System.Drawing.Point(113, 83);
			this.cmdExport.Name = "cmdExport";
			this.cmdExport.Size = new System.Drawing.Size(75, 23);
			this.cmdExport.TabIndex = 15;
			this.cmdExport.Text = "Export";
			this.cmdExport.UseVisualStyleBackColor = true;
			this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
			// 
			// cboConfigurations
			// 
			this.cboConfigurations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboConfigurations.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboConfigurations.FormattingEnabled = true;
			this.cboConfigurations.Location = new System.Drawing.Point(113, 46);
			this.cboConfigurations.Name = "cboConfigurations";
			this.cboConfigurations.Size = new System.Drawing.Size(149, 24);
			this.cboConfigurations.TabIndex = 14;
			this.cboConfigurations.SelectedIndexChanged += new System.EventHandler(this.cboConfigurations_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(13, 49);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 16);
			this.label3.TabIndex = 13;
			this.label3.Text = "Configurations:";
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdBrowse.Location = new System.Drawing.Point(371, 9);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(24, 23);
			this.cmdBrowse.TabIndex = 11;
			this.cmdBrowse.Text = "...";
			this.cmdBrowse.UseVisualStyleBackColor = true;
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// txtAssembly
			// 
			this.txtAssembly.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAssembly.Location = new System.Drawing.Point(113, 9);
			this.txtAssembly.Name = "txtAssembly";
			this.txtAssembly.Size = new System.Drawing.Size(252, 22);
			this.txtAssembly.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(36, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "Assembly:";
			// 
			// txtScript
			// 
			this.txtScript.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtScript.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtScript.Location = new System.Drawing.Point(7, 7);
			this.txtScript.Name = "txtScript";
			this.txtScript.Size = new System.Drawing.Size(677, 392);
			this.txtScript.TabIndex = 0;
			this.txtScript.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(691, 517);
			this.Controls.Add(this.splitContainer1);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Schema Exporter";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog dlgFileOpen;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button cmdExport;
		private System.Windows.Forms.ComboBox cboConfigurations;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button cmdBrowse;
		private System.Windows.Forms.TextBox txtAssembly;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtParameter;
		private System.Windows.Forms.Label lblParameters;
		private System.Windows.Forms.ComboBox cboParameters;
		private System.Windows.Forms.RichTextBox txtScript;
	}
}

