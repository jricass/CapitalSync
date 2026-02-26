namespace CapitalSync.Exception.ExceptionsBase;

public abstract class CapitalSyncException : SystemException
{
    // "protected" - impede a criação direta e permite que somente classes derivadas a inicializem
    // "base(message)" - chama o construtor da classe base 'SystemException' inicializando com a propriedade 'message' herdada
    protected CapitalSyncException(string message) : base(message) { }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}