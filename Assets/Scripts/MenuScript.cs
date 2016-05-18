using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour 
{

	public Canvas exitMenu;
	public Button startButton;
	public Button quitButton;

	public Canvas creditsMenu;
	public Button creditsButton;

	public Canvas controlsMenu;
	public Button controlsButton;

		public Canvas settingsMenu;
	public Button settingsButton;

	// Use this for initialization
	void Start () 
	{

		exitMenu = exitMenu.GetComponent<Canvas> ();
		startButton = startButton.GetComponent<Button> ();
		quitButton = quitButton.GetComponent<Button> ();
		exitMenu.enabled = false;

		creditsMenu = creditsMenu.GetComponent<Canvas> ();
		creditsMenu.enabled = false;

		controlsMenu = controlsMenu.GetComponent<Canvas> ();
		controlsMenu.enabled = false;

		settingsMenu = settingsMenu.GetComponent<Canvas> ();
		settingsMenu.enabled = false;

	}

	public void ExitPress ()
	{

		exitMenu.enabled = true;
		startButton.enabled = false;
		quitButton.enabled = false;

		creditsButton.enabled = false;
		creditsMenu.enabled = false;

		controlsButton.enabled = false;
		controlsMenu.enabled = false;

		settingsButton.enabled = false;
		settingsMenu.enabled = false;

	}

	public void NoPress ()
	{

		exitMenu.enabled = false;
		startButton.enabled = true;
		quitButton.enabled = true;

		creditsButton.enabled = true;
		creditsMenu.enabled = false;

		controlsButton.enabled = true;
		controlsMenu.enabled = false;

		settingsButton.enabled = true;
		settingsMenu.enabled = false;

	}

	public void CreditPress ()
	{

		exitMenu.enabled = false;
		startButton.enabled = false;
		quitButton.enabled = false;

		creditsButton.enabled = false;
		creditsMenu.enabled = true;

		controlsButton.enabled = false;
		controlsMenu.enabled = false;


		settingsButton.enabled = false;
		settingsMenu.enabled = false;

	}

	public void ControlsPress ()
	{

		exitMenu.enabled = false;
		startButton.enabled = false;
		quitButton.enabled = false;

		creditsButton.enabled = false;
		creditsMenu.enabled = false;

		controlsButton.enabled = false;
		controlsMenu.enabled = true;

		settingsButton.enabled = false;
		settingsMenu.enabled = false;

	}

	public void SettingsPress ()
		{

		exitMenu.enabled = false;
		startButton.enabled = false;
		quitButton.enabled = false;

		creditsButton.enabled = false;
		creditsMenu.enabled = false;

		controlsButton.enabled = false;
		controlsMenu.enabled = false;

		settingsButton.enabled = false;
		settingsMenu.enabled = true;

		}



	//	public void StartGame ()
	//{

	//	SceneManager.LoadScene ();

	//	}

	public void ExitGame ()
	{

		Application.Quit () ;

	}

	// Update is called once per frame
	void Update () 
	{

	}
}


