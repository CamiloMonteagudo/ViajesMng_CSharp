using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static UnViaje.DBViaje;

namespace UnViaje
  {
  public partial class ctlCompras : UserControl
    {
    DBViaje.ComprasDataTable table;
    private DataTable tbCompras;

    frmUnViaje frm;
    SmartSearch Names = new SmartSearch();

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    public ctlCompras()
      {
      InitializeComponent();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void ctlCompras_Load( object sender, EventArgs e )
      {
      if( Datos.BD == null ) return;

      table = Datos.tableCompras;

      frm = (frmUnViaje)Parent.Parent.Parent;

      cbMoneda.SelectedIndex = 0;

      tbCompras = CreateTableCompras();
      FillGridCompras();

      Grid.AutoGenerateColumns = false;
      Grid.DataSource = tbCompras;
      Grid.Refresh();

      Names.Load();
      lstNames.Items.AddRange( Names.Items );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    // Obtiene los parametros que se pueden pasar como argumento a la aplicación
    private void ExecParametros()
      {
      btnFilterOff.Visible  = Datos.HasFilter(1);

      int iTab = Datos.GetIntParam( "Tab" );                        // Tab pasado como parametro
      if( iTab != 3 ) return;                                       // Si no es el actual no hace nada

      foreach( DataGridViewRow row in Grid.Rows )
        {
        if( Datos.F_ProdID != (int)row.Cells[0].Value ) continue;

        row.Selected = true;
        OnSelectProduct( Datos.F_ProdID );
        Grid.FirstDisplayedScrollingRowIndex = row.Index;
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Crea una tabla vacia para mostrar las compras</summary>
    private DataTable CreateTableCompras()
      {
      DataTable tb = new DataTable("ViewCompras");

      tb.Columns.Add( "id"         , typeof( Int32 )    );
      tb.Columns.Add( "item"       , typeof( String )   );
      tb.Columns.Add( "count"      , typeof( Int32 )    );
      tb.Columns.Add( "value"      , typeof( String )  );
      tb.Columns.Add( "valItem"    , typeof( String )  );
      tb.Columns.Add( "valCUC"     , typeof( decimal )   );
      tb.Columns.Add( "valCucItem" , typeof( decimal ) );

      return tb;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Llena el Grid con los pagos realizados</summary>
    private void FillGridCompras()
      {
      var sFilter = txtFilter.Text.ToLower().Trim();

      tbCompras.Clear();
      foreach( ComprasRow row in Datos.tableCompras )
        {
        if( Datos.FilterProd(row.id) ) continue;

        var sItem = row.item;

        if( chkShowComent.Checked && !row.IscomentarioNull() && row.comentario.Trim().Length>0 )
          sItem = sItem + " | " + row.comentario;

        if( sFilter.Length>0 && !sItem.ToLower().Contains(sFilter) )
          continue;

        tbCompras.Rows.Add( row.id, sItem, row.count, row.value, row.valItem, row.valCUC, row.valCucItem );
        }

      SumaCostos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void btnAdd_Click( object sender, EventArgs e )
      {
      try
        {
        lstNames.Visible = false;                               // Esconde el listado de los nombres de los item si esta visible
        int id = Datos.GetCompraID();

        GetValores();

//        table.Rows.Add( id, item, count, value, valItem, valCUC, valCucItem  );
        table.AddComprasRow( id, item, count, value, valItem, valCUC, valCucItem, 0, 0, txtComent.Text );
        tbCompras.Rows.Add( id, item, count, value, valItem, valCUC, valCucItem );

        Sumatorias();
        Datos.SetChanges("CompraAdd");
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
        lstNames.Visible = false;                               // Esconde el listado de los nombres de los item si esta visible
        GetValores();

        var idx = Grid.SelectedRows[0].Index;

        var cell = Grid["IdItem", idx].Value; 
        if( cell==null ) return;

        int idCompra = (int)cell;
        var Row = table.FindByid( idCompra );

        Row.item       = item;
        Row.count      = count;
        Row.value      = value;
        Row.valItem    = valItem;
        Row.valCUC     = valCUC;
        Row.valCucItem = valCucItem;
        Row.comentario = txtComent.Text;

        var row2 = FindPagoId( idCompra );
        if( row2 != null )
          {
          row2["item"      ] = item;
          row2["count"     ] = count;
          row2["value"     ] = value;
          row2["valItem"   ] = valItem;
          row2["valCUC"    ] = valCUC;
          row2["valCucItem"] = valCucItem;
          }

        Sumatorias();
        Datos.SetChanges("CompraModify");
        }
      catch( Exception exc)
        {
        MessageBox.Show( "ERROR: " + exc.Message );
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene los datos de una compra sabiendo su ID</summary>
    private DataRow FindPagoId( int CompraId )
      {
      foreach( DataRow row in tbCompras.Rows )
        if( (int)row[0] == CompraId ) return row;

      return null;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void btnDelete_Click( object sender, EventArgs e )
      {
      if( Grid.SelectedRows.Count == 0 ) return;

      var idx = Grid.SelectedRows[0].Index;
      if( table==null || idx >= table.Rows.Count ) return;

      var cell = Grid["IdItem", idx].Value; 
      if( cell==null ) return;

      int idCompra = (int)cell;
      var Row = table.FindByid( idCompra );
      if( Row != null )
        {
        ClearDatos();
        Row.Delete();
        Sumatorias();
        }
      else
        MessageBox.Show( "No se encontro una compra con el ID " +  (int)cell);

      var row2 = FindPagoId( idCompra );
      if( row2 != null ) row2.Delete();

      Datos.SetChanges("CompraDelete");
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Limpia los datos de edicción</summary>
    private void btnClear_Click( object sender, EventArgs e )
      {
      ClearDatos();
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
    private void Grid_SelectionChanged( object sender, EventArgs e )
      {
      if( Grid.SelectedRows.Count == 0 ) return;

      var idx = Grid.SelectedRows[0].Index;
      if( table==null || idx >= table.Rows.Count ) return;

      var cell = Grid["IdItem", idx].Value; 
      if( cell!=null )
        OnSelectProduct( (int)cell );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Muestra los datos para modificar del producto cuyo ID es 'IdProd'</summary>
    private void OnSelectProduct( int IdProd )
      {
      var Row = table.FindByid( IdProd );
      if( Row == null )
        {
        MessageBox.Show( "No se encontro una compra con el ID " +  IdProd );
        return;
        }

      txtItem.Text  = Row.item;
      txtCount.Text = Row.count.ToString();

      var parts = Row.value.Split(' ');
      txtValue.Text = parts[0];

      txtComent.Text = (Row.IscomentarioNull())? "" : Row.comentario;

      var Mond = Money.Idx(parts[1]);
      cbMoneda.SelectedIndex = (int)Mond;

      SumaCostos();

      btnModify.Visible = true;
      btnDelete.Visible = true;

      frm.AcceptButton = btnModify;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private void Sumatorias()
      {
      Datos.GetCompras();

      ClearDatos();
      SumaCostos();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Calcula la suma de todos los costos</summary>
    private void SumaCostos()
      {
      var nSel = Grid.SelectedRows.Count;

      var SumaCosto = 0m;
      foreach( DataGridViewRow row in Grid.Rows )
        {
        if( (nSel<2 || row.Selected) && row.Cells[5].Value!=null )
          SumaCosto += (decimal)row.Cells[5].Value;
        }

      lbSumaCostos.Text = SumaCosto.ToString("0.##") + " cuc";
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Quita todos los datos que se estan editando y pone los valores por defecto</summary>
    private void ClearDatos()
      {
      Grid.ClearSelection();

      txtItem.Text = "";
      txtValue.Text = "";
      txtCount.Text = "1";
      txtComent.Text = "";

      btnModify.Visible = false;
      btnDelete.Visible = false;

      frm.AcceptButton = btnAdd;

//      txtItem.Focus();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    string item, value, valItem;
    int    count;

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Guarda la información de todos los Items mostrados en la tabla de compras</summary>
    private void btnSaveVentas_Click( object sender, EventArgs e )
      {
      var sFootter = "\t\t\t\t\tCosto:\t"+ lbSumaCostos.Text +"\t";
      Datos.SaveGrid( Grid, sFootter);
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cada vez que se cambia el texto para filtrar</summary>
    private void txtFilter_TextChanged( object sender, EventArgs e )
      {
      FillGridCompras();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando el control se hace visible</summary>
    private void ctlCompras_VisibleChanged( object sender, EventArgs e )
      {
      if( Visible )
        {
        FillGridCompras();
        Sumatorias();

        ExecParametros();
        }
      }

    private void chkShowComent_CheckedChanged( object sender, EventArgs e )
      {
      FillGridCompras();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Busca el producto seleccionado y lo pone como predetermindo para todos los modulos</summary>
    private void btnFilterProd_Click( object sender, EventArgs e )
      {
      FillGridCompras();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuandro el cuadro de edición del nombre del item pierde el foco</summary>
    private void txtItem_Leave( object sender, EventArgs e )
      {
      if( !lstNames.ContainsFocus )
        lstNames.Visible = false;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se selecciona el nombre de un item</summary>
    private void lstNames_SelectedIndexChanged( object sender, EventArgs e )
      {
      if( lstNames.ContainsFocus )  
        {
        var Text = (string)lstNames.SelectedItem;

        txtItem.Text = Text;
        txtItem.SelectionStart = Text.Length; 
        txtItem.Focus();

        lstNames.Visible = false;
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando el nombre del item cambia</summary>
    private void txtItem_TextChanged( object sender, EventArgs e )
      {
      if( txtItem.ContainsFocus )  
        {
        var FilteredItems = Names.FilterItems2(txtItem.Text);

        lstNames.Items.Clear();
        lstNames.Items.AddRange( FilteredItems );

        if( lstNames.Items.Count > 0 )
          lstNames.SelectedIndex = 0;

        lstNames.Visible = (lstNames.Items.Count>0);
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Atiende las teclas del editor que manejan la lista de palabras</summary>
    private void txtItem_PreviewKeyDown( object sender, PreviewKeyDownEventArgs e )
      {
      switch( e.KeyCode )
        {
        case Keys.Return:                                   // Se perciono la tecla return
          if( lstNames.Visible )                            // Si la lista esta visible
            {
            var Text = (string)lstNames.SelectedItem;
            if( !string.IsNullOrWhiteSpace( Text ) )        // Si hay un item seleccionado en la lista
              e.IsInputKey = true;                          // Pone bandera para que la tecla de sea atendida por el contorl
            }
          break;
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Atiende las teclas de las flechas y el return para manejar la lista de items</summary>
    private void txtItem_KeyDown( object sender, KeyEventArgs e )
      {
      switch( e.KeyCode )
        {
        case Keys.Down:
            {
            var idx = lstNames.SelectedIndex;
            if( idx < lstNames.Items.Count - 1 ) lstNames.SelectedIndex = idx + 1;
            }
          e.Handled = true;
          break;
        case Keys.Up:
            {
            var idx = lstNames.SelectedIndex;
            if( idx > 0 ) lstNames.SelectedIndex = idx - 1;
            }
          e.Handled = true;
          break;
        case Keys.Return:
            {
            var Text = (string)lstNames.SelectedItem;
            if( !string.IsNullOrWhiteSpace(Text) )
              {
              txtItem.Text = Text;
              txtItem.SelectionStart = Text.Length;

              lstNames.Visible = false;
              }
            }
          e.Handled = true;
          break;
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Quita los filtros si existen</summary>
    private void BtnFilterOff_Click(object sender, EventArgs e)
      {
      Datos.RemoveFilter(1);

      btnFilterOff.Hide();
      FillGridCompras();
      }

    decimal valCUC,  valCucItem;

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene los valores entrados por el usuario</summary>
    private void GetValores()
      {
      item = txtItem.Text;
      if( item.Trim().Length == 0 ) throw new Exception( "Debe indicar el nombre del Item" );

      count = int.Parse( txtCount.Text );

      valCUC = Money.GetCucValue( txtValue.Text, (Mnd)cbMoneda.SelectedIndex );
      value = Money.FormatLastValue();

      Money.LastValue /= count;
      valItem = Money.FormatLastValue();
          
      valCucItem = valCUC / count;
      }


    //--------------------------------------------------------------------------------------------------------------------------------------
    }
  }
