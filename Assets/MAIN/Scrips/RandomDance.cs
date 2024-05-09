using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDance : MonoBehaviour
{
    private Animator anim;

    private IEnumerator Start()
    {
        anim = GetComponent<Animator>();

        while(true)
        {
            yield return new WaitForSeconds(1);

            anim.SetInteger("DanceIndex", Random.Range(0, 5));
            anim.SetTrigger("Dance");
        }
    }
}
