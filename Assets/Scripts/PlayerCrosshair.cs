using UnityEngine;
using UnityEngine.Networking;

public class PlayerCrosshair : NetworkBehaviour
{
    [SerializeField]
    private float scale;
    [SerializeField]
    private Texture2D crosshairImage;

    public void Start()
    {
        if (!isLocalPlayer) return;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
        }

        Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void OnGUI()
    {
        float xMin = ((float) Screen.width / 2f) - ((float) crosshairImage.width / (scale * 2f));
        float yMin = ((float) Screen.height / 2f) - ((float) crosshairImage.height / (scale * 2f));
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width / scale, crosshairImage.height / scale), crosshairImage);
    }

    public void OnDisable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}