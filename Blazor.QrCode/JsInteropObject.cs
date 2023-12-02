using Microsoft.JSInterop;

namespace Blazor.QrCodeGen
{
    /// <summary>
    /// Class BabylonObject.
    /// Base object
    /// </summary>
    public class JsInteropObject : IAsyncDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsInteropObject"/> class.
        /// </summary>
        /// <param name="jsObjRef">The java script object reference.</param>
        protected JsInteropObject(IJSObjectReference jsObjRef)
        {
            JsObjRef = jsObjRef;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.</summary>
        /// <returns>A task that represents the asynchronous dispose operation.</returns>
        public async ValueTask DisposeAsync()
        {
            await JsObjRef.DisposeAsync().ConfigureAwait(false);
        }

        public IJSObjectReference JsObjRef { get; }
    }
}
