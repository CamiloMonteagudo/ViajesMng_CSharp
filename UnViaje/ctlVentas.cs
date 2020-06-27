using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static UnViaje.DBViaje;

namespace UnViaje
  {
  public partial class ctlVentas : UserControl
    {
    DataTable tbSelProd, tbVentas;
    Dictionary<Int32,Int32> Counts = new Dictionary<Int32,Int32>();

    private bool FillProd;
    private int NowItemId;
    private string NowItem;
    private decimal NowPrec;
    private Mnd NowMond;
    private int NowCant;
    private int NowMaxCant;
    private string NowVend;
    private int NowVentId;
    private int actualMode;

    private decimal PagoCUC;

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    public ctlVentas()
      {
      InitializeComponent();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void ctlVentas_Load( object sender, EventArgs e )
      {
      if( Datos.BD == null ) return;

      ClearDatos();

      foreach( var VendName in Datos.Vendedores )
        {
        cbFilter.Items.Add( VendName );
        cbProdVend.Items.Add( VendName );
        }

      tbSelProd = CreateTableSelProds();
      GridProds.AutoGenerateColumns = false;
      GridProds.DataSource = tbSelProd;

      tbVentas = CreateTableVentas();
      Grid.AutoGenerateColumns = false;
      Grid.DataSource = tbVentas;

      cbFilter.SelectedIndex = 0;
      cbPagoState.SelectedIndex = 0;

      pnlSelProd.Dock = DockStyle.Fill;

      SetWorkMode( 0 );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cada vez que el control se hace visible</summary>
    private void ctlVentas_VisibleChanged( object sender, EventArgs e )
      {
      if( Datos.BD == null ) return;

      UpdateVentas();
      Grid.ClearSelection();

      SetWorkMode( 0 );
      ExecParametros();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Actualiza la lista de venta de acuerdo a los criterios de busqueda</summary>
    private void UpdateVentas()
      {
      decimal PagadoTotal = 0;
      tbVentas.Clear();

      var ShowVendedor = cbFilter.Text;
      var sFilter      = txtFilter.Text.ToLower().Trim();

      var consumo = Datos.Vendedores[0];

      foreach( DBViaje.VentasRow row in Datos.tableVentas )
        {
        if( Datos.FilterVenta(row.id) || Datos.FilterProd(row.idProd) ) continue;

        var vendedor = row.vendedor;
        if( ShowVendedor!="Todas" && ShowVendedor!=vendedor ) continue;

        var Monto   = row.count * row.precio;
        if( row.count<0 && Monto>0 ) Monto = -Monto;

        var sMoneda = Money.Code((Mnd)row.moneda);

        var sItem = "Nombre desconocido";
        var ItemRow = Datos.tableCompras.FindByid( row.idProd );
        if( ItemRow != null )
          sItem = ItemRow.item;

        if( chkShowComent.Checked && !row.IscomentarioNull() && row.comentario.Trim().Length>0 )
          sItem += " | " + row.comentario + ' ';

        var Pago = GetPagado( row );
        if( chkShowPagos.Checked )
          {
          if( Pago > 0 )
            sItem += " | " + Pago + ' ' + sMoneda + ' ';
          }

        var pagado = ( Pago>=Monto || vendedor==consumo || Monto==0 );

        if( sFilter.Length>0 && !sItem.ToLower().Contains(sFilter) )
          continue;

        if( cbPagoState.SelectedIndex==1 && !pagado )
          continue;

        if( cbPagoState.SelectedIndex==2 && pagado )
          continue;

        tbVentas.Rows.Add( row.id, sItem, vendedor, row.count, row.precio, Monto, sMoneda, row.fecha );
        PagadoTotal += PagoCUC;

        }

      Grid.Refresh();

      RefreshEstadisticas();
      lbPagado.Text = PagadoTotal.ToString("0.00");
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene la cantidad pagada para la venta especificada por 'row'</summary>
    private decimal GetPagado( VentasRow row )
      {
      var mond  = (Mnd)row.moneda;
      var relac = Datos.BD.Relations["Ventas_Pagos"];

      PagoCUC = 0;
      decimal pagado = 0;
      PagosRow[] Pagos = (PagosRow[])row.GetChildRows(relac);
      foreach( PagosRow rowPago in Pagos )
        {
        if( rowPago.cuc > 0 )
          {
          var Cuc = rowPago.cuc;
          if( mond != Mnd.Cuc )
            Cuc = Money.Convert( Cuc, Mnd.Cuc, mond );

          PagoCUC += rowPago.cuc;
          pagado += Cuc;
          }

        if( rowPago.cup > 0 )
          {
          var Cup = rowPago.cup;
          if( mond != Mnd.Cup )
            Cup = Money.Convert( Cup, Mnd.Cup, mond );

          PagoCUC += Money.Convert( rowPago.cup, Mnd.Cup, Mnd.Cuc );
          pagado += Cup;
          }
        }

      return pagado;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Llena la lista de selección de productos</summary>
    private void FillGridProds()
      {
      FillProd = true;
      tbSelProd.Clear();

      CalculateCounts();

      decimal porVenderCuc=0, porVenderMn=0 ;
      foreach( ComprasRow row in Datos.tableCompras )
        {
        var idProd = row.id;
        if( Datos.FilterProd(idProd) ) continue;

        var Item   = row.item;
        var Cant   = row.count; 
        var Precio = row.precio;
        Mnd Moned  = (Mnd)row.moneda;
        string MondCode = Money.Code( Moned );

        var Resto = Cant;    
        if( Counts.ContainsKey(idProd) )  Resto -= Counts[idProd]; 

        if( Resto>0 )
          {
          var monto = Resto*Precio;
               if( Moned==Mnd.Cuc ) porVenderCuc += monto;
          else if( Moned==Mnd.Cup ) porVenderMn  += monto;
          else                      porVenderCuc += Money.Convert( monto, Moned, Mnd.Cuc);
          }

        var sCant = Resto.ToString() + '/' + Cant;
        if( Resto>0 || chkShowNoExist.Checked )
          tbSelProd.Rows.Add(idProd, Item, sCant, Precio, MondCode, Moned );
        }

      var sCuc   = porVenderCuc.ToString("0.##");
      var sCup   = porVenderMn.ToString("0.##"); 

      porVenderCuc += Money.Convert( porVenderMn, Mnd.Cup, Mnd.Cuc);
      var sTotal = porVenderCuc.ToString("0.##");

      lbPorVender.Text = "POR VENDER: " + sCuc + " cuc     " + sCup + " mn     TOTAL:" + sTotal + " cuc";
      FillProd = false;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Calcula las cantidades disponible de cada tipo de item</summary>
    private void CalculateCounts()
      {
      Counts.Clear();

      foreach( VentasRow row in Datos.tableVentas )
        {
        var idProd = row.idProd;
        var Cant   = row.count; 

        if( !Counts.ContainsKey(idProd) )  Counts[idProd]  = Cant;
        else                               Counts[idProd] += Cant;
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea una tabla para seleccionar a los productos, pero sin datos</summary>
    private DataTable CreateTableSelProds()
      {
      DataTable tb = new DataTable("SelProds");

      tb.Columns.Add("ID"      , typeof(Int32) );
      tb.Columns.Add("Item"    , typeof(String));
      tb.Columns.Add("Cant"    , typeof(String));
      tb.Columns.Add("Precio"  , typeof(decimal));
      tb.Columns.Add("MondCode", typeof(String));
      tb.Columns.Add("MondTipo", typeof(Int32));

      return tb;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea una tabla vacia para mostrar las ventas</summary>
    private DataTable CreateTableVentas()
      {
      DataTable tb = new DataTable("ViewVentas");

      tb.Columns.Add("IdVent", typeof(Int32) );
      tb.Columns.Add("Item"  , typeof(String));
      tb.Columns.Add("Vend"  , typeof(String));
      tb.Columns.Add("Cant"  , typeof(Int32));
      tb.Columns.Add("Precio", typeof(decimal));
      tb.Columns.Add("Monto" , typeof(decimal));
      tb.Columns.Add("Moned" , typeof(String));
      tb.Columns.Add("Fecha" , typeof(DateTime));

      return tb;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama al seleccionar un item de la lista de productos</summary>
    private void GridProds_CellMouseClick( object sender, DataGridViewCellMouseEventArgs e )
      {
      if( e.RowIndex < 0 ) return;

      var Row = GridProds.Rows[e.RowIndex];

      OnSelectProd( Row );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se selecciona una venta en la tabla de ventas</summary>
    private void OnSelectProd( DataGridViewRow Row )
      {
      ClearDatosVenta();
      if( FillProd )
        {
        GridProds.ClearSelection();
        return;
        }

      NowItemId  = (int)     Row.Cells[0].Value;
      NowItem    = (string)  Row.Cells[1].Value;
      var sCant  = (string)  Row.Cells[2].Value;
      NowPrec    = (decimal) Row.Cells[3].Value;
      NowMond    = (Mnd)(int)Row.Cells[5].Value;

      var Parts = sCant.Split('/');
      int.TryParse( Parts[0], out NowMaxCant );
      int.TryParse( Parts[1], out NowCant );

      txtProdItem.Text = NowItem + " (quedan " + NowMaxCant + " de " + NowCant +")";
      txtProdPrecio.Text = NowPrec.ToString("0.####");
      cbProdMond.SelectedIndex = (int)NowMond;

      NowCant = 1;
      txtProdCant.Text   = NowCant.ToString();
      txtProdMonto.Text  = (NowCant * NowPrec).ToString("0.####");
      txtProdComent.Text = "";
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Limpia los datos del producto actual</summary>
    private void ClearDatos()
      {
      NowItemId  = -1;
      NowItem    = "";
      NowPrec    = 0m;
      NowMond    = 0;
      NowCant    = 0;
      NowMaxCant = 0;

      txtItem.Text  = "";
      txtValue.Text = "";
      txtCant.Text  = "";

      cbMoneda.SelectedIndex = 0;
      ((frmUnViaje)Parent.Parent.Parent).AcceptButton = null;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Limpia los datos de venta</summary>
    private void ClearDatosVenta()
      {
      NowItemId  = -1;
      NowItem    = "";
      NowPrec    = 0m;
      NowMond    = 0;
      NowCant    = 0;
      NowMaxCant = 0;

      txtProdItem.Text = txtProdComent.Text = txtProdCant.Text = txtProdPrecio.Text = "";
      cbProdMond.SelectedIndex = 0;

      ((frmUnViaje)Parent.Parent.Parent).AcceptButton = null;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se selecciona una venta</summary>
    private void Grid_SelectionChanged( object sender, EventArgs e )
      {
      if( Grid.Focused ) RefreshEstadisticas();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama al cambiar la seleccion para mostrar lo productos sin existencia o no</summary>
    private void chkShowNoExist_CheckedChanged( object sender, EventArgs e )
      {
      FillGridProds();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama al cambiar el filtro de las ventas a mostrar</summary>
    private void cbFilter_SelectedIndexChanged( object sender, EventArgs e )
      {
      if( !SelectingVend && cbFilter.Focused ) SelectVendor( cbFilter.Text );

      UpdateVentas();
      if( cbFilter.SelectedIndex>0 && cbFilter.Focused) SetWorkMode(0);
      }

    private void cbProdVend_SelectedIndexChanged( object sender, EventArgs e )
      {
      if( !SelectingVend  && cbProdVend.Focused ) SelectVendor( cbProdVend.Text );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Selecciona el vendedor 'sVend' en todos los com</summary>
    static bool SelectingVend;
    private decimal nowMonto;

    private void SelectVendor( string sVend )
      {
      SelectingVend = true;

      int idx = cbProdVend.FindString( sVend );
      if( cbProdVend.SelectedIndex!= idx ) cbProdVend.SelectedIndex = idx;
       
      idx = cbFilter.FindString( sVend );
      if( cbFilter.SelectedIndex!= idx ) cbFilter.SelectedIndex = idx;

      SelectingVend = false;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama al adicionar una venta nueva</summary>
    private void btnNewVenta_Click( object sender, EventArgs e )
      {
      ClearDatosVenta();
      FillGridProds();
      SetWorkMode( 1 );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    // Obtiene los parametros que se pueden pasar como argumento a la aplicación
    private void ExecParametros()
      {
      btnFilterOff.Visible  = Datos.HasFilter(2);
      btnFilterOff2.Visible  = Datos.HasFilter(2);

      int iTab = Datos.GetIntParam( "Tab" );                        // Tab pasado como parametro
      if( iTab != 5 ) return;                                       // Si no es el actual no hace nada

      int pNew = Datos.GetIntParam( "New" );                        // Obtiene si es un nuevo cobro
      if( pNew==1 )
        {
        btnNewVenta_Click( btnNewVenta, null );
        SelectProd( Datos.F_ProdID );
        }
      else
        SelectVent( Datos.F_VentID );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Selecciona el producto 'IdProd' en el grid de productos</summary>
    private void SelectProd( int IdProd )
      {
      foreach( DataGridViewRow row in GridProds.Rows )            // Selecciona la venta en el grid
        {
        if( IdProd == (int)row.Cells[0].Value )
          {
          row.Selected = true;
          OnSelectProd( row );
          GridProds.FirstDisplayedScrollingRowIndex = row.Index;
          }
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Selecciona el la venta 'IdVent' en el grid de productos</summary>
    private void SelectVent( int IdVent )
      {
      foreach( DataGridViewRow row in Grid.Rows )                // Selecciona la venta en el grid
        {
        if( IdVent == (int)row.Cells[0].Value )
          {
          row.Selected = true;
          OnSelectVenta( row );
          Grid.FirstDisplayedScrollingRowIndex = row.Index;
          }
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Acomada la interface según el modo de trabajo</summary>
    private void SetWorkMode( int m )
      {
      pnlDatos.Visible    = ( m==3 );
      pnlSelProd.Visible  = ( m==1 );

      actualMode = m;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se selecciona una venta en la tabla de ventas</summary>
    private void Grid_CellMouseClick( object sender, DataGridViewCellMouseEventArgs e )
      {
      if( FillProd || e.RowIndex<0 ) return;

      OnSelectVenta( Grid.Rows[e.RowIndex] );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Actauliza los datos de la venta seleccionada en los controles de modificación</summary>
    private void OnSelectVenta( DataGridViewRow Row )
      {
      pnlDatos.Visible = true;                                          // Garantiza que se muestre la zona de datos

      NowVentId  = (int)    Row.Cells[0].Value;
      NowItem    = (string) Row.Cells[1].Value;
      NowVend    = (string) Row.Cells[2].Value; 
      NowCant    = (int)    Row.Cells[3].Value;
      NowPrec    = (decimal)Row.Cells[4].Value; 
      NowMond    =  Money.Idx( (string)Row.Cells[6].Value ); 
      NowMaxCant = NowCant;

      NowItem = NowItem.Split('|')[0];

      txtItem.Text = NowVentId.ToString() + " | " + NowItem ;
      txtCant.Text = NowCant.ToString();
      txtValue.Text = NowPrec.ToString("0.####");
      txtMonto.Text = (NowCant*NowPrec).ToString("0.####");

      var row = Datos.BD.Ventas.FindByid( NowVentId );
      if( row != null  )
        txtComent.Text = (row.IscomentarioNull())? "" : row.comentario;

      txtVendedor.Text = NowVend;
      cbMoneda.SelectedIndex = (int)NowMond;

      SetWorkMode(3);

      if( NowCant<0 ) btnProdBack.Visible = false;

      ((frmUnViaje)Parent.Parent.Parent).AcceptButton = btnModify;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Modifica los datos de una venta</summary>
    private void btnModify_Click( object sender, EventArgs e )
      {
      try
        {
        if( !int.TryParse(txtCant.Text, out NowCant) || NowCant<0 )
          throw new Exception( "El valor para la cantidad es incorrecto." );

        NowMond = (Mnd)cbMoneda.SelectedIndex;
        if( (int)NowMond == -1 )
          throw new Exception( "Debe seleccionar la moneda a utilizar" );

        if( !decimal.TryParse(txtValue.Text, out NowPrec) )
          throw new Exception( "El valor para el precio es incorrecto" );

        var Parts = txtItem.Text.Split('|');
        NowVentId = int.Parse( Parts[0] );

        var row = Datos.BD.Ventas.FindByid( NowVentId );
        if( row == null )
          throw new Exception( "No se encuentra la venta correspondiente" );

        NowMaxCant = ItemRemanet( row.idProd, NowVentId );
        if( NowCant > NowMaxCant )
          throw new Exception( "La cantidad máxima de items permitidos es " + NowMaxCant );

        var fecha   = DateTime.Today;
        if( row.vendedor == Datos.Vendedores[0] )
          NowPrec = 0;

        row.count = NowCant;
        row.precio = NowPrec;
        row.moneda = (int)NowMond;
        row.fecha  = fecha;
        row.comentario = txtComent.Text;

        var Monto   = NowCant * NowPrec;
        var sMoneda = Money.Code(NowMond);

        foreach( DataRow row2 in tbVentas.Rows )
          {
          if( (int)row2["IdVent"] == NowVentId )
            {
            row2["Cant"] = NowCant;
            row2["Precio"] = NowPrec;
            row2["Monto"] = Monto;
            row2["Moned"] = sMoneda;
            row2["Fecha"] = fecha;

            var sItem = ((string)row2["Item"]).Split('|')[0].TrimEnd();
            row2["Item"] = sItem;

            if( chkShowComent.Checked )
              row2["Item"] += " | " + row.comentario;

            break;
            }
          }

        SetWorkMode(0);
        RefreshEstadisticas();
        Datos.SetChanges( "VentaModify" );
        }
      catch( Exception exc )
        {
        MessageBox.Show( "ERROR: " + exc.Message );
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se oprime el boton para devolver una venta</summary>
    private void btnProdBack_Click( object sender, EventArgs e )
      {
      try
        {
        if( !int.TryParse(txtCant.Text, out NowCant) || NowCant<=0 )
          throw new Exception( "El valor para la cantidad es incorrecto." );

        var row1 = Datos.BD.Ventas.FindByid( NowVentId );
        if( row1 == null )
          throw new Exception( "No se encuentra la venta correspondiente" );

        if( NowCant > row1.count )
          throw new Exception( "No se pueden devolver más items que los asignados a la venta" );

        var fecha   = DateTime.Today;
        if( row1.IscomentarioNull() ) row1.comentario = "";

        row1.count -= NowCant;
        row1.comentario += " {" + fecha.ToString("d MMM") + ". Devolvio " + NowCant +'}';

        var Monto   = row1.count * row1.precio;

        foreach( DataRow row2 in tbVentas.Rows )
          {
          if( (int)row2["IdVent"] == NowVentId )
            {
            row2["Cant"]  = row1.count;
            row2["Monto"] = Monto;

            var sItem = ((string)row2["Item"]).Split('|')[0].TrimEnd();
            row2["Item"] = sItem;

            if( chkShowComent.Checked )
              row2["Item"] += " | " + row1.comentario;

            break;
            }
          }

        SetWorkMode(0);
        RefreshEstadisticas();
        Datos.SetChanges( "VentaDevolver" );
        }
      catch( Exception exc )
        {
        MessageBox.Show( "ERROR: " + exc.Message );
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se oprime el botón de borrar una venta</summary>
    private void btnDelVenta_Click( object sender, EventArgs e )
      {
      var ret = MessageBox.Show("Si borra la venta, no quedrá constancia en el sistema\r\n¿Estas seguro?", "Aviso", MessageBoxButtons.YesNo );

      if( ret == DialogResult.Yes )
        {
        var Row1 = Datos.BD.Ventas.FindByid( NowVentId );
        if( Row1==null )
          {
          MessageBox.Show("No se encontro el item a borrar");
          return;
          }

        Row1.Delete();

        foreach( DataRow Row2 in tbVentas.Rows )
          {
          if( (int)Row2[0] == NowVentId )
            {
            Row2.Delete();
            break;
            }
          }

        SetWorkMode(0);
        RefreshEstadisticas();
        Datos.SetChanges( "VentaDelete" );
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Esconde la lista de productos</summary>
    private void btnCloseSelProd_Click( object sender, EventArgs e )
      {
      SetWorkMode(0);
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Busca la cantidad de items del tipo dado que quedan por vender</summary>
    private int ItemRemanet( int ItemId, int VentId )
      {
      int count = 0;
      var rowCp = Datos.tableCompras.FindByid( ItemId );
      if( rowCp==null )
        throw new Exception( "No se encuentra el Item " + ItemId + " en la tabla de compras" );

      count = rowCp.count;
      foreach( VentasRow rowVt in Datos.tableVentas )
        if( rowVt.idProd == ItemId && rowVt.id!=VentId )
          count -= rowVt.count;

      return count;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama al oprimir el botón para adicionar una venta</summary>
    private void btnAddVenta_Click( object sender, EventArgs e )
      {
      NowVentId = -1;
      try
        {
        GetValores();
        if( NowVend == Datos.Vendedores[0] )
          NowPrec = 0;

        nowMonto   = NowCant * NowPrec;
        var sMoneda = Money.Code(NowMond);
        var fecha   = DateTime.Today;
        var Coment  = txtProdComent.Text;

        var row = Datos.BD.Ventas.AddVentasRow(NowItemId, NowVend, NowCant, NowPrec, (int)NowMond, fecha, Coment );

        NowVentId = row.id;
        if( chkShowComent.Checked )
          NowItem += " | " + Coment;

        var row2 = tbVentas.Rows.Add( NowVentId, NowItem, NowVend, NowCant, NowPrec, nowMonto, sMoneda, fecha );

        SelectVentaInGrid( NowVentId );
        RefreshEstadisticas();
        SetWorkMode(0);
        Datos.SetChanges( "VentaAdd" );
        }
      catch( Exception exc )
        {
        MessageBox.Show( "ERROR: " + exc.Message );
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama para hacer un pago de una venta directamente</summary>
    private void btnPagar_Click( object sender, EventArgs e )
      {
      btnAddVenta_Click( null, null );
      if( NowVentId<0 ) return;

      var fecha   = DateTime.Today;
      var rowVent = Datos.BD.Ventas.FindByid( NowVentId );
      if( rowVent == null )
        {
        MessageBox.Show( "ERROR: No existe la venta con el ID " + NowVentId );
        return;
        }

      rowVent.comentario += " (Pago directo)";
      decimal PagoCuc=0, PagoCup=0;

           if( NowMond==Mnd.Cuc ) PagoCuc = nowMonto;
      else if( NowMond==Mnd.Cup ) PagoCup = nowMonto;
      else                        PagoCuc = Money.Convert( nowMonto, NowMond, Mnd.Cuc);

      Datos.BD.Pagos.AddPagosRow( rowVent, NowCant, PagoCuc, PagoCup, "Pago directo", fecha );
      Datos.SetChanges( "VentaDirect" );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene los valores introducidos por el usuario</summary>
    private void GetValores()
      {
      var ItemVend = cbProdVend.SelectedItem;
      if( ItemVend == null )
        throw new Exception( "Debe seleccionar un vendedor" );

      NowVend = ItemVend.ToString();

      if( NowItemId == -1 || NowItem == "" )
        throw new Exception( "No hay un item seleccionado para la venta" );

      NowMond = (Mnd)cbProdMond.SelectedIndex;
      if( (int)NowMond == -1 )
        throw new Exception( "Debe seleccionar la moneda a utilizar" );

      if( !decimal.TryParse(txtProdPrecio.Text, out NowPrec) )
        throw new Exception( "El valor para el precio es incorrecto" );

      if( !int.TryParse(txtProdCant.Text, out NowCant) || NowCant<=0 )
        throw new Exception( "El valor para la cantidad es incorrecto" );

      if( NowCant > NowMaxCant )
        throw new Exception( "La cantidad de items especificados excede los disponibles" );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Selecciona la venta con identificador 'IdVent' en el grid</summary>
    private void SelectVentaInGrid( int IdVent)
      {
      Grid.ClearSelection();

      for( int i=0; i<Grid.Rows.Count; i++ )
        {
        var gdRow = Grid.Rows[i];
        if( (int)(gdRow.Cells[0].Value) == IdVent )
          {
          Grid.Rows[i].Selected = true;
          Grid.FirstDisplayedScrollingRowIndex = i;

          break;
          }
          }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama al cambiar el monto de la venta</summary>
    private void txtMonto_TextChanged( object sender, EventArgs e )
      {
      if( !txtMonto.Focused ) return;

      UpdatePrecio( txtCant, txtValue, txtMonto );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama al cambiar el precio de los item que se estan vendiendo</summary>
    private void txtValue_TextChanged( object sender, EventArgs e )
      {
      if( !txtValue.Focused ) return;

      UpdateMonto( txtCant, txtValue, txtMonto );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama al cambiar la cantidad de item que se estan vendiendo</summary>
    private void txtCant_TextChanged( object sender, EventArgs e )
      {
      if( !txtCant.Focused ) return;

      UpdateMonto( txtCant, txtValue, txtMonto );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se cambia el monto del producto en una nueva venta</summary>
    private void txtProdMonto_TextChanged( object sender, EventArgs e )
      {
      if( !txtProdMonto.Focused ) return;

      UpdatePrecio( txtProdCant, txtProdPrecio, txtProdMonto );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se cambia el precio del producto</summary>
    private void txtProdPrecio_TextChanged( object sender, EventArgs e )
      {
      if( !txtProdPrecio.Focused ) return;

      UpdateMonto( txtProdCant, txtProdPrecio, txtProdMonto );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se cambia la cantidad de productos</summary>
    private void txtProdCant_TextChanged( object sender, EventArgs e )
      {
      if( !txtProdCant.Focused ) return;

      UpdateMonto( txtProdCant, txtProdPrecio, txtProdMonto );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Actualiza el precio en el editor 'edPrecio' con las cantidad y el monto en 'edCant' y 'edMonto' respertivamente </summary>
    private void UpdatePrecio( TextBox edCant, TextBox edPrecio, TextBox edMonto )
      {
      decimal Monto, Cant;
      if( !decimal.TryParse( edMonto.Text, out Monto) ) return;
      if( !decimal.TryParse( edCant.Text , out Cant ) ) return;
      if( Cant==0 ) return;

      var Precio = Monto / Cant;

      edPrecio.Text = Precio.ToString("0.####");
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Actualiza el monto en el editor 'edMonto' con las cantidad y el precio en 'edCant' y 'edPrecio' respertivamente </summary>
    private void UpdateMonto( TextBox edCant, TextBox edPrecio, TextBox edMonto )
      {
      decimal Precio, Cant;

      if( !decimal.TryParse( edPrecio.Text, out Precio) ) return;
      if( !decimal.TryParse( edCant.Text  , out Cant  ) ) return;
      if( Cant==0 ) return;

      var Monto = Precio * Cant;

      edMonto.Text = Monto.ToString("0.####");
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se cambia el checkMark de mostrar los comentarios</summary>
    private void chkShowComent_CheckedChanged( object sender, EventArgs e )
      {
      UpdateVentas();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se escribe algo en el control de filtro</summary>
    private void txtFilter_TextChanged( object sender, EventArgs e )
      {
      UpdateVentas();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama para guardar los resultados de las ventas en un fichero</summary>
    private void btnSaveVentas_Click( object sender, EventArgs e )
      {
      var sFootter = "\tPagado:" + lbPagado.Text + "\tCantidad:\t" + lbSumaCant.Text + "\tTotal:\t" + lbTotalMonto.Text + "\t" + lbMotoDetail.Text + "\t\t";
      Datos.SaveGrid( Grid, sFootter);
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Quita los filtros si existen</summary>
    private void BtnFilterOff_Click(object sender, EventArgs e)
      {
      Datos.RemoveFilter(2);

      btnFilterOff.Hide();
      btnFilterOff2.Hide();

      UpdateVentas();
      FillGridProds();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Calcula de estadisticas de las ventas según los datos entrados y las filas seleccionadas</summary>
    public void RefreshEstadisticas()
      {
      decimal SumaMonto=0, SumaCuc=0, SumaCup=0, SumaItems=0;

      var SelRows = (Grid.SelectedRows.Count>1);
      foreach( DataGridViewRow Row in Grid.Rows )
        {
        var Cant    = (int)Row.Cells[3].Value;
        var Monto   = (decimal)Row.Cells[5].Value;
        var MondCod = (string)Row.Cells[6].Value;
        var MondIdx = Money.Idx( MondCod ); 

        var MontoCuc = Monto;
        if( MondIdx != Mnd.Cuc)
          MontoCuc = Money.Convert(Monto, MondIdx, Mnd.Cuc );

        if( !SelRows || Row.Selected )
          {
          SumaMonto += MontoCuc;

          if( MondIdx == Mnd.Cuc ) SumaCuc += Monto;
          if( MondIdx == Mnd.Cup ) SumaCup += Monto;

          SumaItems += Cant;
          }
        }

      lbTotalMonto.Text = SumaMonto.ToString("0.00");
      lbMotoDetail.Text = "(" + SumaCuc.ToString("0.00") + " cuc " + SumaCup.ToString("0.00") + " mn )"; 

      lbSumaCant.Text = SumaItems.ToString();
      }
    }
  }
