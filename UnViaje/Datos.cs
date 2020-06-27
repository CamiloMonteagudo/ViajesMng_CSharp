using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static UnViaje.DBViaje;

namespace UnViaje
  {
  public static class Datos
    {
    public static DBViaje BD;
    public static PresupuestoDataTable tablePresupesto;
    public static GastosDataTable  tableGastos;
    public static ComprasDataTable tableCompras;
    public static VentasDataTable  tableVentas;
    public static PagosDataTable   tablePagos;

    public static decimal sumaUSD;                          // Suma de dinero invertido en USD

    public static string   Titulo =  "";

    public static string[] Vendedores = { "Consumo" };
    public static string   ConfigFile = "ConfigViaje.ini";
    public static string   DatosFile  = "DataBase.xml";

    public static int  F_ProdID = -1;                         // Mostrar solo ese producto
    public static int  F_VentID = -1;                         // Mostrar solo esa venta
    public static int  F_PagoID = -1;                         // Mostrar solo ese pago

    private static Dictionary<string,string> Params;          // Diccionario con todos los paramtros de la aplicación

    private static HashSet<string> Changes;                 // Registra el conjunto cambios realizado durante la sección
    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Carga todas la variable de comfiguración desde un fichero</summary>
    internal static void Configuration()
      {
      Changes = new HashSet<string>();                      // Inicializa un conjuto de cambios vacios

      if( !File.Exists(ConfigFile) ) return;

      var lines = File.ReadAllLines(ConfigFile);
      foreach( var line in lines )
        {
        var Parts = line.Split(':');
        switch( Parts[0] )
          {
          case "Vededores": Vendedores = Parts[1].Split(',');   break;
          case "UsdToCuc": decimal.TryParse( Parts[1], out Money.UsdToCuc); break;
          case "CupToCuc": decimal.TryParse( Parts[1], out Money.CupToCuc); break;
          case "UsdToDop": decimal.TryParse( Parts[1], out Money.UsdToDop); break;
          case "Titulo": Titulo = Parts[1];   break;
          }
        }

      Money.CupToCuc = 1.0m/Money.CucToCup;
      Money.DopToUsd = 1.0m/Money.UsdToDop;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Carga la base de datos e inicializa todas las tablas</summary>
    internal static void GetParametros()
      {
      Params = new Dictionary<string,string>();                 // Diccionario con lo parametros

      var cmd = Environment.CommandLine;                        // Obtiene la linea de comandos
      int iArgs = cmd.IndexOf("\" ");                           // Salta el nombre del programa
      if( iArgs<4 ) return;                                     // El programa fue llamado sin arqumentos

      var args  = cmd.Substring(iArgs+2);                       // Obtiene la cadena de los argumentos
      var Segms = args.Split('|');                              // Divide los argumentos en segmentos
      for( int i=0; i<Segms.Length; ++i )                       // Recorre los segmentos
        {
        var NameVal = Segms[i].Split('=');                      //  Obtiene nombre y valor del parametro

        if( NameVal.Length == 2 )                               // Solo si son 2 cadenas
          if( NameVal[0]=="DatosFile" )                         // Si es el nombre del fichero de datos
            GetFileByParam( NameVal[1] );                       // Lo toma directo
          else
            Params[ NameVal[0] ] = NameVal[1];                  // Toma el parametro nombre=valor
        }

      GetFilterByParam();                                       // Trata de obtener los fitros pasados por parametro
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene donde estan los datos a utilizar, por un parametro de llamada</summary>
    internal static void GetFileByParam( string dFile )
      {
      dFile = dFile.ToLower();                                  // Lleva todo a minisculas
      if( !dFile.EndsWith(".xml") ) return;                     // Si no es un fichero XML lo ignora                                                              

      DatosFile  = dFile;                                       // Lo toma como fichero de datos
      ConfigFile = dFile.Replace( ".xml", ".ini" );             // Fichero de configuración, igual al de datos con ext. ini
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene los filtros pasados por parametros</summary>
    internal static void GetFilterByParam()
      {
      F_ProdID = GetIntParam( "ProdID" );
      F_VentID = GetIntParam( "VentID" );
      F_PagoID = GetIntParam( "PagoID" );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene un parametro con un valor entero</summary>
    internal static int GetIntParam( string key )
      {
      int val = -1;
      var sVal = GetParam(key);
      if( sVal == null ) return val;

      if( int.TryParse( sVal, out int value) )  val = value;
      return val;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Retorna el valor del parametro solicitado, si no se encuentra retorna null</summary>
    internal static string GetParam( string key )
      {
      if( !Params.ContainsKey(key ) ) return null;

      return Params[key];
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Borra todos los parametros iniciales</summary>
    internal static void RemoveFilter( int nivel )
      {
      if( nivel<2 )
        { 
        F_ProdID = -1;
        DelParam( "ProdID" );
        }

      F_VentID = -1;
      DelParam( "VentID" );

      F_PagoID = -1;
      DelParam( "PagoID" );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Borra todos los parametros iniciales</summary>
    internal static bool HasFilter( int nivel )
      {
      if( nivel<2 )
        return F_ProdID != -1;

      return F_VentID != -1 || F_PagoID != -1;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene el valor del paramentro una sola vez (siempre entero)</summary>
    internal static void DelParam( string key )
      {
      if( !Params.ContainsKey(key ) ) return;
      Params.Remove(key);
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Carga la base de datos e inicializa todas las tablas</summary>
    internal static void LoadDataBase()
      {
      BD = new DBViaje(); 
      BD.DataSetName = "DataBase";
      BD.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;

      if( File.Exists(DatosFile) )
        BD.ReadXml(DatosFile);

      tablePresupesto = BD.Presupuesto;
      tableGastos     = BD.Gastos;
      tableCompras    = BD.Compras;
      tableVentas     = BD.Ventas;
      tablePagos      = BD.Pagos;

      GetPresupuesto();
      GetGastos();
      GetCompras();
      }

  //--------------------------------------------------------------------------------------------------------------------------------------
  /// <summary> Guarda los datos en la misma base de datos donde los leyo </summary>
  internal static void SaveDataBase()
      {
      BD.WriteXml(DatosFile);
      }

  //--------------------------------------------------------------------------------------------------------------------------------------
    public static decimal sumaCUC;              // Suma de dinero invertido en CUC
    public static decimal totalUSD;             // Suma total de todo el dinero invertido convertido a USD
    public static decimal totalCUC;             // Suma total de todo el dinero invertido convertido a CUC
    public static decimal GastosCUC;            // Dinero gastado para garantizar la inversión
    public static decimal CompasCUC;            // Dinero Invertido en la compa de mercancias
    public static decimal RecupIdx;             // Indice de recuperación de la inversión
    public static decimal MontoInvers;          // Monto total de la inversion
    public static decimal MontoPrecios;         // Monto total según los precios estimados
    public static decimal GanancPrecios;        // Ganacia sesun los precios
    public static decimal MontoVentas;          // Moto total si se ejecutan todoas las ventas
    public static decimal GanacVentas;          // Ganacia si se ejecutan todas las ventas
    public static decimal MontoConsumo;         // Costo de todos los item que se usaron para consumo
    public static decimal MontoConsumoRecp;     // Costo de recueperación de todos los item que se usaron para consumo
    public static decimal GanacConsumo;         // Ganancia producto de los item que se consumen
    public static decimal NumChgPrecio;         // Número de items que se les cambia el precio
    public static decimal MontoChgPrecio;       // Cantidad de dinero involucrado en el cambio de precio
    public static decimal NumSinVender;         // Número de items sin vender
    public static decimal MontoSinVender;       // Monto de todos los items sin vender
    public static decimal NumDevoluc;           // Número de items sin vender
    public static decimal MontoDevoluc;         // Monto de todos los items sin vender
    public static decimal MontoCobros;          // Cantidad total de dinero que ha sido cobrado
    public static decimal NumSinPagar;          // Número de items sin pagar
    public static decimal MontoSinPagar;        // Monto de todos los items sin pagar

    public static int lastCompraID;

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Returna True, si la venta 'idVent' debe ser filtrada </summary>
    internal static bool FilterVenta(int idVent)
      {
      return( F_VentID!=-1 && F_VentID!=idVent );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Returna True, si el producto 'idProd' debe ser filtrado </summary>
    internal static bool FilterProd(int idProd)
      {
      return( F_ProdID!=-1 && F_ProdID!=idProd );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Returna True, si el pago 'idPago' debe ser filtrado </summary>
    internal static bool FilterPago(int idPago)
      {
      return( F_PagoID!=-1 && F_PagoID!=idPago );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene un identificador nuevo para un item de compra</summary>
    public static int GetCompraID()
      {
      return ++lastCompraID;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene los datos generales sobre los cobros realizados</summary>
    internal static void CobrosSumary()
      {
      MontoCobros = 0;
      foreach( PagosRow row in Datos.tablePagos )
        {
        var Pagado = row.cuc;
        Pagado += Money.Convert( row.cup, Mnd.Cup, Mnd.Cuc);

        MontoCobros += Pagado;
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene los datos generales sobre las compras</summary>
    public static Regex reMark = new Regex(@" Devolvio ([0-9]+)\}", RegexOptions.Compiled);
    public static void GetVentas()
      {
      Dictionary<Int32,Int32> GroupVentas = new Dictionary<Int32,Int32>();

      MontoVentas = GanacVentas = MontoConsumo = GanacConsumo = MontoConsumoRecp = NumChgPrecio = MontoChgPrecio = NumDevoluc = MontoDevoluc = NumSinPagar = MontoSinPagar = 0;

      foreach( VentasRow row in Datos.tableVentas )
        {
        var Cant   = row.count;
        var idProd = row.idProd;

        if( !GroupVentas.ContainsKey(idProd) )  GroupVentas[idProd]  = Cant;
        else                                    GroupVentas[idProd] += Cant;

        var rowProd = Datos.tableCompras.FindByid( idProd );
        if( rowProd == null ) continue;

        var montoProd = Money.Convert( Cant*rowProd.precio, (Mnd)rowProd.moneda, Mnd.Cuc);

        if( row.vendedor == Datos.Vendedores[0] )           // Item para consumo
          {
          var costo    = Cant * rowProd.valCucItem;
          var costoRcp = costo * RecupIdx;

          Debug.WriteLine( "idProd=" + idProd + " Cant=" + Cant + " Precio=" + montoProd.ToString("#.##") );

          MontoConsumo     += costo;
          MontoConsumoRecp += costoRcp;
          GanacConsumo     += (montoProd-costoRcp);
          continue;
          }

        var precioVenta = Money.Convert( row.precio, (Mnd)row.moneda, Mnd.Cuc);
        var montoVenta  = Cant * precioVenta;

        MontoVentas += montoVenta;

        if( montoProd != montoVenta )
          {
          NumChgPrecio += Cant;
          MontoChgPrecio += (montoVenta - montoProd );

          Debug.WriteLine( "ID:" + row.id + " Precio:" + montoProd  + " por:" + montoVenta  + " Dif:" + (montoVenta - montoProd )  );
          }

        if( !row.IscomentarioNull() )
          {
          var matches = reMark.Matches(row.comentario);  
          foreach( Match match in matches )              
            {
            var Num = int.Parse(match.Groups[1].Value);  

            NumDevoluc   += Num;
            MontoDevoluc += (Num * precioVenta);
            }
          }

        var Pago = GetCucPagado( row );
        var SinPagar = montoVenta - Pago; 

        if( precioVenta != 0 )
          NumSinPagar   += SinPagar/precioVenta; 

        MontoSinPagar += SinPagar;
        }

      GanacVentas = MontoVentas - MontoInvers;

      NumSinVender = MontoSinVender = 0;
      foreach( ComprasRow row in Datos.tableCompras )
        {
        var idProd = row.id;
        var Cant   = row.count; 

        var Resto = Cant;    
        if( GroupVentas.ContainsKey(idProd) )  Resto -= GroupVentas[idProd]; 

        if( Resto <= 0 ) continue;

        var Precio = Money.Convert( row.precio, (Mnd)row.moneda, Mnd.Cuc );

        NumSinVender   += Resto;
        MontoSinVender += (Resto*Precio);
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene la cantidad pagada para la venta especificada por 'row'</summary>
    public static decimal GetCucPagado( VentasRow row )
      {
      var relac = Datos.BD.Relations["Ventas_Pagos"];

      decimal pagado = 0;
      PagosRow[] Pagos = (PagosRow[])row.GetChildRows(relac);
      foreach( PagosRow rowPago in Pagos )
        {
        var pago = rowPago.cuc;
        pago +=  Money.Convert( rowPago.cup, Mnd.Cup, Mnd.Cuc );

        pagado += pago;
        }

      return pagado;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene los datos generales sobre el presupuesto</summary>
    public static void GetPresupuesto()
      {
      sumaUSD = sumaCUC = totalUSD = totalCUC = 0;

      foreach( PresupuestoRow row in tablePresupesto.Rows )
        {
        var cambio = row.cambio;
        var moneda = (Mnd)row.moneda;
        var value  = row.value;

        if( moneda == Mnd.Usd )
          {
          sumaUSD  += value;
          totalUSD += value;
          totalCUC += (value * cambio);
          }
        else
          {
          sumaCUC  += value;
          totalCUC += value;
          totalUSD += (value / cambio);
          }
        }

      if( totalUSD>0 && totalCUC>0 )
        Money.UsdToCuc = totalCUC/totalUSD;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene los datos generales sobre los gastos</summary>
    public static void GetGastos()
      {
      GastosCUC = 0;

      foreach( GastosRow row in tableGastos.Rows )
        GastosCUC += row.cuc;

      MontoInvers    = GastosCUC + CompasCUC;
      RecupIdx = (CompasCUC>0)? (MontoInvers/CompasCUC) : 9999;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene los datos generales sobre las Compras</summary>
    public static void GetCompras()
      {
      CompasCUC = 0;
      MontoPrecios  = 0;
      GanancPrecios = 0;

      foreach( ComprasRow row in tableCompras.Rows )
        {
        var mond = (Mnd)row.moneda;
        var prec = row.precio;
        var cant = row.count;

        if( mond != Mnd.Cuc )
          prec = Money.Convert(prec, mond, Mnd.Cuc );

        MontoPrecios += (prec * cant);
        CompasCUC    += row.valCUC;

        if( row.id > lastCompraID ) lastCompraID = row.id;
        }

      MontoInvers   = GastosCUC + CompasCUC;
      GanancPrecios = MontoPrecios - MontoInvers;

      RecupIdx = (CompasCUC>0)? (MontoInvers/CompasCUC) : 9999;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Guarda en un fichero de texto con tabulación el contenido del grid 'Grid'</summary>
    public static void SaveGrid( DataGridView Grid, string sFootter )
      {
      var dlg = new SaveFileDialog();

     dlg.Filter = "Fichero de resultados (*.txt)|*.txt"  ;
     dlg.FilterIndex = 2 ;
     dlg.RestoreDirectory = true ;

     if( dlg.ShowDialog() != DialogResult.OK ) return;

      var txt = new StringBuilder();

      bool[] ColsVisible = new bool[Grid.Columns.Count];

      for( int i=0; i<Grid.Columns.Count; ++i )
        {
        var Col = Grid.Columns[i];

        ColsVisible[i] = Col.Visible;
        if( !Col.Visible ) continue;

        txt.Append( Col.HeaderText );
        txt.Append( '\t' );
        }

      txt.Append( "\r\n" );

      foreach( DataGridViewRow Row in Grid.Rows )
        {
        for( int i=0; i<Row.Cells.Count; ++i )
          {
          if( !ColsVisible[i] ) continue;

          var Cell = Row.Cells[i];
          string sVal;
          if(  Cell.ValueType == typeof(decimal) )
             sVal = ((decimal)Cell.Value).ToString("0.00");
          else 
            sVal = Cell.Value.ToString();

          txt.Append( sVal );
          txt.Append( '\t' );
          }

        txt.Append( "\r\n" );
        }

      txt.Append( sFootter );

      File.WriteAllText( dlg.FileName, txt.ToString(), Encoding.Default );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Registra un cambio de datos dentro de la sección</summary>
    internal static void SetChanges( string chgkey )
      {
      if( Changes.Contains(chgkey) ) return;

      Changes.Add(chgkey);
      SaveChanges();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Guarda los cambios realizados durante la sección</summary>
    internal static void SaveChanges()
      {
      var lines = Changes.ToArray();
      File.WriteAllLines( "Changes.txt", lines );
      }
    }
  }
