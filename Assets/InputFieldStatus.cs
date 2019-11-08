using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFieldStatus : MonoBehaviour {

    public GameObject input_button;
    public GameObject QRCode_button;
    public GameObject selectPDB_button;
    public GameObject showMolecule_button;
    public GameObject download_button;

    public void InputButtonStatus()
    {
        if (input_button.activeInHierarchy == true)
        {
            input_button.SetActive(false);
        }
        else
        {
            input_button.SetActive(true);
        }

        if (QRCode_button.activeInHierarchy == true)
        {
            QRCode_button.SetActive(false);
        }
        else
        {
            QRCode_button.SetActive(true);
        }

        if (selectPDB_button.activeInHierarchy == true)
        {
            selectPDB_button.SetActive(false);
        }
        else
        {
            selectPDB_button.SetActive(true);
        }

        if (showMolecule_button.activeInHierarchy == true)
        {
            showMolecule_button.SetActive(false);
        }
        else
        {
            showMolecule_button.SetActive(true);
        }

        if (download_button.activeInHierarchy == true)
        {
            download_button.SetActive(false);
        }
        else
        {
            download_button.SetActive(true);
        }
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
