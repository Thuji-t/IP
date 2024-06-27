using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DrillProxyReceiver
{
    public ConnectionData ConnectionData;
    private readonly string BaseUrl = @"https://proxima.bigdata.fh-aachen.de:9095";
    private readonly string QueryUrlPostfix = @"query";

    private Uri BaseEndpoint;


    public DrillProxyReceiver(ConnectionData credentials, string baseUrl = @"https://proxima.bigdata.fh-aachen.de:9095")
    {
        this.ConnectionData = credentials;
        this.BaseUrl = baseUrl;
        this.BaseEndpoint = new Uri(baseUrl);
    }

    public async Task<string> RawQueryAsync(string query)
    {
        var receiveTask = Task.Run(() =>
        {
            BaseEndpoint = new Uri(BaseUrl);
            Uri DataEndPoint = new Uri(BaseEndpoint, QueryUrlPostfix);
            string json = SendQuery(DataEndPoint, ConnectionData, query);
            return json;
        });

        return await receiveTask;
    }
    public string RawQuery(string query)
    {
        BaseEndpoint = new Uri(BaseUrl);
        Uri DataEndPoint = new Uri(BaseEndpoint, QueryUrlPostfix);
        return SendQuery(DataEndPoint, ConnectionData, query);
    }


    private static string SendQuery(Uri endpoint, ConnectionData credentials, string query)
    {
        string json = $"{{\"queryType\":\"SQL\", \"query\": \"{query}\", \"format\": \"json\"}}";
        return SendJson(endpoint, credentials, json);
    }

    private static string SendJson(Uri endpoint, ConnectionData credentials, string json)
    {
        HttpClientHandler handler = new HttpClientHandler
        {
            UseCookies = false,
            AllowAutoRedirect = false
        };

        HttpMethod post = new HttpMethod("POST");

        using (HttpClient httpClient = new HttpClient(handler))
        {
            using (HttpRequestMessage request = new HttpRequestMessage(post, endpoint))
            {
                request.Headers.TryAddWithoutValidation("Authorization", credentials.Username + ":" + credentials.Password);
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                // This disables caching. However, bimsim has a delay of 24 hours anyway :)
                // request.Content.Headers.TryAddWithoutValidation("Cache-Control", "max-age=0");

                HttpResponseMessage response = httpClient.SendAsync(request).Result;
                string content = response.Content.ReadAsStringAsync().Result;

                return content;
            }
        }
    }
}
