namespace UnViaje
  {
  partial class ctlCompras
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
      this.cbMoneda = new System.Windows.Forms.ComboBox();
      this.btnClear = new System.Windows.Forms.Button();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnModify = new System.Windows.Forms.Button();
      this.btnAdd = new System.Windows.Forms.Button();
      this.txtCount = new System.Windows.Forms.TextBox();
      this.txtValue = new System.Windows.Forms.TextBox();
      this.txtItem = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.Grid = new System.Windows.Forms.DataGridView();
      this.IdItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.UnidadCosto = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.CostoCUC = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.UnidadCUC = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.label10 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.txtComent = new System.Windows.Forms.TextBox();
      this.label12 = new System.Windows.Forms.Label();
      this.lbSumaCostos = new System.Windows.Forms.Label();
      this.btnSaveVentas = new System.Windows.Forms.Button();
      this.txtFilter = new System.Windows.Forms.TextBox();
      this.label13 = new System.Windows.Forms.Label();
      this.chkShowComent = new System.Windows.Forms.CheckBox();
      this.lstNames = new System.Windows.Forms.ListBox();
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
            "MN ",
            "DOP"});
      this.cbMoneda.Location = new System.Drawing.Point(766, 590);
      this.cbMoneda.Name = "cbMoneda";
      this.cbMoneda.Size = new System.Drawing.Size(53, 23);
      this.cbMoneda.TabIndex = 47;
      // 
      // btnClear
      // 
      this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnClear.Location = new System.Drawing.Point(0, 589);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(22, 23);
      this.btnClear.TabIndex = 50;
      this.btnClear.Text = "X";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
      // 
      // btnDelete
      // 
      this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.btnDelete.ForeColor = System.Drawing.Color.Red;
      this.btnDelete.Location = new System.Drawing.Point(821, 589);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(25, 25);
      this.btnDelete.TabIndex = 51;
      this.btnDelete.Text = "X";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Visible = false;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // btnModify
      // 
      this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnModify.Location = new System.Drawing.Point(614, 616);
      this.btnModify.Name = "btnModify";
      this.btnModify.Size = new System.Drawing.Size(83, 25);
      this.btnModify.TabIndex = 49;
      this.btnModify.Text = "Modificar";
      this.btnModify.UseVisualStyleBackColor = true;
      this.btnModify.Visible = false;
      this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
      // 
      // btnAdd
      // 
      this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAdd.Location = new System.Drawing.Point(731, 616);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(87, 25);
      this.btnAdd.TabIndex = 48;
      this.btnAdd.Text = "Adiccionar";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // txtCount
      // 
      this.txtCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.txtCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.txtCount.Location = new System.Drawing.Point(608, 590);
      this.txtCount.Name = "txtCount";
      this.txtCount.Size = new System.Drawing.Size(57, 22);
      this.txtCount.TabIndex = 45;
      // 
      // txtValue
      // 
      this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.txtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.txtValue.Location = new System.Drawing.Point(668, 590);
      this.txtValue.Name = "txtValue";
      this.txtValue.Size = new System.Drawing.Size(95, 22);
      this.txtValue.TabIndex = 46;
      // 
      // txtItem
      // 
      this.txtItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.txtItem.Location = new System.Drawing.Point(24, 590);
      this.txtItem.Name = "txtItem";
      this.txtItem.Size = new System.Drawing.Size(576, 22);
      this.txtItem.TabIndex = 44;
      this.txtItem.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
      this.txtItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
      this.txtItem.Leave += new System.EventHandler(this.txtItem_Leave);
      this.txtItem.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtItem_PreviewKeyDown);
      // 
      // label8
      // 
      this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(607, 574);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(56, 15);
      this.label8.TabIndex = 64;
      this.label8.Text = "Cantidad";
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
            this.IdItem,
            this.colItem,
            this.colCount,
            this.colValue,
            this.UnidadCosto,
            this.CostoCUC,
            this.UnidadCUC,
            this.precio,
            this.monto,
            this.moneda,
            this.rate});
      this.Grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
      this.Grid.Location = new System.Drawing.Point(0, 26);
      this.Grid.Name = "Grid";
      this.Grid.ReadOnly = true;
      this.Grid.RowHeadersVisible = false;
      this.Grid.RowTemplate.Height = 24;
      this.Grid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.Grid.Size = new System.Drawing.Size(849, 523);
      this.Grid.TabIndex = 67;
      this.Grid.TabStop = false;
      this.Grid.SelectionChanged += new System.EventHandler(this.Grid_SelectionChanged);
      this.Grid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Grid_KeyUp);
      // 
      // IdItem
      // 
      this.IdItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.IdItem.DataPropertyName = "id";
      this.IdItem.HeaderText = "ID";
      this.IdItem.Name = "IdItem";
      this.IdItem.ReadOnly = true;
      this.IdItem.Width = 40;
      // 
      // colItem
      // 
      this.colItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colItem.DataPropertyName = "item";
      dataGridViewCellStyle19.NullValue = null;
      this.colItem.DefaultCellStyle = dataGridViewCellStyle19;
      this.colItem.HeaderText = "Item";
      this.colItem.Name = "colItem";
      this.colItem.ReadOnly = true;
      // 
      // colCount
      // 
      this.colCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.colCount.DataPropertyName = "count";
      dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle20.Format = "N0";
      dataGridViewCellStyle20.NullValue = null;
      this.colCount.DefaultCellStyle = dataGridViewCellStyle20;
      this.colCount.HeaderText = "Cant.";
      this.colCount.Name = "colCount";
      this.colCount.ReadOnly = true;
      this.colCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.colCount.Width = 40;
      // 
      // colValue
      // 
      this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.colValue.DataPropertyName = "value";
      dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle21.Format = "N2";
      dataGridViewCellStyle21.NullValue = null;
      this.colValue.DefaultCellStyle = dataGridViewCellStyle21;
      this.colValue.HeaderText = "Costo Total";
      this.colValue.Name = "colValue";
      this.colValue.ReadOnly = true;
      this.colValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.colValue.Width = 90;
      // 
      // UnidadCosto
      // 
      this.UnidadCosto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.UnidadCosto.DataPropertyName = "valItem";
      dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle22.NullValue = null;
      this.UnidadCosto.DefaultCellStyle = dataGridViewCellStyle22;
      this.UnidadCosto.HeaderText = "Costo Unidad";
      this.UnidadCosto.Name = "UnidadCosto";
      this.UnidadCosto.ReadOnly = true;
      this.UnidadCosto.Width = 90;
      // 
      // CostoCUC
      // 
      this.CostoCUC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.CostoCUC.DataPropertyName = "valCUC";
      dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle23.Format = "N2";
      dataGridViewCellStyle23.NullValue = null;
      this.CostoCUC.DefaultCellStyle = dataGridViewCellStyle23;
      this.CostoCUC.HeaderText = "CUC Costo ";
      this.CostoCUC.Name = "CostoCUC";
      this.CostoCUC.ReadOnly = true;
      this.CostoCUC.Width = 60;
      // 
      // UnidadCUC
      // 
      this.UnidadCUC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.UnidadCUC.DataPropertyName = "valCucItem";
      dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle24.Format = "N2";
      dataGridViewCellStyle24.NullValue = null;
      this.UnidadCUC.DefaultCellStyle = dataGridViewCellStyle24;
      this.UnidadCUC.HeaderText = "CUC Unidad";
      this.UnidadCUC.Name = "UnidadCUC";
      this.UnidadCUC.ReadOnly = true;
      this.UnidadCUC.Width = 60;
      // 
      // precio
      // 
      this.precio.DataPropertyName = "precio";
      this.precio.HeaderText = "precio";
      this.precio.Name = "precio";
      this.precio.ReadOnly = true;
      this.precio.Visible = false;
      // 
      // monto
      // 
      this.monto.DataPropertyName = "monto";
      this.monto.HeaderText = "monto";
      this.monto.Name = "monto";
      this.monto.ReadOnly = true;
      this.monto.Visible = false;
      // 
      // moneda
      // 
      this.moneda.DataPropertyName = "moneda";
      this.moneda.HeaderText = "moneda";
      this.moneda.Name = "moneda";
      this.moneda.ReadOnly = true;
      this.moneda.Visible = false;
      // 
      // rate
      // 
      this.rate.DataPropertyName = "rate";
      this.rate.HeaderText = "rate";
      this.rate.Name = "rate";
      this.rate.ReadOnly = true;
      this.rate.Visible = false;
      // 
      // label10
      // 
      this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(764, 574);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(53, 15);
      this.label10.TabIndex = 65;
      this.label10.Text = "Moneda";
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(668, 574);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(38, 15);
      this.label4.TabIndex = 66;
      this.label4.Text = "Costo";
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(25, 574);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(31, 15);
      this.label2.TabIndex = 62;
      this.label2.Text = "Item";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(0, 6);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(113, 15);
      this.label1.TabIndex = 63;
      this.label1.Text = "Listado de Compas";
      // 
      // txtComent
      // 
      this.txtComent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtComent.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.txtComent.Location = new System.Drawing.Point(24, 616);
      this.txtComent.Name = "txtComent";
      this.txtComent.Size = new System.Drawing.Size(576, 22);
      this.txtComent.TabIndex = 44;
      // 
      // label12
      // 
      this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.label12.Location = new System.Drawing.Point(551, 550);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(162, 17);
      this.label12.TabIndex = 53;
      this.label12.Text = "Costo Total:";
      this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lbSumaCostos
      // 
      this.lbSumaCostos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.lbSumaCostos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbSumaCostos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.lbSumaCostos.Location = new System.Drawing.Point(710, 548);
      this.lbSumaCostos.Name = "lbSumaCostos";
      this.lbSumaCostos.Size = new System.Drawing.Size(113, 18);
      this.lbSumaCostos.TabIndex = 58;
      this.lbSumaCostos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btnSaveVentas
      // 
      this.btnSaveVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSaveVentas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.btnSaveVentas.FlatAppearance.BorderSize = 0;
      this.btnSaveVentas.Image = global::UnViaje.Properties.Resources.Save;
      this.btnSaveVentas.Location = new System.Drawing.Point(0, 549);
      this.btnSaveVentas.Name = "btnSaveVentas";
      this.btnSaveVentas.Size = new System.Drawing.Size(23, 23);
      this.btnSaveVentas.TabIndex = 126;
      this.btnSaveVentas.UseVisualStyleBackColor = true;
      this.btnSaveVentas.Click += new System.EventHandler(this.btnSaveVentas_Click);
      // 
      // txtFilter
      // 
      this.txtFilter.Location = new System.Drawing.Point(234, 3);
      this.txtFilter.Name = "txtFilter";
      this.txtFilter.Size = new System.Drawing.Size(142, 21);
      this.txtFilter.TabIndex = 137;
      this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(189, 5);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(41, 15);
      this.label13.TabIndex = 63;
      this.label13.Text = "Filtrar:";
      // 
      // chkShowComent
      // 
      this.chkShowComent.AutoSize = true;
      this.chkShowComent.Location = new System.Drawing.Point(416, 4);
      this.chkShowComent.Name = "chkShowComent";
      this.chkShowComent.Size = new System.Drawing.Size(132, 19);
      this.chkShowComent.TabIndex = 138;
      this.chkShowComent.Text = "Mostrar cometarios";
      this.chkShowComent.UseVisualStyleBackColor = true;
      this.chkShowComent.CheckedChanged += new System.EventHandler(this.chkShowComent_CheckedChanged);
      // 
      // lstNames
      // 
      this.lstNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lstNames.FormattingEnabled = true;
      this.lstNames.ItemHeight = 15;
      this.lstNames.Location = new System.Drawing.Point(24, 450);
      this.lstNames.Name = "lstNames";
      this.lstNames.Size = new System.Drawing.Size(575, 139);
      this.lstNames.TabIndex = 139;
      this.lstNames.Visible = false;
      this.lstNames.SelectedIndexChanged += new System.EventHandler(this.lstNames_SelectedIndexChanged);
      // 
      // btnFilterOff
      // 
      this.btnFilterOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnFilterOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.btnFilterOff.FlatAppearance.BorderSize = 0;
      this.btnFilterOff.Image = global::UnViaje.Properties.Resources.FiterOff;
      this.btnFilterOff.Location = new System.Drawing.Point(25, 549);
      this.btnFilterOff.Name = "btnFilterOff";
      this.btnFilterOff.Size = new System.Drawing.Size(23, 23);
      this.btnFilterOff.TabIndex = 140;
      this.btnFilterOff.UseVisualStyleBackColor = true;
      this.btnFilterOff.Visible = false;
      this.btnFilterOff.Click += new System.EventHandler(this.BtnFilterOff_Click);
      // 
      // ctlCompras
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.Controls.Add(this.lstNames);
      this.Controls.Add(this.chkShowComent);
      this.Controls.Add(this.txtFilter);
      this.Controls.Add(this.btnSaveVentas);
      this.Controls.Add(this.Grid);
      this.Controls.Add(this.cbMoneda);
      this.Controls.Add(this.btnClear);
      this.Controls.Add(this.btnDelete);
      this.Controls.Add(this.btnModify);
      this.Controls.Add(this.btnAdd);
      this.Controls.Add(this.txtCount);
      this.Controls.Add(this.txtValue);
      this.Controls.Add(this.txtComent);
      this.Controls.Add(this.txtItem);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.lbSumaCostos);
      this.Controls.Add(this.label12);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label13);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnFilterOff);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
      this.Name = "ctlCompras";
      this.Size = new System.Drawing.Size(852, 640);
      this.Load += new System.EventHandler(this.ctlCompras_Load);
      this.VisibleChanged += new System.EventHandler(this.ctlCompras_VisibleChanged);
      ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

      }

    #endregion
    private System.Windows.Forms.ComboBox cbMoneda;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnModify;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.TextBox txtCount;
    private System.Windows.Forms.TextBox txtValue;
    private System.Windows.Forms.TextBox txtItem;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.DataGridView Grid;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridViewTextBoxColumn IdItem;
    private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
    private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
    private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
    private System.Windows.Forms.DataGridViewTextBoxColumn UnidadCosto;
    private System.Windows.Forms.DataGridViewTextBoxColumn CostoCUC;
    private System.Windows.Forms.DataGridViewTextBoxColumn UnidadCUC;
    private System.Windows.Forms.DataGridViewTextBoxColumn precio;
    private System.Windows.Forms.DataGridViewTextBoxColumn monto;
    private System.Windows.Forms.DataGridViewTextBoxColumn moneda;
    private System.Windows.Forms.DataGridViewTextBoxColumn rate;
    private System.Windows.Forms.TextBox txtComent;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label lbSumaCostos;
    private System.Windows.Forms.Button btnSaveVentas;
    private System.Windows.Forms.TextBox txtFilter;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.CheckBox chkShowComent;
    private System.Windows.Forms.ListBox lstNames;
    private System.Windows.Forms.Button btnFilterOff;
    }
  }
