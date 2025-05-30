using AutoMapper;
using LojaManoelApi.Data.Dtos;
using LojaManoelApi.Data.Entities;
using LojaManoelApi.Interfaces.Repositories;
using LojaManoelApi.Interfaces.Services;

namespace LojaManoelApi.Service
{
    public class PedidoServico : IPedidoServico
    {
        private readonly IPedidoRepositorio _pedidoRepository;
        private readonly IMapper _mapper;

        public PedidoServico(IPedidoRepositorio pedidoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PedidoDto>> GetAllAsync()
        {
            var pedidos = await _pedidoRepository.ObterTodos();
            return _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
        }

        public async Task<PedidoDto?> GetByIdAsync(int id)
        {
            var pedido = await _pedidoRepository.ObterPorId(id);
            return pedido == null ? null : _mapper.Map<PedidoDto>(pedido);
        }

        public async Task AddAsync(PedidoDto pedidoDto)
        {
            var pedidoEntity = _mapper.Map<Pedido>(pedidoDto);

            await _pedidoRepository.Adicionar(pedidoEntity);
        }

        public async Task UpdateAsync(int id, PedidoDto updatedPedidoDto)
        {
            var existingPedido = await _pedidoRepository.ObterPorId(id);
            if (existingPedido == null) throw new Exception("Pedido não encontrado");

            _mapper.Map(updatedPedidoDto, existingPedido);
            await _pedidoRepository.Atualizar(existingPedido);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pedido = await _pedidoRepository.ObterPorId(id);
            if (pedido == null) return false;

            await _pedidoRepository.Deletar(id);
            return true;
        }
    }
}
