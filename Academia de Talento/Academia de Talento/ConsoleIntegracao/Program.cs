using ConsoleIntegracao.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleIntegracao
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrganizationService service = ConnectionFactory.GetCrmService();

            Contato contact = new Contato(service);

            EntityCollection contactsCrm = contact.ContatoByAccount(new Guid("e07030ea-2b26-ec11-b6e6-0022483722e5"));


            foreach(Entity contactCRM in contactsCrm.Entities)
            {
                string emailAddress = contactCRM.Contains("emailaddress1") ? contactCRM["emailaddress1"].ToString() : "Contato não possui e-mail";
                string telephoneDaConta = contactCRM.Contains("conta.telephone1") ? ((AliasedValue)contactCRM["conta.telephone1"]).Value.ToString() : "Conta não tem telefone cadastrado";
                //EntityReference parentCustomerId = (EntityReference)contactCRM["parentcustomerid"];

                OptionSetValue tipoDaConta = (OptionSetValue)((AliasedValue)contactCRM["conta.new_tipodaconta"]).Value;
                int totalDeOportunidades = (int)((AliasedValue)contactCRM["conta.new_totaldeoportunidade"]).Value;
                Money valorTotalDeOportunidades = (Money)((AliasedValue)contactCRM["conta.new_valortotaldeoportunidade"]).Value;

                Console.WriteLine(telephoneDaConta);
                Console.WriteLine(contactCRM["fullname"].ToString());
                Console.WriteLine(emailAddress);

                Console.WriteLine("O NOME DA CONTA É:");
                //Console.WriteLine(parentCustomerId.Name);
                //Console.WriteLine(parentCustomerId.Id);
                //Console.WriteLine(parentCustomerId.LogicalName);

                Console.WriteLine($"O tipo da conta é {tipoDaConta.Value}");

                Console.WriteLine($"O total de oportunidades é: {totalDeOportunidades}");
                Console.WriteLine($"O valor total de oportunidade é {valorTotalDeOportunidades.Value}");
            }

            Console.ReadKey(); 
        }

        
    }
}
