using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{

    public Animator animator;
    
    public void OnEnterButton()
    {

        animator.SetBool("Start",true);

    }

    public void OnExitButton()
    {

        animator.SetBool("Start", false);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
