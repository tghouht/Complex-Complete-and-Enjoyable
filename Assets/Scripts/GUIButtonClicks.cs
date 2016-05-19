using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIButtonClicks : MonoBehaviour
{
    public void onClickStart()
    {
        GameObject titleCanvas = GameObject.Find("InputField1");
        string text = titleCanvas.GetComponent<InputField>().text;
        SceneChanger.ipAddress = text;
        GameManager.instance = null;
        Debug.Log("Player has clicked start button with ip address: " + text);

        SceneManager.LoadScene(1);
    }
}