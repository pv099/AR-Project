using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DownloadPDB : MonoBehaviour {

    public Text myInputField;

    IEnumerator GetText()
    {
        string text_from_input = myInputField.text.ToString();
        Debug.Log(text_from_input);
        string file_name = text_from_input;

        string url = "https://files.rcsb.org/download/" + file_name + ".pdb";
        using (UnityWebRequest www1 = UnityWebRequest.Get(url))
        {
            yield return www1.Send();
            if (www1.isNetworkError || www1.isHttpError)
            {
                Debug.Log(www1.error);
            }
            else
            {
                string savePath = string.Format("{0}/{1}.pdb", Application.persistentDataPath, file_name);
                System.IO.File.WriteAllText(savePath, www1.downloadHandler.text);
            }
        }
    }
    
    // Use this for initialization
    public void Start () {
        StartCoroutine(GetText());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
