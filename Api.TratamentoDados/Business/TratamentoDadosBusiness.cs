using CommonLibrary.Business;
using CommonLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.TratamentoDados.Business
{
    public class TratamentoDadosBusiness
    {
        public TratamentoDadosBusiness()
        {

        }
        public List<MassaDadosModel> GetListMassaDadosModelFull() 
        {
            try 
            { 
                RequestBusiness _RequestBusiness = new RequestBusiness();
                var json = _RequestBusiness.RunRequest("https://localhost:44370/GetFullDados");
                var _ListReturn = JsonConvert.DeserializeObject<List<MassaDadosModel>>(json);
                return _ListReturn;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public List<AgrupamentoModel> Agrupamento(AgrupamentoEnum _tipo)
        {
            try 
            {
                var listFull = this.GetListMassaDadosModelFull();
                List<AgrupamentoModel> result;

                switch ((int)_tipo)
                {
                    case 1: // Conta
                        result = listFull.GroupBy(l => l.conta)
                        .Select(cl => new AgrupamentoModel
                        {
                            descricao = cl.First().conta.ToString(),
                            precomedio = cl.Sum(c => c.quantidade) == 0 ? 0 : Math.Round((cl.Sum(c => c.quantidade * c.preco) / cl.Sum(c => c.quantidade)),2),
                            quantidade = cl.Sum(c => c.quantidade)
                        }).ToList();
                        break;
                    case 2: // Ativo
                        result = listFull.GroupBy(l => l.ativo )
                       .Select(cl => new AgrupamentoModel
                       {
                           descricao = cl.First().ativo.ToString(),
                           precomedio = cl.Sum(c => c.quantidade) == 0 ? 0 : Math.Round((cl.Sum(c => c.quantidade * c.preco) / cl.Sum(c => c.quantidade)),2),
                           quantidade = cl.Sum(c => c.quantidade)
                       }).ToList();
                        break;
                    default: //Tipo Operacao
                        result = listFull.GroupBy(l => l.tipoOperacao)
                       .Select(cl => new AgrupamentoModel
                       {
                           descricao = cl.First().tipoOperacao.ToString(),
                           precomedio = cl.Sum(c => c.quantidade) == 0 ? 0 : Math.Round((cl.Sum(c => c.quantidade * c.preco) / cl.Sum(c => c.quantidade)), 2),
                           quantidade = cl.Sum(c => c.quantidade)
                       }).ToList();
                        break;
                }

                
                
                return result;

            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }



    }




}