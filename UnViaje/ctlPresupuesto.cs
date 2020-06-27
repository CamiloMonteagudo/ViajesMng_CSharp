using System;
using System.Data;
using System.Windows.Forms;
using static UnViaje.DBViaje;

namespace UnViaje
  {
  public partial class ctlPresupuesto : UserControl
    {
    PresupuestoDataTable table;
    frmUnViaje frm;

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    public ctlPresupuesto()
      {
      InitializeComponent();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void ctlPresupuesto_Load( object sender, EventArgs e )
      {
      if( Datos.BD == null ) return;

      table = Datos.tablePresupesto;

      frm = (frmUnViaje)Parent.Parent.Parent;

      cbMoneda.SelectedIndex = 0;

      tbPresup = CreateTablePresup();
      Grid.AutoGenerateColumns = false;
      Grid.DataSource = tbPresup;

      FillGrid();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Llena el grid con todos los datos del presupuesto</summary>
    private void FillGrid()
      {
      tbPresup.Clear();
      foreach( PresupuestoRow row in Datos.tablePresupesto )
        {
        var value  = row.value.ToString("0.##");
        var moneda = (Mnd)row.moneda;

        var sCuc = (moneda==Mnd.Cuc)? value : "";
        var sUsd = (moneda==Mnd.Usd)? value : "";

        tbPresup.Rows.Add( row.id, row.source, sCuc, row.cambio, sUsd );
        }

      Sumatorias();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea una tabla vacia para mostrar las ventas</summary>
    private DataTable CreateTablePresup()
      {
      DataTable tb = new DataTable("ViewPresup");

      tb.Columns.Add( "id"     , typeof( Int32 )   );
      tb.Columns.Add( "src"    , typeof( String )  );
      tb.Columns.Add( "cuc"    , typeof( String )  );
      tb.Columns.Add( "cambio" , typeof( decimal ) );
      tb.Columns.Add( "usd"    , typeof( String  ) );

      return tb;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void btnAdd_Click( object sender, EventArgs e )
      {
      try
        {
        GetValores();

        var row = table.AddPresupuestoRow( nowDesc, nowValue, (int)nowMoneda, nowCambio );

        var sValue = nowValue.ToString("0.##");
        var sCuc   = (nowMoneda==Mnd.Cuc)? sValue : "";
        var sUsd   = (nowMoneda==Mnd.Usd)? sValue : "";

        tbPresup.Rows.Add( row.id, row.source, sCuc, row.cambio, sUsd );

        Sumatorias();
        Datos.SetChanges( "PresupuestoAdd" );
        }
      catch( Exception exc )
        {
        MessageBox.Show( "ERROR: " + exc.Message );
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void btnModify_Click( object sender, EventArgs e )
      {
      try
        {
        GetValores();

        var Row1 = table.FindByid( nowIdPres );
        if( Row1!=null )
          {
          Row1.source = nowDesc;
          Row1.cambio = nowCambio;
          Row1.value  = nowValue;
          Row1.moneda = (int)nowMoneda;
          }
        else
          {
          MessageBox.Show( "No se encontro en la base de datos el presupuesto a modificar");
          return;
          }

        DataRow Row2 = FindPresupId( nowIdPres );
        if( Row2!=null )
          {
          var sValue = nowValue.ToString("0.##");
          var sCuc   = (nowMoneda==Mnd.Cuc)? sValue : "";
          var sUsd   = (nowMoneda==Mnd.Usd)? sValue : "";

          Row2["src"   ] = nowDesc;
          Row2["cuc"   ] = sCuc;
          Row2["cambio"] = nowCambio;
          Row2["usd"   ] = sUsd;
          }
        else
          {
          MessageBox.Show( "No se encontro en la lista el presupuesto a modificar");
          return;
          }

        Sumatorias();
        Datos.SetChanges( "PresupuestoModify" );
        }
      catch( Exception exc)
        {
        MessageBox.Show( "ERROR: " + exc.Message );
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Encuentra el presupuesto con el Id dado</summary>
    private DataRow FindPresupId( int IdPres )
      {
      foreach( DataRow row in tbPresup.Rows )
        if( (int)row[0] == IdPres ) return row;

      return null;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void btnDelete_Click( object sender, EventArgs e )
      {
      if( Grid.SelectedRows.Count == 0 )
        {
        MessageBox.Show( "Seleccione un presupuesto para borrar");
        return;
        }

      var row1 = table.FindByid( nowIdPres );
      if( row1!=null )
        row1.Delete();
      else
        {
        MessageBox.Show( "No se pudo borrar el pesupuesto de la base de datos");
        return;
        }

      var row2 = FindPresupId( nowIdPres );
      if( row2!=null )
        row2.Delete();
      else
        {
        MessageBox.Show( "No se pudo borrar el pesupuesto de la lista");
        return;
        }


      ClearDatos();
      Sumatorias();
      Datos.SetChanges( "PresupuestoDelete" );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Atiende el boton para limpiar todos los datos en ediccion</summary>
    private void btnClear_Click( object sender, EventArgs e )
      {
      ClearDatos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void Grid_SelectionChanged( object sender, EventArgs e )
      {
      if( Grid.SelectedRows.Count == 0 ) return;

      var idx = Grid.SelectedRows[0].Index;
      if( table==null || idx >= table.Rows.Count ) return;

      nowIdPres = -1;
      var IdPres =  Grid[ "colId", idx ].Value;
      if( IdPres==null ) return;

      nowIdPres = (int)IdPres;
      var Row = table.FindByid( nowIdPres );
      if( Row==null )  return;

      if( Row.RowState == DataRowState.Detached )  return;

      txtSrc.Text    = Row.source;
      txtChange.Text = Row.cambio.ToString("0.####");
      txtValue.Text  = Row.value.ToString("0.####");

      cbMoneda.SelectedIndex = Row.moneda;

      btnModify.Visible = true;
      btnDelete.Visible = true;

      frm.AcceptButton = btnModify;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void Grid_KeyUp( object sender, KeyEventArgs e )
      {
      if( e.KeyCode == Keys.Delete )
        btnDelete_Click( Grid, null );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void ClearDatos()
      {
      Grid.ClearSelection();

      txtChange.Text = Money.UsdToCuc.ToString("0.####");
      txtSrc.Text = "";
      txtValue.Text = "";

      btnModify.Visible = false;
      btnDelete.Visible = false;

      frm.AcceptButton = btnAdd;

      txtSrc.Focus();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private int     nowIdPres;
    private string  nowDesc;
    private decimal nowValue;
    private decimal nowCambio;
    private Mnd     nowMoneda;
    private DataTable tbPresup;

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void GetValores()
      {
      nowDesc = txtSrc.Text;
      if( nowDesc.Trim().Length == 0 ) throw new Exception( "Debe indicar una descripción" );

      nowMoneda = (Mnd)cbMoneda.SelectedIndex;

      if( !decimal.TryParse( txtChange.Text, out nowCambio ) )
        throw new Exception( "El valor del cambio es incorrecto" );

      if( !decimal.TryParse( txtValue.Text, out nowValue ) )
        throw new Exception( "El valor del presupuesto es incorrecto" );

      if( nowMoneda!=Mnd.Cuc && nowMoneda!=Mnd.Usd)
        {
        nowValue = Money.Convert(nowValue, nowMoneda, Mnd.Cuc );
        nowMoneda = Mnd.Cuc;
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void Sumatorias()
      {
      Datos.GetPresupuesto();

      RefreshData();

      ClearDatos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    public void RefreshData()
      {
      lbSumaCUC.Text = Datos.sumaCUC.ToString("0.00");
      lbSumaUSD.Text = Datos.sumaUSD.ToString("0.00");
      lbTotalCUC.Text = Datos.totalCUC.ToString("0.00");
      lbTotalUSD.Text = Datos.totalUSD.ToString("0.00");
      }

    }
  }
