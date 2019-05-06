using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;

public class WeatherData : MonoBehaviour
{
    private string url;
    

    [SerializeField]  //show in inspector
    private bool MakeUrlCall;
        
    [SerializeField]  //show in inspector
    private TextMeshPro weatherInfo;

    [SerializeField]  //show in inspector
    private string apiKey;

    [SerializeField]
    private TextMeshPro searchText;

    public TouchScreenKeyboard keyboard;

    private void Update()
    {
        if (MakeUrlCall)
        {
            MakeUrlCall = false;
            url = "https://api.openweathermap.org/data/2.5/weather?q=Stavanger&units=metric&appid=" + apiKey;
            StartCoroutine(GetRequest(url));
        }

        if (keyboard != null)
        {
            searchText.SetText(keyboard.text);
        }
    }

    public void OpenSystemKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
    }

    public void Search()
    {
        if (keyboard != null)
        { 
            var newUrl = "https://api.openweathermap.org/data/2.5/weather?q=" + keyboard.text + "&units=metric&appid=" + apiKey;
            StartCoroutine(GetRequest(newUrl));
        }
    }


    //not threadsafe. will block game at runtime, not a problem for small requests i imagine 
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            weatherInfo.SetText("Loading weather...");

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                weatherInfo.SetText("Error. Check internet connection!");
            }

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
                weatherInfo.SetText("Error: " + webRequest.error);
            }
            else
            {
                var data = webRequest.downloadHandler.text;
                Debug.Log("\nRecived: " + data);

                var a = JSON.Parse(webRequest.downloadHandler.text);

                var weather = "City: " + a["name"].Value + 
                    "\n Weather: " +  a["weather"][0]["description"].Value + 
                    "\n Temp: " + a["main"]["temp"].Value + " °C";

                print(weather);
                weatherInfo.SetText(weather);
            }
        }
    }
}
