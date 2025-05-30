using LojaManoelApi.Data.Dtos;
using LojaManoelApi.Data.Entities;

namespace LojaManoelApi.Interfaces.Services
{
    public interface IEmpacotamentoServico
    {
        List<CaixaDto> EmpacotarProdutos(List<ProdutoDto> produtos);
    }
}
