using System;
using System.Data;
using System.Windows.Forms;
using static UnViaje.DBViaje;

namespace UnViaje
  {
  public partial class ctlCobros : UserControl
    {
    private DataTable tbSelVent;
    private int NowVentId;
    private string NowVend;
    private decimal NowPrec;
    private Mnd NowMond;
    private decimal NowPagoCant;
    private decimal NowPagoCuc;
    private decimal NowPagoCup;
    private decimal NowCantMax;
    private DataTable tbPagos;
    private int NowPagoId;

    private decimal ModifyCuc;
    private decimal ModifyCup;
    private decimal ModifyCant;

    public ctlCobros()
      {
      InitializeComponent();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void ctlCobros_Load( object sender, EventArgs e )
      {
      if( Datos.BD == null ) return;

      foreach( var VendName in Datos.Vendedores )
        {
        cbFilter.Items.Add( VendName );
        cbVendedor.Items.Add( VendName );
        }

      tbSelVent = CreateTableSelVentas();
      GridVentas.AutoGenerateColumns = false;
      GridVentas.DataSource = tbSelVent;

      tbPagos = CreateTablePagos();
      Grid.AutoGenerateColumns = false;
      Grid.DataSource = tbPagos;

      cbFilter.SelectedIndex = 0;
      cbVendedor.SelectedIndex = 0;

      pnlSelVenta.Dock = DockStyle.Fill;

      ClearDatos();

      FillGridPagos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama todas las veces que se vaya a mostrar el tag</summary>
    private void ctlCobros_VisibleChanged( object sender, EventArgs e )
      {
      if( Datos.BD == null ) return;

      ClearDatos();

      pnlSelVenta.Visible = false;

      FillGridPagos();
      ExecParametros();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    // Ejecuta los parametros que se le pasan a la aplicación
    private void ExecParametros()
      {
      btnFilterOff.Visible  = Datos.HasFilter(2);
      btnFilterOff2.Visible = Datos.HasFilter(2);

      int iTab = Datos.GetIntParam( "Tab" );                        // Tab pasado como parametro
      if( iTab != 6 ) return;                                       // Si no es el actual no hace nada

      int pNew = Datos.GetIntParam( "New" );                        // Obtiene si es un nuevo cobro
      if( pNew==1 )
        {
        btnNewPago_Click( btnNewPago, null );                       // Activa la pantalla de cobro
        SelectVent( Datos.F_VentID );
        }
      else
        SelectPago( Datos.F_PagoID );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Selecciona el producto 'IdProd' en el grid de productos</summary>
    private void SelectPago( int IdPago )
      {
      foreach( DataGridViewRow row in Grid.Rows )            // Selecciona la venta en el grid
        {
        if( IdPago == (int)row.Cells[0].Value )
          {
          row.Selected = true;
          OnSelectPago( row );
          Grid.FirstDisplayedScrollingRowIndex = row.Index;
          }
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Selecciona el la venta 'IdVent' en el grid de productos</summary>
    private void SelectVent( int IdVent )
      {
      foreach( DataGridViewRow row in GridVentas.Rows )                // Selecciona la venta en el grid
        {
        if( IdVent == (int)row.Cells[0].Value )
          {
          row.Selected = true;
          OnSelectVenta( row );
          GridVentas.FirstDisplayedScrollingRowIndex = row.Index;
          }
        }
      }
    
    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Limpia los datos seleccionados</summary>
    private void ClearDatos()
      {
      Grid.ClearSelection();
      txtModItem.Text = txtModCant.Text = txtModCuc.Text = txtModCup.Text = txtModComent.Text = "";
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea una tabla para seleccionar a las ventas a pagar, pero sin datos</summary>
    private DataTable CreateTableSelVentas()
      {
      DataTable tb = new DataTable("SelVentas");

      tb.Columns.Add( "IdVent", typeof( Int32 ) );
      tb.Columns.Add( "Item", typeof( String ) );
      tb.Columns.Add( "Vend", typeof( String ) );
      tb.Columns.Add( "Cant", typeof( String ) );
      tb.Columns.Add( "Precio", typeof( String ) );
      tb.Columns.Add( "Monto", typeof( decimal ) );
      tb.Columns.Add( "Pagado", typeof( String ) );

      return tb;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea una tabla vacia para mostrar las ventas</summary>
    private DataTable CreateTablePagos()
      {
      DataTable tb = new DataTable("ViewCobros");

      tb.Columns.Add( "IdPago", typeof( Int32 )    );
      tb.Columns.Add( "IdVent", typeof( Int32 )    );
      tb.Columns.Add( "Item"  , typeof( String )   );
      tb.Columns.Add( "Vend"  , typeof( String )   );
      tb.Columns.Add( "Cant"  , typeof( decimal )  );
      tb.Columns.Add( "Precio", typeof( String )   );
      tb.Columns.Add( "Cuc"   , typeof( decimal )  );
      tb.Columns.Add( "Cup"   , typeof( decimal )  );
      tb.Columns.Add( "Coment", typeof( String )   );
      tb.Columns.Add( "Fecha" , typeof( DateTime ) );

      return tb;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se oprime el boton de adicionar un nuevo pago</summary>
    private void btnNewPago_Click( object sender, EventArgs e )
      {
      txtPagoItem.Text = "";
      txtPagoComent.Text = "";
      txtPagoCant.Text = "";
      txtPagoCuc.Text = "";
      txtPagoMn.Text = "";

      pnlSelVenta.Visible = true;
      FillGridVentas();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se oprime el boton de cerrar mientra se agraga un pago nuevo</summary>
    private void btnCloseSelPago_Click( object sender, EventArgs e )
      {
      pnlSelVenta.Visible = false;

      ClearDatos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Llena el Grid con la ventas que faltan por pagar</summary>
    private void FillGridVentas()
      {
      var VendTxt = cbVendedor.Text;
      var VendIdx = cbVendedor.SelectedIndex;
      var ShowAll = chkShowAll.Checked;
      var sFilter = txtFilterPago.Text.ToLower().Trim();

      tbSelVent.Clear();
      decimal porPagarCuc=0, porPagarCup=0; ; 
      foreach( VentasRow row in Datos.tableVentas )
        {
        if( VendIdx>0 && VendTxt!=row.vendedor ) continue;
        if( Datos.FilterVenta(row.id) || Datos.FilterProd(row.idProd) ) continue;

        var precio = row.precio;
        var Pagado = GetPagado( row );
        var Monto  = row.count*precio;

        if( !ShowAll && Pagado>=Monto ) continue;

        var sItem = "No se encontro el Item";
        var rowProd = Datos.tableCompras.FindByid( row.idProd );
        if( rowProd != null )
          sItem = rowProd.item;

        if( !row.IscomentarioNull() && row.comentario.Trim().Length>0 )
          sItem += " | " + row.comentario;

        if( sFilter.Length>0 && !sItem.ToLower().Contains(sFilter) )
          continue;

        var ItemPago = 0m;
        if( precio!=0 ) ItemPago = Pagado/precio;

        var Moned = (Mnd)row.moneda;
        var sPrecio = precio.ToString("0.##") + ' ' + Money.Code( Moned );

        var sPagado = "";
        if( Pagado>0 )
          sPagado = Pagado.ToString("0.##") + ' ' +Money.Code( Moned ) + " = " + ItemPago.ToString("0.##");

        var resto = row.count-ItemPago;

        var porPagar = resto * precio;
             if( Moned==Mnd.Cuc ) porPagarCuc += porPagar;
        else if( Moned==Mnd.Cup ) porPagarCup  += porPagar;
        else                      porPagarCuc += Money.Convert( porPagar, Moned, Mnd.Cuc);

        Monto  = resto*precio;
        var sCant = resto.ToString("0.#");
        if( resto != row.count ) sCant +=" / " + row.count;

        tbSelVent.Rows.Add( row.id, sItem, row.vendedor, sCant, sPrecio, Monto, sPagado );
        }

      var sCuc   = porPagarCuc.ToString("0.##");
      var sCup   = porPagarCup.ToString("0.##"); 

      porPagarCuc += Money.Convert( porPagarCup, Mnd.Cup, Mnd.Cuc);
      var sTotal = porPagarCuc.ToString("0.##");

      lbPorPagar.Text = "POR PAGAR: " + sCuc + " cuc    " + sCup + " mn    TOTAL:" + sTotal + " cuc";
      }
                                        
    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Llena el Grid con los pagos realizados</summary>
    private void FillGridPagos()
      {
      var VendId   = cbFilter.SelectedIndex;
      var VendName = cbFilter.Text;
      var sFilter = txtFilter.Text.ToLower().Trim();

      tbPagos.Clear();
      foreach( PagosRow row in Datos.tablePagos )
        {
        var IdVent = row.idVent;
        if( Datos.FilterVenta( IdVent ) || Datos.FilterPago(row.id) ) continue;

        var sItem = "Nombre del Item sin determinar";
        var sVend = "Desconcido";
        var sPrec = "";

        var rowVent = Datos.tableVentas.FindByid( IdVent );
        if( rowVent != null )
          {
          var idProd  = rowVent.idProd;
          if( Datos.FilterProd( idProd ) ) continue;

          var rowProd = Datos.tableCompras.FindByid( idProd );
          if( rowProd != null )
            sItem = rowProd.item;

          sVend = rowVent.vendedor;
          sPrec = rowVent.precio.ToString("0.##") + ' ' + Money.Code( (Mnd)rowVent.moneda );
          }

        if( chkShowComent.Checked && !row.IscomentarioNull() && row.comentario.Trim().Length>0 )
          sItem += " | " + row.comentario + ' ';

        if( sFilter.Length>0 && !sItem.ToLower().Contains(sFilter) )
          continue;

        if( VendId==0 || sVend==VendName )
          tbPagos.Rows.Add( row.id, IdVent, sItem, sVend, row.count, sPrec, row.cuc, row.cup, row.comentario, row.fecha );
        }

      RefreshEstadisticas();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Filtra los pagos a mostrar según el vendedor</summary>
    private void cbFilter_SelectedIndexChanged( object sender, EventArgs e )
      {
      cbVendedor.SelectedIndex = cbFilter.SelectedIndex;

      FillGridPagos();
      }

    //-----------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama al seleccionar una venta para pagarla</summary>
    private void GridVentas_CellMouseClick( object sender, DataGridViewCellMouseEventArgs e )
      {
      if( e.RowIndex<0 ) return;

      var Row = GridVentas.Rows[e.RowIndex];

      OnSelectVenta( Row );
      }

    //-----------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Selecciona la fila Row para edicción</summary>
    private void OnSelectVenta( DataGridViewRow Row )
      {
      txtPagoCuc.Text = "";
      txtPagoMn.Text  = "";
      txtPagoComent.Text  = "";

      NowVentId = (int)Row.Cells[0].Value;
      var sItem = Row.Cells[1].Value.ToString();

      var row = Datos.tableVentas.FindByid( NowVentId );
      if( row==null )
        {
        MessageBox.Show("No se encontro una venta con el código especificado");
        return;
        }
      
      NowVend  = row.vendedor;
      NowPrec  = row.precio;
      NowMond  = (Mnd)row.moneda;
      var Cant = row.count;

      var Pagado  = GetPagado( row );

      var Monto = (Cant * NowPrec) - Pagado;
      NowCantMax = Monto/NowPrec;


      chkCUC.Checked = chkCUP.Checked = false; 
      if( NowMond==Mnd.Cuc )
        {
        txtPagoCuc.Text = Monto.ToString("0.####");
        chkCUC.Checked  = true;
        }
      else
        {
        txtPagoMn.Text = Monto.ToString("0.####");
        chkCUP.Checked = true;
        }

      var NPagados = Cant - NowCantMax;
      txtPagoItem.Text = sItem + " | Pagados " + NPagados.ToString("0.##") +" de " + row.count;
      txtPagoCant.Text = NowCantMax.ToString("0.##");

      txtPagoComent.Text = NowVentId.ToString();
      if( !row.IscomentarioNull() && row.comentario.Trim().Length>0 )
        txtPagoComent.Text += " - " + row.comentario;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se cambia la cantidad de items</summary>
    private void txtPagoCant_TextChanged( object sender, EventArgs e )
      {
      if( !txtPagoCant.Focused ) return;

      UpdatePrecios();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Actualiza los precios, según la cantidad de item, la moneda y las casillas chequeadas</summary>
    private void UpdatePrecios()
      {
      GetPagoValues();

      var Monto = NowPagoCant *NowPrec;

      if( (chkCUC.Checked && chkCUP.Checked) ||  (!chkCUC.Checked && !chkCUP.Checked) )
        {
        txtPagoCuc.Text = txtPagoMn.Text = "";

        if( NowMond==Mnd.Cuc ) txtPagoCuc.Text = Monto.ToString( "0.####" );
        else                    txtPagoMn.Text = Monto.ToString( "0.####" );

        return;
        }

      if( chkCUC.Checked )
        {
        var Resto = Monto - ValToNow( NowPagoCup, Mnd.Cup );
        var ValCuc = ValFromNow( Resto, Mnd.Cuc );

        txtPagoCuc.Text = ValCuc.ToString( "0.####" );
        }
      else
        {
        var Resto = Monto - ValToNow( NowPagoCuc, Mnd.Cuc );
        var ValCup = ValFromNow( Resto, Mnd.Cup );

        txtPagoMn.Text = ValCup.ToString( "0.####" );
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se cambia el pago en CUC</summary>
    private void txtPagoCuc_TextChanged( object sender, EventArgs e )
      {
      if( !txtPagoCuc.Focused ) return;

      GetPagoValues();

      if( chkCUP.Checked )
        {
        var Monto = NowPagoCant *NowPrec;
        var Resto = Monto - ValToNow( NowPagoCuc, Mnd.Cuc );
        var ValCup = ValFromNow( Resto, Mnd.Cup );

        txtPagoMn.Text = ValCup.ToString( "0.####" );
        }
      else
        {
        var total = ValToNow( NowPagoCuc, Mnd.Cuc ) + ValToNow( NowPagoCup, Mnd.Cup );
        txtPagoCant.Text = (total/NowPrec).ToString( "0.####" );
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se cambia el pago en Pesos Cubanos</summary>
    private void txtPagoMn_TextChanged( object sender, EventArgs e )
      {
      if( !txtPagoMn.Focused ) return;

      GetPagoValues();

      if( chkCUC.Checked )
        {
        var Monto = NowPagoCant *NowPrec;
        var Resto = Monto - ValToNow( NowPagoCup, Mnd.Cup );
        var ValCuc = ValFromNow( Resto, Mnd.Cuc );

        txtPagoCuc.Text = ValCuc.ToString( "0.####" );
        }
      else
        {
        var total = ValToNow( NowPagoCuc, Mnd.Cuc ) + ValToNow( NowPagoCup, Mnd.Cup );
        txtPagoCant.Text = (total/NowPrec).ToString( "0.####" );
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    // Se llama cuando se modifica el valor en CUC del pago
    private void txtModCuc_TextChanged( object sender, EventArgs e )
      {
      if( !txtModCuc.Focused ) return;

      GetModifyValues();

      var total = ValToNow( ModifyCuc, Mnd.Cuc ) + ValToNow( ModifyCup, Mnd.Cup );
      txtModCant.Text = (total/NowPrec).ToString( "0.#" );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    // Se llama cuando se modifica el valor en CUP del pago
    private void txtModCup_TextChanged( object sender, EventArgs e )
      {
      if( !txtModCup.Focused ) return;

      GetModifyValues();

      var total = ValToNow( ModifyCuc, Mnd.Cuc ) + ValToNow( ModifyCup, Mnd.Cup );
      txtModCant.Text = (total/NowPrec).ToString( "0.#" );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Transforma el valor 'val' en la moneda 'moneda', al valor de la monerada actual</summary>
    private decimal ValToNow( decimal val, Mnd moneda )
      {
      return Money.Convert( val, moneda, NowMond );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Transforma el valor 'val' en la moneda actual, al valor en la moneda 'moneda'</summary>
    private decimal ValFromNow( decimal val, Mnd moneda )
      {
      return Money.Convert( val, NowMond, moneda );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene los valores entrados por el usuario cuando se esta realizando un pago</summary>
    private void GetPagoValues()
      {
      NowPagoCant = 0;
      decimal.TryParse( txtPagoCant.Text, out NowPagoCant );

      NowPagoCuc = 0;
      decimal.TryParse( txtPagoCuc.Text, out NowPagoCuc );

      NowPagoCup = 0;
      decimal.TryParse( txtPagoMn.Text, out NowPagoCup );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama al oprimir el boton para realizar un cobro</summary>
    private void btnCobrar_Click( object sender, EventArgs e )
      {
      try
        {
        GetPagoValues();
        if( NowPagoCant <= 0  )
          throw new Exception("La cantidad de items a pagar es incorrecta");
        
        if( NowPagoCant > Math.Round(NowCantMax,2) )
          throw new Exception("Se estan pagando más items de los que estan en venta");

        if( NowPagoCuc<0 || NowPagoCup<0 )
          throw new Exception("Al menos uno de los 2 precios es incorrecto");

        var pago = ValToNow( NowPagoCuc, Mnd.Cuc ) + ValToNow( NowPagoCup, Mnd.Cup );
        if( Math.Round(pago/NowPrec,2) != Math.Round(NowPagoCant,2) )
          throw new Exception("El valor pagado no se corresponde con la cantidad de items");

        var sItem   = txtPagoItem.Text;
        if( sItem.Length == 0 )
          throw new Exception("Debe seleccionar un item de la lista");

        sItem = sItem.Split('|')[0];

        var sComent = txtPagoComent.Text;
        var fecha   = DateTime.Today;
        var sPrec   = NowPrec.ToString("0.####") + ' ' + Money.Code( NowMond );

        var rowVent = Datos.BD.Ventas.FindByid( NowVentId );

        var rowPago = Datos.BD.Pagos.AddPagosRow( rowVent, NowPagoCant, NowPagoCuc, NowPagoCup, sComent, fecha );

        tbPagos.Rows.Add( rowPago.id, rowVent.id, sItem, NowVend, NowPagoCant, sPrec, NowPagoCuc, NowPagoCup, sComent, fecha );

        pnlSelVenta.Visible = false;

        SelectRowWithId( rowPago.id );
        RefreshEstadisticas();
        Datos.SetChanges( "CobroAdd" );
        }
      catch( Exception exc)
        {
        MessageBox.Show("ERROR: " + exc.Message );
        }

      ClearDatos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Selecciona en el grid principal el pago con el Id dado</summary>
    private void SelectRowWithId( int Id )
      {
      Grid.ClearSelection();

      for( int i=0; i<Grid.Rows.Count; i++ )
        {
        var gdRow = Grid.Rows[i];
        if( (int)(gdRow.Cells[0].Value) == Id )
          {
          Grid.Rows[i].Selected = true;
          Grid.FirstDisplayedScrollingRowIndex = i;
          break;
          }
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene la cantidad pagada para la venta especificada por 'row'</summary>
    private decimal GetPagado( VentasRow row )
      {
      var mond  = (Mnd)row.moneda;
      var relac = Datos.BD.Relations["Ventas_Pagos"];

      decimal pagado = 0;
      PagosRow[] Pagos = (PagosRow[])row.GetChildRows(relac);
      foreach( PagosRow rowPago in Pagos )
        {
        if( rowPago.cuc > 0 )
          {
          var PagoCuc = rowPago.cuc;
          if( mond != Mnd.Cuc )
            PagoCuc = Money.Convert( PagoCuc, Mnd.Cuc, mond );

          pagado += PagoCuc;
          }

        if( rowPago.cup > 0 )
          {
          var PagoCup = rowPago.cup;
          if( mond != Mnd.Cup )
            PagoCup = Money.Convert( PagoCup, Mnd.Cup, mond );

          pagado += PagoCup;
          }
        }

      return pagado;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se selecciona otro vendedor</summary>
    private void cbVendedor_SelectedIndexChanged( object sender, EventArgs e )
      {
      FillGridVentas();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se chequea/deschequea motrar todos los items</summary>
    private void chkShowAll_CheckedChanged( object sender, EventArgs e )
      {
      FillGridVentas();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void chkCUP_CheckedChanged( object sender, EventArgs e )
      {
      if( !chkCUP.Focused ) return;

      UpdatePrecios();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void chkCUC_CheckedChanged( object sender, EventArgs e )
      {
      if( !chkCUC.Focused ) return;

      UpdatePrecios();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se oprime una fila en el grid de pagos</summary>
    private void Grid_CellMouseClick( object sender, DataGridViewCellMouseEventArgs e )
      {
      if( e.RowIndex<0 ) return;

      OnSelectPago( Grid.Rows[e.RowIndex] );
      }

    private void OnSelectPago( DataGridViewRow Row )
      {
      NowPagoId = (int)Row.Cells[0].Value;

      var row = FindPagoId( NowPagoId );
      if( row==null )
        {
        ClearDatos();
        }
      else
        {
        var parts = ((string)row[2]).Split('|');

        txtModItem.Text   = parts[0];
        txtModCant.Text   = ((decimal)row[4]).ToString("0.#");
        txtModCuc.Text    = ((decimal)row[6]).ToString("0.####");
        txtModCup.Text    = ((decimal)row[7]).ToString("0.####");
        txtModComent.Text = (string)row[8];

        var rowPago = Datos.tablePagos.FindByid(NowPagoId);
        if( rowPago != null )
          {
          var IdVent = rowPago.idVent;
          var rowVent = Datos.tableVentas.FindByid( IdVent );
          if( rowVent != null )
            NowPrec = rowVent.precio;
          }
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene los datos de un paga sabiendo su ID</summary>
    private DataRow FindPagoId( int PagoId )
      {
      foreach( DataRow row in tbPagos.Rows )
        if( (int)row[0] == PagoId ) return row;

      return null;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama al oprimir el botón de modificar pago</summary>
    private void btnModifyPago_Click( object sender, EventArgs e )
      {
      GetModifyValues();

      if( ModifyCuc<0 || ModifyCup<0 )
        {
        MessageBox.Show("Los pagos no pueden ser negativos");
        return;
        }

      if( ModifyCuc==0 && ModifyCup==0 )
        {
        MessageBox.Show("Al menos uno de los dos pagos tiene que tener valor");
        return;
        }

      if( ModifyCant<=0 )
        {
        MessageBox.Show("La cantidad debe ser mayor que cero");
        return;
        }

      var row1 = Datos.tablePagos.FindByid(NowPagoId);
      if( row1 != null )
        {
        row1.cuc = ModifyCuc;
        row1.cup = ModifyCup;
        row1.count = ModifyCant;
        row1.comentario = txtModComent.Text;
        }
      else
        MessageBox.Show("No se actulizaron los datos de base de datos, porque no se encontro un pago con el ID " + NowPagoId );

      var row2 = FindPagoId( NowPagoId );
      if( row2 != null )
        {
        row2["Cuc"]    = ModifyCuc;
        row2["Cup"]    = ModifyCup;
        row2["Cant"]   = ModifyCant;
        row2["Coment"] = txtModComent.Text;
        }
      else
        MessageBox.Show("No se actulizaron los datos en la lista, porque no se encontro un pago con el ID " + NowPagoId );

      SelectRowWithId( NowPagoId );
      RefreshEstadisticas();
      Datos.SetChanges( "CobroModify" );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    // Obtiene los valores que se pueden modificar
    private void GetModifyValues()
      {
      ModifyCuc = 0m;
      decimal.TryParse( txtModCuc.Text, out ModifyCuc );

      ModifyCup = 0m;
      decimal.TryParse( txtModCup.Text, out ModifyCup );

      ModifyCant = 0m;
      decimal.TryParse( txtModCant.Text, out ModifyCant );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama al oprimir el botón de borrar un pago</summary>
    private void btnDelPago_Click( object sender, EventArgs e )
      {
      if( txtModItem.Text.Length == 0 )
        {
        MessageBox.Show("Seleccione el pago que desea borrar");
        return;
        }

      var ret = MessageBox.Show("Si borra el pago, no quedrá constancia en el sistema\r\n¿Estas seguro?", "Aviso", MessageBoxButtons.YesNo );
      if( ret != DialogResult.Yes ) return;

      var row1 = Datos.tablePagos.FindByid(NowPagoId);
      if( row1 != null )
        row1.Delete();
      else
        MessageBox.Show("No se borro el pago de la base de datos, porque no se encontro un pago con el ID " + NowPagoId);

      var row2 = FindPagoId( NowPagoId );
      if( row2 != null )
        row2.Delete();
      else
        MessageBox.Show("No se borro el pago de la lista, porque no se encontro un pago con el ID " + NowPagoId);

      txtModItem.Text = txtModCant.Text = txtModCuc.Text = txtModCup.Text = txtModComent.Text = "";
      RefreshEstadisticas();
      Datos.SetChanges( "CobroDelete" );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Calcula de estadisticas de las ventas según los datos entrados y las filas seleccionadas</summary>
    public void RefreshEstadisticas()
      {
      decimal SumaCuc=0, SumaCup=0, SumaItems=0;

      var SelRows = (Grid.SelectedRows.Count>1);
      foreach( DataGridViewRow Row in Grid.Rows )
        {
        var Cant = (decimal)Row.Cells[4].Value;
        var Cuc  = (decimal)Row.Cells[6].Value;
        var Cup  = (decimal)Row.Cells[7].Value;

        if( !SelRows || Row.Selected )
          {
          SumaCuc   += Cuc;
          SumaCup   += Cup;
          SumaItems += Cant;
          }
        }

      lbSumaCant.Text = SumaItems.ToString();
      lbSumaCuc.Text = SumaCuc.ToString("0.##");
      lbSumaCup.Text = SumaCup.ToString("0.##");

      var SumaTotal = SumaCuc + Money.Convert( SumaCup, Mnd.Cup, Mnd.Cuc );
      lbSumaTotal.Text = SumaTotal.ToString("0.#") + " cuc";
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando cambia los pagos seleccionados</summary>
    private void Grid_SelectionChanged( object sender, EventArgs e )
      {
      if( Grid.Focused ) RefreshEstadisticas();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando cambia el check Mark de comentarios</summary>
    private void chkShowComent_CheckedChanged( object sender, EventArgs e )
      {
      FillGridPagos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando cambia el texto del filtro de las ventas realizadas</summary>
    private void txtFilter_TextChanged( object sender, EventArgs e )
      {
      FillGridPagos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando cambia el texto del filtro de items a pagar</summary>
    private void txtFilterPago_TextChanged( object sender, EventArgs e )
      {
      FillGridVentas();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Guarda en un fichero todos los items mostrados en la lista</summary>
    private void btnSaveVentas_Click( object sender, EventArgs e )
      {
      var sFootter = "\t\tTotales:\t" + lbSumaCant.Text + "\t\t" + lbSumaCuc.Text + "\t" + lbSumaCup.Text + "\t" + lbSumaTotal.Text + "\t";
      Datos.SaveGrid( Grid, sFootter);
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    // Imprime los resultados en la regilla de los item que faltan por pagar
    private void btnSavePorPagar_Click( object sender, EventArgs e )
      {
      var sFootter = "\t" + lbPorPagar.Text;
      Datos.SaveGrid( GridVentas, sFootter);
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Quita los filtros si existen</summary>
    private void BtnFilterOff_Click(object sender, EventArgs e)
      {
      Datos.RemoveFilter(2);

      btnFilterOff.Hide();
      btnFilterOff2.Hide();

      FillGridPagos();
      FillGridVentas();
      }
    }
  }
