using UnityEngine;

namespace NPA
{
    [CreateAssetMenu(fileName = "ToyInfo", menuName = "ScriptableObjects/TOY Information Object")]
    public class ToyInfoObject : ScriptableObject
    {
        public string ToyUrl;
        public string ServiceId;
    }
}