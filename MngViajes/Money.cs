using System;
using System.Text;

namespace MngViajes
  {
  //------------------------------------------------------------------------------------------------------------------
  ///<summary>Define los tipos de monedas usados por el sistema</summary>
  //------------------------------------------------------------------------------------------------------------------
  public enum Mnd:sbyte
    {          ///<summary>Peso cubano convertible</summary>
    Cuc = 0,   ///<summary>Dolar de Estados Unidos</summary> 
    Usd = 1,   ///<summary>Peso cubano o Moneda nacional</summary>
    Cup = 2,   ///<summary>Peso de Republica Dominicana</summary>
    Dop = 3,   ///<summary>Tipo de moneda sin definir</summary>   
    NA  =-1   
    }


  public class Money
    {
    public decimal UsdToCuc = 0.935m;   
    public decimal CucToCup = 25m;
    public decimal UsdToDop = 48.8m;                    
    public decimal CupToCuc;
    public decimal DopToUsd;

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Contructor del objeto </summary>
    public Money( decimal UsdToCuc, decimal CucToCup, decimal UsdToDop )
      {
      this.UsdToCuc = UsdToCuc;
      this.CucToCup = CucToCup;
      this.UsdToDop = UsdToDop;

      CupToCuc = 1.0m/CucToCup;
      DopToUsd = 1.0m/UsdToDop;
      }

  //--------------------------------------------------------------------------------------------------------------------------------------
  /// <summary></summary>
  internal decimal GetValue( string text )
      {
      Mnd moneyType = Mnd.NA;
      return GetValue( text, ref moneyType );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary></summary>
    internal decimal GetValue( string text, ref Mnd moneyType )
      {
      var sNum    = new StringBuilder();
      var sMoneda = new StringBuilder();

      bool noPunto = true;
      for( int i = 0; i<text.Length; i++ )
        {
        var c = text[i];
        if( c==' ' ) continue;

        var noNum    = (sNum.Length==0);
        var noMoneda = (sMoneda.Length==0);

        if( c=='-' &&  noNum && noMoneda )
          sNum.Append( c );
        else if( c=='.' && noPunto && noMoneda )
          {
          sNum.Append( c );
          noPunto = false;
          }
        else if( char.IsDigit( c ) && noMoneda )
          sNum.Append( c );
        else if( char.IsLetter( c ) && moneyType>=0 )
          sMoneda.Append( c );
        else
          throw new Exception( "Caracter no válido para el valor esperado" );
        }

      var val = decimal.Parse( sNum.ToString() );

      if( sMoneda.Length!=0 )
        {
        moneyType = Idx( sMoneda.ToString() );
        if( moneyType == Mnd.NA )
          throw new Exception( "El tipo de moneda no es reconocido" );
        }

      return val;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene el código de 3 letras que represeta la moneda</summary>
    internal string Code( Mnd moned )
      {
      switch( moned )
        {
        case Mnd.Usd: return "usd";
        case Mnd.Cup: return "mn";
        case Mnd.Dop: return "dop";
        }

      return "cuc";
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene el tipo de moneda con teniendo el código de 3 letras</summary>
    internal Mnd Idx( string moned )
      {
      var sMnd = moned.Trim().ToUpper();

      switch( sMnd )
        {
        case "CUC": return Mnd.Cuc;
        case "USD": return Mnd.Usd;
        case "MN" : return Mnd.Cup;
        case "CUP": return Mnd.Cup;
        case "DOP": return Mnd.Dop;
        }

      return Mnd.NA;
      }

    public Mnd     LastMoney;
    public decimal LastValue;
    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene la cantidad de dinero representada por 'text' en CUC</summary>
    public decimal GetCucValue( string text, Mnd DefMoney=Mnd.Cuc )
      {
      LastMoney = DefMoney;
      LastValue = GetValue( text, ref LastMoney );

      return Convert( LastValue, LastMoney, Mnd.Cuc );
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Obtiene el ultimo valor leido por GetCucValue, en un formato con la especificación de la moneda</summary>
    public string FormatLastValue()
      {
      return LastValue.ToString("0.##") + ' ' + Code(LastMoney);
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>Convierte un tipo de moneda en otro</summary>
    internal decimal Convert( decimal prec, Mnd oldMoney, Mnd newMoney )
      {
      if( newMoney == oldMoney ) return prec;

      var precCuc = prec;
      switch( oldMoney )
        {
        case  Mnd.Usd: precCuc *= UsdToCuc; break;
        case  Mnd.Cup: precCuc *= CupToCuc; break;
        case  Mnd.Dop: precCuc *= DopToUsd * UsdToCuc; break;
        }

      var precConv = precCuc;
      switch( newMoney )
        {
        case  Mnd.Usd: precConv = precCuc / UsdToCuc; break;
        case  Mnd.Cup: precConv = precCuc * CucToCup; break;
        case  Mnd.Dop: precConv = precCuc / DopToUsd / UsdToCuc; break;
        }

      return precConv;
      }
    }
  }
