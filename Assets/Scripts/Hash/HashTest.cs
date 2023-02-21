using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class HashTest : MonoBehaviour
{
    public TMP_InputField _textInput;
    private byte[] goodHash;
    private void Start()
    {
        var hash = Hash("PainSandwish");
        string output = string.Join(", ", hash);
        print(output);
        goodHash = new byte[]
        {
            237, 234, 252, 133, 60, 132, 132, 167, 125, 94, 133, 111, 34, 225, 75, 180, 234, 152, 149, 175, 79, 66, 95,
            7, 101, 212, 178, 13, 134, 2, 233, 128, 236, 133, 110, 81, 252, 229, 26, 3, 1, 119, 150, 244, 45, 233, 65,
            221, 145, 143, 210, 130, 126, 241, 83, 201, 133, 218, 72, 157, 246, 172, 236, 195


        };
    }

    public void VerifyPassword()
    {
        byte[] _inputTry = Hash(_textInput.text);
        
        if (_inputTry.Length != goodHash.Length)
        {
            print("Wrong password");
            return;
        }

        for (int i = 0; i < _inputTry.Length; i++)
        {
            if (_inputTry[i] != goodHash[i])
            {
            print("Wrong password");
            return;
                
            }
        }
        
      
        
        Debug.Log(("Good password"));
    }

    public byte[] Hash(string _input)
    {
        var _inputToByte = System.Text.Encoding.ASCII.GetBytes(_input);
        return SHA512.Create().ComputeHash(_inputToByte).ToArray();
    }

}
