using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Read_v2 : MonoBehaviour {
    public Transform Molecule;
    public Transform SphereC;
    public Transform SphereO;
    public Transform SphereN;
    public Transform SphereP;
    public Text myInputField;
    Transform carbon;
    Transform nitrogen;
    Transform oxygen;
    Transform phosphate;
    List<float> list = new List<float>();
    float posX;
    float posY;
    float posZ;
    int cpt;
    public string filePath;

    public void ChargerPDB()
	{
        //GameObject Molecule = new GameObject("Molecule");

        foreach(Transform child in Molecule)
        {
            GameObject.Destroy(child.gameObject);
        }

        // Get string from input

        //string text_from_input = myInputField.text.ToString();
        //Debug.Log(text_from_input);

        //string file_path = text_from_input;
        //Debug.Log(file_path);
        FileSelectorExample _FileSelectorExample = GameObject.Find("ARCamera").GetComponent<FileSelectorExample>();
        filePath = _FileSelectorExample.file_selected;
        string inp_ln;
        StreamReader inp_stm = new StreamReader(filePath, Encoding.ASCII);

        while (!inp_stm.EndOfStream)
		{
			inp_ln = inp_stm.ReadLine();
			// Do Something with the input. 
			// If the line begins with "ATOM" then add the x, y, z pos to an array
			if (inp_ln.StartsWith("ATOM") || inp_ln.StartsWith("HETATM"))
            { // select current line beginning with "ATOM"
                cpt = 0;
                MatchCollection matches = Regex.Matches(inp_ln, @"([0-9]{2}|[0-9])\.[0-9]{3}"); // select the coordinates xxx.xxx -> example 345.268
                foreach (Match match in matches) {
                    foreach (Capture capture in match.Captures)
                    {
                        cpt++;
                        list.Add(float.Parse(capture.Value, CultureInfo.InvariantCulture.NumberFormat)); // to capture each match, normally 3 matches per line --- I need to convert the string to float 
                        if (cpt == 3) // to only capture three first matches
                        {
                            posX = list[0];
                            posY = list[1];
                            posZ = list[2];
                            list.Clear();
                            if(inp_ln.EndsWith("C  "))
                            {
                                carbon = Instantiate(SphereC, new Vector3(posX, posY, posZ), Quaternion.identity);
                                carbon.transform.SetParent(Molecule.transform, false);
                                //carbon.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                            }
                            else if(inp_ln.EndsWith("O  "))
                            {
                                oxygen = Instantiate(SphereO, new Vector3(posX, posY, posZ), Quaternion.identity); //as GameObject;
                                oxygen.transform.SetParent(Molecule.transform, false);
                                //oxygen.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

                            }
                            else if(inp_ln.EndsWith("N  "))
                            {
                                nitrogen = Instantiate(SphereN, new Vector3(posX, posY, posZ), Quaternion.identity); //as GameObject;
                                nitrogen.transform.SetParent(Molecule.transform, false);
                               // nitrogen.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

                            }
                            else if(inp_ln.EndsWith("P  "))
                            {
                                phosphate = Instantiate(SphereP, new Vector3(posX, posY, posZ), Quaternion.identity); //as GameObject;
                                phosphate.transform.SetParent(Molecule.transform, false);
                                //phosphate.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

                            }
                            break;
                        }
                    }
                }
			} 
		}
        inp_stm.Close();
    }
}

		