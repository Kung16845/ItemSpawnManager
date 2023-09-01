using UnityEngine;

namespace SuperGame.DoodleJump
{
    public class character : MonoBehaviour
    {
        private Rigidbody2D rb;
    
        [SerializeField] float jumpforce = 100f;
        public float jumpboost = 2f;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
       
        }
        void OnCollisionEnter2D(Collision2D other)
        {
            var otherObjectTag = other.gameObject.tag;
            if(otherObjectTag == "ground")
            { 
                PlayJumpSound();
                rb.AddForce(Vector2.up*jumpforce);
            }
            if(otherObjectTag == "Superjump")
            { 
                PlayJumpSound();
                rb.AddForce(Vector2.up*jumpforce*jumpboost);
            }
        }

        void PlayJumpSound()
        {
            AudioManager.Instance.Play("jump");
        }
    }
}
