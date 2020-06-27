using System;
using System.Data;
using System.Windows.Forms;
using static UnViaje.DBViaje;

namespace UnViaje
  {
  public partial class ctlPrecios : UserControl
    {
    DBViaje.ComprasDataTable table;
    private DataTable tbCompras;

    private Mnd     nowMond;
    private decimal nowPrec;
    private int     nowCant;
    private decimal nowMonto;
    private decimal nowGanc;
    private double  nowRate;

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    public ctlPrecios()
      {
      InitializeComponent();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void ctlPrecios_Load( object sender, EventArgs e )
      {
      if( Datos.BD == null ) return;

      table = Datos.tableCompras;

      ((frmUnViaje)Parent.Parent.Parent).AcceptButton = btnModify;

      SetMoney( 0 );

      tbCompras = CreateTableCompras();
      Grid.AutoGenerateColumns = false;
      Grid.DataSource = tbCompras;

      FillGridCompras();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    // Obtiene los parametros que se pueden pasar como argumento a la aplicación
    private void ExecParametros()
      {
      btnFilterOff.Visible  = Datos.HasFilter(1);

      int iTab = Datos.GetIntParam( "Tab" );                        // Tab pasado como parametro
      if( iTab != 4 ) return;                                       // Si no es el actual no hace nada

      foreach( DataGridViewRow row in Grid.Rows )
        {
        if( Datos.F_ProdID != (int)row.Cells[0].Value ) continue;

        row.Selected = true;
        OnSelectProduct( Datos.F_ProdID );
        Grid.FirstDisplayedScrollingRowIndex = row.Index;
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea una tabla para mostrar los datos de las compras</summary>
    private DataTable CreateTableCompras()
      {
      DataTable tb = new DataTable("ShowCompras");

      tb.Columns.Add( "id"        , typeof( Int32   ) );
      tb.Columns.Add( "item"      , typeof( String  ) );
      tb.Columns.Add( "count"     , typeof( Int32   ) );
      tb.Columns.Add( "valCUC"    , typeof( decimal ) );
      tb.Columns.Add( "valCucItem", typeof( decimal ) );
      tb.Columns.Add( "precio"    , typeof( String  ) );
      tb.Columns.Add( "monto"     , typeof( decimal ) );
      tb.Columns.Add( "rate"      , typeof( decimal ) );
      tb.Columns.Add( "ganancia"  , typeof( decimal ) );

      return tb;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Llena el Grid con las compras disponibles</summary>
    private void FillGridCompras()
      {
      var ratioRecp = 1m;
      if( Datos.CompasCUC != 0 )
        ratioRecp = Datos.MontoInvers / Datos.CompasCUC;      // Relación entre en monto de la inversión y el de las compras

      var sFilter = txtFilter.Text.ToLower().Trim();

      tbCompras.Clear();
      foreach( ComprasRow row in table )
        {
        if( Datos.FilterProd(row.id) ) continue;
        RowCalculate( row );

        var sItem = row.item;

        if( chkShowComent.Checked && !row.IscomentarioNull() && row.comentario.Trim().Length>0 )
          sItem = sItem + " | " + row.comentario;

        if( sFilter.Length>0 && !sItem.ToLower().Contains(sFilter) )
          continue;

        var sPrecio = row.precio.ToString("0.##");
        if( nowPrec != 0 )  sPrecio += ' ' + Money.Code( nowMond ); 

        decimal PrecCUC = Money.Convert( row.precio, nowMond, Mnd.Cuc); 
        decimal rawRate = 100;
        if( row.valCucItem != 0 )
          rawRate = PrecCUC / row.valCucItem;

        tbCompras.Rows.Add( row.id, sItem, row.count, row.valCUC, row.valCucItem, sPrecio, nowMonto, rawRate, nowGanc );
//        tbCompras.Rows.Add( row.id, sItem, row.count, row.valCUC, row.valCucItem, sPrecio, nowMonto, nowRate, nowGanc );
        }

      Sumatorias();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Calcula valores de la fila</summary>
    private void RowCalculate( ComprasRow row )
      {
      var ratioRecp = 1m;
      if( Datos.CompasCUC != 0 )
        ratioRecp = Datos.MontoInvers / Datos.CompasCUC;      // Relación entre en monto de la inversión y el de las compras

      var PrecRecp  = ratioRecp * row.valCucItem;             // Precio de recueración de la inversión

      nowMond = (Mnd)row.moneda;
      nowPrec = row.precio;
      nowCant = row.count;

      if( nowMond != Mnd.Cuc )
        nowPrec = Money.Convert(nowPrec, nowMond, Mnd.Cuc );

      nowMonto = nowPrec * nowCant;                           // Valor del venta completa (en cuc)
      nowGanc  = (nowCant*nowPrec) - (nowCant*PrecRecp);      // Ganancia neta

      if(PrecRecp!=0 ) nowRate = (double)(nowPrec/PrecRecp);  // Relación entre el precio y el precio de recuperación
      else             nowRate = (double) nowPrec;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Refreca las estadisticas relacionadas con los precios</summary>
    public void RefreshStadist()
      {
      ((frmUnViaje)Parent.Parent.Parent).AcceptButton = btnModify;

      decimal SumaCosto = 0, SumaMonto = 0, SumaGanancia = 0;
      var ratioRecp = 1m;
      if( Datos.CompasCUC != 0 )
        ratioRecp = Datos.MontoInvers / Datos.CompasCUC;      // Relación entre en monto de la inversión y el de las compras

      var SelRows = (Grid.SelectedRows.Count>1);
      foreach( DataGridViewRow Row in Grid.Rows )
        {
        var id = (int) Row.Cells[0].Value;

        var row = table.FindByid( id );

        RowCalculate( row );

        if( !SelRows || Row.Selected )
          {
          SumaCosto += row.valCUC;
          SumaMonto += nowMonto;
          SumaGanancia += nowGanc;
          }
        }

      lbTotalCosto.Text = SumaCosto.ToString("0.##");
      lbTotalMonto.Text = SumaMonto.ToString("0.##");
      lbGanancia.Text   = SumaGanancia.ToString("0.##") + " cuc";
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Pone el timpo de moneda vigente para el precio</summary>
    private void SetMoney( Mnd v )
      {
      nowMond = v;
      cbMoneda.SelectedIndex = (int)v;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void Sumatorias()
      {
      ClearDatos();
      RefreshStadist();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void ClearDatos()
      {
      txtItem.Text = "";
      txtValue.Text = "";
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void Grid_SelectionChanged( object sender, EventArgs e )
      {
      if( Grid.SelectedRows.Count == 0 ) return;

      var idxSel = Grid.SelectedRows[0].Index;
      if( idxSel<0 || idxSel >= Grid.RowCount ) return;

      var IdProd = (int)Grid[0,idxSel].Value;
      OnSelectProduct( IdProd );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Muestra los datos para modificar del producto cuyo ID es 'IdProd'</summary>
    private void OnSelectProduct( int IdProd )
      {
      var Row = table.FindByid( IdProd );
      if( Row==null  ) return;

      ((frmUnViaje)Parent.Parent.Parent).AcceptButton = btnModify;
      txtItem.Text  = Row.item;

      var ratioRecp = 1m;
      var ratioGanc = 1m;
      if( Datos.CompasCUC != 0 )
        {
        ratioRecp = Datos.MontoInvers / Datos.CompasCUC;      // Relación entre en monto de la inversión y el de las compras
        ratioGanc =  (1.5m * Datos.MontoInvers) / Datos.CompasCUC;
        }

      var valRecp = ratioRecp * Row.valCucItem;
      var valGanc = ratioGanc * Row.valCucItem;

      lbPrecRec.Text = valRecp.ToString("0.##");
      lbPrecOK.Text  = valGanc.ToString("0.##" );

      var ItemPrec = valGanc;                                     // Toma valores por defecto
      Mnd Moned    = Mnd.Cuc;

      if( Row.precio>0 )
        {
        ItemPrec = Row.precio;
        Moned    = (Mnd)Row.moneda;
        }

      SetMoney( Moned );
      txtValue.Text   = ItemPrec.ToString( "0.##" );

      if( Moned != Mnd.Cuc )
        ItemPrec = Money.Convert( ItemPrec, Moned, Mnd.Cuc );

      lbItemGanc.Text = (ItemPrec-valRecp).ToString( "0.##" );

      //txtValue.Focus();
      RefreshStadist();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void btnClear_Click( object sender, EventArgs e )
      {
      ClearDatos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se oprime el boton de modificar el precio del producto</summary>
    private void btnModify_Click( object sender, EventArgs e )
      {
      try
        {
        if( Grid.SelectedRows.Count == 0 )
          throw new Exception("Seleccione un Item para modificar el precio");

        var GridRow = Grid.SelectedRows[0];
        var idx = GridRow.Index;
        if( idx<0 || idx >= Grid.RowCount || string.IsNullOrEmpty(txtItem.Text.Trim()) )
          throw new Exception("Seleccione un Item para modificar el precio");

        var prec = decimal.Parse( txtValue.Text );

        var Id  = (int)Grid[0,idx].Value;
        var rowComp = table.FindByid( Id );
        if( rowComp==null )
          throw new Exception("No se encontro el ID del item seleccionado en la base de datos");

        rowComp.precio = prec;
        rowComp.moneda = (int)nowMond;

        var rowGrid = FindTableGridId( Id );
        if( rowGrid==null )
          throw new Exception("No se encontro el ID del item seleccionado en la lista");
        
        var sPrecio = prec.ToString("0.##");
        if( prec != 0 )  sPrecio += ' ' + Money.Code( nowMond ); 

        rowGrid["precio"] = sPrecio;

        Grid.Refresh();
        Sumatorias();
        Datos.SetChanges( "CompraPrecios" );
        }
      catch( Exception exc)
        {
        MessageBox.Show( "ERROR: " + exc.Message );
        }

      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Busca en la tabla asociada al Grid, la fila con el identificador 'id'</summary>
    private DataRow FindTableGridId( int id )
      {
      foreach( DataRow row in tbCompras.Rows )
        if( (int)row[0] == id )  return row;

      return null;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se cambia el tipo de moneda en el combo correspondiente</summary>
    private void cbMoneda_SelectedIndexChanged( object sender, EventArgs e )
      {
      Mnd selMoney = (Mnd)cbMoneda.SelectedIndex;

      decimal prec;
      if( decimal.TryParse( txtValue.Text, out prec ) )
        {
        var precConv = Money.Convert( prec, nowMond, selMoney);

        txtValue.Text = precConv.ToString("0.00");
        }

      nowMond = selMoney;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Actualiza todos los datos calculados reletivos a precios</summary>
    private void btnUpdate_Click( object sender, EventArgs e )
      {
      FillGridCompras();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cada vez que el control se pone visible</summary>
    private void ctlPrecios_VisibleChanged( object sender, EventArgs e )
      {
      if( this.Visible )
        {
        FillGridCompras();
        ExecParametros();
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Imprime los precios de los items mostrados en la pantalla</summary>
    private void btnSaveVentas_Click( object sender, EventArgs e )
      {
      var sFootter = "\t\tCosto:\t" + lbTotalCosto.Text + "\t\tMonto:\t" + lbTotalMonto.Text + "\tGanancia:\t" + lbGanancia.Text + "\t";
      Datos.SaveGrid( Grid, sFootter);
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando cambia el texto del filtro</summary>
    private void txtFilter_TextChanged( object sender, EventArgs e )
      {
      FillGridCompras();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando cambia el checkmark de mostrar los comentarios</summary>
    private void chkShowComent_CheckedChanged( object sender, EventArgs e )
      {
      FillGridCompras();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Quita los filtros si existen</summary>
    private void BtnFilterOff_Click(object sender, EventArgs e)
      {
      Datos.RemoveFilter(1);

      btnFilterOff.Hide();
      FillGridCompras();
      }
    }
  }
