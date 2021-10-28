using System;

namespace WS.Core.Shared.ModelViews
{
    public class ErrorResponse
    {
        public string Id { get; set; }
        public string RequestId { get; set; }
        public DateTime Data { get; set; }
        public string Mensagem { get; set; }
        
        public ErrorResponse( string id, string requestId)
        {
            Id = id;
            RequestId = requestId;
            Data = DateTime.Now;
            Mensagem = "Erro inesperado";

        }
    }
}
