namespace Exercicio01
{
  partial class FrmMain
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
      this.components = new System.ComponentModel.Container();
      this.btnCreate = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnRegra = new System.Windows.Forms.Button();
      this.bsMain = new System.Windows.Forms.BindingSource(this.components);
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tpData = new System.Windows.Forms.TabPage();
      this.dgvMain = new System.Windows.Forms.DataGrid();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.tpLog = new System.Windows.Forms.TabPage();
      this.txbLog = new System.Windows.Forms.TextBox();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
      this.tabControl1.SuspendLayout();
      this.tpData.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
      this.tpLog.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnCreate
      // 
      this.btnCreate.Location = new System.Drawing.Point(12, 12);
      this.btnCreate.Name = "btnCreate";
      this.btnCreate.Size = new System.Drawing.Size(152, 23);
      this.btnCreate.TabIndex = 0;
      this.btnCreate.Text = "Criar / Carregar banco";
      this.btnCreate.UseVisualStyleBackColor = true;
      this.btnCreate.Click += new System.EventHandler(this.button1_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnRegra);
      this.panel1.Controls.Add(this.btnCreate);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(182, 413);
      this.panel1.TabIndex = 1;
      // 
      // btnRegra
      // 
      this.btnRegra.Location = new System.Drawing.Point(13, 51);
      this.btnRegra.Name = "btnRegra";
      this.btnRegra.Size = new System.Drawing.Size(151, 23);
      this.btnRegra.TabIndex = 1;
      this.btnRegra.Text = "Aplica Regra";
      this.btnRegra.UseVisualStyleBackColor = true;
      this.btnRegra.Click += new System.EventHandler(this.btnRegra_Click);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tpData);
      this.tabControl1.Controls.Add(this.tpLog);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(182, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(844, 413);
      this.tabControl1.TabIndex = 3;
      // 
      // tpData
      // 
      this.tpData.Controls.Add(this.dgvMain);
      this.tpData.Controls.Add(this.splitter1);
      this.tpData.Location = new System.Drawing.Point(4, 22);
      this.tpData.Name = "tpData";
      this.tpData.Padding = new System.Windows.Forms.Padding(3);
      this.tpData.Size = new System.Drawing.Size(836, 387);
      this.tpData.TabIndex = 0;
      this.tpData.Text = "Dados";
      this.tpData.UseVisualStyleBackColor = true;
      // 
      // dgvMain
      // 
      this.dgvMain.DataMember = "";
      this.dgvMain.DataSource = this.bsMain;
      this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvMain.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      this.dgvMain.Location = new System.Drawing.Point(3, 3);
      this.dgvMain.Name = "dgvMain";
      this.dgvMain.Size = new System.Drawing.Size(830, 378);
      this.dgvMain.TabIndex = 5;
      // 
      // splitter1
      // 
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitter1.Location = new System.Drawing.Point(3, 381);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(830, 3);
      this.splitter1.TabIndex = 4;
      this.splitter1.TabStop = false;
      // 
      // tpLog
      // 
      this.tpLog.Controls.Add(this.txbLog);
      this.tpLog.Location = new System.Drawing.Point(4, 22);
      this.tpLog.Name = "tpLog";
      this.tpLog.Padding = new System.Windows.Forms.Padding(3);
      this.tpLog.Size = new System.Drawing.Size(474, 387);
      this.tpLog.TabIndex = 1;
      this.tpLog.Text = "Log";
      this.tpLog.UseVisualStyleBackColor = true;
      // 
      // txbLog
      // 
      this.txbLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txbLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txbLog.Location = new System.Drawing.Point(3, 3);
      this.txbLog.Multiline = true;
      this.txbLog.Name = "txbLog";
      this.txbLog.ReadOnly = true;
      this.txbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txbLog.Size = new System.Drawing.Size(468, 381);
      this.txbLog.TabIndex = 0;
      // 
      // FrmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1026, 413);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.panel1);
      this.Name = "FrmMain";
      this.Text = "Exercicio 01";
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
      this.tabControl1.ResumeLayout(false);
      this.tpData.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
      this.tpLog.ResumeLayout(false);
      this.tpLog.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnCreate;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tpData;
    private System.Windows.Forms.TabPage tpLog;
    private System.Windows.Forms.BindingSource bsMain;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.Button btnRegra;
    private System.Windows.Forms.TextBox txbLog;
    private System.Windows.Forms.DataGrid dgvMain;
  }
}

