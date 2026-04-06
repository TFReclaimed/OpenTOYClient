using UnityEngine;

namespace NPA.UI
{
    public abstract class AbstractToyScreen : MonoBehaviour
    {
        protected ToyUi toyUi;

        public void Initialize(ToyUi toyUi)
        {
            this.toyUi = toyUi;
        }
    }
}