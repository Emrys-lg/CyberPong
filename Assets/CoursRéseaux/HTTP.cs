using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class HTTP : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(PostRequest("https://api.restful-api.dev/objects/10", ""));
    }

    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error.ToString());
            }
            else
            {
                Debug.Log("Response: " + request.downloadHandler.text);
            }

        }
    }

    IEnumerator PostRequest(string url, string json)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error.ToString());
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }

}
