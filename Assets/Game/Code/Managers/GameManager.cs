using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Inject]
        public void Construct(ITimeService timeService, IAirManager airManager)
        {
            timeService.Start();
            airManager.AddAir(1f);
            airManager.StartDeflation();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}