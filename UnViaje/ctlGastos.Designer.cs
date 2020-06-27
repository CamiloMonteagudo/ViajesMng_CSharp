namespace UnViaje
  {
  partial class ctlGastos
    {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
      {
      if( disposing && (components != null) )
        {
        components.Dispose();
        }
      base.Dispose( disposing );
      }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
      {
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      this.cbMoneda = new System.Windows.Forms.ComboBox();
      this.btnClear = new System.Windows.Forms.Button();
      this.label10 = new System.Windows.Forms.Label();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnModify = new System.Windows.Forms.Button();
      this.btnAdd = new System.Windows.Forms.Button();
      this.txtValue = new System.Windows.Forms.TextBox();
      this.txtSrc = new System.Windows.Forms.TextBox();
      this.Grid = new System.Windows.Forms.DataGridView();
      this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colCUC = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.label4 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
      this.SuspendLayout();
      // 
      // cbMoneda
      // 
      this.cbMoneda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbMoneda.FormattingEnabled = true;
      this.cbMoneda.Items.AddRange(new object[] {
            "CUC",
            "USD",
            "MN ",
            "DOP"});
      this.cbMoneda.Location = new System.Drawing.Point(742, 566);
      this.cbMoneda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.cbMoneda.Name = "cbMoneda";
      this.cbMoneda.Size = new System.Drawing.Size(53, 23);
      this.cbMoneda.TabIndex = 61;
      // 
      // btnClear
      // 
      this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnClear.Location = new System.Drawing.Point(4, 566);
      this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(25, 25);
      this.btnClear.TabIndex = 62;
      this.btnClear.Text = "X";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
      // 
      // label10
      // 
      this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(739, 550);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(58, 16);
      this.label10.TabIndex = 63;
      this.label10.Text = "Moneda";
      // 
      // btnDelete
      // 
      this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDelete.Location = new System.Drawing.Point(549, 595);
      this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(80, 27);
      this.btnDelete.TabIndex = 57;
      this.btnDelete.Text = "Borrar";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Visible = false;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // btnModify
      // 
      this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnModify.Location = new System.Drawing.Point(635, 595);
      this.btnModify.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.btnModify.Name = "btnModify";
      this.btnModify.Size = new System.Drawing.Size(80, 27);
      this.btnModify.TabIndex = 58;
      this.btnModify.Text = "Modificar";
      this.btnModify.UseVisualStyleBackColor = true;
      this.btnModify.Visible = false;
      this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
      // 
      // btnAdd
      // 
      this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAdd.Location = new System.Drawing.Point(714, 595);
      this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(81, 28);
      this.btnAdd.TabIndex = 59;
      this.btnAdd.Text = "Adiccionar";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // txtValue
      // 
      this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.txtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.txtValue.Location = new System.Drawing.Point(611, 567);
      this.txtValue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.txtValue.Name = "txtValue";
      this.txtValue.Size = new System.Drawing.Size(127, 23);
      this.txtValue.TabIndex = 56;
      // 
      // txtSrc
      // 
      this.txtSrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.txtSrc.Location = new System.Drawing.Point(30, 567);
      this.txtSrc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.txtSrc.Name = "txtSrc";
      this.txtSrc.Size = new System.Drawing.Size(577, 23);
      this.txtSrc.TabIndex = 54;
      // 
      // Grid
      // 
      this.Grid.AllowUserToAddRows = false;
      this.Grid.AllowUserToDeleteRows = false;
      this.Grid.AllowUserToResizeRows = false;
      this.Grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colSrc,
            this.colCUC,
            this.colValue});
      this.Grid.Location = new System.Drawing.Point(3, 20);
      this.Grid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.Grid.Name = "Grid";
      this.Grid.ReadOnly = true;
      this.Grid.RowHeadersVisible = false;
      this.Grid.RowTemplate.Height = 24;
      this.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.Grid.Size = new System.Drawing.Size(791, 517);
      this.Grid.TabIndex = 60;
      this.Grid.TabStop = false;
      this.Grid.SelectionChanged += new System.EventHandler(this.Grid_SelectionChanged);
      this.Grid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Grid_KeyUp);
      // 
      // colId
      // 
      this.colId.DataPropertyName = "id";
      this.colId.HeaderText = "ID";
      this.colId.Name = "colId";
      this.colId.ReadOnly = true;
      this.colId.Width = 30;
      // 
      // colSrc
      // 
      this.colSrc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colSrc.DataPropertyName = "descric";
      dataGridViewCellStyle1.NullValue = null;
      this.colSrc.DefaultCellStyle = dataGridViewCellStyle1;
      this.colSrc.HeaderText = "Descricción";
      this.colSrc.Name = "colSrc";
      this.colSrc.ReadOnly = true;
      // 
      // colCUC
      // 
      this.colCUC.DataPropertyName = "cuc";
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle2.Format = "N2";
      dataGridViewCellStyle2.NullValue = null;
      this.colCUC.DefaultCellStyle = dataGridViewCellStyle2;
      this.colCUC.HeaderText = "CUC";
      this.colCUC.Name = "colCUC";
      this.colCUC.ReadOnly = true;
      this.colCUC.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      // 
      // colValue
      // 
      this.colValue.DataPropertyName = "value";
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle3.Format = "N2";
      dataGridViewCellStyle3.NullValue = null;
      this.colValue.DefaultCellStyle = dataGridViewCellStyle3;
      this.colValue.HeaderText = "Valor";
      this.colValue.Name = "colValue";
      this.colValue.ReadOnly = true;
      this.colValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(609, 550);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(40, 16);
      this.label4.TabIndex = 55;
      this.label4.Text = "Valor";
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(30, 551);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(48, 16);
      this.label2.TabIndex = 52;
      this.label2.Text = "Origen";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 4);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(338, 16);
      this.label1.TabIndex = 53;
      this.label1.Text = "Listado de Gastos (No incluye la compra e mercancias)";
      // 
      // ctlGastos
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.cbMoneda);
      this.Controls.Add(this.btnClear);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.btnDelete);
      this.Controls.Add(this.btnModify);
      this.Controls.Add(this.btnAdd);
      this.Controls.Add(this.txtValue);
      this.Controls.Add(this.txtSrc);
      this.Controls.Add(this.Grid);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.Name = "ctlGastos";
      this.Size = new System.Drawing.Size(797, 625);
      this.Load += new System.EventHandler(this.ctlGastos_Load);
      ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

      }

    #endregion
    private System.Windows.Forms.ComboBox cbMoneda;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnModify;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.TextBox txtValue;
    private System.Windows.Forms.TextBox txtSrc;
    private System.Windows.Forms.DataGridView Grid;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridViewTextBoxColumn colId;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSrc;
    private System.Windows.Forms.DataGridViewTextBoxColumn colCUC;
    private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
    }
  }
