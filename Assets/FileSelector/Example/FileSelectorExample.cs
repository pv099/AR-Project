using UnityEngine;
using System.Collections;

public class FileSelectorExample : MonoBehaviour {
	
	private GUIStyle style;
	private string path = "";
	private bool windowOpen;
	public string file_selected;
    bool browserStatus = false;

    void Start()
	{
		style = new GUIStyle();
		style.fontSize = 30;
		
		style.normal.textColor = Color.white;
	}
	
	void OnGUI(){
		//Instructions
		GUI.Label(new Rect(10, 10, 500, 500), "", style); //"Press [enter] to open File Selection Window."
        GUI.Label(new Rect(10, 60, 500, 500), "", style); //"Path : "+path
    }
	
	// Update is called once per frame
	void Update () {
		
		//if we don't have an open window yet, and enter is down.
		if(!windowOpen && browserStatus == true)
		{
			FileSelector.GetFile(GotFile); //generate a new FileSelector window
			windowOpen = true; //record that we have a window open
		}
	}
				
	//This is called when the FileSelector window closes for any reason.
	//'Status' is an enumeration that tells us why the window closed and if 'path' is valid.
	string GotFile(FileSelector.Status status, string path){
		Debug.Log("File Status : "+status+", Path : "+path);
		this.path = path;
        file_selected = this.path;
        Debug.Log(file_selected);
        this.windowOpen = false;
        browserStatus = false;

        return file_selected;
	}

    public string GetFilePath()
    {
        return file_selected;
    }

    public void FileBrowserStatus()
    {
        if(browserStatus == false)
        {
            browserStatus = true;
        }
    }
}
