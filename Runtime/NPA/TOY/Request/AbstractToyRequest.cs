using System.Collections;

namespace NPA.TOY.Request
{
    internal abstract class AbstractToyRequest
    {
        public abstract IEnumerator Execute();
    }
}