using System;
using System.Drawing;
using System.Windows.Forms;
using static UnViaje.DBViaje;

namespace UnViaje
  {
  public partial class frmUnViaje : Form
    {
    //--------------------------------------------------------------------------------------------------------------------------------------
    public frmUnViaje()
      {
      InitializeComponent();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    private void frmUnViaje_Load( object sender, EventArgs e )
      {
      Datos.GetParametros();
      Datos.Configuration();
      Datos.LoadDataBase();

      if( Datos.Titulo.Length > 0 )  Text = Datos.Titulo;

      int iTab = Datos.GetIntParam( "Tab" );
      if( iTab != -1 ) Tab.SelectedIndex = iTab;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cada vez que se cambia de un Tab a otro</summary>
    private void Tab_Selecting( object sender, TabControlCancelEventArgs e )
      {
      int iTab = Datos.GetIntParam( "Tab" );
      if( e.TabPageIndex != iTab ) 
        Datos.DelParam( "Tab" );

      Datos.SaveDataBase();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se muestra el tab de estadisticas</summary>
    private void tabEstadist_Enter( object sender, EventArgs e )
      {
      UpdateSumary();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se muestra el tab de presupuesto</summary>
    private void tabPresupuesto_Enter( object sender, EventArgs e )
      {
      ctlPresupuesto1.RefreshData();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se muestra el tab de gastos</summary>
    private void tabGastos_Enter( object sender, EventArgs e )
      {
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se muestra el tab de compras</summary>
    private void tabCompras_Enter( object sender, EventArgs e )
      {
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se muestra el tab de precios</summary>
    private void tabPrecios_Enter( object sender, EventArgs e )
      {
      //ctlPrecios1.RefreshData();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se muestra el tab de ventas</summary>
    private void tabVentas_Enter( object sender, EventArgs e )
      {

      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se muestra el tab de cobros</summary>
    private void tabCobros_Enter( object sender, EventArgs e )
      {

      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Se llama cuando se va a cerrar el formulario principal</summary>
    private void frmUnViaje_FormClosing( object sender, FormClosingEventArgs e )
      {
      Datos.SaveDataBase();
      Datos.SaveChanges();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Actualiza todos los datos pertenceientes al sumaio</summary>
    private void UpdateSumary()
      {
      Datos.GetPresupuesto();
      Datos.GetGastos();
      Datos.GetCompras();
      Datos.GetVentas();
      Datos.CobrosSumary();

      lbPresupuesto.Text  = Datos.totalCUC.ToString("0.00") + " CUC";
      lbPresupUtiliz.Text = Datos.MontoInvers.ToString("0.00") + " CUC";
      lbPresupSobra.Text  = (Datos.totalCUC - Datos.MontoInvers).ToString("0.00") + " CUC";

      lbGastos.Text      = Datos.GastosCUC.ToString("0.00") + " CUC";
      lbCompras.Text     = Datos.CompasCUC.ToString("0.00") + " CUC";

      decimal GastoIndex = 0m;
      if( Datos.CompasCUC > 0 )  GastoIndex = (Datos.GastosCUC) / Datos.CompasCUC;

      lbGastosRate.Text  =  GastoIndex.ToString("0.00") + " (" + (GastoIndex*100).ToString("0.0") + "%)" ;

      lbMontoTotal.Text = "Monto de la Inversión "+ Datos.MontoInvers.ToString("0.00")  +" CUC";

      lbMontoPrecios.Text    = Datos.MontoPrecios.ToString("0.00") + " CUC";
      lbGananciaPrecios.Text = Datos.GanancPrecios.ToString("0.00") + " CUC";

      var GanancIndex = 0m;
      if( Datos.MontoInvers != 0 )  GanancIndex = Datos.MontoPrecios/Datos.MontoInvers;

      lbGananciaIndice.Text  = GanancIndex.ToString("0.0") + " (" + ((GanancIndex-1)*100).ToString("0.0") + "%)" ;

      var PrecioIndex = 0m;
      if( Datos.CompasCUC != 0 )  PrecioIndex = Datos.MontoPrecios/Datos.CompasCUC;

      lbPrecioIndex.Text  = PrecioIndex.ToString("0.0") + " (" + ((PrecioIndex-1)*100).ToString("0.0") + "%)" ;

      lbMontoVentas.Text    = Datos.MontoVentas.ToString("0.00") + " CUC";
      lbGananciasVenta.Text = Datos.GanacVentas.ToString("0.00") + " CUC";

      lbMontoConsumo.Text      = Datos.MontoConsumo.ToString("0.00") + " CUC";
      lbMontoConsumoRecp.Text  = Datos.MontoConsumoRecp.ToString("0.00") + " CUC";
      lbMontoConsumoTotal.Text = (Datos.MontoConsumoRecp + Datos.GanacConsumo).ToString("0.00") + " CUC";
      lbGananciaConsumo.Text   = Datos.GanacConsumo.ToString("0.00") + " CUC";

      lbGananciaVentaTotal.Text = (Datos.GanacVentas+Datos.GanacConsumo).ToString("0.00") + " CUC";

      lbCantModfPrecio.Text = Datos.NumChgPrecio.ToString();
      lbMontoModfPrecio.Text = Datos.MontoChgPrecio.ToString("0.00") + " CUC";

      lbCantPorVender.Text  = Datos.NumSinVender.ToString();
      lbMontoPorVender.Text = Datos.MontoSinVender.ToString("0.00") + " CUC";

      lbCantDevoluciones.Text  = Datos.NumDevoluc.ToString();
      lbMontoDevoluciones.Text = Datos.MontoDevoluc.ToString("0.00") + " CUC";

      lbCobroTotal.Text = "Monto cobrado "+ Datos.MontoCobros.ToString("0.00")  +" CUC";

      var GanancPagada     = Datos.MontoCobros - Datos.MontoInvers;
      var GanancSinConsumo = Datos.MontoCobros - (Datos.MontoInvers - Datos.MontoConsumo);
      var GanancConConsumo = GanancPagada + Datos.GanacConsumo;

      lbGananciaCobros.Text         = GanancPagada.ToString("0.00") + " CUC";
      lbGananciaCobrosSConsumo.Text = GanancSinConsumo.ToString("0.00") + " CUC";
      lbGananciaCobrosCConsumo.Text = GanancConConsumo.ToString("0.00") + " CUC";

      lbGananciaCobros.ForeColor         = (GanancPagada<0)? Color.Red :  Color.ForestGreen;
      lbGananciaCobrosSConsumo.ForeColor = (GanancSinConsumo<0)? Color.Red :  Color.ForestGreen;
      lbGananciaCobrosCConsumo.ForeColor = (GanancConConsumo<0)? Color.Red :  Color.ForestGreen;

      lbCantPorCobrar.Text  = Datos.NumSinPagar.ToString("0.0");
      lbMontoPorCobrar.Text = Datos.MontoSinPagar.ToString("0.00") + " CUC";

      lbCucToCup.Text = Money.CucToCup.ToString("0.0000") + " CUP";
      lbUsdToCuc.Text = Money.UsdToCuc.ToString("0.0000") + " CUC";
      lbUSDtoDop.Text = Money.UsdToDop.ToString("0.0000") + " DOP";
      }

    }
  }
