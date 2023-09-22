public interface IEditorDebugLogger
{
    bool EnabledPrintDebugLogInEditor { get; set; }
    void PrintLogInEditor(string text);
}