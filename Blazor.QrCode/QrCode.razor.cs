using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.QrCode
{
    public partial class QrCode
    {
        private const string InitialText = "qrcode example text";

        private QrCodeModule? _qrCodeModule = null;

        private string _text = InitialText;

        private QrCodeOptions? _options = null;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(false);
            if (firstRender)
            {
                try
                {
                    //if (_qrCodeModule is not null)
                    //{
                    //    await _qrCodeModule.DisposeAsync();
                    //}
                    //await CreateOrUpdateQrCode(_text);
                }
                catch (Exception ex)
                {
                    if (ModuleCreator != null)
                    {
                        await ModuleCreator.JsInstance.InvokeVoidAsync("console.log", $"***Exception:{ex}").ConfigureAwait(false);
                    }
                }
            }
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in
        /// the render tree, and the incoming values have been assigned to properties.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> representing any asynchronous operation.</returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            

            if (Options is not null && !Options.Equals(_options))
            {
                _options = Options;
                _options.SetChangedMode();
                //if (_text != Text)
                {
                    _text = Text;
                }
                await UpdateTextAsync(_text, _options);
            }

            if (_text != Text && _options is null)
            {
                _text = Text;
                await UpdateTextAsync(_text);
            }
        }

        private async Task CreateOrUpdateQrCode(string text, QrCodeOptions? qrCodeOptions = null)
        {
            //await JS.InvokeVoidAsync("console.log", "***Try create JS qr code module instance");
            
            if (ModuleCreator != null && _qrCodeModule is null)
            {
                _qrCodeModule = await ModuleCreator.CreateAsync();
                await _qrCodeModule.ShowAsync(CanvasId, text, qrCodeOptions);
            }
            else if (_qrCodeModule is not null)
            {
                await _qrCodeModule.ShowAsync(CanvasId, text, qrCodeOptions);
            }
        }

        private async Task UpdateTextAsync(string text, QrCodeOptions? qrCodeOptions = null)
        {
            await CreateOrUpdateQrCode(text, qrCodeOptions);
        }

        [Inject]
        public ModuleCreator? ModuleCreator { get; set; }

        [Parameter]
        public string Text { get; set; } = InitialText;

        //[Inject]
        //private IJSRuntime? JS { get; set; }
    }
}
