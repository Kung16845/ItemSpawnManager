using UnityEngine;

namespace SuperGame.DoodleJump
{
   public class ColliderJumpLogic : MonoBehaviour
   {
      public CapsuleCollider2D cap;
      void OnTriggerStay2D(Collider2D other)
      {
         if(other.gameObject.tag == "ground"||other.gameObject.tag == "Superjump")
         {
            cap.isTrigger = true;
         }
      }
      void OnTriggerExit2D(Collider2D other)
      {
         if(other.gameObject.tag == "ground"||other.gameObject.tag == "Superjump")
         {
            cap.isTrigger = false;
         }
      }
    
   }
}
