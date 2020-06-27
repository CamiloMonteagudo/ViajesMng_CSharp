using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MngViajes
  {
  public partial class frmMngViajes : Form
    {
    DataTable tbSumario, tbSinVender, tbSinPagar, tbPagos, tbVentas, tbProds;

    public frmMngViajes()
      {
      InitializeComponent();
      }

    #region Eventos
    private void frmMngViajes_Load( object sender, EventArgs e )
      {
      App.CargaViajes();

      FillSuamary();

      GridTotalesSumary.Rows.Add();
      GridTotalesSinVender.Rows.Add();
      GridTotalesSinPagar.Rows.Add();
      GridTotalesPagos.Rows.Add();
      GridTotalesVentas.Rows.Add();
      GridTotalesProducts.Rows.Add();

      TotalesSumary( false );

      cbVendVenta.Items.AddRange( App.Vendedores );
      cbVendVenta.SelectedIndex = 0;

      cbVendPagar.Items.AddRange( App.Vendedores );
      cbVendPagar.SelectedIndex = 0;

      cbVendSinCobrar.Items.AddRange( App.Vendedores );
      cbVendSinCobrar.SelectedIndex = 0;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    // Reacarga todos los datos y actualiza el contenido del tab actual
    private void ReloadDatos()
      {
      App.CargaViajes();

      tbSumario = tbSinVender = tbSinPagar = tbPagos = tbVentas = tbProds = null;

      IntitTabInfo( Tab.SelectedIndex );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se selecciona alguno del los Tab del formulario</summary>
    private void Tab_Selected( object sender, TabControlEventArgs e )
      {
      if( e.Action != TabControlAction.Selected ) return;

      IntitTabInfo( e.TabPageIndex );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    // Actualiza la informacion del tab especificado
    private void IntitTabInfo( int tabIndex )
      {
      var selTab = Tab.TabPages[tabIndex];

           if( selTab==tabSumary    ) { if( tbSumario   == null ) FillSuamary();   }
      else if( selTab==tabSinVender ) { if( tbSinVender == null ) FillSinVender(); }
      else if( selTab==tabSinPagar  ) { if( tbSinPagar == null ) FillSinPagar(); }
      else if( selTab==tabPagos     ) { if( tbPagos     == null ) FillPagos();     }
      else if( selTab==tabVentas    ) { if( tbVentas    == null ) FillVentas();    }
      else if( selTab==tabProductos ) { if( tbProds     == null ) FillProductos(); }
      }

    bool inUpdate = false;
    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cada vez que cambia el filtro en una de las páginas</summary>
    private void filter_TextChanged( object sender, EventArgs e )
      {
      if( inUpdate ) return;
      inUpdate = true;

      var txt = ((TextBox)sender).Text;
      int sw = PROD|SVDR|VENT|SPGR|PAGO;

      txtFindProds.Text     = txt;
      txtFindSinVender.Text = txt;
      txtFindVenta.Text     = txt;
      txtFindSinPagar.Text  = txt;
      txtFindPagar.Text     = txt;

      UpdateNowTab( sw );
      inUpdate = false;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cada vez que cambia la seleccón en una de las tablas</summary>
    private void Grid_SelectionChanged( object sender, EventArgs e )
      {
      if( sender == GridSumary    ) TotalesSumary( true );
      if( sender == GridSinVender ) TotalesSinVender( true );
      if( sender == GridSinPagar  ) TotalesSinCobrar( true );
      if( sender == GridPagos     ) TotalesPagos( true );
      if( sender == GridVentas    ) TotalesVentas( true );
      if( sender == GridProds     ) TotalesProductos( true );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cada vez que cambia la seleccón en una de las tablas</summary>
    private void Grid_ColumnWidthChanged( object sender, DataGridViewColumnEventArgs e )
      {
      if( sender == GridSumary    ) IgualarColumsWidth( GridSumary   , GridTotalesSumary    );
      if( sender == GridSinVender ) IgualarColumsWidth( GridSinVender, GridTotalesSinVender );
      if( sender == GridSinPagar  ) IgualarColumsWidth( GridSinPagar , GridTotalesSinPagar  );
      if( sender == GridPagos     ) IgualarColumsWidth( GridPagos    , GridTotalesPagos     );
      if( sender == GridVentas    ) IgualarColumsWidth( GridVentas   , GridTotalesVentas    );
      if( sender == GridProds     ) IgualarColumsWidth( GridProds    , GridTotalesProducts  );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se da doble click sobre una fila en el grid</summary>
    private void Grid_DoubleClick( object sender, EventArgs e )
      {
      if( sender == GridSumary    ) IrASumario();
      if( sender == GridSinVender ) IrAVender();
      if( sender == GridSinPagar  ) IrACobrar();
      if( sender == GridPagos     ) IrAPagos();
      if( sender == GridVentas    ) IrAVentas();
      if( sender == GridProds     ) IrAProductos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    private void btnIr_Click( object sender, EventArgs e )
      {
      if( sender == btnIrSumario    ) IrASumario();
      if( sender == btnIrASinVender ) IrAVender();
      if( sender == btnIrASinPagar  ) IrACobrar();
      if( sender == btnIrAPagos     ) IrAPagos();
      if( sender == btnIrAVentas    ) IrAVentas();
      if( sender == btnIrAProd      ) IrAProductos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Cuando cambia el checkbox para modificar la forma de mostras los items </summary>
    private void chk_CheckedChanged( object sender, EventArgs e )
      {
      if( sender==chkPagos || sender==chkVentComent ) 
        {
        SaveGridPos( GridVentas );
        FillVentas();
        RestoreGridPos();
        }
      if( sender==chkProdsComent || sender==chkGanacRaw || sender==chkGanancItem ) 
        {
        SaveGridPos( GridProds );
        FillProductos();
        RestoreGridPos();
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    private void btnSave_Click( object sender, EventArgs e )
      {
      if( sender == btnSaveSumario   ) App.SaveGrids( GridSumary   , GridTotalesSumary    );
      if( sender == btnSaveProductos ) App.SaveGrids( GridProds    , GridTotalesProducts  );
      if( sender == btnSavePagos     ) App.SaveGrids( GridPagos    , GridTotalesPagos     );
      if( sender == btnSaveVentas    ) App.SaveGrids( GridVentas   , GridTotalesVentas    );
      if( sender == btnSaveSinPagar  ) App.SaveGrids( GridSinPagar, GridTotalesSinPagar );
      if( sender == btnSaveSinVender ) App.SaveGrids( GridSinVender, GridTotalesSinVender );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Boton para decidir si se va a el tab de precios o de productos </summary>
    private void chkPrecios_CheckedChanged( object sender, EventArgs e )
      {
      btnIrAProd.Text = ( chkPrecios.Checked ) ? "Ir a Precios" : "Ir a Compras";
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    private void cbVend_SelectedIndexChanged( object sender, EventArgs e )
      {
      if( inUpdate ) return;
      inUpdate = true;

      var idx = ((ComboBox)sender).SelectedIndex;
      int sw = VENT|PAGO|SPGR;

      cbVendVenta.SelectedIndex = idx;
      cbVendPagar.SelectedIndex = idx;
      cbVendSinCobrar.SelectedIndex = idx;

      UpdateNowTab( sw );
      inUpdate = false;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Muestra la ganancia sin tener en cuenta los articulos para el consumo </summary>
    private void chkSinConsumo_Click(object sender, EventArgs e)
      {
      FillSuamary();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Pone los colorcitos de los numeros </summary>
    private void GridSumary_CellFormatting( object sender, DataGridViewCellFormattingEventArgs e )
      {
      var col = e.ColumnIndex;
      var row = e.RowIndex;

      if( col == 10 || col == 5 )
        {
        e.CellStyle.Font = new Font( e.CellStyle.Font, FontStyle.Bold );
        }
      else if( col == 11 )
        {
        var val = (decimal)GridSumary[col,row].Value;

        e.CellStyle.ForeColor = ( val<0 ) ? Color.Red : Color.Green;
        e.CellStyle.Font = new Font( e.CellStyle.Font, FontStyle.Bold );
        }
      else if( col == 12 )
        {
        var val = (decimal)GridSumary[col,row].Value;

        e.CellStyle.ForeColor = ( val<1 ) ? Color.Red : ( ( val>=1.5m ) ? Color.Green : Color.Black );
        e.CellStyle.Font = new Font( e.CellStyle.Font, FontStyle.Bold );
        }
      }

    #endregion

    #region Funciones para ir a modificar los datos
    //--------------------------------------------------------------------------------------------------------------------------------------
    DataGridView SavedGrid;
    int          SavedFirst;
    int          SavedSel;
    //--------------------------------------------------------------------------------------------------------------------------------------
    private void SaveGridPos( DataGridView grid )
      {
      SavedGrid  = grid;
      SavedFirst = grid.FirstDisplayedScrollingRowIndex;

      SavedSel = -1;
      if( grid.SelectedRows.Count>0 )
        SavedSel = grid.SelectedRows[0].Index;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    private void RestoreGridPos()
      {
      if( SavedGrid==null ) return;
      if( SavedFirst<0 || SavedFirst>=SavedGrid.Rows.Count ) return;

      SavedGrid.FirstDisplayedScrollingRowIndex = SavedFirst;

      if( SavedSel<0 || SavedSel>=SavedGrid.Rows.Count ) return;

      SavedGrid.Rows[SavedSel].Selected = true;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene el valor de un campo perteneciente a una fila de un grid</summary>
    private object FieldValue( DataGridViewRow GridRow, string FieldName )
      {
      var row    = ((DataRowView)GridRow.DataBoundItem).Row;
      var data   = row.ItemArray;
      var Colums = row.Table.Columns;

      return data[ Colums[FieldName].Ordinal ];
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Llama la aplicación de manejo del viaje, para ver el sumario del viaje seleccionado</summary>
    private void IrASumario()
      {
      var Sel = GridSumary.SelectedRows;
      if( Sel.Count != 1 )
        {
        MessageBox.Show( "Debe de seleccionar la fila del viaje que se desea ver" );
        return;
        }

      var row = GridSumary.SelectedRows[0];
      var idViaje = (int)FieldValue( row, "Num" );

      var Args = "DatosFile="+ App.Viajes[idViaje-1].DBFile +"|Tab=0";

      RunViajeApp( Args, GridSumary );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Llama la aplicación de manejo del viaje para vender el item seleccionado</summary>
    private void IrAVender()
      {
      var Sel = GridSinVender.SelectedRows;
      if( Sel.Count != 1 )
        {
        MessageBox.Show( "Debe de seleccionar la fila del producto que desea vender" );
        return;
        }

      var row = GridSinVender.SelectedRows[0];
      var idViaje = (int)FieldValue( row, "idxViaje" );
      var idProd  = (int)FieldValue( row, "ID" );

      var path = App.Viajes[idViaje].DBFile;
      var Args = "DatosFile="+ path +"|Tab=5|ProdID="+idProd+"|New=1";

      RunViajeApp( Args, GridSinVender );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Llama la aplicación de manejo del viaje para cobar el item seleccionado</summary>
    private void IrACobrar()
      {
      var Sel = GridSinPagar.SelectedRows;
      if( Sel.Count != 1 )
        {
        MessageBox.Show( "Debe de seleccionar la fila del producto que desea cobrar" );
        return;
        }

      var row = GridSinPagar.SelectedRows[0];

      var idViaje = (int)FieldValue( row, "ViajeID" );
      var idVenta = (int)FieldValue( row, "IdVent"  );
      var idProd  = (int)FieldValue( row, "IdProd"  );

      var BD   = App.Viajes[idViaje].DBFile;
      var Args = "DatosFile="+ BD +"|Tab=6|ProdID="+idProd+"|VentID="+idVenta+"|New=1";

      RunViajeApp( Args, GridSinPagar );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Llama la aplicación de manejo del viaje para cobar el item seleccionado</summary>
    private void IrAPagos()
      {
      var Sel = GridPagos.SelectedRows;
      if( Sel.Count != 1 )
        {
        MessageBox.Show( "Debe de seleccionar la fila del producto que desea cobrar" );
        return;
        }

      var row = GridPagos.SelectedRows[0];

      var idViaje = (int)FieldValue( row, "idxViaje" );
      var idPago  = (int)FieldValue( row, "IdPago"   );
      var idProd  = (int)FieldValue( row, "IdProd"   );
      var idVent  = (int)FieldValue( row, "IdVent"   );

      var BD   = App.Viajes[idViaje].DBFile;;
      var Args = "DatosFile="+ BD +"|Tab=6|ProdID="+idProd+"|VentID="+idVent+"|PagoID="+idPago;

      RunViajeApp( Args, GridPagos );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Llama la aplicación de manejo del viaje para cobar el item seleccionado</summary>
    private void IrAVentas()
      {
      var Sel = GridVentas.SelectedRows;
      if( Sel.Count != 1 )
        {
        MessageBox.Show( "Debe de seleccionar la fila de la venta que desea nodificar" );
        return;
        }

      var row = GridVentas.SelectedRows[0];

      var iViaje = (int)FieldValue( row, "idxViaje" );
      var idProd = (int)FieldValue( row, "IdProd"   );
      var idVent = (int)FieldValue( row, "IdVent"   );

      var BD   = App.Viajes[iViaje].DBFile;
      var Args = "DatosFile="+ BD +"|Tab=5|ProdID="+idProd+"|VentID="+idVent;

      RunViajeApp( Args, GridVentas );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Llama la aplicación de manejo del viaje para ver el producto seleccionado</summary>
    private void IrAProductos()
      {
      var Sel = GridProds.SelectedRows;
      if( Sel.Count != 1 )
        {
        MessageBox.Show( "Debe de seleccionar la fila del producto que desea ver" );
        return;
        }

      var row = GridProds.SelectedRows[0];

      var iViaje = (int)FieldValue( row, "idxViaje" );
      var idProd = (int)FieldValue( row, "idItem"   );

      var nTab = (chkPrecios.Checked)? "4" : "3";
      var BD   = App.Viajes[iViaje].DBFile;
      var Args = "DatosFile="+ BD +"|Tab="+nTab+"|ProdID="+idProd;

      RunViajeApp( Args, GridProds );
      }

    #endregion

    #region Funciones para llenar los grids
    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea la tabla de sumario y le pone su contenido</summary>
    private void FillSuamary()
      {
      tbSumario = CreateTableSumary();
      int  nViajes = App.Viajes.Count;

      for( int i = 0; i<nViajes; ++i )
        {
        if( App.FilterFor(i) ) continue;

        var viaje = App.Viajes[i];
        var PrecioIndex = 0m;
        if( viaje.CompasCUC != 0 ) PrecioIndex = viaje.MontoPrecios/viaje.CompasCUC;

        var Inversion = viaje.MontoInvers;
        if( chkSinConsumo.Checked ) Inversion -= viaje.MontoConsumo;

        var Ganancia = viaje.MontoCobros - Inversion;

        var GananciaIndex = 0m;
        if( Inversion != 0 )
          GananciaIndex = viaje.MontoCobros/ Inversion;

        tbSumario.Rows.Add( i+1,                                          // Número de orden del viaje
                            viaje.Title,                                  // Viaje
                            viaje.GastosCUC,                              // Gastos
                            viaje.CompasCUC,                              // Compras
                            viaje.RecupIdx,                               // Indice de Gastos
                            viaje.MontoInvers,                            // Inversion
                            viaje.MontoConsumo,                           // Consumo
                            PrecioIndex,                                  // Indice de precios
                            viaje.MontoSinVender,                         // XVender
                            viaje.MontoSinPagar,                          // XCobar
                            viaje.MontoCobros,                            // Cobrado
                            Ganancia,                                     // Ganancia
                            GananciaIndex                                 // Indice de Ganancia
                            );

        }

      GridSumary.AutoGenerateColumns = false;
      GridSumary.DataSource = tbSumario;
      GridSumary.ClearSelection();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Rellena la tabla de productos sin vender</summary>
    private void FillSinVender()
      {
      var sFilter = txtFindSinVender.Text;

      if( tbSinVender==null )  tbSinVender = CreateTableProdsSinVender();
      else                     tbSinVender.Clear();

      for( int i = 0; i<App.Viajes.Count; ++i )
        {
        if( App.FilterFor(i) ) continue;

        var viaje = App.Viajes[i];
        viaje.FillTableProdsSinVender( tbSinVender, sFilter );
        }

      GridSinVender.AutoGenerateColumns = false;
      GridSinVender.DataSource = tbSinVender;
      GridSinVender.ClearSelection();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Rellena la tabla de productos sin cobrar</summary>
    private void FillSinPagar()
      {
      var sFilter = txtFindSinPagar.Text;

      if( tbSinPagar==null ) tbSinPagar = CreateTableProdsSinCobrar();
      else tbSinPagar.Clear();

      var idx   = cbVendSinCobrar.SelectedIndex;
      var sVent = (idx<=0)? "" : cbVendSinCobrar.Items[idx].ToString();

      for( int i=0; i<App.Viajes.Count; ++i )
        {
        if( App.FilterFor(i) ) continue;

        var viaje = App.Viajes[i];
        viaje.FillVentasSinCobrar( tbSinPagar, sFilter, sVent );
        }

      GridSinPagar.AutoGenerateColumns = false;
      GridSinPagar.DataSource = tbSinPagar;
      GridSinPagar.ClearSelection();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Rellena la tabla de pagos realizados</summary>
    private void FillPagos()
      {
      var sFilter = txtFindPagar.Text;

      if( tbPagos==null ) tbPagos = CreateTablePagos();
      else tbPagos.Clear();

      var idx   = cbVendPagar.SelectedIndex;
      var sVent = (idx<=0)? "" : cbVendPagar.Items[idx].ToString();

      for( int i = 0; i<App.Viajes.Count; ++i )
        {
        if( App.FilterFor(i) ) continue;
        App.Viajes[i].FillTablePagos( tbPagos, sFilter, sVent );
        }

      GridPagos.AutoGenerateColumns = false;
      GridPagos.DataSource = tbPagos;
      GridPagos.ClearSelection();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Rellena la tabla de las ventas realizadas</summary>
    private void FillVentas()
      {
      var sFilter = txtFindVenta.Text;

      if( tbVentas==null ) { tbVentas = CreateTableVentas(); }
      else tbVentas.Clear();

      var idx  = cbVendVenta.SelectedIndex;
      var sVent = (idx<=0)? "" : cbVendVenta.Items[idx].ToString(); 
      int sw = 0;
      if( chkVentComent.Checked ) sw |= 0x01;
      if( chkPagos.Checked      ) sw |= 0x02;


      for( int i = 0; i<App.Viajes.Count; ++i )
        {
        if( App.FilterFor(i) ) continue;

        App.Viajes[i].FillTableVentas( tbVentas, sFilter, sVent, sw );
        }

      GridVentas.AutoGenerateColumns = false;
      GridVentas.DataSource = tbVentas;
      GridVentas.ClearSelection();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Rellena la tabla de los productos comprados</summary>
    private void FillProductos()
      {
      var sFilter = txtFindProds.Text;

      if( tbProds==null ) { tbProds = CreateTableProducts(); }
      else tbProds.Clear();

      int sw = 0;
      if( chkProdsComent.Checked ) sw |= 0x01;
      if( chkGanacRaw.Checked    ) sw |= 0x04;
      if( chkGanancItem.Checked  ) sw |= 0x08;

      for( int i = 0; i<App.Viajes.Count; ++i )
        {
        if( App.FilterFor(i) ) continue;

        App.Viajes[i].FillTableProducts( tbProds, sFilter, sw );
        }

      GridProds.AutoGenerateColumns = false;
      GridProds.DataSource = tbProds;
      GridProds.ClearSelection();
      }

    #endregion

    #region Creacion de tablas
    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea con los datos del sumario</summary>
    private DataTable CreateTableSumary()
      {
      DataTable tb = new DataTable("ViewSumario");

      tb.Columns.Add( "Num"      , typeof( int     ) );
      tb.Columns.Add( "Viaje"    , typeof( String  ) );
      tb.Columns.Add( "Gastos"   , typeof( decimal ) );
      tb.Columns.Add( "Compras"  , typeof( decimal ) );
      tb.Columns.Add( "IdxComp"  , typeof( decimal ) );
      tb.Columns.Add( "Inversion", typeof( decimal ) );
      tb.Columns.Add( "Consumo"  , typeof( decimal ) );
      tb.Columns.Add( "idxPrec"  , typeof( decimal ) );
      tb.Columns.Add( "XVender"  , typeof( decimal ) );
      tb.Columns.Add( "XCobrar"  , typeof( decimal ) );
      tb.Columns.Add( "Cobrado"  , typeof( decimal ) );
      tb.Columns.Add( "Ganancia" , typeof( decimal ) );
      tb.Columns.Add( "IdxGan"   , typeof( decimal ) );

      return tb;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea una tabla vacia para los productos sin vender</summary>
    private DataTable CreateTableProdsSinVender()
      {
      DataTable tb = new DataTable("SinVender");

      tb.Columns.Add( "idxViaje" , typeof(int    ) );
      tb.Columns.Add( "Num"      , typeof(int    ) );
      tb.Columns.Add( "Viaje"    , typeof(String ) );
      tb.Columns.Add( "ID"       , typeof(int    ) );
      tb.Columns.Add( "Item"     , typeof(String ) );
      tb.Columns.Add( "Cant"     , typeof(String ) );
      tb.Columns.Add( "Costo"    , typeof(String ) );
      tb.Columns.Add( "PrecioRec", typeof(String ) );
      tb.Columns.Add( "PrecioOK" , typeof(String ) );
      tb.Columns.Add( "Precio"   , typeof(String ) );
      tb.Columns.Add( "Monto"    , typeof(String ) );
      tb.Columns.Add( "Total"    , typeof(decimal) );

      return tb;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea una tabla vacia para los productos sin cobrar</summary>
    private DataTable CreateTableProdsSinCobrar()
      {
      DataTable tb = new DataTable("SinCobrar");

      tb.Columns.Add( "ViajeID", typeof( int     ) );
      tb.Columns.Add( "Num"    , typeof( Int32   ) );
      tb.Columns.Add( "IdVent" , typeof( Int32   ) );
      tb.Columns.Add( "Viaje"  , typeof( String  ) );
      tb.Columns.Add( "Item"   , typeof( String  ) );
      tb.Columns.Add( "Vend"   , typeof( String  ) );
      tb.Columns.Add( "Cant"   , typeof( String  ) );
      tb.Columns.Add( "Precio" , typeof( String  ) );
      tb.Columns.Add( "Monto"  , typeof( String  ) );
      tb.Columns.Add( "Total"  , typeof( decimal ) );
      tb.Columns.Add( "Pagado" , typeof( String  ) );
      tb.Columns.Add( "IdProd" , typeof( Int32   ) );

      return tb;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea una tabla vacia para mostrar los pagos realizados</summary>
    private DataTable CreateTablePagos()
      {
      DataTable tb = new DataTable("ViewCobros");

      tb.Columns.Add( "idxViaje", typeof( int      ) );
      tb.Columns.Add( "Num"     , typeof( Int32    ) );
      tb.Columns.Add( "IdPago"  , typeof( Int32    ) );
      tb.Columns.Add( "Viaje"   , typeof( String   ) );
      tb.Columns.Add( "Item"    , typeof( String   ) );
      tb.Columns.Add( "Vend"    , typeof( String   ) );
      tb.Columns.Add( "Cant"    , typeof( decimal  ) );
      tb.Columns.Add( "Precio"  , typeof( String   ) );
      tb.Columns.Add( "Cuc"     , typeof( decimal  ) );
      tb.Columns.Add( "Cup"     , typeof( decimal  ) );
      tb.Columns.Add( "Total"   , typeof( decimal  ) );
      tb.Columns.Add( "Fecha"   , typeof( DateTime ) );
      tb.Columns.Add( "IdProd"  , typeof( Int32    ) );
      tb.Columns.Add( "IdVent"  , typeof( Int32    ) );

      return tb;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea una tabla vacia para mostrar las ventas</summary>
    private DataTable CreateTableVentas()
      {
      DataTable tb = new DataTable("ViewVentas");

      tb.Columns.Add( "idxViaje", typeof( int      ) );
      tb.Columns.Add( "Num"     , typeof( Int32    ) );
      tb.Columns.Add( "IdVent"  , typeof( Int32    ) );
      tb.Columns.Add( "Viaje"   , typeof( String   ) );
      tb.Columns.Add( "Item"    , typeof( String   ) );
      tb.Columns.Add( "Vend"    , typeof( String   ) );
      tb.Columns.Add( "Cant"    , typeof( Int32    ) );
      tb.Columns.Add( "Precio"  , typeof( decimal  ) );
      tb.Columns.Add( "Cuc"     , typeof( decimal  ) );
      tb.Columns.Add( "Cup"     , typeof( decimal  ) );
      tb.Columns.Add( "Total"   , typeof( decimal  ) );
      tb.Columns.Add( "Fecha"   , typeof( DateTime ) );
      tb.Columns.Add( "IdProd"  , typeof( Int32    ) );

      //idxViaje,Num,IdVent,Viaje,Item,Vend,Cant,Precio,Cuc,Cup,Total,Fecha
      return tb;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea una tabla vacia para mostrar las ventas</summary>
    private DataTable CreateTableProducts()
      {
      DataTable tb = new DataTable("ViewCompras");

      tb.Columns.Add( "idxViaje", typeof( int     ) );
      tb.Columns.Add( "Num"     , typeof( Int32   ) );
      tb.Columns.Add( "idItem"  , typeof( Int32   ) );
      tb.Columns.Add( "Viaje"   , typeof( String  ) );
      tb.Columns.Add( "Item"    , typeof( String  ) );
      tb.Columns.Add( "count"   , typeof( Int32   ) );
      tb.Columns.Add( "value"   , typeof( String  ) );
      tb.Columns.Add( "valCUC"  , typeof( decimal ) );
      tb.Columns.Add( "precio"  , typeof( String  ) );
      tb.Columns.Add( "monto"   , typeof( String  ) );
      tb.Columns.Add( "montoCUC", typeof( decimal ) );
      tb.Columns.Add( "rate"    , typeof( decimal ) );
      tb.Columns.Add( "ganancia", typeof( decimal ) );


      //idxViaje,Num,idItem,Viaje,item,count,value,valCUC,precio,monto,montoCUC,rate,ganancia
      return tb;
      }

    #endregion

    #region Procesamiento de totales
    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Calcula las cantidades totales del sumario </summary>
    private void TotalesSumary( bool sel ) 
      {
      if( GridTotalesSumary.Rows.Count <= 0 ) return;
      if( GridSumary.Rows.Count < tbSumario.Rows.Count ) return;

      var nSel = GridSumary.SelectedRows.Count;
      if( !sel ) nSel = 0;

      var sumaGastosCUC      = 0m;                                  // Gastos
      var sumaCompasCUC      = 0m;                                  // Sumatoria de Compras
      var sumaRecupIdx       = 0m;                                  // Sumatoria de Indice de Gastos
      var sumaMontoInvers    = 0m;                                  // Sumatoria de Inversion
      var sumaMontoConsumo   = 0m;                                  // Sumatoria de Consumo
      var sumaPrecioIndex    = 0m;                                  // Sumatoria de Indice de precios
      var sumaMontoSinVender = 0m;                                  // Sumatoria de XVender
      var sumaMontoSinPagar  = 0m;                                  // Sumatoria de XCobar
      var sumaMontoCobros    = 0m;                                  // Sumatoria de Cobrado
      var sumaGanancia       = 0m;                                  // Sumatoria de las ganancias
      var sumaGananciaIndex  = 0m;                                  // Sumatoria de Indice de Ganancia

      int nRows = 0;
      foreach( DataGridViewRow Row in GridSumary.Rows )
        {
        if( nSel>1 && !Row.Selected ) continue;

        sumaGastosCUC      += (decimal)Row.Cells[ 2].Value;
        sumaCompasCUC      += (decimal)Row.Cells[ 3].Value;
        sumaRecupIdx       += (decimal)Row.Cells[ 4].Value;
        sumaMontoInvers    += (decimal)Row.Cells[ 5].Value;
        sumaMontoConsumo   += (decimal)Row.Cells[ 6].Value;
        sumaPrecioIndex    += (decimal)Row.Cells[ 7].Value;
        sumaMontoSinVender += (decimal)Row.Cells[ 8].Value;
        sumaMontoSinPagar  += (decimal)Row.Cells[ 9].Value;
        sumaMontoCobros    += (decimal)Row.Cells[10].Value;
        sumaGanancia       += (decimal)Row.Cells[11].Value;
        sumaGananciaIndex  += (decimal)Row.Cells[12].Value;

        ++nRows;
        }

      if( nRows==0 ) return;
      var cells = GridTotalesSumary.Rows[0].Cells;

      cells[ 1].Value = "TOTALES:";
      cells[ 2].Value = sumaGastosCUC;
      cells[ 3].Value = sumaCompasCUC;
      cells[ 4].Value = sumaRecupIdx/nRows;
      cells[ 5].Value = sumaMontoInvers;
      cells[ 6].Value = sumaMontoConsumo;
      cells[ 7].Value = sumaPrecioIndex/nRows;
      cells[ 8].Value = sumaMontoSinVender;
      cells[ 9].Value = sumaMontoSinPagar;
      cells[10].Value = sumaMontoCobros;
      cells[11].Value = sumaGanancia;
      cells[12].Value = sumaGananciaIndex/nRows;

      GridTotalesSumary.ClearSelection();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Calcula las cantidades totales de los productos sin vender </summary>
    private void TotalesSinVender( bool sel )
      {
      if( GridTotalesSinVender.Rows.Count <= 0 ) return;
      if( GridSinVender.Rows.Count < tbSinVender.Rows.Count ) return;

      var nSel = GridSinVender.SelectedRows.Count;
      if( !sel ) nSel = 0;

      var TotalItems = 0f;
      var TotalMonto = 0m;

      foreach( DataGridViewRow Row in GridSinVender.Rows )
        {
        if( nSel>1 && !Row.Selected ) continue;

        TotalItems += ParseCantidad( (string)Row.Cells[5].Value );
        TotalMonto += (decimal)Row.Cells[11].Value;
        }

      var cells = GridTotalesSinVender.Rows[0].Cells;

      cells[4].Value = "Totales";
      cells[5].Value = TotalItems;
      cells[11].Value = TotalMonto;

      GridTotalesSinVender.ClearSelection();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Calcula las cantidades totales de los productos sin cobrar </summary>
    private void TotalesSinCobrar( bool sel )
      {
      if( GridTotalesSinPagar.Rows.Count <= 0 ) return;
      if( GridSinPagar.Rows.Count < tbSinPagar.Rows.Count ) return;

      var nSel = GridSinPagar.SelectedRows.Count;
      if( !sel ) nSel = 0;

      var TotalItems = 0f;
      var TotalMonto = 0m;

      foreach( DataGridViewRow Row in GridSinPagar.Rows )
        {
        if( nSel>1 && !Row.Selected ) continue;

        TotalItems += ParseCantidad( (string)Row.Cells[6].Value );
        TotalMonto += (decimal)Row.Cells[9].Value;
        }

      var cells = GridTotalesSinPagar.Rows[0].Cells;

      cells[4].Value = "Totales";
      cells[6].Value = TotalItems;
      cells[9].Value = TotalMonto;

      GridTotalesSinPagar.ClearSelection();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Calcula las cantidades totales para los pagos </summary>
    private void TotalesPagos( bool sel )
      {
      if( GridTotalesPagos.Rows.Count <= 0 ) return;
      if( GridPagos.Rows.Count < tbPagos.Rows.Count ) return;

      var nSel = GridPagos.SelectedRows.Count;
      if( !sel ) nSel = 0;

      var TotalItems = 0m;
      var MontoCUC   = 0m;
      var MontoCUP   = 0m;
      var MontoTotal = 0m;

      foreach( DataGridViewRow Row in GridPagos.Rows )
        {
        if( nSel>1 && !Row.Selected ) continue;

        TotalItems += (decimal)Row.Cells[ 6].Value;
        MontoCUC   += (decimal)Row.Cells[ 8].Value;
        MontoCUP   += (decimal)Row.Cells[ 9].Value;
        MontoTotal += (decimal)Row.Cells[10].Value;
        }

      var cells = GridTotalesPagos.Rows[0].Cells;

      cells[4].Value  = "Totales";
      cells[6].Value  = TotalItems;
      cells[8].Value  = MontoCUC;
      cells[9].Value  = MontoCUP;
      cells[10].Value = MontoTotal;

      GridTotalesPagos.ClearSelection();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Calcula las cantidades totales para las ventas </summary>
    private void btnUpdateNames_Click( object sender, EventArgs e )
      {
      MngItemsName.UpdateNames();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Aplica/Libera el filtro por viaje</summary>
    private DataGridViewRow GetGridRow( DataGridView grid, bool showMsg=true )
      {
      var rows = grid.SelectedRows;
      if( rows==null || rows.Count==0 )
        {
        if( showMsg )
          MessageBox.Show( "Seleccione un item para obtener información" );
        return null;
        }

      return rows[0];
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Actualiza el Tab actual</summary>
    const int SUMA = 0x0001;
    const int PROD = 0x0002;
    const int VENT = 0x0004;
    const int PAGO = 0x0008;
    const int SVDR = 0x0010;
    const int SPGR = 0x0020;
    const int ALL  = SUMA|PROD|VENT|PAGO|SVDR|SPGR;
    private void UpdateNowTab( int sw=ALL )
      {
      if( (sw&SUMA)!=0 ) tbSumario   = null; 
      if( (sw&SVDR)!=0 ) tbSinVender = null; 
      if( (sw&SPGR)!=0 ) tbSinPagar = null; 
      if( (sw&PAGO)!=0 ) tbPagos     = null; 
      if( (sw&VENT)!=0 ) tbVentas    = null; 
      if( (sw&PROD)!=0 ) tbProds     = null;

      var tab = Tab.SelectedTab;

           if( tab==tabSumary   ) FillSuamary();
      else if( tab==tabProductos) FillProductos();
      else if( tab==tabSinVender) FillSinVender();
      else if( tab==tabVentas   ) FillVentas();
      else if( tab==tabSinPagar ) FillSinPagar();
      else if( tab==tabPagos    ) FillPagos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Muestra la información sobre los filtros activos</summary>
    private void ShowFilterInfo()
      {
      Viaje vj = null;

      btnFilterViaje.Visible = false;
      btnFilterProd.Visible = false;
      btnFilterVenta.Visible = false;

      if( App.FilterViaje >= 0 )
        { 
        vj = App.Viajes[App.FilterViaje];
        var title = vj.Title;
        var ifind = title.IndexOf("  ");
        if( ifind>=0 ) title = title.Substring(0,ifind);

        btnFilterViaje.Text = title;
        btnFilterViaje.Visible = true;
        }

      if( App.FilterProd >= 0 )
        {
        btnFilterProd.Text = vj.ProdDesc( App.FilterProd );
        btnFilterProd.Visible = true;
        }

      if( App.FilterVenta >= 0 )
        {
        btnFilterVenta.Text = vj.VentDesc( App.FilterVenta );
        btnFilterVenta.Visible = true;
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama para filtrar por la fila seleccionada en el grid activo</summary>
    private void BtnFilterGrid_Click(object sender, EventArgs e)
      {
      DataGridViewRow row;

      var tab = Tab.SelectedTab;

      if( tab==tabSumary   )
        {
        row = GetGridRow( GridSumary );
        if( row == null ) return;

        App.FilterViaje = (int)FieldValue( row, "Num" )-1;
        }
      else if( tab==tabProductos)
        {
        row = GetGridRow( GridProds );
        if( row == null ) return;

        App.FilterViaje = (int)FieldValue( row, "idxViaje" );
        App.FilterProd  = (int)FieldValue( row, "idItem"   );
        }
      else if( tab==tabSinVender)
        {
        row = GetGridRow( GridSinVender );
        if( row == null ) return;

        App.FilterViaje = (int)FieldValue( row, "idxViaje" );
        App.FilterProd  = (int)FieldValue( row, "ID" );
        }
      else if( tab==tabVentas   )
        {
        row = GetGridRow( GridVentas );
        if( row == null ) return;

        App.FilterViaje = (int)FieldValue( row, "idxViaje" );
        App.FilterProd  = (int)FieldValue( row, "IdProd"   );
        App.FilterVenta = (int)FieldValue( row, "IdVent"   );
        }
      else if( tab==tabSinPagar)
        {
        row = GetGridRow( GridSinPagar );
        if( row == null ) return;

        App.FilterViaje = (int)FieldValue( row, "ViajeID" );
        App.FilterProd  = (int)FieldValue( row, "IdProd"  );
        App.FilterVenta = (int)FieldValue( row, "IdVent"  );
        }
      else if( tab==tabPagos    )
        {
        row = GetGridRow( GridPagos );
        if( row == null ) return;

        App.FilterViaje = (int)FieldValue( row, "idxViaje" );
        App.FilterProd  = (int)FieldValue( row, "IdProd"   );
        App.FilterVenta = (int)FieldValue( row, "IdVent"   );
        }

      UpdateNowTab();
      ShowFilterInfo();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Quita el filtro por viaje</summary>
    private void BtnFilterViaje_Click(object sender, EventArgs e)
      {
      App.DelFilterViaje();
      UpdateNowTab();
      ShowFilterInfo();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Quita el filtro por producto</summary>
    private void BtnFilterProd_Click(object sender, EventArgs e)
      {
      App.DelFilterProd();
      UpdateNowTab();
      ShowFilterInfo();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Quita el filtro por venta</summary>
    private void BtnFilterVenta_Click(object sender, EventArgs e)
      {
      App.DelFilterVenta();
      UpdateNowTab();
      ShowFilterInfo();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene el nombre del producto seleccionado y lo pone el cuadro de busqueda</summary>
    private void BtnGetNameProd_Click(object sender, EventArgs e)
      {
      DataGridView grid=GridProds; TextBox findEd=txtFindProds;

      if( sender==btnGetNameSVender ) { grid=GridSinVender; findEd=txtFindSinVender; }
      if( sender==btnGetNameVenta   ) { grid=GridVentas   ; findEd=txtFindVenta;     }
      if( sender==btnGetNameSnPagar ) { grid=GridSinPagar ; findEd=txtFindSinPagar;  }
      if( sender==btnGetNamePagar   ) { grid=GridPagos    ; findEd=txtFindPagar;     }

      var row = GetGridRow( grid, false );
      if( row != null )
        {
        var sItem = (string)FieldValue( row, "Item" );
        findEd.Text = sItem;
        }
      else
        {
        var segms = findEd.Text.Split(' ');
        findEd.Text = string.Join( " ", segms, 0, segms.Length-1 );
        }

      findEd.SelectionStart = findEd.Text.Length;
      findEd.Focus();
      grid.ClearSelection();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Calcula las cantidades totales para las ventas </summary>
    private void TotalesVentas( bool sel )
      {
      if( GridTotalesVentas.Rows.Count <= 0 ) return;
      if( GridVentas.Rows.Count < tbVentas.Rows.Count ) return;

      var nSel = GridVentas.SelectedRows.Count;
      if( !sel ) nSel = 0;

      var TotalItems = 0;
      var MontoCUC   = 0m;
      var MontoCUP   = 0m;
      var MontoTotal = 0m;

      foreach( DataGridViewRow Row in GridVentas.Rows )
        {
        if( nSel>1 && !Row.Selected ) continue;

        TotalItems += (int)Row.Cells[6].Value;
        MontoCUC   += (decimal)Row.Cells[8].Value;
        MontoCUP   += (decimal)Row.Cells[9].Value;
        MontoTotal += (decimal)Row.Cells[10].Value;
        }

      var cells = GridTotalesVentas.Rows[0].Cells;

      cells[4].Value  = "Totales";
      cells[6].Value  = TotalItems;
      cells[8].Value  = MontoCUC;
      cells[9].Value  = MontoCUP;
      cells[10].Value = MontoTotal;

      GridTotalesVentas.ClearSelection();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Calcula las cantidades totales para las ventas </summary>
    private void TotalesProductos( bool sel )
      {
      if( GridTotalesProducts.Rows.Count <= 0 ) return;
      if( GridProds.Rows.Count < tbProds.Rows.Count ) return;

      var nSel = GridProds.SelectedRows.Count;
      if( !sel ) nSel = 0;

      var TotalItems = 0;
      var MontoCUC   = 0m;
      var SumRate    = 0m;
      var SumGananc  = 0m;

      int n = 0;
      foreach( DataGridViewRow Row in GridProds.Rows )
        {
        if( nSel>1 && !Row.Selected ) continue;

        TotalItems += (int)Row.Cells[5].Value;
        MontoCUC   += (decimal)Row.Cells[10].Value;
        SumRate    += (decimal)Row.Cells[11].Value;
        SumGananc  += (decimal)Row.Cells[12].Value;

        ++n;
        //0       ,1  ,2     ,3    ,4   ,5    ,6    ,7     ,8     ,9    ,10      ,11  ,12
        //idxViaje,Num,idItem,Viaje,item,count,value,valCUC,precio,monto,montoCUC,rate,ganancia
        }

      var cells = GridTotalesProducts.Rows[0].Cells;

      cells[4].Value  = "Totales";
      cells[5].Value  = TotalItems;

      cells[10].Value = MontoCUC;
      cells[10].ValueType = typeof( decimal );

      cells[11].Value = (n==0)? 1 : SumRate/n;
      cells[11].ValueType = typeof( decimal);

      cells[12].Value = SumGananc;
      if( chkGanancItem.Checked ) cells[12].Value = "";
      cells[12].ValueType = typeof( decimal );

      GridTotalesProducts.ClearSelection();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Ejecuta el programa para modificar los datos </summary>
    private void RunViajeApp( string args, DataGridView grid )
      {
      SaveGridPos( grid );

      Process progam = new Process();

      progam.StartInfo.FileName = "UnViaje.exe";
      progam.StartInfo.Arguments = args;
      progam.StartInfo.UseShellExecute = false;
      //progam.StartInfo.WorkingDirectory = path;
      progam.Start();

      progam.WaitForExit();

      var chg = new LastChanges();
      if( chg.Changed() )
        {
        ReloadDatos();
        RestoreGridPos();

        if( chg.Changed("Compra") )
          MngItemsName.UpdateNames();
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Iguala el ancho de todas la columnas de dos grids</summary>
    private void IgualarColumsWidth( DataGridView gridSrc, DataGridView gridDes )
      {
      for( int i = 0; i<gridSrc.Columns.Count; ++i )
        if( i<gridDes.Columns.Count )
          gridDes.Columns[i].Width = gridSrc.Columns[i].Width;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Parsea la cantidas en una cadena de texto en el formato cant/total </summary>
    private float ParseCantidad( string sCant )
      {
      var Parts = sCant.Split('|');

      float Cant = 0;
      float.TryParse( Parts[0], out Cant );
      return Cant;
      }

    #endregion

    //--------------------------------------------------------------------------------------------------------------------------------------
    }
  }
