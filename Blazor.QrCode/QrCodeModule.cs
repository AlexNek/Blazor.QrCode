using Microsoft.JSInterop;

namespace Blazor.QrCodeGen
{
    /// <summary>
    /// Class QrCodeModule.
    /// Implements the <see cref="JsInteropObject" />
    /// </summary>
    /// <seealso cref="JsInteropObject" />
    public class QrCodeModule : JsInteropObject
    {
        private QrCodeInstance? _qrCodeInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="QrCodeModule"/> class.
        /// </summary>
        /// <param name="jsInstance">The j s instance.</param>
        /// <param name="moduleRef">The qr code wrapper.</param>
        public QrCodeModule(IJSRuntime jsInstance, IJSObjectReference moduleRef)
            : base(moduleRef)
        {
            JsInstance = jsInstance;

            ModuleRef = moduleRef;
        }
  
        public async Task ShowAsync(string canvasId, string text, QrCodeOptions? qrCodeOptions)
        {
            //await ModuleRef.InvokeVoidAsync("showParameters", canvasId, text);
            if (qrCodeOptions != null && qrCodeOptions.IsModified)
            {
                if (_qrCodeInstance is not null)
                {
                    // force instance refresh
                    await _qrCodeInstance.ClearAsync();
                    await _qrCodeInstance.DisposeAsync();
                    _qrCodeInstance = null;
                }
            }
            if (_qrCodeInstance == null)
            {
                _qrCodeInstance = await QrCodeInstance.CreateAsync(ModuleRef, canvasId, text, qrCodeOptions);
            }
            else
            {
                await _qrCodeInstance.UpdateAsync(text);
            }
        }

        public IJSRuntime JsInstance { get; }

        protected IJSObjectReference ModuleRef { get; }
    }
}
