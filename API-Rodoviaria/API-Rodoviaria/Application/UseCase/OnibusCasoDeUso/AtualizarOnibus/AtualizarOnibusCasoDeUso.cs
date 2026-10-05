using API_Rodoviaria.Application.DTO_s.Requisicao.OnibusRequisicaoDTO;
using API_Rodoviaria.Application.DTO_s.Resposta.OnibusRespostaDTO;
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



        public async Task<RespostaCriarOnibusDTO> ExecutarAsync(int id, RequisicaoCriarOnibusDTO requisicao)
        {
            var onibus= await _onibusRepositorio.ObterPorIdAsync(id)?? throw new KeyNotFoundException("Ônibus não encontrado.");

            var placa = requisicao.Placa.Trim().ToUpperInvariant();
            if(await _onibusRepositorio.ExistePlacaAsync(placa, id)){
                throw new InvalidOperationException("Já existe outro ônibus com essa placa");
            }
            onibus.Placa = placa;
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

