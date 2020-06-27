using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace Meroliqueo
  {
  //--------------------------------------------------------------------------------------------------------------------------------------
  /// <summary> Maneja las busqueda cadenas similares a una cadena dada </summary>
  internal class SmartSearch
    {

    //--------------------------------------------------------------------------------------------------------------------------------------
    public string[] Items { get; internal set; }

    SortedList<string, List<WordIdxs>> WordsData;
    List<string> WordKeys;

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Constructor de la clase </summary>
    public SmartSearch()
      { 
      Items = new string[0];
      WordsData = new SortedList<string, List<WordIdxs>>();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Carga toda la información necesaria para la busqueda avanzada </summary>
    internal bool Load()
      {
      try
        {
        Items = File.ReadAllLines("Items.txt");

        var Words = File.ReadAllLines("Words.idx");

        ParseWordsIndexs( Words );

        WordKeys = new List<string>(WordsData.Keys);

        return true;
        }
      catch( Exception )
        {
        return false;
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Anliza las lineas de texto y obtiene los indices de las palabras </summary>
    private void ParseWordsIndexs( string[] words ) 
      {
      // aaa|22305 22706

      foreach( var wrd in words )
        {
        var WrdAndIdx = wrd.Split('|');        
        if( WrdAndIdx.Length != 2 ) continue;

        var Word = WrdAndIdx[0]; 
        var Data = WrdAndIdx[1]; 

        var Idxs = new List<WordIdxs>();
        for( int i=0; i<Data.Length; i+=5 )
          {
          var sIdxs = Data.Substring(i,5);

          Idxs.Add( new WordIdxs(sIdxs) );
          }

        WordsData[ Word ] = Idxs;
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Filtar las cadenas de retorno de acuerdo a su semejanza con el texto 'text' y la posición de edicción </summary>
    internal string[] FilterItems( string text, int pos )
      {
      var Lst  = new List<string>();
      var Wrds = ParseWords( text, pos, out int idxNow );

      for( int i = 0; i < Items.Length; i++ )
        {
        var Str = Items[i].ToLower();

        bool found = false;
        for( int j = 0; j < Wrds.Count; j++ )
          {
          var wrd = Wrds[j];
          var idx = Str.IndexOf( wrd );

          found = (idx!=-1);
          if( !found ) break;
          }

        if( found )  Lst.Add( Items[i] );
        }

      return Lst.ToArray();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Filtar las cadenas de retorno de acuerdo a su semejanza con el texto 'text' y la posición de edicción </summary>
    internal string[] FilterItems___( string text, int pos )
      {
      var Lst  = new List<string>();
      var Wrds = ParseWords( text, pos, out int idxNow );

      for( int i = 0; i < Wrds.Count; i++ )
        {
        var wrd = Wrds[i];

        if( i == idxNow )
          {
          var idx = WordKeys.BinarySearch( wrd, StringComparer.OrdinalIgnoreCase );  // Busca indice en la lista
          if( idx < 0 )                                                                // No la encontro
            {
            wrd = WordKeys[~idx];                                                    // Obtiene la palabra mas cercana
            Console.WriteLine("Palabra más cercana a '" + Wrds[i] + "' es '" + wrd);
            }
          }

        if( !WordsData.ContainsKey(wrd) ) continue;

        var WrdIdxs = WordsData[wrd];
        foreach( var Idxs in WrdIdxs )
          {
          Lst.Add(Items[Idxs.idxStr]);
          }
        }

      return Lst.ToArray();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Obtiene una lista con todas las palabras en 'text' </summary>
    ///-----------------------------------------------------------------------------------------------------------------------------------
    static Dictionary<char,char> Acentos = new Dictionary<char,char>{ {'á','a'},{'é','e'},{'í','i'},{'ó','o'},{'ú','u'} };
    private List<string> ParseWords( string text, int pos, out int idxNow )
      {
      idxNow = -1;
      var Wrds = new List<string>();

      var Str = text.ToLower();
      var len  = Str.Length;

      char c=' ';
      for( int j = 0; j < len; )                                                  // Recorre todos los caracteres
        {
        for( ; j < len; ++j )                                                     // Salta los caracteres que no son letras
          {
          c = Str[j];
          if( char.IsLetter(c) ) break;
          }

        if( j >= len ) break;                                                     // Llego al final de la cadena

        var iWrd = j;                                                             // Inicio de la palabra  
        var Word = new StringBuilder(20);                                         // Crea una palabra vacia
        for(; ; )                                                                 // Obtiene todas las letras seguidas
          {
          if( Acentos.ContainsKey(c) ) c = Acentos[c];                            // Si es una vocal acentuada quita el acento
          Word.Append(c);                                                         // Agrega la letra a la palabra

          ++j;
          if( pos == j ) idxNow = Wrds.Count;                                     // Indice de la palabra que se esta editando
          if( j >= len ) break;                                                   // Llego al final de la cadena

          c = Str[j];                                                             // Obtine el proximo caracter
          if( !char.IsLetter(c) ) break;                                          // El caracter no es una letra
          }

        if( Word.Length <= 2 && idxNow!=Wrds.Count) continue;                     // Ignora las palabras de 2 letras o menos

        Wrds.Add(Word.ToString());
        }

      return Wrds;
      }
    }

  //--------------------------------------------------------------------------------------------------------------------------------------
  /// <summary> Almacena los indices de una palabra </summary>
  public class WordIdxs
    {
    /// <summary> Indice a la cadena donde se encuentra la palabra </summary>
    public int idxStr;

    /// <summary> Indice a la posicion de la palabra dentro de la cadena </summary>
    public int idxWrd;

    /// <summary> Obtiene los indices desde una cadena de texto </summary>
    public WordIdxs( string sIdxs )
      { 
      var sIStr = sIdxs.Substring(0,3);
      var sIWrd = sIdxs.Substring(3);

      idxStr = Convert.ToInt32(sIStr,16);
      idxWrd = Convert.ToInt32(sIWrd,16);
      }
    }
  }