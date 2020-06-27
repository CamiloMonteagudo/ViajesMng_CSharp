namespace UnViaje
  {
  partial class ctlPrecios
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
      this.cbMoneda = new System.Windows.Forms.ComboBox();
      this.btnClear = new System.Windows.Forms.Button();
      this.btnModify = new System.Windows.Forms.Button();
      this.txtValue = new System.Windows.Forms.TextBox();
      this.txtItem = new System.Windows.Forms.TextBox();
      this.Grid = new System.Windows.Forms.DataGridView();
      this.colIdItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colCostoCUC = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colUnidadCUC = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colMonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colGanancia = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.label10 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.lbGanancia = new System.Windows.Forms.Label();
      this.lbTotalMonto = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.lbTotalCosto = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.lbPrecRec = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.lbPrecOK = new System.Windows.Forms.Label();
      this.label14 = new System.Windows.Forms.Label();
      this.btnUpdate = new System.Windows.Forms.Button();
      this.lbItemGanc = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.chkShowComent = new System.Windows.Forms.CheckBox();
      this.txtFilter = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.btnSaveVentas = new System.Windows.Forms.Button();
      this.btnFilterOff = new System.Windows.Forms.Button();
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
            "MN "});
      this.cbMoneda.Location = new System.Drawing.Point(796, 585);
      this.cbMoneda.Name = "cbMoneda";
      this.cbMoneda.Size = new System.Drawing.Size(53, 23);
      this.cbMoneda.TabIndex = 73;
      this.cbMoneda.SelectedIndexChanged += new System.EventHandler(this.cbMoneda_SelectedIndexChanged);
      // 
      // btnClear
      // 
      this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnClear.Location = new System.Drawing.Point(0, 584);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(22, 23);
      this.btnClear.TabIndex = 76;
      this.btnClear.Text = "X";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
      // 
      // btnModify
      // 
      this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnModify.Location = new System.Drawing.Point(698, 611);
      this.btnModify.Name = "btnModify";
      this.btnModify.Size = new System.Drawing.Size(150, 23);
      this.btnModify.TabIndex = 75;
      this.btnModify.Text = "Modificar";
      this.btnModify.UseVisualStyleBackColor = true;
      this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
      // 
      // txtValue
      // 
      this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.txtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.txtValue.Location = new System.Drawing.Point(698, 585);
      this.txtValue.Name = "txtValue";
      this.txtValue.Size = new System.Drawing.Size(95, 22);
      this.txtValue.TabIndex = 72;
      this.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // txtItem
      // 
      this.txtItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtItem.Enabled = false;
      this.txtItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.txtItem.Location = new System.Drawing.Point(24, 585);
      this.txtItem.Name = "txtItem";
      this.txtItem.Size = new System.Drawing.Size(668, 22);
      this.txtItem.TabIndex = 70;
      this.txtItem.TabStop = false;
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
            this.colIdItem,
            this.colItem,
            this.colCount,
            this.colCostoCUC,
            this.colUnidadCUC,
            this.colPrecio,
            this.colMonto,
            this.colRate,
            this.colGanancia});
      this.Grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.Grid.Location = new System.Drawing.Point(0, 26);
      this.Grid.Name = "Grid";
      this.Grid.ReadOnly = true;
      this.Grid.RowHeadersVisible = false;
      this.Grid.RowTemplate.Height = 24;
      this.Grid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.Grid.Size = new System.Drawing.Size(849, 520);
      this.Grid.TabIndex = 93;
      this.Grid.TabStop = false;
      this.Grid.SelectionChanged += new System.EventHandler(this.Grid_SelectionChanged);
      // 
      // colIdItem
      // 
      this.colIdItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.colIdItem.DataPropertyName = "id";
      this.colIdItem.HeaderText = "ID";
      this.colIdItem.Name = "colIdItem";
      this.colIdItem.ReadOnly = true;
      this.colIdItem.Width = 40;
      // 
      // colItem
      // 
      this.colItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colItem.DataPropertyName = "item";
      dataGridViewCellStyle25.NullValue = null;
      this.colItem.DefaultCellStyle = dataGridViewCellStyle25;
      this.colItem.HeaderText = "Item";
      this.colItem.Name = "colItem";
      this.colItem.ReadOnly = true;
      // 
      // colCount
      // 
      this.colCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.colCount.DataPropertyName = "count";
      dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle26.Format = "N0";
      dataGridViewCellStyle26.NullValue = null;
      this.colCount.DefaultCellStyle = dataGridViewCellStyle26;
      this.colCount.HeaderText = "Cant.";
      this.colCount.Name = "colCount";
      this.colCount.ReadOnly = true;
      this.colCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.colCount.Width = 40;
      // 
      // colCostoCUC
      // 
      this.colCostoCUC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.colCostoCUC.DataPropertyName = "valCUC";
      dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle27.Format = "N2";
      dataGridViewCellStyle27.NullValue = null;
      this.colCostoCUC.DefaultCellStyle = dataGridViewCellStyle27;
      this.colCostoCUC.HeaderText = "Costo (CUC)";
      this.colCostoCUC.Name = "colCostoCUC";
      this.colCostoCUC.ReadOnly = true;
      this.colCostoCUC.Width = 60;
      // 
      // colUnidadCUC
      // 
      this.colUnidadCUC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.colUnidadCUC.DataPropertyName = "valCucItem";
      dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle28.Format = "N2";
      dataGridViewCellStyle28.NullValue = null;
      this.colUnidadCUC.DefaultCellStyle = dataGridViewCellStyle28;
      this.colUnidadCUC.HeaderText = "Unidad  (CUC)";
      this.colUnidadCUC.Name = "colUnidadCUC";
      this.colUnidadCUC.ReadOnly = true;
      this.colUnidadCUC.Width = 60;
      // 
      // colPrecio
      // 
      this.colPrecio.DataPropertyName = "precio";
      dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle29.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle29.Format = "N2";
      dataGridViewCellStyle29.NullValue = null;
      this.colPrecio.DefaultCellStyle = dataGridViewCellStyle29;
      this.colPrecio.HeaderText = "Precio";
      this.colPrecio.Name = "colPrecio";
      this.colPrecio.ReadOnly = true;
      this.colPrecio.Width = 85;
      // 
      // colMonto
      // 
      this.colMonto.DataPropertyName = "monto";
      dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle30.Format = "0.##";
      dataGridViewCellStyle30.NullValue = null;
      this.colMonto.DefaultCellStyle = dataGridViewCellStyle30;
      this.colMonto.HeaderText = "Monto (CUC)";
      this.colMonto.Name = "colMonto";
      this.colMonto.ReadOnly = true;
      this.colMonto.Width = 60;
      // 
      // colRate
      // 
      this.colRate.DataPropertyName = "rate";
      dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle31.Format = "N2";
      dataGridViewCellStyle31.NullValue = null;
      this.colRate.DefaultCellStyle = dataGridViewCellStyle31;
      this.colRate.HeaderText = "Rate";
      this.colRate.Name = "colRate";
      this.colRate.ReadOnly = true;
      this.colRate.Width = 40;
      // 
      // colGanancia
      // 
      this.colGanancia.DataPropertyName = "ganancia";
      dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle32.Format = "N2";
      dataGridViewCellStyle32.NullValue = null;
      this.colGanancia.DefaultCellStyle = dataGridViewCellStyle32;
      this.colGanancia.HeaderText = "Ganancia (CUC)";
      this.colGanancia.Name = "colGanancia";
      this.colGanancia.ReadOnly = true;
      this.colGanancia.Width = 70;
      // 
      // label10
      // 
      this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(794, 569);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(53, 15);
      this.label10.TabIndex = 91;
      this.label10.Text = "Moneda";
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(698, 569);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(42, 15);
      this.label4.TabIndex = 92;
      this.label4.Text = "Precio";
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(345, 546);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(115, 17);
      this.label6.TabIndex = 86;
      this.label6.Text = "Costo Total:";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lbGanancia
      // 
      this.lbGanancia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.lbGanancia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbGanancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.lbGanancia.Location = new System.Drawing.Point(760, 545);
      this.lbGanancia.Name = "lbGanancia";
      this.lbGanancia.Size = new System.Drawing.Size(71, 18);
      this.lbGanancia.TabIndex = 84;
      this.lbGanancia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lbTotalMonto
      // 
      this.lbTotalMonto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.lbTotalMonto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbTotalMonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.lbTotalMonto.Location = new System.Drawing.Point(660, 545);
      this.lbTotalMonto.Name = "lbTotalMonto";
      this.lbTotalMonto.Size = new System.Drawing.Size(61, 18);
      this.lbTotalMonto.TabIndex = 83;
      this.lbTotalMonto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(722, 546);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(41, 17);
      this.label7.TabIndex = 79;
      this.label7.Text = "Gan:";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lbTotalCosto
      // 
      this.lbTotalCosto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.lbTotalCosto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbTotalCosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.lbTotalCosto.Location = new System.Drawing.Point(455, 545);
      this.lbTotalCosto.Name = "lbTotalCosto";
      this.lbTotalCosto.Size = new System.Drawing.Size(61, 18);
      this.lbTotalCosto.TabIndex = 81;
      this.lbTotalCosto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label5
      // 
      this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(557, 546);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(106, 17);
      this.label5.TabIndex = 78;
      this.label5.Text = "Monto Total:";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(25, 569);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(31, 15);
      this.label2.TabIndex = 88;
      this.label2.Text = "Item";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(0, 6);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(122, 15);
      this.label1.TabIndex = 89;
      this.label1.Text = "Listado de Productos";
      // 
      // lbPrecRec
      // 
      this.lbPrecRec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lbPrecRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.lbPrecRec.Location = new System.Drawing.Point(168, 610);
      this.lbPrecRec.Name = "lbPrecRec";
      this.lbPrecRec.Size = new System.Drawing.Size(64, 17);
      this.lbPrecRec.TabIndex = 81;
      this.lbPrecRec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label12
      // 
      this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.label12.Location = new System.Drawing.Point(-4, 610);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(175, 17);
      this.label12.TabIndex = 86;
      this.label12.Text = "Precio de recuperación:";
      this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lbPrecOK
      // 
      this.lbPrecOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lbPrecOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.lbPrecOK.Location = new System.Drawing.Point(394, 611);
      this.lbPrecOK.Name = "lbPrecOK";
      this.lbPrecOK.Size = new System.Drawing.Size(67, 17);
      this.lbPrecOK.TabIndex = 81;
      this.lbPrecOK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label14
      // 
      this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.label14.Location = new System.Drawing.Point(250, 610);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(147, 17);
      this.label14.TabIndex = 86;
      this.label14.Text = "Precio con ganacia:";
      this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // btnUpdate
      // 
      this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnUpdate.Location = new System.Drawing.Point(743, 0);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size(106, 23);
      this.btnUpdate.TabIndex = 75;
      this.btnUpdate.Text = "Actualizar";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
      // 
      // lbItemGanc
      // 
      this.lbItemGanc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lbItemGanc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.lbItemGanc.Location = new System.Drawing.Point(611, 610);
      this.lbItemGanc.Name = "lbItemGanc";
      this.lbItemGanc.Size = new System.Drawing.Size(67, 17);
      this.lbItemGanc.TabIndex = 81;
      this.lbItemGanc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label8
      // 
      this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(476, 609);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(138, 17);
      this.label8.TabIndex = 86;
      this.label8.Text = "Ganancia por Item:";
      this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // chkShowComent
      // 
      this.chkShowComent.AutoSize = true;
      this.chkShowComent.Location = new System.Drawing.Point(386, 4);
      this.chkShowComent.Name = "chkShowComent";
      this.chkShowComent.Size = new System.Drawing.Size(132, 19);
      this.chkShowComent.TabIndex = 141;
      this.chkShowComent.Text = "Mostrar cometarios";
      this.chkShowComent.UseVisualStyleBackColor = true;
      this.chkShowComent.CheckedChanged += new System.EventHandler(this.chkShowComent_CheckedChanged);
      // 
      // txtFilter
      // 
      this.txtFilter.Location = new System.Drawing.Point(204, 3);
      this.txtFilter.Name = "txtFilter";
      this.txtFilter.Size = new System.Drawing.Size(142, 21);
      this.txtFilter.TabIndex = 140;
      this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(159, 5);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(41, 15);
      this.label3.TabIndex = 139;
      this.label3.Text = "Filtrar:";
      // 
      // btnSaveVentas
      // 
      this.btnSaveVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSaveVentas.FlatAppearance.BorderSize = 0;
      this.btnSaveVentas.Image = global::UnViaje.Properties.Resources.Save;
      this.btnSaveVentas.Location = new System.Drawing.Point(0, 546);
      this.btnSaveVentas.Name = "btnSaveVentas";
      this.btnSaveVentas.Size = new System.Drawing.Size(23, 23);
      this.btnSaveVentas.TabIndex = 125;
      this.btnSaveVentas.UseVisualStyleBackColor = true;
      this.btnSaveVentas.Click += new System.EventHandler(this.btnSaveVentas_Click);
      // 
      // btnFilterOff
      // 
      this.btnFilterOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnFilterOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.btnFilterOff.FlatAppearance.BorderSize = 0;
      this.btnFilterOff.Image = global::UnViaje.Properties.Resources.FiterOff;
      this.btnFilterOff.Location = new System.Drawing.Point(27, 546);
      this.btnFilterOff.Name = "btnFilterOff";
      this.btnFilterOff.Size = new System.Drawing.Size(23, 23);
      this.btnFilterOff.TabIndex = 142;
      this.btnFilterOff.UseVisualStyleBackColor = true;
      this.btnFilterOff.Visible = false;
      this.btnFilterOff.Click += new System.EventHandler(this.BtnFilterOff_Click);
      // 
      // ctlPrecios
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.Grid);
      this.Controls.Add(this.chkShowComent);
      this.Controls.Add(this.txtFilter);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnSaveVentas);
      this.Controls.Add(this.cbMoneda);
      this.Controls.Add(this.btnClear);
      this.Controls.Add(this.btnUpdate);
      this.Controls.Add(this.btnModify);
      this.Controls.Add(this.txtValue);
      this.Controls.Add(this.txtItem);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.label14);
      this.Controls.Add(this.label12);
      this.Controls.Add(this.lbItemGanc);
      this.Controls.Add(this.lbPrecOK);
      this.Controls.Add(this.lbPrecRec);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lbTotalCosto);
      this.Controls.Add(this.lbGanancia);
      this.Controls.Add(this.lbTotalMonto);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.btnFilterOff);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.Name = "ctlPrecios";
      this.Size = new System.Drawing.Size(852, 640);
      this.Load += new System.EventHandler(this.ctlPrecios_Load);
      this.VisibleChanged += new System.EventHandler(this.ctlPrecios_VisibleChanged);
      ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

      }

    #endregion
    private System.Windows.Forms.ComboBox cbMoneda;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Button btnModify;
    private System.Windows.Forms.TextBox txtValue;
    private System.Windows.Forms.TextBox txtItem;
    private System.Windows.Forms.DataGridView Grid;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label lbGanancia;
    private System.Windows.Forms.Label lbTotalMonto;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label lbTotalCosto;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lbPrecRec;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label lbPrecOK;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.Label lbItemGanc;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.DataGridViewTextBoxColumn colIdItem;
    private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
    private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
    private System.Windows.Forms.DataGridViewTextBoxColumn colCostoCUC;
    private System.Windows.Forms.DataGridViewTextBoxColumn colUnidadCUC;
    private System.Windows.Forms.DataGridViewTextBoxColumn colPrecio;
    private System.Windows.Forms.DataGridViewTextBoxColumn colMonto;
    private System.Windows.Forms.DataGridViewTextBoxColumn colRate;
    private System.Windows.Forms.DataGridViewTextBoxColumn colGanancia;
    private System.Windows.Forms.Button btnSaveVentas;
    private System.Windows.Forms.CheckBox chkShowComent;
    private System.Windows.Forms.TextBox txtFilter;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnFilterOff;
    }
  }
