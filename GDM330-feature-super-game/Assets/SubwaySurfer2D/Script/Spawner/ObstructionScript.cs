using UnityEngine;

namespace SuperGame.SubwaySurfer2D
{
    public class ObstructionScript : MonoBehaviour
    {
        [SerializeField] private float ObstructionSpeed;
        [SerializeField] Rigidbody2D rb;
        void Start()
        {
            Invoke("Destroy",3);    
        }
        void Destroy()
        {
            Destroy(this.gameObject);
        }
        void Update()
        {
            rb.velocity = Vector2.down * ObstructionSpeed;
        }

    }
}
