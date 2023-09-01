using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperGame.DoodleJump
{
    public class foot : MonoBehaviour
    {
        void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag == "ground")
            {
                GetComponent<CapsuleCollider2D>().isTrigger = false;
            }

        }
    }
}