using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterPanel : MonoBehaviour
{
    public int research, work;  // キャラクターのパラメータ
    public Text researchText, workText;

    private int index;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        research = Random.Range(0, 101);
        work = Random.Range(0, 101);

        researchText.text = "研究　" + research.ToString();
        workText.text = "作業　" + work.ToString();

        index = Random.Range(1, 6);
        image = transform.Find("Character/Image").GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>($"character{index}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
