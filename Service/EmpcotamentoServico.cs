using LojaManoelApi.Data.Dtos;
using LojaManoelApi.Data.Entities;
using LojaManoelApi.Interfaces.Services;
using LojaManoelApi.Shared.Exceptions;

namespace LojaManoelApi.Service
{
    public class EmpacotamentoServico:IEmpacotamentoServico
    {
        private readonly List<CaixaDto> _tiposCaixa;

        public EmpacotamentoServico()
        {
            _tiposCaixa = new List<CaixaDto>
            {
            new CaixaDto { CaixaId = "Caixa 1", Dimensoes = new DimensaoDto { Altura = 30, Largura = 40, Comprimento = 80 } },
            new CaixaDto { CaixaId = "Caixa 2", Dimensoes = new DimensaoDto { Altura = 80, Largura = 50, Comprimento = 40 } },
            new CaixaDto { CaixaId = "Caixa 3", Dimensoes = new DimensaoDto { Altura = 50, Largura = 80, Comprimento = 60 } }
            };
        }

        public List<CaixaDto> EmpacotarProdutos(List<ProdutoDto> produtos)
        {
            var caixasUsadas = new List<CaixaDto>();

            var produtosOrdenados = produtos.OrderByDescending(p => p.Dimensao.Volume).ToList();

            foreach (var produto in produtosOrdenados)
            {
                bool alocado = false;

                foreach (var caixa in caixasUsadas)
                {
                    if (CabeNaCaixa(produto, caixa))
                    {
                        caixa.Produtos.Add(produto);
                        caixa.VolumeDisponivel -= produto.Dimensao.Volume;
                        alocado = true;
                        break;
                    }
                }

                if (!alocado)
                {
                    var novaCaixa = _tiposCaixa.FirstOrDefault(c => CabeNaCaixa(produto, c));

                    if (novaCaixa != null)
                    {
                        var caixa = new CaixaDto
                        {
                            CaixaId = novaCaixa.CaixaId,
                            Dimensoes = novaCaixa.Dimensoes,
                            VolumeDisponivel = novaCaixa.Volume - produto.Dimensao.Volume,
                            Produtos = new List<ProdutoDto> { produto }
                        };
                        caixasUsadas.Add(caixa);
                    }
                    else
                    {
                        throw new CaixaForaDoPadrao($"O produto {produto.ProdutoId} com dimensões {produto.Dimensao.Altura}x{produto.Dimensao.Largura}x{produto.Dimensao.Comprimento} não cabe em nenhuma caixa disponível.");
                    }
                }
            }

            return caixasUsadas;
        }

        private bool CabeNaCaixa(ProdutoDto produto, CaixaDto caixa)
        {
            var d1 = new List<double> { produto.Dimensao.Altura, produto.Dimensao.Largura, produto.Dimensao.Comprimento };
            var d2 = new List<double> { caixa.Dimensoes.Altura, caixa.Dimensoes.Largura, caixa.Dimensoes.Comprimento };

            d1.Sort();
            d2.Sort();

            return d1[0] <= d2[0] && d1[1] <= d2[1] && d1[2] <= d2[2];
        }
    }

}
