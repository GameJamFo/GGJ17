using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {

    public GameObject ExplosionPrefab;
    public int Stage = 0;

    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public GameObject JammerPrefab;

    private bool loading = false;

    private int screenCount = 0;

    private void Start()
    {
        StartCoroutine(FadeOut("Ready To Go Boom?", true));
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.M) && Application.isEditor)
        {
            screenCount++;
            Application.CaptureScreenshot(string.Format("Screenshot{0}.png", screenCount));
        }
        GameObject.Find("StageText").GetComponent<Text>().text = string.Format("Stage: {0}", Stage);
	}

    void ResetScene(int Stage)
    {
        List<GameObject> removable = new List<GameObject>();
        removable.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        removable.AddRange(GameObject.FindGameObjectsWithTag("Jammer"));
        removable.AddRange(new[] { GameObject.FindWithTag("Player") });
        for(int i = 0; i < removable.Count; i++)
        {
            Destroy(removable[i]);
        }
        GetComponent<AudioSource>().Play();
        Vector3 PlayerPosition = RandomNavMeshPoint.GetRandomPointOnNavMesh();
        GameObject newPlayer = Instantiate(PlayerPrefab, PlayerPosition, Quaternion.identity);
        for(int i = 0; i < Stage; i++)
        {
            Vector3 enemyPosition = GetPointWithDistanceOf(PlayerPosition, 10);
            GameObject nEnemy = Instantiate(EnemyPrefab, enemyPosition, Quaternion.identity);
        }

        Vector3 jammerPosition = GetPointWithDistanceOf(PlayerPosition, 15);
        GameObject nJammer = Instantiate(JammerPrefab, jammerPosition, Quaternion.identity);

        StartCoroutine(FadeIn());
    }

    public Vector3 GetPointWithDistanceOf(Vector3 point, float v)
    {
        Vector3 p = RandomNavMeshPoint.GetRandomPointOnNavMesh();
        while (Vector3.Distance(point, p) < v)
        {
            p = RandomNavMeshPoint.GetRandomPointOnNavMesh();
        }
        return p;
    }

    public IEnumerator FadeIn()
    {
        float startTime = Time.time;
        Vortex vortex = FindObjectOfType<Vortex>();
        CanvasGroup fIN = GameObject.Find("GameUI").GetComponent<CanvasGroup>();
        CanvasGroup fOUT = GameObject.Find("InfoText").GetComponent<CanvasGroup>();
        while (vortex.angle != 0 || fIN.alpha != 1 || fOUT.alpha != 0)
        {
            yield return new WaitForEndOfFrame();
            float t = (Time.time - startTime);
            vortex.angle = Mathf.SmoothStep(vortex.angle, 0, t / 10);
            fIN.alpha = Mathf.SmoothStep(0, 1, t / 3);
            fOUT.alpha = Mathf.SmoothStep(1, 0, t / 3);
        }
        loading = false;
    }

    public IEnumerator FadeOut(string msg, bool nextLvl)
    {
        if (!loading)
        {
            loading = true;
            float startTime = Time.time;
            Vortex vortex = FindObjectOfType<Vortex>();
            CanvasGroup fIN = GameObject.Find("GameUI").GetComponent<CanvasGroup>();
            CanvasGroup fOUT = GameObject.Find("InfoText").GetComponent<CanvasGroup>();
            GameObject.Find("InfoText").GetComponent<Text>().text = msg;
            if (vortex != null)
            {
                while (vortex.angle != 0 || fIN.alpha != 1 || fOUT.alpha != 0)
                {
                    yield return new WaitForEndOfFrame();
                    float t = (Time.time - startTime);
                    vortex.angle = Mathf.SmoothStep(0, -1000, t / 10);
                    fIN.alpha = Mathf.SmoothStep(1, 0, t / 3);
                    fOUT.alpha = Mathf.SmoothStep(0, 1, t / 3);
                }
            }
            if (nextLvl == false)
            {
                yield return new WaitForSeconds(1);
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(0);
            }
            else
            {
                yield return new WaitForSeconds(1);
                ResetScene(Stage++);
            }
        }
    }

}
