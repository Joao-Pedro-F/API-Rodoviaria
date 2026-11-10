using API_Rodoviaria.Application.DTO_s.Requisicao.OnibusRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.OnibusRespostaDTO;
using API_Rodoviaria.Domain.Exceptions;
using API_Rodoviaria.Domain.Interfaces.IOnibus;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioOnibus;

namespace API_Rodoviaria.Application.UseCase.OnibusCasoDeUso.AtualizarOnibus
{
    public class AtualizarOnibusCasoDeUso : IAtualizarOnibusCasoDeUso
    {
        private readonly IRepositorioOnibus _onibusRepositorio;

        public AtualizarOnibusCasoDeUso(IRepositorioOnibus onibusRepositorio)
        {
            _onibusRepositorio = onibusRepositorio;
        }



        public async Task<RespostaCriarOnibusDTO> ExecutarAsync(int id, RequisicaoAtualizarOnibusDTO requisicao)
        {
            var onibus = await _onibusRepositorio.ObterPorIdAsync(id) ?? throw new ExcecaoDeNegocio("Ônibus não encontrado.");


            if (string.IsNullOrWhiteSpace(requisicao.Placa)) requisicao.Placa = onibus.Placa;
            //if (requisicao.FkMotorista is  null)
            //{
            //    var viagem = onibus.Viagens.Where(x => x.FkMotorista == requisicao.FkMotorista).FirstOrDefault();
                
            //}


            if (await _onibusRepositorio.ExistePlacaAsync(requisicao.Placa, id))
            {
                throw new ExcecaoDeNegocio("Já existe outro ônibus com essa placa");

            }
            // requisicao.Placa = requisicao.Placa ?? onibus.Placa; <--- Vai servir como if para substituir  o valor da esquerda pelo valor da direita caso for null( também serve para funções e métodos)
            onibus.Placa = requisicao.Placa;
            await _onibusRepositorio.AtualizarAsync(onibus);

            return new RespostaCriarOnibusDTO
            {
                Id = onibus.Id,
                Placa = onibus.Placa,
                CapacidadeTotal = onibus.CapacidadeTotal,
            };
        }

    }
}

