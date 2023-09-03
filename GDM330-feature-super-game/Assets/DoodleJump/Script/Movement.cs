using UnityEngine;

namespace SuperGame.DoodleJump
{
    public class Movement: MonoBehaviour
    {
        private float horizontal;
        private Rigidbody2D rb;
        [SerializeField] private float speed = 10f;
    
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            horizontal = Input.GetAxisRaw("Horizontal");

        }
        void FixedUpdate()
        {
            if (ItemEffect.Instance.isPoisoning) { rb.velocity = new Vector2(horizontal * speed * Time.deltaTime*(-1), rb.velocity.y); }
            else {rb.velocity = new Vector2(horizontal*speed*Time.deltaTime,rb.velocity.y); }
            
        }
    }
}
