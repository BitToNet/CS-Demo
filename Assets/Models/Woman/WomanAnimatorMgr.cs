using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanAnimatorMgr : MonoBehaviour
{
    Animator am;
    // Start is called before the first frame update
    void Start()
    {
        am = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // am.SetBool("run", false);
        if (
            Input.GetKeyDown(KeyCode.A)
        // || Input.GetKey(KeyCode.W)
        // || Input.GetKey(KeyCode.S)
        // || Input.GetKey(KeyCode.D)
        )
        {
            // am.SetBool("run", true);
            am.SetInteger("id", 1);
        }
    }
}
