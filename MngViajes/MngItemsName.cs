using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static MngViajes.DataBase;

namespace MngViajes
  {
  //--------------------------------------------------------------------------------------------------------------------------------------
  /// <summary> Maneja los nombre de los items usados en todos los viajes</summary>
  internal class MngItemsName
    {
    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Actualiza globalmente todos los nombres de los Items utilizados en todos los modulos registrados </summary>
    internal static void UpdateNames()
      { 
      var Items = new SortedSet<string>();

      for( int i=0; i<App.Viajes.Count; ++i )
        {
        var viaje = App.Viajes[i];

        foreach( ComprasRow row in viaje.tableCompras )
          {
          var Item = row.item;
          if( !Items.Contains(Item) ) 
            Items.Add( Item );
          }
        }

      File.WriteAllLines( "Items.txt", Items.ToArray() );
      }
    } // End class
  } // End namespace