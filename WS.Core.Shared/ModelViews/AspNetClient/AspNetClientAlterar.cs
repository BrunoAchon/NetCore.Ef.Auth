namespace WS.Core.Shared.ModelViews
{
    /// <summary>
    /// Objeto utilizado para alteração do cliente para utilizar as ferramentas
    /// </summary>
    public class AspNetClientAlterar : AspNetClientNovo
    {
        /// <summary>
        /// ID do cliente
        /// </summary>
        /// <example>1</example>>
        public int ClientId { get; set; }
    }
}
