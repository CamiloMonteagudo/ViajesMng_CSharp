using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Linq;

namespace UnViaje
  {
  //--------------------------------------------------------------------------------------------------------------------------------------
  /// <summary> Maneja las busqueda cadenas similares a una cadena dada </summary>
  internal class SmartSearch
    {

    //--------------------------------------------------------------------------------------------------------------------------------------
    public string[] Items { get; internal set; }   // Listado de todos los Items

    List<string> cmpItems;                         // Listado de los Items modificados para hacer comparaciones

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Constructor de la clase </summary>
    public SmartSearch()
      { 
      Items = new string[0];
      cmpItems = new List<string>();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Carga toda la información necesaria para la busqueda avanzada </summary>
    internal bool Load()
      {
      try
        {
        Items = File.ReadAllLines("Items.txt");

        foreach( var item in Items )
          {
          var Str = item.ToLower();             // Lleva el items a minusculas

          Str = Str.Replace('á','a');           // Quita todos los acentos
          Str = Str.Replace('é','e');
          Str = Str.Replace('í','i');
          Str = Str.Replace('ó','o');
          Str = Str.Replace('ú','u');

          cmpItems.Add( Str );                  // Lo pone en lista de items de comparación
          }
        return true;
        }
      catch( Exception )
        {
        return false;
        }
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Filtar las cadenas de retorno de acuerdo a su semejanza con el texto 'text' y la posición de edicción </summary>
    internal string[] FilterItems( string text )
      {
      var Lst  = new List<string>();
      var Wrds = ParseWords( text );

      for( int i = 0; i < Items.Length; i++ )
        {
        var Str = cmpItems[i];

        bool found = false;
        for( int j = 0; j < Wrds.Count; j++ )
          {
          var wrd = Wrds[j];                                         
          var idx = Str.IndexOf( wrd );

          found = (idx!=-1);
          if( !found ) break;
          }

        if( found )  
          Lst.Add( Items[i] );
        }

      return Lst.ToArray();
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Filtar las cadenas de retorno de acuerdo a su semejanza con el texto 'text' y la posición de edicción </summary>
    internal string[] FilterItems2( string text )
      {
      var Wrds  = ParseWords2( text );                                                  // Separa el texto a buscar en palabras
     
      var Matched = new List<OraInfo>();
      for( int i=0; i < Items.Length; i++ )                                             // Recorre todas las oraciones para buscar
        {
        var Str = cmpItems[i];                                                          // Toma la oración actual

        bool found=false; long rank=0;  long lenMatch=0;                                // Inicialización de variables
        for( int j=0; j < Wrds.Count; j++ )                                             // Recorre todas la palabras de la cadena de busqueda
          {
          var wrdInfo = Wrds[j];                                                        // Toma la información de la palabra actual
          var sWord   =  wrdInfo.sWord;                                                 // Toma la palabra actual
          
          var idx = Str.IndexOf( sWord );                                               // Busca la palabra en la oración actual

          found = (idx!=-1);                                                            // Si la encontro o no
          if( !found ) break;                                                           // No la encontro, termina el analisis

          var len = sWord.Length;                                                       // Obtiene la cantidad de letras buscadas
          lenMatch += len;                                                              // Acumula la cantidad de letras matcheadas

          var wrdIni = ( idx<=0 || !char.IsLetterOrDigit(Str, idx-1) );                 // Si matcheo el final de la palabra

          var iEnd = idx + len;
          var wrdEnd = ( iEnd>=Str.Length || !char.IsLetterOrDigit(Str, iEnd) );       // Si matcheo el final de la palabra

          rank += 200*len;                                                              // Bonificación por cantidad de letras matcheadas
          if( wrdIni ) rank += 400;                                                     // Bonificación por machear el inicio de la palabra
          if( wrdEnd ) rank += 200;                                                     // Bonificación por machear el final de la palabra

          var dtPos = Math.Abs( idx - wrdInfo.Pos );
          rank += -5 * dtPos;                                                           // Penalización por el corrimiento de la posición
          }

        if( found )  
          {
          rank += -3 * (Str.Length-lenMatch);                                           // Penaliza por los caracteres que no se matchearon  
          Matched.Add( new OraInfo( Items[i], rank ) );                                 // Adiciona información de la oración a una lista
          }
        }

      return Matched.AsParallel().OrderByDescending(x=>x.Rank).Select(x=>x.Text).ToArray();  // Ordena por Rank y retorna un arreglo de oraciones

//      return Matched.AsParallel().OrderByDescending(x=>x.Rank).Select(x=>x.Text+'('+x.Rank+')').ToArray();     // Para pruebas
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Obtiene una lista con todas las palabras en 'text' </summary>
    static Dictionary<char,char> Acentos = new Dictionary<char,char>{ {'á','a'},{'é','e'},{'í','i'},{'ó','o'},{'ú','u'} };
    private List<string> ParseWords( string text )
      {
      var Wrds = new List<string>();

      var Str = text.ToLower();
      var len  = Str.Length;

      char c=' ';
      for( int j = 0; j < len; )                                                  // Recorre todos los caracteres
        {
        for( ; j < len; ++j )                                                     // Salta los caracteres que no son letras
          {
          c = Str[j];
          if( char.IsLetterOrDigit(c) ) break;
          }

        if( j >= len ) break;                                                     // Llego al final de la cadena

        var iWrd = j;                                                             // Inicio de la palabra  
        var Word = new StringBuilder(20);                                         // Crea una palabra vacia
        for(; ; )                                                                 // Obtiene todas las letras seguidas
          {
          if( Acentos.ContainsKey(c) ) c = Acentos[c];                            // Si es una vocal acentuada quita el acento
          Word.Append(c);                                                         // Agrega la letra a la palabra

          ++j;
          if( j >= len ) break;                                                   // Llego al final de la cadena

          c = Str[j];                                                             // Obtine el proximo caracter
          if( !char.IsLetterOrDigit(c) ) break;                                   // El caracter no es una letra
          }

        Wrds.Add(Word.ToString());
        }

      return Wrds;
      }

    //--------------------------------------------------------------------------------------------------------------------------------------
    /// <summary> Divide el texto en palabas y retorna la información sobre las palabaras encontradas </summary>
    private List<WordInfo> ParseWords2( string text )
      {
      var Wrds = new List<WordInfo>();

      var Str = text.ToLower();
      var len  = Str.Length;

      char c=' ';
      for( int j = 0; j < len; )                                                  // Recorre todos los caracteres
        {
        for( ; j < len; ++j )                                                     // Salta los caracteres que no son letras
          {
          c = Str[j];
          if( char.IsLetterOrDigit(c) ) break;
          }

        if( j >= len ) break;                                                     // Llego al final de la cadena

        var iWrd = j;                                                             // Inicio de la palabra  
        var Word = new StringBuilder(20);                                         // Crea una palabra vacia
        for(; ; )                                                                 // Obtiene todas las letras seguidas
          {
          if( Acentos.ContainsKey(c) ) c = Acentos[c];                            // Si es una vocal acentuada quita el acento
          Word.Append(c);                                                         // Agrega la letra a la palabra

          ++j;
          if( j >= len ) break;                                                   // Llego al final de la cadena

          c = Str[j];                                                             // Obtine el proximo caracter
          if( !char.IsLetterOrDigit(c) ) break;                                   // El caracter no es una letra
          }

        Wrds.Add( new WordInfo( Word.ToString(), iWrd ) );
        }

      return Wrds;
      }

    }

  //--------------------------------------------------------------------------------------------------------------------------------------
  /// <summary> Guarda la información de una oración </summary>
  internal class OraInfo
    {
    public string Text;
    public   long Rank;

    public OraInfo( string Text, long rank )
      {
      this.Text = Text;
      this.Rank = rank;
      }
    }

  //--------------------------------------------------------------------------------------------------------------------------------------
  /// <summary> Guarda la información de una palabra </summary>
  public class WordInfo
    {
    public string sWord;
    public     int Pos;

    public WordInfo( string sWord, int pos )
      {
      this.sWord = sWord;
      this.Pos = pos;
      }
    }
  }
