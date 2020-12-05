using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF_Core_Code_First.Domains;

namespace EF_Core_Code_First.Interfaces
{
    interface IPedidoRepository
    {
        //QUANDO VOCÊ DOCUMENTA O MÉTODO PELA INTERFACE, AO PASSAR O MOUSE SOBRE ELE VOCÊ CONSEGUE VER A DOCUMENTAÇÃO.

        List<Pedido> Listar();
        Pedido Adicionar(List<PedidoProduto> pedidosProdutos);
        Pedido BuscarPorId(Guid id);
    }
}