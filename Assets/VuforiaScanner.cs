using UnityEngine;
using System;
using System.Collections;
using Vuforia;
using System.Threading;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine.Networking;

[AddComponentMenu("System/VuforiaScanner")]
public class VuforiaScanner : MonoBehaviour
{
    private bool cameraInitialized;
    private BarcodeReader barCodeReader;
    private bool activateQRCodeReader = false;
    public string downloadedFileName;
    private string urlExtracted = "https://files.rcsb.org/download/0000";

    public void activateReader()
    {
        if (activateQRCodeReader == false)
        {
            activateQRCodeReader = true;
            Start();
        }
    }

    void Start()
    {
        if (activateQRCodeReader == true)
        {
            barCodeReader = new BarcodeReader();
            StartCoroutine(InitializeCamera());
        }
        if (activateQRCodeReader == false)
        {
            StartCoroutine(GetText1(urlExtracted, downloadedFileName));
        }
    }

    IEnumerator InitializeCamera()
    {
        // Waiting a little seem to avoid the Vuforia's crashes.
        yield return new WaitForSeconds(1.25f);

        var isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(Vuforia.Image.PIXEL_FORMAT.GRAYSCALE, true);
        Debug.Log(String.Format("FormatSet : {0}", isFrameFormatSet));

        // Force autofocus.
        var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        if (!isAutoFocus)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        }
        Debug.Log(String.Format("AutoFocus : {0}", isAutoFocus));
        cameraInitialized = true;
    }

    IEnumerator GetText1(string url1, string file_name1)
    {
        using (UnityWebRequest www1 = UnityWebRequest.Get(url1))
        {
            yield return www1.Send();
            if (www1.isNetworkError || www1.isHttpError)
            {
                Debug.Log(www1.error);
            }
            else
            {
                string savePath1 = string.Format("{0}/{1}.pdb", Application.persistentDataPath, file_name1);
                System.IO.File.WriteAllText(savePath1, www1.downloadHandler.text);
            }
        }
    }

    void Update()
    {
        if (cameraInitialized)
        {
            try
            {
                var cameraFeed = CameraDevice.Instance.GetCameraImage(Vuforia.Image.PIXEL_FORMAT.GRAYSCALE);
                if (cameraFeed == null)
                {
                    return;
                }
                var data = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.Gray8);
                if (data != null)
                {
                    // QRCode detected.
                    Debug.Log("DECODED TEXT FROM QR: " + data.Text);
                    urlExtracted = data.Text;
                    Match match = Regex.Match(data.Text, @"[0-9][A-Z][0-9]{2}");
                    downloadedFileName = match.ToString();
                    Debug.Log(downloadedFileName);
                    activateQRCodeReader = false;
                    cameraInitialized = false;
                    Start();
                    return;
                }
                else if (Input.GetKeyUp(KeyCode.Return))
                {
                    activateQRCodeReader = false;
                    cameraInitialized = false;
                }
                else
                {
                    Debug.Log("No QR code detected !");
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
