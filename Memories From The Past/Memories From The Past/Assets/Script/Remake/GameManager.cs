using UnityEngine.SceneManagement;
using UnityEngine;

namespace Remake
{    
    public sealed class GameManager : MonoBehaviour
    {
        void Awake()
        {
            PlayerCtrl.FallInLimbo += PlayerCtrl_FallInLimbo;
        }

        private void PlayerCtrl_FallInLimbo()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
