using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace TechnoPlugins
{
    public class OpportunityNumber : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity opportunity = (Entity)Context.InputParameters["Target"];

            bool validAlphaNumeric = false;

            string newAlphaNumeric = "OPP-00000-A0A0";

            do
            {
                newAlphaNumeric = GenerateAlphaNumericNumber();


                //Retrive all AlphaNumeric Numbers in the system: AlphaNumericNumbers
                QueryExpression queryOpportunity = new QueryExpression("opportunity");
                queryOpportunity.ColumnSet.AddColumns("tc4_numero");
                queryOpportunity.Criteria.AddCondition("tc4_numero", ConditionOperator.Equal, newAlphaNumeric);
                EntityCollection alphaNumericNumbers = Service.RetrieveMultiple(queryOpportunity);



                if (alphaNumericNumbers.Entities.Count > 0)
                {
                    validAlphaNumeric = false;
                }
                else
                {
                    validAlphaNumeric = true;
                }
            } while (!validAlphaNumeric);

            opportunity["tc4_numero"] = newAlphaNumeric;
        }

        //Function to generate a Alphanumeric value in the format required
        public string GenerateAlphaNumericNumber()
        {
            string alphaNumeric = "OPP-";

            //Generate 5 numbers
            alphaNumeric += GetRandomNumber(5);

            alphaNumeric += "-";

            //Generate 4 AlphaNumerics chars
            alphaNumeric += GetRandomChar(4);

            return alphaNumeric;
        }

        //Function to get a random number in the string format form 0 to 9
        public string GetRandomNumber(int times)
        {
            Random random = new Random();
            string numbers = "";

            for (int i = 0; i < times; i++)
            {
                int number = random.Next(0, 10);
                numbers += number.ToString();
            }

            return numbers;
        }

        //Function to get a char in a alphanumeric combination
        public string GetRandomChar(int times)
        {
            Random random = new Random();

            string possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string values = "";

            for (int i = 0; i < times; i++)
            {
                int number = random.Next(0, 36);
                values += possibleChars[number].ToString();
            }

            return values;
        }
    }
}
