using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoConsole.ConnectionsFactory;

namespace TechnoPlugins
{
    public class DontChange : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            string usuarioIntegracao = "d5686635-8a2e-ec11-b6e6-00224837b5f6";
            string usuarioAtual = this.Context.UserId.ToString();

            if (usuarioAtual != usuarioIntegracao)
            {

                throw new InvalidPluginExecutionException("Você não tem autorização para alterar produtos.");

            }
            else
            {

                return;

            }


        }
    }
}
