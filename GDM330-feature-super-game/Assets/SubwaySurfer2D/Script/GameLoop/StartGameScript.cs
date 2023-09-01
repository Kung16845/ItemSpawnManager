using UnityEngine;

namespace SuperGame.SubwaySurfer2D
{
    public class StartGameScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.Pause();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.Resume();
                this.gameObject.SetActive(false);
            }
        }
    }
}
