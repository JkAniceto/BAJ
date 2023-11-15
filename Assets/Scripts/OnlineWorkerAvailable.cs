using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class OnlineWorkerAvailable : MonoBehaviour
{
    public string apiUrl = "http://127.0.0.1:8000/api/getOnlineWorkers";

    IEnumerator Start()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResult = request.downloadHandler.text;

            // Parse JSON response
            HandleWorkerData(jsonResult);
        }
        else
        {
            Debug.LogError("Error fetching worker data: " + request.error);
        }
    }

    void HandleWorkerData(string json)
    {
        // Parse JSON and display worker information
        // Example: Deserialize JSON using JsonUtility or third-party library like Newtonsoft.Json
        // For example purposes, let's assume a Worker class with necessary properties

        // Deserialize JSON response
        WorkerList workerList = JsonUtility.FromJson<WorkerList>(json);

        if (workerList != null && workerList.data != null)
        {
            foreach (var worker in workerList.data)
            {
                Debug.Log("Worker ID: " + worker.id);
                Debug.Log("Worker Name: " + worker.first_name + " " + worker.last_name);

                // Perform actions or display image using worker information
                // Example: Instantiate objects, display images, etc.
            }
        }
    }
}

[System.Serializable]
public class Worker
{
    public int id;
    public string first_name;
    public string last_name;
    // Add other properties needed for a worker
}

[System.Serializable]
public class WorkerList
{
    public Worker[] data;
}

