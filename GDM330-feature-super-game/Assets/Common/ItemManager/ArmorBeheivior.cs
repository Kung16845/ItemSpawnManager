using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperGame
{
    public class ArmorBeheivior : MonoBehaviour
    {
        [SerializeField] GameObject ArmorCore;
        void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.tag == "Obstacle")
            {
                Destroy(collision.gameObject);
                Destroy(ArmorCore.gameObject);
            }
            else if (collision.gameObject.tag == "train")
            {
                Destroy(this.gameObject);
            }
        }
    }
}
