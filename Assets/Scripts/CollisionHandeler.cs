
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandeler : MonoBehaviour
{
    [SerializeField] float  levelLoadDelay  = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem successPartical;
    [SerializeField] ParticleSystem deathPartical;
   
    AudioSource audio;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();



    }


     void Update()
    {
        RespondToDebugKeys();

        
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();

        }
        else if (Input.GetKeyDown(KeyCode.C))
        {

            collisionDisabled = !collisionDisabled;

        }

    }
     void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning  || collisionDisabled) { return; }
        {

                switch (collision.gameObject.tag)
            { 
                case "Friendly":
                    Debug.Log("Welcome");
                    break;

                case "Finish":

                    NextLevelCrash();

                    break;

                default:

                    StartCrashSequence();

                    break;

            }
        
        }

    }

    void StartCrashSequence()
    {

        isTransitioning = true;
        audio.Stop();

        GetComponent<Movment>().enabled = false;

        Invoke("ReloadLevel", levelLoadDelay);
        audio.PlayOneShot(death);
        deathPartical.Play();
       

    }


    void NextLevelCrash()
    {
        isTransitioning = true;
        audio.Stop();
        audio.PlayOneShot(success);
        successPartical.Play();
        GetComponent<Movment>().enabled = false;

        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {

        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      
        SceneManager.LoadScene(CurrentSceneIndex);

    }


    void LoadNextLevel()
    {


        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = CurrentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {

            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);


    }

}
