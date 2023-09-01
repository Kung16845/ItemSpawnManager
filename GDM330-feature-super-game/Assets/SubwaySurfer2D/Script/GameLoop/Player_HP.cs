using SuperGame.SubwaySurfer2D;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperGame.SubwaySurfer2D
{
    public class Player_HP : MonoBehaviour
    {
        [SerializeField] PlayerMovement move;
        [SerializeField] SpriteRenderer renderer;
        public GameObject Player;
        public int playerHealth = 5;
        public TMP_Text HPText;
        public int coinScore = 0;
        public TMP_Text scoreText;
        // Start is called before the first frame update
        void Start()
        {
            HealthUI();
            CoinScoreUI();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Train")
            {
                Vector2 collisionDirection = other.transform.position - transform.position;

                if (Mathf.Abs(collisionDirection.x) > Mathf.Abs(collisionDirection.y))
                {
                    if(move.currentLine == 1 || move.currentLine == -1) 
                    {
                        move.targetPosition = move.targetmiddle.position;
                        move.currentLine= 0;
                    }

                    else if(move.left == true && move.currentLine == 0)
                    {
                        move.left = false;
                        move.targetPosition = move.targetleft.position;
                        move.currentLine = -1; 
                    }

                    else if(move.right == true && move.currentLine == 0)
                    {
                        move.right = false;
                        move.targetPosition = move.targetright.position;
                        move.currentLine = 1;
                    }
                    TakeDamage();
                }
                else
                {
                    GameManager.Instance.Lose();
                }
            }
        }

        public void TakeDamage()
        {
            playerHealth--;
            DamageEffectPlayer.Instance.PlayOn(renderer);
            if (playerHealth == 0)
            {
                GameManager.Instance.Lose();
            }

            HealthUI();
        }

        public void GetCoins()
        {
            coinScore = coinScore + 5;
            AudioManager.Instance.Play("coin");
            CoinScoreUI();
        }

        void HealthUI()
        {
            HPText.text = "Life :" + playerHealth;
        }

        void CoinScoreUI()
        {
            scoreText.text = "Coin :" + coinScore;
        }
    }
}
