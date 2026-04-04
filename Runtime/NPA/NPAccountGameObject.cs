using NPA.TOY.Request;
using NPA.TOY.Result;
using UnityEngine;

namespace NPA
{
    public class NPAccountGameObject : MonoBehaviour
    {
        internal void ExecuteRequest<TResult>(ToyRequest<TResult> request) where TResult : ToyResult
        {
            StartCoroutine(request.Execute());
        }
    }
}