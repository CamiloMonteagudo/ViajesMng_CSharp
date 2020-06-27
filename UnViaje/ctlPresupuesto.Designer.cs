namespace UnViaje
  {
  partial class ctlPresupuesto
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      this.cbMoneda = new System.Windows.Forms.ComboBox();
      this.btnClear = new System.Windows.Forms.Button();
      this.label10 = new System.Windows.Forms.Label();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnModify = new System.Windows.Forms.Button();
      this.btnAdd = new System.Windows.Forms.Button();
      this.txtValue = new System.Windows.Forms.TextBox();
      this.txtChange = new System.Windows.Forms.TextBox();
      this.txtSrc = new System.Windows.Forms.TextBox();
      this.Grid = new System.Windows.Forms.DataGridView();
      this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colSrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colCUC = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colUSD = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.lbSumaCUC = new System.Windows.Forms.Label();
      this.lbTotalUSD = new System.Windows.Forms.Label();
      this.lbSumaUSD = new System.Windows.Forms.Label();
      this.lbTotalCUC = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
      this.SuspendLayout();
      // 
      // cbMoneda
      // 
      this.cbMoneda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.cbMoneda.FormattingEnabled = true;
      this.cbMoneda.Items.AddRange(new object[] {
            "CUC",
            "USD",
            "MN ",
            "DOP"});
      this.cbMoneda.Location = new System.Drawing.Point(635, 485);
      this.cbMoneda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.cbMoneda.Name = "cbMoneda";
      this.cbMoneda.Size = new System.Drawing.Size(53, 21);
      this.cbMoneda.TabIndex = 59;
      // 
      // btnClear
      // 
      this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnClear.Location = new System.Drawing.Point(3, 484);
      this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(23, 23);
      this.btnClear.TabIndex = 60;
      this.btnClear.Text = "X";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
      // 
      // label10
      // 
      this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(635, 469);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(58, 16);
      this.label10.TabIndex = 61;
      this.label10.Text = "Moneda";
      // 
      // btnDelete
      // 
      this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDelete.Location = new System.Drawing.Point(443, 512);
      this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(80, 28);
      this.btnDelete.TabIndex = 55;
      this.btnDelete.Text = "Borrar";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Visible = false;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // btnModify
      // 
      this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnModify.Location = new System.Drawing.Point(529, 512);
      this.btnModify.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.btnModify.Name = "btnModify";
      this.btnModify.Size = new System.Drawing.Size(80, 28);
      this.btnModify.TabIndex = 56;
      this.btnModify.Text = "Modificar";
      this.btnModify.UseVisualStyleBackColor = true;
      this.btnModify.Visible = false;
      this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
      // 
      // btnAdd
      // 
      this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAdd.Location = new System.Drawing.Point(609, 512);
      this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(81, 28);
      this.btnAdd.TabIndex = 57;
      this.btnAdd.Text = "Adiccionar";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // txtValue
      // 
      this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.txtValue.Location = new System.Drawing.Point(523, 485);
      this.txtValue.Name = "txtValue";
      this.txtValue.Size = new System.Drawing.Size(109, 21);
      this.txtValue.TabIndex = 54;
      // 
      // txtChange
      // 
      this.txtChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.txtChange.Location = new System.Drawing.Point(467, 485);
      this.txtChange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.txtChange.Name = "txtChange";
      this.txtChange.Size = new System.Drawing.Size(55, 21);
      this.txtChange.TabIndex = 53;
      // 
      // txtSrc
      // 
      this.txtSrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSrc.Location = new System.Drawing.Point(26, 485);
      this.txtSrc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.txtSrc.Name = "txtSrc";
      this.txtSrc.Size = new System.Drawing.Size(440, 21);
      this.txtSrc.TabIndex = 45;
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
            this.colChange,
            this.colUSD});
      this.Grid.Location = new System.Drawing.Point(3, 20);
      this.Grid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.Grid.Name = "Grid";
      this.Grid.ReadOnly = true;
      this.Grid.RowHeadersVisible = false;
      this.Grid.RowTemplate.Height = 24;
      this.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.Grid.Size = new System.Drawing.Size(685, 411);
      this.Grid.TabIndex = 58;
      this.Grid.TabStop = false;
      this.Grid.SelectionChanged += new System.EventHandler(this.Grid_SelectionChanged);
      this.Grid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Grid_KeyUp);
      // 
      // colId
      // 
      this.colId.DataPropertyName = "id";
      this.colId.HeaderText = "Id";
      this.colId.Name = "colId";
      this.colId.ReadOnly = true;
      this.colId.Width = 30;
      // 
      // colSrc
      // 
      this.colSrc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colSrc.DataPropertyName = "src";
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
      // colChange
      // 
      this.colChange.DataPropertyName = "cambio";
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle3.Format = "N3";
      dataGridViewCellStyle3.NullValue = null;
      this.colChange.DefaultCellStyle = dataGridViewCellStyle3;
      this.colChange.HeaderText = "Cambio";
      this.colChange.Name = "colChange";
      this.colChange.ReadOnly = true;
      this.colChange.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.colChange.Width = 60;
      // 
      // colUSD
      // 
      this.colUSD.DataPropertyName = "usd";
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle4.Format = "N2";
      dataGridViewCellStyle4.NullValue = null;
      this.colUSD.DefaultCellStyle = dataGridViewCellStyle4;
      this.colUSD.HeaderText = "USD";
      this.colUSD.Name = "colUSD";
      this.colUSD.ReadOnly = true;
      this.colUSD.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(523, 469);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(40, 16);
      this.label4.TabIndex = 47;
      this.label4.Text = "Valor";
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(467, 469);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(55, 16);
      this.label3.TabIndex = 48;
      this.label3.Text = "Cambio";
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(3, 441);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(87, 17);
      this.label6.TabIndex = 49;
      this.label6.Text = "Total CUC:";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lbSumaCUC
      // 
      this.lbSumaCUC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.lbSumaCUC.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbSumaCUC.Location = new System.Drawing.Point(404, 441);
      this.lbSumaCUC.Name = "lbSumaCUC";
      this.lbSumaCUC.Size = new System.Drawing.Size(87, 17);
      this.lbSumaCUC.TabIndex = 50;
      this.lbSumaCUC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbTotalUSD
      // 
      this.lbTotalUSD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lbTotalUSD.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbTotalUSD.Location = new System.Drawing.Point(255, 441);
      this.lbTotalUSD.Name = "lbTotalUSD";
      this.lbTotalUSD.Size = new System.Drawing.Size(87, 17);
      this.lbTotalUSD.TabIndex = 51;
      this.lbTotalUSD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbSumaUSD
      // 
      this.lbSumaUSD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.lbSumaUSD.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbSumaUSD.Location = new System.Drawing.Point(594, 441);
      this.lbSumaUSD.Name = "lbSumaUSD";
      this.lbSumaUSD.Size = new System.Drawing.Size(87, 17);
      this.lbSumaUSD.TabIndex = 52;
      this.lbSumaUSD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lbTotalCUC
      // 
      this.lbTotalCUC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lbTotalCUC.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbTotalCUC.Location = new System.Drawing.Point(89, 441);
      this.lbTotalCUC.Name = "lbTotalCUC";
      this.lbTotalCUC.Size = new System.Drawing.Size(78, 17);
      this.lbTotalCUC.TabIndex = 44;
      this.lbTotalCUC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label5
      // 
      this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(348, 441);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(53, 17);
      this.label5.TabIndex = 43;
      this.label5.Text = "Suma:";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(26, 469);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(48, 16);
      this.label2.TabIndex = 46;
      this.label2.Text = "Origen";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 4);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(62, 16);
      this.label1.TabIndex = 42;
      this.label1.Text = "Entradas";
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(171, 441);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(87, 17);
      this.label7.TabIndex = 49;
      this.label7.Text = "Total USD:";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // ctlPresupuesto
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Controls.Add(this.lbTotalUSD);
      this.Controls.Add(this.cbMoneda);
      this.Controls.Add(this.btnClear);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.btnDelete);
      this.Controls.Add(this.btnModify);
      this.Controls.Add(this.btnAdd);
      this.Controls.Add(this.txtValue);
      this.Controls.Add(this.txtChange);
      this.Controls.Add(this.txtSrc);
      this.Controls.Add(this.Grid);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.lbSumaCUC);
      this.Controls.Add(this.lbSumaUSD);
      this.Controls.Add(this.lbTotalCUC);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.Name = "ctlPresupuesto";
      this.Size = new System.Drawing.Size(691, 544);
      this.Load += new System.EventHandler(this.ctlPresupuesto_Load);
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
    private System.Windows.Forms.TextBox txtChange;
    private System.Windows.Forms.TextBox txtSrc;
    private System.Windows.Forms.DataGridView Grid;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label lbSumaCUC;
    private System.Windows.Forms.Label lbTotalUSD;
    private System.Windows.Forms.Label lbSumaUSD;
    private System.Windows.Forms.Label lbTotalCUC;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridViewTextBoxColumn colId;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSrc;
    private System.Windows.Forms.DataGridViewTextBoxColumn colCUC;
    private System.Windows.Forms.DataGridViewTextBoxColumn colChange;
    private System.Windows.Forms.DataGridViewTextBoxColumn colUSD;
    private System.Windows.Forms.Label label7;
    }
  }
