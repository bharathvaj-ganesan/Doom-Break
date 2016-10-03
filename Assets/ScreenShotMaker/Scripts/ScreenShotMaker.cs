using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using System.Collections;

public class ScreenShotMaker : MonoBehaviour {


	public string FolderName = "ScreenShots"; // create a folder called screenshots

	public float ScreenShotFrequency = 5f; //-1 or 0 will turn auto off
	private float LSSTm = 0f; //last time screenshot was taken


	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(gameObject); //no not destory this GameObject

		//creates a screenshot folder in asset folder if there isn't one
		CreateScreenShotFolder(); 

	}


	private void CreateScreenShotFolder() 
	{
		if (!Directory.Exists(Application.dataPath + "/" + FolderName))
		{
			AssetDatabase.CreateFolder("Assets",FolderName );
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(
			(
			Time.time - LSSTm >= ScreenShotFrequency) //if it's time to take a screenshot or...
			|| 
			( Input.GetKeyDown(KeyCode.F12)) //F12 is pressed
			)
		{

			print ("ScreenShot Taken 'Screenshot_" + System.DateTime.Now.ToString() + "'"); //print that a screenshot was created

			string TimeTag = System.DateTime.Now.ToString().Replace("/","").Replace(" ","").Replace(":",""); //get DatetimeTag
			Application.CaptureScreenshot("Assets/" + FolderName + "/Screenshot_" + TimeTag + ".png"); //Save the Image

			LSSTm = Time.time; //update last screenshot time
		}

	}
	

}
