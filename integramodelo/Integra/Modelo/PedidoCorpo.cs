namespace Integra.Modelo
{
    using System;
    using System.Collections.Generic;

    public class PedidoCorpo
    {
        public string clientName;
        public string creationDate;
        public List<PedidoItem> items;
        public string orderId;
        public string salesChannel;
        public string totalValue;
    }
}

