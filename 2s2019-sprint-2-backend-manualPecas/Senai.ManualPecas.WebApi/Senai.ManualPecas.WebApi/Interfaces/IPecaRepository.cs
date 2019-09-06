using Senai.ManualPecas.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.ManualPecas.WebApi.Interfaces
{
    interface IPecaRepository
    {
        List<Pecas> ListarTodos();
        Pecas BuscarPorId(int id);
        void Cadastrar(Pecas peca);
        void Atualizar(Pecas peca);
        void Excluir(int id);
    }
}
