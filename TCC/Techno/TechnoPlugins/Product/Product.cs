using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace TechnoPlugins
{
    public class Product : PluginImplementation
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity product = new Entity();            

            if (this.Context.MessageName == "Create" || this.Context.MessageName == "Update")
            {
                product = (Entity)this.Context.InputParameters["Target"];
            }            
            else
            {
                product = (Entity)this.Context.PreEntityImages["PreImage"];
            }
            

            IOrganizationService service = Integrate.GetCrmService();


            if (this.Context.MessageName == "Create")
            {
                CreateProduct(service, product);
            }

            else if (this.Context.MessageName == "Update")
            {
                UpdateProducts(service);

            }
            else if (this.Context.MessageName == "Delete")
            {
                DeleteProducts(service);

            }

        }       

        

        private void CreateProduct(IOrganizationService service, Entity product)
        {
            
            EntityReference grupoUnidades = product.Contains("defaultuomscheduleid") ? (EntityReference)product["defaultuomscheduleid"] : null;
            EntityReference unidadePadrao = product.Contains("defaultuomid") ? (EntityReference)product["defaultuomid"] : null;
            EntityReference primario = product.Contains("parentproductid") ? (EntityReference)product["parentproductid"] : null;
            EntityReference assunto = product.Contains("subjectid") ? (EntityReference)product["subjectid"] : null;
            int decimais = product.Contains("quantitydecimal") ? (int)product["quantitydecimal"] : 0;
            string idProduto = product.Contains("productnumber") ? product["productnumber"].ToString() : string.Empty;
            string description = product.Contains("description") ? product["description"].ToString() : null;
            string productName = product.Contains("name") ? product["name"].ToString() : string.Empty;            
            DateTime validoap = product.Contains("validfromdate") ? (DateTime)product["validfromdate"] : DateTime.Now;
            DateTime validoate = product.Contains("validtodate") ? (DateTime)product["validtodate"] : DateTime.Now;


            Entity createProduct = new Entity("product");
            createProduct["defaultuomscheduleid"] = grupoUnidades;
            createProduct["defaultuomid"] = unidadePadrao;
            createProduct["parentproductid"] = primario;
            createProduct["quantitydecimal"] = decimais;
            createProduct["productnumber"] = idProduto;
            createProduct["description"] = description;
            createProduct["name"] = productName;
            createProduct["subjectid"] = assunto;
            createProduct["validfromdate"] = validoap;
            createProduct["validtodate"] = validoate;
            service.Create(createProduct);
        }
        private void UpdateProducts(IOrganizationService service)
        {
            Entity updateProduct = this.Context.PostEntityImages["PostImage"];

            EntityReference grupoUnidades = updateProduct.Contains("defaultuomscheduleid") ? (EntityReference)updateProduct["defaultuomscheduleid"] : null;
            EntityReference unidadePadrao = updateProduct.Contains("defaultuomid") ? (EntityReference)updateProduct["defaultuomid"] : null;
            EntityReference primario = updateProduct.Contains("parentproductid") ? (EntityReference)updateProduct["parentproductid"] : null;
            EntityReference assunto = updateProduct.Contains("subjectid") ? (EntityReference)updateProduct["subjectid"] : null;
            int decimais = updateProduct.Contains("quantitydecimal") ? (int)updateProduct["quantitydecimal"] : 0;
            string idProduto = updateProduct.Contains("productnumber") ? updateProduct["productnumber"].ToString() : string.Empty;
            string description = updateProduct.Contains("description") ? updateProduct["description"].ToString() : null;
            string productName = updateProduct.Contains("name") ? updateProduct["name"].ToString() : string.Empty;            
            DateTime validoap = updateProduct.Contains("validfromdate") ? (DateTime)updateProduct["validfromdate"] : DateTime.Now;
            DateTime validoate = updateProduct.Contains("validtodate") ? (DateTime)updateProduct["validtodate"] : DateTime.Now;

            QueryExpression queryProduct = new QueryExpression("product");
            queryProduct.ColumnSet.AddColumns("productnumber", "name", "description", "defaultuomscheduleid", "defaultuomid", "quantitydecimal", "validtodate", "validfromdate", "parentproductid", "subjectid");
            queryProduct.Criteria.AddCondition("productnumber", ConditionOperator.Equal, updateProduct["productnumber"]);
            EntityCollection productForms = service.RetrieveMultiple(queryProduct);               
            

            foreach (Entity form in productForms.Entities)
            {
                form["defaultuomscheduleid"] = grupoUnidades;
                form["defaultuomid"] = unidadePadrao;
                form["parentproductid"] = primario;
                form["subjectid"] = assunto;
                form["quantitydecimal"] = decimais;
                form["productnumber"] = idProduto;
                form["description"] = description;
                form["name"] = productName;              
                form["validfromdate"] = validoap;
                form["validtodate"] = validoate;
                service.Update(form);
            }           

        }
        private void DeleteProducts(IOrganizationService service)
        {
            Entity deleteProduct = this.Context.PreEntityImages["PreImage"];

            QueryExpression queryProduct = new QueryExpression(deleteProduct.LogicalName);
            queryProduct.ColumnSet.AddColumn("productnumber");
            queryProduct.Criteria.AddCondition("productnumber", ConditionOperator.Equal, deleteProduct["productnumber"]);
            EntityCollection productForms = service.RetrieveMultiple(queryProduct);

            foreach (Entity form in productForms.Entities)
            {
                service.Delete(form.LogicalName, form.Id);
            }

        }        
            
    }
}
