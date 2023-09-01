using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperGame.DoodleJump
{
    public class ScreenWrap : MonoBehaviour
    {
        private Camera mainCamera;
        private float screenWidth;
        public string sceneToLoad; // The name of the scene to load when player falls out of the screen

        private void Start()
        {
            mainCamera = Camera.main;
            screenWidth = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        }

        private void Update()
        {
            Vector3 currentPosition = transform.position;
            Vector3 newPosition = currentPosition;

            if (currentPosition.x < -screenWidth)
            {
                newPosition.x = screenWidth;
            }
            else if (currentPosition.x > screenWidth)
            {
                newPosition.x = -screenWidth;
            }

            transform.position = newPosition;

            CheckIfPlayerIsOutOfCameraView();
        }

        private void CheckIfPlayerIsOutOfCameraView()
        {
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

            if (viewportPosition.y < 0) // Player falls out of the screen
            {
                LoadScene();
            }
        }

        private void LoadScene()
        {
            GameManager.Instance.Lose();
        }
    }
}
