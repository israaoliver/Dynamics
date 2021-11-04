using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace TechnoPlugins
{
    public class Product : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            if (Context.MessageName == "Create")
            {
                CreateProduct();

            }

            else if (Context.MessageName == "Update")
            {
                UpdateProducts();

            }
            else if (Context.MessageName == "Delete")
            {
                DeleteProduct();

            }

        }

        private void DeleteProduct()
        {
            Entity preDeleteImage = Context.PreEntityImages["PreImage"];
            string productName = preDeleteImage["name"].ToString();
            

            EntityCollection productId = RetrieveProduct(productName);

            foreach (Entity product in productId.Entities)
            {
                Guid ProductId = (Guid)product["productid"];
                Service.Delete("product", ProductId);
            }
        }

        private void UpdateProducts()
        {
            Entity preProductImage = Context.PreEntityImages["PreImage"];
            

            Entity UpdateProduct = new Entity("product");
            UpdateProduct["name"] = preProductImage["name"];            

            Service.Update(UpdateProduct);
        }

        private void CreateProduct()
        {
            Entity product = (Entity)this.Context.InputParameters["Target"];

            string productName = product.Contains("name") ? product["name"].ToString() : string.Empty;          
                       

            Entity createProduct = new Entity("product");
            createProduct["name"] = productName;
           

            Service.Create(createProduct);
        }

        private EntityCollection RetrieveProduct(string name)
        {
            QueryExpression queryRetrieveProduct = new QueryExpression("product");
            queryRetrieveProduct.ColumnSet.AddColumns("productid");
            queryRetrieveProduct.Criteria.AddCondition("name", ConditionOperator.Equal, name);


            return this.Service.RetrieveMultiple(queryRetrieveProduct);
        }
    }
}