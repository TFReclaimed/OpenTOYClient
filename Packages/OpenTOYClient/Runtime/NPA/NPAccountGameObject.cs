using NPA.TOY.Request;
using UnityEngine;

namespace NPA
{
    public class NPAccountGameObject : MonoBehaviour
    {
        internal void ExecuteRequest(AbstractToyRequest request)
        {
            StartCoroutine(request.Execute());
        }
    }
}