using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanedUI : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    GameObject recruitCanvasPrefab;

    [SerializeField] AudioClip rankUpSound;

    AudioSource audioSource;



    bool go = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void Create(GameObject island)
    {
        string name = island.GetComponent<IslandBase>().name;

        StartCoroutine(DisplayCoroutine());

        audioSource.PlayOneShot(rankUpSound);
    }

    public void OnClickClose()
    {
        if (!go) return;

        go = false;

        if(TutorialManager.Instance.tutorialState == TutorialManager.TutorialState.Cleanning)
        {
            TutorialManager.Instance.NextStep();
            Destroy(this.gameObject);
            return;
        }

   
        //êlçﬁëIëUIï\é¶
        CharacterManager.Instance.CreateCandidateCharacter();
        GameObject obj = Instantiate(recruitCanvasPrefab);
        obj.GetComponent<RecruitCanvas>();
        Destroy(this.gameObject);
    }

    IEnumerator DisplayCoroutine()
    {
        yield return new WaitForSeconds(2.0f);

        go = true;
    }
}
