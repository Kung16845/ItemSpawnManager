using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhEngine.Core;

namespace SuperGame
{
    public class Player_CheckEnterItem : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Heart")
            {
                ItemEffect.Instance.CollectItem_Heart();
            }
            else if (collision.gameObject.tag == "Armor")
            {
                ItemEffect.Instance.CollectItem_Armor();
            }
            else if (collision.gameObject.tag == "Poison")
            {

            }
            Destroy(collision.gameObject);
        }
    }
}
