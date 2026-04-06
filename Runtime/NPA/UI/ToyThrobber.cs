using UnityEngine;

namespace NPA.UI
{
    public class ToyThrobber : MonoBehaviour
    {
        private void Update()
        {
            transform.Rotate(0, 0, -360 * Time.deltaTime);
        }
    }
}