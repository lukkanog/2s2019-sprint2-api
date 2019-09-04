using Senai.AutoPecas.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.AutoPecas.WebApi.Interfaces
{
    public interface IPecaRepository
    {
        List<Pecas> Listar(int idFornecedor);
        Pecas BuscarPorId(int idPeca,int idUsuario);
        void Cadastrar(Pecas peca);
        void Atualizar(Pecas peca);
        void Excluir(Pecas peca);
    }
}
