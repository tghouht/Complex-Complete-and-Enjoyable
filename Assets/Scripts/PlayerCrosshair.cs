using UnityEngine;
using UnityEditor;

public class PlayerCrosshair : MonoBehaviour
{
    [SerializeField]
    private float scale;
    [SerializeField]
    private Texture2D crosshairImage;

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {

    }

    public void OnGUI()
    {
        float xMin = ((float) Screen.width / 2f) - ((float) crosshairImage.width / (scale * 2));
        float yMin = ((float) Screen.height / 2f) - ((float) crosshairImage.height / (scale * 2));
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width / scale, crosshairImage.height / scale), crosshairImage);
    }
}