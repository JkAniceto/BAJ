using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class BookerInputField : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text suggestionText;

    string url = "http://127.0.0.1:8000/api/getallwork";

    private void Start()
    {
        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    void OnInputValueChanged(string input)
    {
        StartCoroutine(GetWorkSuggestions(input));
    }

    IEnumerator GetWorkSuggestions(string input)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to fetch data: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            ProcessWorkSuggestions(jsonResponse, input);
        }
    }

    void ProcessWorkSuggestions(string jsonResponse, string input)
    {
        ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonResponse);

        List<string> suggestions = new List<string>();
        foreach (WorkCategory category in response.data)
        {
            if (category.work_category.ToLower().Contains(input.ToLower()))
            {
                suggestions.Add(category.work_category);
            }
        }

        // Convert the list of suggestions to a single string with line breaks
       // string suggestionsText = string.Join("\n", suggestions);
       // suggestionText.text = suggestionsText;

        // Check the count of suggestions
        Debug.Log("Suggestions count: " + suggestions.Count);

        // Convert the list of suggestions to a single string with line breaks
        string suggestionsText = string.Join("\n", suggestions);

        // Log the suggestions text
        Debug.Log("Suggestions text: " + suggestionsText);

        // Set the text to suggestionText
        suggestionText.text = suggestionsText;
    }
}

[System.Serializable]
public class ApiResponse
{
    public WorkCategory[] data;
}

[System.Serializable]
public class WorkCategory
{
    public int id;
    public string work_category;
}
