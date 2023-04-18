using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class BinaryReaderWriter
{
    private static readonly string m_Path = Application.persistentDataPath;
    private static readonly string m_FileType = ".bin";


    private static string FilePath(string fileName)
    {
        return Path.Combine(m_Path , fileName + m_FileType);
    }

    public static void Serialize(int level, int money,int playtime, string fileName)
    {
        Hashtable hashvalues = new Hashtable();
        hashvalues.Add("Level", level);
        hashvalues.Add("money", money);
        hashvalues.Add("playtime", playtime);

        var path = FilePath(fileName);
        FileStream fs = File.Exists(path) ? new FileStream(path, FileMode.Open) : new FileStream(path, FileMode.Create);

        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, hashvalues);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }

    public static void Deserialize(string fileName, out Hashtable hash)
    {
        var path = FilePath(fileName);
        FileStream fs = new FileStream(path, FileMode.Open);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            hash = (Hashtable)formatter.Deserialize(fs);
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }
}