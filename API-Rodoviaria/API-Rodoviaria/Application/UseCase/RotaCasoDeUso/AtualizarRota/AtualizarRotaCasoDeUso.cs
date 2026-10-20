using API_Rodoviaria.Application.DTO_s.Requisicao.RotaRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.RotaRespostaDTO;
using API_Rodoviaria.Domain.Interfaces.IRota;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioRota;
using Microsoft.AspNetCore.Authorization;

namespace API_Rodoviaria.Application.UseCase.RotaCasoDeUso.AtualizarRota
{
    public class AtualizarRotaCasoDeUso : IAtualizarRotaCasoDeUso
    {
        private readonly IRepositorioRota _repositorioRota;

        public AtualizarRotaCasoDeUso(IRepositorioRota repositorioRota)
        {
            _repositorioRota = repositorioRota;
        }

        public async Task<RespostaCriarRota> ExecutarAsync(int id, RequisicaoAtualizarRotaDTO requisicao)
        {
            var rota = await _repositorioRota.ObterPorIdAsync(id) ?? throw new KeyNotFoundException("Rota não encontrada");

            if (string.IsNullOrWhiteSpace(requisicao.EnderecoInicial)) requisicao.EnderecoInicial = rota.EnderecoInicio;
            if(string.IsNullOrWhiteSpace(requisicao.EnderecoFinal)) requisicao.EnderecoFinal= rota.EnderecoFim;
            
            rota.EnderecoInicio = requisicao.EnderecoInicial.Trim();
            rota.EnderecoFim = requisicao.EnderecoFinal.Trim();

            await _repositorioRota.AtualizarAsync(rota);

            return new RespostaCriarRota
            {
                Id = rota.Id,
                EnderecoInicio = rota.EnderecoInicio,
                EnderecoFim = rota.EnderecoFim,
            };
        }
    }
}
