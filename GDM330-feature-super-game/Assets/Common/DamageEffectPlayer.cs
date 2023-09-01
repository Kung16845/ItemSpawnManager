using System.Collections;
using PhEngine.Core;
using UnityEngine;

namespace SuperGame
{
    public class DamageEffectPlayer : Singleton<DamageEffectPlayer>
    {
        [SerializeField] Color damageColor = Color.red;
        protected override void InitAfterAwake()
        {
            
        }

        public void PlayOn(SpriteRenderer spriteRenderer)
        {
            AudioManager.Instance.Play("hurt");
            StartCoroutine(ChangeColorTemporarily(spriteRenderer));
        }
        
        IEnumerator ChangeColorTemporarily(SpriteRenderer spriteRenderer)
        {
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(0.2f); 
            spriteRenderer.color = originalColor;
        }
    }
}