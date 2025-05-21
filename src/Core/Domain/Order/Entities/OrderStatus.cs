using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order.Entities
{
    public enum OrderStatus
    {
        Recebido = 0,
        EmPreparacao = 1,
        Pronto = 2,
        Finalizado = 3,
        Cancelado = 4
    }
}
