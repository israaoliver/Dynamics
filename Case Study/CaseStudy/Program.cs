using CaseStudy.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy
{
    class Program
    {
        static void Main(string[] args)
        {

            IOrganizationService service = ConnectionFactory.GetCrmService();


            // Usuario a o qual vai fazer o processo
            string usuario = "692d8e37-552c-ec11-b6e6-00224837a6f1";

            // Mandando o service(conexão) para a classe model se conectar
            Conta contato = new Conta(service);

            // Guid = Valor do Id da Conta para pegar o Contatos
            EntityCollection contatosCrm = contato.ContatoByAccount(new Guid("d1b8b42a-d227-ec11-b6e6-002248372ef6"));

            

            foreach (Entity contatoCrm in contatosCrm.Entities)
            {
               // Entrando em outra tabela conectada a Contato
               EntityReference nivel = (EntityReference)contatoCrm["g07_niveldocliente"];


                if (contatoCrm["contactid"].ToString() == usuario)
                {
                    Console.WriteLine("Ola senhor(a)!!");
                    Console.WriteLine(contatoCrm["fullname"].ToString());
                    Console.WriteLine("Qual oportunidade você deseja aplicar o desconto ? (Descreve em valor)");
                    double valor = int.Parse(Console.ReadLine());
                    double vFinal = 1;

                    

                    switch (nivel.Name)
                    {
                        case "Diamond":
                            Console.WriteLine("Seu desconto é de 10%");
                            vFinal = valor - (valor * 0.1) ;
                            Console.WriteLine($"Sendo o valor da oportunidade R${vFinal} , Valor anterior R${valor}");
                            contato.AtualizandoDesconto(vFinal, usuario);
                            break;
                        case "Platinum":
                            Console.WriteLine("Seu desconto é de 7%");
                            vFinal = valor - (valor * 0.07);
                            Console.WriteLine($"Sendo o valor da oportunidade R${vFinal} , Valor anterior R${valor}");
                            contato.AtualizandoDesconto(vFinal, usuario);
                            break;
                        case "Gold":
                            Console.WriteLine("Seu desconto é de 5%");
                            vFinal = valor - (valor * 0.05);
                            Console.WriteLine($"Sendo o valor da oportunidade R${vFinal} , Valor anterior R${valor}");
                            contato.AtualizandoDesconto(vFinal, usuario);
                            break;
                        case "Silver":
                            Console.WriteLine("Seu desconto é de 3%");
                            vFinal = valor - (valor * 0.03);
                            Console.WriteLine($"Sendo o valor da oportunidade R${vFinal} , Valor anterior R${valor}");
                            contato.AtualizandoDesconto(vFinal, usuario);
                            break;
                        default:
                            Console.WriteLine("Seu nivel de cliente não possui desconto :( ");
                            break;
                    }


                }

            }
            Console.WriteLine("...");
        }

        
    }
}
