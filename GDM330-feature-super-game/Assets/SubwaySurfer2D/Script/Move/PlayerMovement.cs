using System.Collections;
using UnityEngine;

namespace SuperGame.SubwaySurfer2D
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool left = false;
        public bool right = false;
        public Transform targetleft;
        public Transform targetmiddle;
        public Transform targetright;
        public float speed;
        public float currentLine;
        public Vector3 targetPosition;
        private Collider2D playerCollider;
        private SpriteRenderer playerJumpColor;

        void Start()
        {
            playerCollider = GetComponent<Collider2D>();
            playerJumpColor = GetComponent<SpriteRenderer>();
            targetPosition = targetmiddle.position;
            currentLine = 0;
        }
        IEnumerator Jump()
        {
            playerCollider.enabled = false;
            playerJumpColor.color = Color.magenta;
            yield return new WaitForSeconds(0.5f);
            playerCollider.enabled = true;
            playerJumpColor.color = Color.white;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentLine == 0)
                {
                    targetPosition = targetleft.position;
                    currentLine--;
                }
                else if (currentLine == 1)
                {
                    targetPosition = targetmiddle.position;
                    currentLine--;
                    right = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentLine == 0)
                {
                    targetPosition = targetright.position;
                    currentLine++;
                }
                else if (currentLine == -1)
                {
                    targetPosition = targetmiddle.position;
                    currentLine++;
                    left = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                AudioManager.Instance.Play("jump");
                StartCoroutine("Jump");
            }
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
}