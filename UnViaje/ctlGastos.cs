using System;
using System.Data;
using System.Windows.Forms;
using static UnViaje.DBViaje;

namespace UnViaje
  {
  public partial class ctlGastos : UserControl
    {
    DBViaje.GastosDataTable table;
    frmUnViaje frm;

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    public ctlGastos()
      {
      InitializeComponent();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void ctlGastos_Load( object sender, EventArgs e )
      {
      if( Datos.BD == null ) return;

      table = Datos.tableGastos;
      frm = (frmUnViaje)Parent.Parent.Parent;

      cbMoneda.SelectedIndex = 0;

      Grid.AutoGenerateColumns = false;
      Grid.DataSource = Datos.BD;
      Grid.DataMember = table.TableName;

      Sumatorias();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se ejecuta cuando se oprime el boton de adiccionar un gasto</summary>
    private void btnAdd_Click( object sender, EventArgs e )
      {
      try
        {
        GetValores();

        var row = table.AddGastosRow( desc, valCuc, value );

        SelectGatoInGrid( row.id );

        Sumatorias();
        Datos.SetChanges( "GastoAdd" );
        }
      catch( Exception exc )
        {
        MessageBox.Show( "ERROR: " + exc.Message );
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Busca el gasto con el ID dado y lo selecciona en la lista</summary>
    private void SelectGatoInGrid( int id )
      {
      Grid.ClearSelection();

      for( int i=0; i<Grid.Rows.Count; i++ )
        {
        var gdRow = Grid.Rows[i];
        if( (int)(gdRow.Cells[0].Value) == id )
          {
          gdRow.Selected = true;
          Grid.FirstDisplayedScrollingRowIndex = i;
          break;
          }
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void btnModify_Click( object sender, EventArgs e )
      {
      try
        {
        GetValores();

        var    idx = Grid.SelectedRows[0].Index;
        nowIdGasto = (int)Grid["colId", idx].Value; 
        var    Row = table.FindByid( nowIdGasto );

        Row.descric = desc;
        Row.value   = value;
        Row.cuc     = valCuc;

        Sumatorias();
        Datos.SetChanges( "GastoModiy" );
        }
      catch( Exception exc)
        {
        MessageBox.Show( "ERROR: " + exc.Message );
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void btnDelete_Click( object sender, EventArgs e )
      {
      if( Grid.SelectedRows.Count == 0 ) return;

      var idx = Grid.SelectedRows[0].Index;
      if( table==null || idx >= table.Rows.Count ) return;

      nowIdGasto = (int)Grid["colId", idx].Value; 

      var Row = table.FindByid( nowIdGasto );
      if( Row == null )
        {
        MessageBox.Show( "No se encontro un gasto con el ID actual" );
        return;
        }

      Row.Delete();
      ClearDatos();
      Sumatorias();
      Datos.SetChanges( "GastoDelete" );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando el usuario selecciona una fila en la lista</summary>
    private void Grid_SelectionChanged( object sender, EventArgs e )
      {
      if( Grid.SelectedRows.Count == 0 ) return;

      var idx = Grid.SelectedRows[0].Index;
      if( table==null || idx >= table.Rows.Count ) return;

      var cell = Grid["colId", idx].Value; 
      if( cell==null ) return;

      nowIdGasto = (int)cell; 
      var Row = table.FindByid( nowIdGasto );
      if( Row == null )
        {
        MessageBox.Show( "No se encontro un gasto con el ID actual" );
        return;
        }

      txtSrc.Text   = Row.descric;

      var parts = Row.value.Split(' ');
      txtValue.Text = parts[0];

      var Mond = Money.Idx(parts[1]);
      cbMoneda.SelectedIndex = (int)Mond;

      btnModify.Visible = true;
      btnDelete.Visible = true;

      frm.AcceptButton = btnModify;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Borra una fila de la lista con la tecla DEL</summary>
    private void Grid_KeyUp( object sender, KeyEventArgs e )
      {
      if( e.KeyCode == Keys.Delete )
        btnDelete_Click( Grid, null );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Limpia los datos que se estan editando</summary>
    private void btnClear_Click( object sender, EventArgs e )
      {
      ClearDatos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    string desc, value;
    decimal valCuc;
    private int nowIdGasto;

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void GetValores()
      {
      desc = txtSrc.Text;
      if( desc.Trim().Length == 0 ) throw new Exception( "Debe indicar una descripción" );

      valCuc = Money.GetCucValue( txtValue.Text, (Mnd)cbMoneda.SelectedIndex );
      value  = Money.FormatLastValue();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void ClearDatos()
      {
      Grid.ClearSelection();

      txtSrc.Text = "";
      txtValue.Text = "";

      btnModify.Visible = false;
      btnDelete.Visible = false;

      frm.AcceptButton = btnAdd;

      txtSrc.Focus();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void Sumatorias()
      {
      Datos.GetGastos();
      }
    }
  }
