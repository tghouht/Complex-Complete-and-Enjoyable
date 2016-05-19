using UnityEngine;

public class PlayerCrosshair : MonoBehaviour
{
    [SerializeField]
    private float scale;
    [SerializeField]
    private Texture2D crosshairImage;

    public static bool mouseLocked;

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mouseLocked = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mouseLocked = !mouseLocked;
        }

        Cursor.visible = !mouseLocked;
        Cursor.lockState = mouseLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void OnGUI()
    {
        float xMin = ((float) Screen.width / 2f) - ((float) crosshairImage.width / (scale * 2));
        float yMin = ((float) Screen.height / 2f) - ((float) crosshairImage.height / (scale * 2));
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width / scale, crosshairImage.height / scale), crosshairImage);
    }

    public void OnDisable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mouseLocked = false;
    }
}