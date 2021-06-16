using CommonLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Api.Base.Business
{
    public static class GerarMassaDados
    {

        //Gerar String Randomica
        public static string RandomString(int size)
        {
            var _random = new Random();
            var builder = new StringBuilder(size);

            char offset = 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return builder.ToString().ToUpper();
        }
        private static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

            rngCsp.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static void Generate()
        {
            try { 
            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/MassaDeDados.json");

            //Se não existe o arquivo de massa de dados
            if (!File.Exists(fullPath))
            {

                var listMassa = new  List<MassaDadosModel>();
                //Gera 20 mil registros conforme solicitado.
                for (int i = 1; i <= 20000; i++)
                {

                        int remainder;
                        int quotient = Math.DivRem(i, 2, out remainder);
                        var _tipoOperacao = "V";
                        if (remainder != 0)
                            _tipoOperacao = "C";


                        MassaDadosModel massa = new MassaDadosModel
                        {
                            id = i,
                            conta =  int.Parse(string.Concat("123", (new Random(GetRandomSeed()).Next(0,9)).ToString())),
                            datetime = DateTime.Now.AddDays(new Random().Next(99)).ToString("yyyy-MM-dd HH:mm:ss"),
                            preco = new Random(GetRandomSeed()).Next(1,9999),
                            quantidade = new Random(GetRandomSeed()).Next(1,999),
                            tipoOperacao = _tipoOperacao,
                            ativo = string.Concat("PETR", new Random(GetRandomSeed()).Next(0,9).ToString())
                        };
                        listMassa.Add(massa);
                }

                string json = JsonConvert.SerializeObject(listMassa);
                CreateFile(fullPath, json);
            }
            }catch(Exception ex) 
            {
                throw ex;
            }
        }

        public static void CreateFile(string p_path,string p_content)
        { 
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(p_path))
                {
                    sw.WriteLine(p_content);
                }
        }
    }



        
    
}