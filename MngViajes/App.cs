using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MngViajes
  {
  public static class App
    {
    public static string[] Vendedores = new string[0];
    public static string   Titulo =  "";

    const string ConfigFile = "ViajesConfig.ini";

    public static List<Viaje> Viajes;

    static int ftr_Viaje = -1;
    static int ftr_Prod  = -1;
    static int ftr_Venta = -1;

    private static HashSet<string> VendSet = new HashSet<string> {"Consumo"};
    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Carga todas la variable de comfiguración desde un fichero</summary>
    internal static void CargaViajes()
      {
      Viajes = new List<Viaje>();

      if( !File.Exists(ConfigFile) )
        { 
        MessageBox.Show("NO SE ENCONTRO EL FICHERO DE CONFIGURACIÓN:\r\n" + ConfigFile );
        return;
        }

      var lines = File.ReadAllLines(ConfigFile);
      var path  = lines[0];
      if( !Directory.Exists(path) )
        { 
        MessageBox.Show("NO SE ENCONTRO EL DIRECCTORIO DE LOS DATOS:\r\n" + path );
        return;
        }

      var files = Directory.GetFiles(path,"*.xml");
      foreach( var fl in files )
        {
        var code  = Path.GetFileName(fl).Split('-')[0];
        if( code.Length>4 ) continue;

        var viaje = new Viaje( Viajes.Count, code, fl );

        if( viaje.Load() )
          {
          Viajes.Add( viaje );
          AddVendedores( viaje.Vendedores );
          }
        else
          MessageBox.Show( "No se puedo cargar el viaje '" + path + "' " );
        }
      
      if( Viajes.Count == 0 )
        { 
        MessageBox.Show("NO SE ENCONTRO NINGÚN VIAJE EN EL DIRECTORIO:\r\n" + path );
        return;
        }

      Vendedores = new string[VendSet.Count];
      VendSet.CopyTo( Vendedores );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    private static void AddVendedores( string[] vendedores )
      {
      foreach( string sVend in vendedores )
        if( !VendSet.Contains(sVend) )
          VendSet.Add( sVend );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    public static void SaveGrids( DataGridView Grid1, DataGridView Grid2 )
      {
      var dlg = new SaveFileDialog();

     dlg.Filter = "Fichero de resultados (*.txt)|*.txt"  ;
     dlg.FilterIndex = 2 ;
     dlg.RestoreDirectory = true ;

     if( dlg.ShowDialog() != DialogResult.OK ) return;

      var txt = new StringBuilder();

      for( int i=0; i<Grid1.Columns.Count; ++i )
        {
        var Col = Grid1.Columns[i];

        txt.Append( Col.HeaderText );
        txt.Append( '\t' );
        }

      txt.Append( "\r\n" );

      foreach( DataGridViewRow Row in Grid1.Rows )
        ApendRowText( Row, txt );

      ApendRowText( Grid2.Rows[0], txt );

      File.WriteAllText( dlg.FileName, txt.ToString(), Encoding.Default );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    private static void ApendRowText( DataGridViewRow Row, StringBuilder txt )
      {
      var count = Row.Cells.Count;
      for( int i = 0; i<count; ++i )
        {
        string sVal = "";

        var Cell = Row.Cells[i];
        var Val  = Cell.Value;
        if( Val != null )
          {
          if( Val.GetType()==typeof(decimal) ) sVal = ((decimal)Val).ToString( "0.00" );
          else sVal = Val.ToString();
          }

        txt.Append( sVal );

        if( i<count-1 )  txt.Append( '\t' );
        }

      txt.Append( "\r\n" );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Define el indice del viaje que se debe filtar </summary>
    public static int FilterViaje { get{ return ftr_Viaje; } set{ ftr_Viaje=value; } }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Define el identificador del principio que se deve filtar que se debe filtar </summary>
    public static int FilterProd { get{ return ftr_Prod; } set{ ftr_Prod=value; } }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Define el identificador de la venta que se debe filtar </summary>
    public static int FilterVenta { get{ return ftr_Venta; } set{ ftr_Venta=value; } }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Determina si los valores indicado deben ser filtrados o no</summary>
    internal static bool FilterFor( int viaje=-1, int prod=-1, int venta=-1 )
      {
      if( ftr_Viaje != -1 && viaje!= -1 )
        if( ftr_Viaje != viaje ) return true;

      if( ftr_Prod != -1 && prod!= -1 )
        if( ftr_Prod != prod ) return true;

      if( ftr_Venta != -1 && venta!= -1 )
        if( ftr_Venta != venta ) return true;

      return false;
      }

    internal static bool DelFilterViaje()
      {
      if( ftr_Viaje == -1) return false;

      ftr_Viaje = -1;
      ftr_Prod  = -1;
      ftr_Venta = -1;
      return true;
      }

    internal static bool DelFilterProd()
      {
      if( ftr_Prod == -1) return false;

      ftr_Prod  = -1;
      ftr_Venta = -1;
      return true;
      }

    internal static bool DelFilterVenta()
      {
      if( ftr_Venta == -1) return false;
      ftr_Venta = -1;
      return true;
      }


    }
  }
