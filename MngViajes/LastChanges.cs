using System;
using System.IO;

namespace MngViajes
  {
  //--------------------------------------------------------------------------------------------------------------------------------------
  /// <summary> Maneja los cambios realizados en la ultima llamada a un modulo externo </summary>
  internal class LastChanges
    {
    private string chgText = "";

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Crea el objeto y carga los últimos cambios realizado en el camino dado </summary>
    public LastChanges()
      {
      try
        {
        chgText = File.ReadAllText( "Changes.txt" );
        }
      catch
        { 
        chgText = "";
        }

      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Determina si se realizo un cambio del tipo especificado, si 'sChg' es vacio significa cualquier cambio</summary>
    internal bool Changed( string sChg = "" )
      { 
      if( chgText == "" ) return false;
      if( sChg    == "" ) return true;

      return chgText.Contains(sChg);
      }
    }
  }