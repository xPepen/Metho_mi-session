using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class UnityWebRequestBuilder
{
    private string m_Url;
    private string m_Method;
    private string json;
    private bool m_IsRevocable;

    public UnityWebRequest Build()
    {
        var UnityRequest = new UnityWebRequest(m_Url, m_Method);
        if (checkForNull(ref m_Url) && checkForNull(ref m_Method))
        {
            Debug.LogError("Invalid build for UnityWebRequestBuilder");
        }
        UnityRequest.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
        UnityRequest.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);

        if (m_IsRevocable)
            UnityRequest.SetRequestHeader("X-Parse-Revocable-Session", "1");
        if (!string.IsNullOrEmpty(json))
        {
            UnityRequest.SetRequestHeader("Content-type", "application/json");
            UnityRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        }

        UnityRequest.downloadHandler = new DownloadHandlerBuffer();

        return UnityRequest;
    }

    public UnityWebRequestBuilder SetJson(string json)
    {
        this.json = json; 
        return this;
    }

    public UnityWebRequestBuilder SetUrl(string url)
    {
        m_Url = url;
        return this;
    }

    public UnityWebRequestBuilder SetMethod(string method)
    {
        m_Method = method;
        return this;
    }

    public UnityWebRequestBuilder Revocable()
    {
        m_IsRevocable = true;
        return this;
    }

    private bool checkForNull(ref string value)
    {
        return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
    }
    
}