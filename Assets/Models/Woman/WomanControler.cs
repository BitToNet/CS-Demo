using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanControler : MonoBehaviour
{

    public SkinnedMeshRenderer body;
    public SkinnedMeshRenderer eye;
    public SkinnedMeshRenderer face;
    public SkinnedMeshRenderer hair;
    public SkinnedMeshRenderer hand;
    public SkinnedMeshRenderer pouch;
    public Material eyeball1;
    public Material skin1;
    public Material ware1;
    public Material eyeball2;
    public Material skin2;
    public Material ware2;
    public Material eyeball3;
    public Material skin3;
    public Material ware3;
    Animator am;
    // Start is called before the first frame update
    void Start()
    {
        am = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        am.SetBool("run", false);
        if (
            Input.GetKeyDown(KeyCode.A)
        || Input.GetKey(KeyCode.W)
        || Input.GetKey(KeyCode.S)
        || Input.GetKey(KeyCode.D)
        )
        {
            am.SetBool("run", true);
            // am.SetInteger("id", 1);
        }
        // 换肤
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // am.SetInteger("id", 1);
            body.material = ware1;
            eye.material = eyeball1;
            face.material = skin1;
            hair.material = skin1;
            hand.material = ware1;
            pouch.material = ware1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            body.material = ware2;
            eye.material = eyeball2;
            face.material = skin2;
            hair.material = skin2;
            hand.material = ware2;
            pouch.material = ware2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            body.material = ware3;
            eye.material = eyeball3;
            face.material = skin3;
            hair.material = skin3;
            hand.material = ware3;
            pouch.material = ware3;
        }

    }
}
