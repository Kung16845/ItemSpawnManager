using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperGame
{
    public class ArmorMoveWithPlayer : MonoBehaviour
    {
        Transform player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            transform.position = new Vector3(player.position.x,player.position.y+1)   ;
        }
    }
}
