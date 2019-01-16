using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using GBLib;

public class ResourceManager : MonoBehaviour {

    //Sound Controller
    public SoundController Sounds;
    //Ad controller
    public AdController Ads;
    //Settings
    public Settings GameSettings;
	//Storage
	public StorageSystem Storage;
	//Scene Changer
	public SceneChanger SceneChanger;

	public bool AutoLoadScene;

    public string LoadSceneName;

    private static ResourceManager TheOne;

    public static ResourceManager Get() {
        return TheOne;
    }

    void Awake() {
        if (TheOne != null) {
            GameObject.Destroy(this.gameObject);
        } else if (SceneManager.GetActiveScene().name != "Startup") {
            Debug.Log("Using a duplicate ResourceManager, hopefully intentionally for debug purposes.");
            TheOne = this;
        } else {
            Debug.Log("Initializing the one true Resource Manager");
            TheOne = this;
        }
		Storage = new StorageSystem();
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        GameSettings.Reload();
		if (AutoLoadScene) {
			GoToScene();
		}
        
    }

    void GoToScene() {
		if (SceneManager.GetActiveScene().name.Equals(LoadSceneName)) {
			//we're starting in the first scene, so don't bother switching to it
			//hopefully this is because we're starting on a scene for debugging
			return; 
		} else {
			SceneManager.LoadScene(LoadSceneName);
		}
    }
	
}
