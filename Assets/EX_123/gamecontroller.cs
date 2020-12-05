using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamecontroller : MonoBehaviour
{
    public dungeon dungeon;
    public Animator tresureBox;
    public GameObject ark;
    public GameObject thirdperson;
    public GameObject arkCamera;
    public GameObject UI;
    public GameObject startUI;
    public GameObject startCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dungeon.dead == true)
        {
            tresureBox.SetBool("open", true);
        }
    }

    public void ClickStartButton()
    {
        Debug.Log("Start");
        startCamera.SetActive(false);
        startUI.SetActive(false);

        ark.SetActive(true);
        arkCamera.SetActive(true);
        thirdperson.SetActive(true);
        
        UI.SetActive(true);
    }
}

