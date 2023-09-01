using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperGame.FlappyBird
{

    public class Hp : MonoBehaviour
    {
        public float hp;
        public float currenthp;
        public Movement moveMent;
        public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
        public Color damageColor = Color.red; // Color when taking damage
        private Color originalColor; // Store
        public float immunityDuration = 2.0f; // เวลาอมตะ (immunity duration)

        private bool isImmune = false; // สถานะอมตะ

        // Start is called before the first frame update
        void Start()
        {
            currenthp = hp;
            spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
            originalColor = spriteRenderer.color;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeDamage(float damge)
        {
            if (currenthp <= 0 || isImmune)
            {
                return;
            }

            currenthp -= damge;
            Debug.Log("Take Damege");
            StartCoroutine(ApplyImmunity()); // เริ่มการนับเวลาอมตะ
            StartCoroutine(ChangeColorTemporarily());
            if (currenthp <= 0)
            {
                this.moveMent.isDead = true;
            }
        }

        private IEnumerator ApplyImmunity()
        {
            isImmune = true; // เริ่มสถานะอมตะ
            Debug.Log("Immunity");
            yield return new WaitForSeconds(immunityDuration); // รอเวลาอมตะ
            isImmune = false; // สิ้นสถานะอมตะ
        }

        private IEnumerator ChangeColorTemporarily()
        {
            AudioManager.Instance.Play("hurt");
            spriteRenderer.color = damageColor; // Change to damage color

            yield return new WaitForSeconds(0.2f); // Change color temporarily for 0.2 seconds

            spriteRenderer.color = originalColor; // Revert to original color
        }
    }
}