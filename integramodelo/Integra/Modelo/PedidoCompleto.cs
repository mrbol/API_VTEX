namespace Integra.Modelo
{
    using System;
    using System.Collections.Generic;

    public class PedidoCompleto
    {
        public string clientName;
        public string creationDate;
        public List<PedidoItem> items;
        public string orderId;
        public string salesChannel;
        public List<itensValorTotal> totals;
        public string totalValue;
    }
}

