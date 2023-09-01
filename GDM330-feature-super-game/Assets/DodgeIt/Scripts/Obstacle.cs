using UnityEngine;

namespace SuperGame.DodgeIt
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<Player>(out var player))
            {
                player.TakeDamage();
            }
        
            Destroy(gameObject);
        }
    }
}
