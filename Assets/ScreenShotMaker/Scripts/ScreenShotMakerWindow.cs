//this script makes the ScreenShotMaker Window, and Creates the ScreenShotMaker GameObject on Play

using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class ScreenShotMakerWindow : EditorWindow
{

	public static string FolderName = "ScreenShots"; // create a folder called screenshots
	
	public static float ScreenShotFrequency ; //1 - 10 seconds...the frequency screenshots are taken

	public static bool Enable = false; //weather or not this tool is turned on

	public static bool GameObjectMade = false; //weather or not the ScreenShotMaker GameObject has been made yet


	[MenuItem("Window/ScreenShotMaker")] //creates option in menu to bring up window
	static void SSMW()
	{
		EditorWindow.GetWindow(typeof(ScreenShotMakerWindow)); //creates window
	}
	
	void OnGUI () 
	{
		ScreenShotFrequency = GetScreenShotFrequency(); //get the stored Frequency in PlayerPrefs

		Enable = GetScreenShotEnable(); //get the stored bool in PlayerPrefs

		// Title Label
		GUILayout.Label ("Screen Shot Maker Settings", EditorStyles.boldLabel); 

		//all Settings are Enabled/Disabled using the Enable bool
		Enable = EditorGUILayout.BeginToggleGroup ("Enable", Enable);
			//GUI to change the settings in the window
			FolderName = EditorGUILayout.TextField ("Folder Name: ", FolderName); 
			ScreenShotFrequency = EditorGUILayout.Slider ("ScreenShot Frequency", ScreenShotFrequency, 1f, 10f);
			GUILayout.Label (" ", EditorStyles.boldLabel);
			GUILayout.Label ("Manually take a screenshot with F12", EditorStyles.miniLabel);
		EditorGUILayout.EndToggleGroup ();

		//Save the ScreenShotFrequency and Enable Values
		PlayerPrefs.SetFloat("ScreenShotFrequency",ScreenShotFrequency);

		if (Enable)
		{
			PlayerPrefs.SetString("ScreenShotMakerEnabled","true");
		}
		else
		{
			PlayerPrefs.SetString("ScreenShotMakerEnabled","false");
		}
	}


	void Update()
	{
		//get the ScreenShotFrequency and Enable Values
		ScreenShotFrequency = GetScreenShotFrequency();
		Enable = GetScreenShotEnable();

		//if editor is playing
		if (EditorApplication.isPlaying)
		{
			if (Enable && !GameObjectMade) //if the tool is Enabled and the GameObject has not been created yet...create the game object
			{
				GameObject gameObject = new GameObject("ScreenShotMaker");
				gameObject.AddComponent<ScreenShotMaker>();
				gameObject.GetComponent<ScreenShotMaker>().FolderName = FolderName;
				gameObject.GetComponent<ScreenShotMaker>().ScreenShotFrequency = ScreenShotFrequency;
		      	gameObject.tag = "EditorOnly";
				gameObject.name = "ScreenShotMaker";

				GameObjectMade = true;
			}
		}

	}

	//method for getting the ScreenShotFrequency from PlayerPrefs 
	private float GetScreenShotFrequency()
	{
		if (PlayerPrefs.HasKey("ScreenShotFrequency"))
		{
			return PlayerPrefs.GetFloat("ScreenShotFrequency");
		}
		else
		{
			return 5f;
		}
	}

	//method for getting the Enable from PlayerPrefs 
	private bool GetScreenShotEnable()
	{
		if (PlayerPrefs.HasKey("ScreenShotMakerEnabled"))
		{
			if (PlayerPrefs.GetString("ScreenShotMakerEnabled") == "true")
			{
				return true;
			}
			else
			{
				return false;
			}
			
		}
		else
		{
			return false;
		}
	}


	
}
