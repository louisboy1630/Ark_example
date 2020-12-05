using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamecontroller : MonoBehaviour
{
    public dungeon dungeon;
    public Animator tresureBox;
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
}
