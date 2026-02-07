using Microsoft.AspNetCore.Components;

namespace Blazor.QrCodeGen
{
    public partial class QrCode
    {
        private const string InitialText = "qrcode example text";

        private string? _errorMessage;

        private bool _hasRendered;

        private QrCodeOptions? _options;

        private QrCodeModule? _qrCodeModule;

        private int? _size;

        private string _text = InitialText;

        [Parameter]
        public string CanvasId { get; set; } = "qrcode";

        public string? ErrorMessage => _errorMessage;

        [Inject]
        public ModuleCreator? ModuleCreator { get; set; }

        [Parameter]
        public QrCodeOptions? Options { get; set; }

        [Parameter]
        public int? Size { get; set; }

        [Parameter]
        public string Text { get; set; } = InitialText;

        /// <summary>
        /// Method invoked after each time the component has been rendered. This is good for operations that depend on the DOM.
        /// </summary>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!_hasRendered && firstRender)
            {
                _hasRendered = true;
                _errorMessage = null;

                try
                {
                    // Add a small delay to ensure DOM is fully ready for JavaScript interop
                    await Task.Delay(50);

                    if (Options is not null && !Options.Equals(_options))
                    {
                        _options = Options;
                        _text = Text;
                        _size = Size;
                        if (_size.HasValue)
                        {
                            _options.Size = _size.Value;
                        }

                        await UpdateTextAsync(_text, _options);
                    }
                    else if (_size.HasValue && _options is null)
                    {
                        var qrCodeOptions = new QrCodeOptions { Size = 0 };
                        qrCodeOptions.Size = _size.Value;
                        await UpdateTextAsync(_text, qrCodeOptions);
                    }
                    else
                    {
                        await UpdateTextAsync(_text);
                    }
                }
                catch (Exception ex)
                {
                    _errorMessage =
                        $"Failed to render QR code for element '{CanvasId}': {ex.Message}";
                    StateHasChanged();
                }
            }
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            bool shouldUpdate = false;

            if (Options is not null && !Options.Equals(_options))
            {
                _options = Options;
                _text = Text;
                _size = Size;
                if (_size.HasValue)
                {
                    _options.Size = _size.Value;
                }

                shouldUpdate = true;
            }
            else if (_text != Text)
            {
                _text = Text;
                shouldUpdate = true;
            }
            else if (_size != Size && _options is null)
            {
                _size = Size;
                shouldUpdate = true;
            }

            if (shouldUpdate && _hasRendered)
            {
                if (Options is not null)
                {
                    await UpdateTextAsync(_text, _options);
                }
                else if (_size.HasValue)
                {
                    var qrCodeOptions = new QrCodeOptions { Size = 0 };
                    qrCodeOptions.Size = _size.Value;
                    await UpdateTextAsync(_text, qrCodeOptions);
                }
                else
                {
                    await UpdateTextAsync(_text);
                }
            }
        }

        private async Task CreateOrUpdateQrCode(string text, QrCodeOptions? qrCodeOptions = null)
        {
            try
            {
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
            catch (Exception ex)
            {
                _errorMessage = $"Error creating QR code: {ex.Message}";
                throw;
            }
        }

        private async Task UpdateTextAsync(string text, QrCodeOptions? qrCodeOptions = null)
        {
            await Task.Delay(100);
            await CreateOrUpdateQrCode(text, qrCodeOptions);
        }
    }
}
