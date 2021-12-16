using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    public Animator animator;

    public AudioClip sound1;
    AudioSource audioSource;

    public void OnEnterButton()
    {
        audioSource.PlayOneShot(sound1);
        animator.SetBool("Start",true);
    }

    public void OnExitButton()
    { 
        animator.SetBool("Start", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
