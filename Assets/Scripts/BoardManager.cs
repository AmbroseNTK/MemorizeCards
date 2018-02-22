using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {

    public int width = 2;
    public int height = 1;
    public Image imgWatchOut;
    public Image imgTouchIt;
    public Image imgCorrect;
    public Image panelTitle;
    public Text textRound;

    public Text textStep;
    public Button btPlayAgain;

    public int round = 1;
    private List<Vector2> path;
    private int step = 0;
    private bool isOnShowing = false;

    public AudioClip audioGameOver;
    public AudioClip audioCorrect;
    public AudioClip audioError;
    public AudioClip audioYourTurn;
    public AudioClip audioWatchOut;

    AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        panelTitle.GetComponent<Animator>().SetBool("isShow", false);
        btPlayAgain.GetComponent<Animator>().SetBool("isShow", true);
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    IEnumerator ShowBoard()
    {
        Debug.Log("Showing Board...");
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Debug.Log("Anim");
                GameObject.Find(j.ToString() + "," + i.ToString()).GetComponent<Animator>().SetBool("isShow", true);
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield break;
    }

    public void HideBoard()
    {
        Debug.Log("Hiding Board...");
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Debug.Log("Anim");
                GameObject.Find(j.ToString() + "," + i.ToString()).GetComponent<Animator>().SetBool("isShow", false);

            }
        }

    }

    IEnumerator CreateNewRound()
    {
        isOnShowing = true;
        step = 0;
        HideBoard();
        textRound.text = "Round " + round;
       
        panelTitle.GetComponent<Animator>().SetBool("isShow", true);
        yield return new WaitForSecondsRealtime(1);
        imgWatchOut.GetComponent<Animator>().SetBool("isShow", true);
        PlayClip(audioWatchOut);
        yield return new WaitForSecondsRealtime(2);
        imgWatchOut.GetComponent<Animator>().SetBool("isShow", false);
        yield return new WaitForSecondsRealtime(1);
        Vector2 preVec = new Vector2();
        Vector2 randVec = new Vector2();
        path = new List<Vector2>();
        for (int i = 0; i < 0.03f*Mathf.Pow(round,2)+2; i++)
        {
            do
            {
                randVec = new Vector2(Random.Range(0, width), Random.Range(0, height));
            }
            while (randVec == preVec);
            yield return new WaitForSeconds(1f);
            GameObject.Find(randVec.x.ToString() + "," + randVec.y.ToString()).GetComponent<Animator>().SetBool("isShow", true);
            yield return new WaitForSeconds(1f);
            GameObject.Find(randVec.x.ToString() + "," + randVec.y.ToString()).GetComponent<Animator>().SetBool("isShow", false);
            path.Add(randVec);
            preVec = randVec;
        }
        yield return new WaitForSecondsRealtime(1);
        imgTouchIt.GetComponent<Animator>().SetBool("isShow", true);
        yield return new WaitForSecondsRealtime(1);
        imgTouchIt.GetComponent<Animator>().SetBool("isShow", false);
        textStep.text = "Your step 0/" + path.Count;
        PlayClip(audioYourTurn);
        StartCoroutine(ShowBoard());
        isOnShowing = false;
        yield break;
    }

    public void ClickCard(string name)
    {
        if (!isOnShowing)
        {
            if (step < path.Count)
            {
                int x = int.Parse(name.Split(',')[0]);
                int y = int.Parse(name.Split(',')[1]);
                Vector2 currVec = new Vector2(x, y);
                if (path[step] == currVec)
                {
                    step++;
                    textStep.text = "Your steps " + step + "/" + path.Count;
                }
                else
                {

                    StartCoroutine(DoGameOver());
                    
                    return;
                }
            }

            if (step == path.Count)
            {
                round++;
                PlayClip(audioCorrect);
                StartCoroutine(DoCorrect());
                StartCoroutine(CreateNewRound());
            }
        }
    }
    private IEnumerator DoGameOver()
    {
        HideBoard();
        PlayClip(audioError);
        yield return new WaitForSeconds(1);
        PlayClip(audioGameOver);
        textStep.text = "";
        panelTitle.GetComponent<Animator>().SetBool("isShow", false);

        //Leaderboard

        yield return new WaitForSeconds(2f);
        btPlayAgain.GetComponent<Animator>().SetBool("isShow", true);
        round = 1;
        yield break;
    }
    private IEnumerator DoCorrect()
    {
        textStep.text = "Done";
        imgCorrect.GetComponent<Animator>().SetBool("isShow", true);
        yield return new WaitForSeconds(2f);
        imgCorrect.GetComponent<Animator>().SetBool("isShow", false);
        yield return new WaitForSeconds(2f);
        yield break;
    }

    public void PlayAgain()
    {
        round = 1;
        btPlayAgain.GetComponent<Animator>().SetBool("isShow", false);
        StartCoroutine(CreateNewRound());
    }

}
