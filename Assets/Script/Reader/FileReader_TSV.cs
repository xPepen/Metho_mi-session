using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileReader_TSV
{
    private Dictionary<string, string> m_DictonnaryReader;
    private string m_Path;
    
    private const string m_fileType = ".tsv";

    /// <summary>
    /// path file must be inside Application.streamingAssets Folder
    /// </summary>
    /// <param name="path"></param>
    public FileReader_TSV(string pathFile, string setMCurrentKey)
    {
        m_DictonnaryReader = new();
        m_Path = Path.Combine(Application.streamingAssetsPath, pathFile + m_fileType);
        Init();
    }

    private void Init()
    {
        List<string> possibleKey = new();
        using (StreamReader streamReader = new StreamReader(m_Path))
        {
            string[] keys = streamReader.ReadLine()!.Split('\t');
            possibleKey.AddRange(keys.Skip(1));

            while (!streamReader.EndOfStream)
            {
                ReadOnlySpan<string> data = streamReader.ReadLine()!.Split('\t');
                for (int i = 1; i < data.Length; i++)
                {
                    ReadOnlySpan<char> key = data[0] + possibleKey[i - 1];
                    ReadOnlySpan<char> value = data[i];
                    m_DictonnaryReader.Add(key.ToString(), value.ToString());
                }
            }
        }
    }


    public string GetValueWithCurrentKey(ReadOnlySpan<char> _key)
    {
        return m_DictonnaryReader[_key.ToString()];
    }
}