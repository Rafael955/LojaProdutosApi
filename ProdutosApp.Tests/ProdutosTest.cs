using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProdutosApp.Domain.Dtos.Requests;
using ProdutosApp.Domain.Dtos.Responses;
using System.Net;
using System.Text;

namespace ProdutosApp.Tests
{
    public class ProdutosTest
    {
        [Fact]
        public void CreateProduct_Successfully()
        {
            //criando a requisição / solicitação para a API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //pegando uma categoria qualquer para teste
            var response = client.GetAsync("/api/fornecedores/listar-fornecedores").Result;

            //capturando a resposta obtida pela API
            var content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            dynamic objeto = JsonConvert.DeserializeObject<dynamic>(content);

            List<FornecedorResponseDto> fornecedor = JsonConvert.DeserializeObject<List<FornecedorResponseDto>>(objeto.data.ToString());

            //pegando um fornecedor qualquer para o teste
            var fornecedorEscolhido = fornecedor[new Random().Next(0, fornecedor.Count - 1)];

            //criando os dados do teste
            var request = new Faker<ProdutoRequestDto>()
                .RuleFor(p => p.Nome, f => f.Commerce.ProductName())
                .RuleFor(p => p.Preco, f => Convert.ToDecimal(f.Commerce.Price(1, 9999, 2, string.Empty)))
                .RuleFor(p => p.Quantidade, f => f.Random.Number(0, 1000))
                .RuleFor(p => p.FornecedorId, fornecedorEscolhido.Id)
                .Generate();

            //serailizar os dados da requisição em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //executando a chamada para o endpoint de cadastro de produto
            response = client.PostAsync("/api/produtos/cadastrar-produto", jsonRequest)?.Result; //json

            //verificando se a API retornou código 201 (HTTP CREATED)
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            //capturando a resposta obtida pela API
            content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            objeto = JsonConvert.DeserializeObject<dynamic>(content);

            ProdutoResponseDto produto = JsonConvert.DeserializeObject<ProdutoResponseDto>(objeto.data.ToString());

            //verificando o conteudo dos dados do usuario
            produto?.Id.Should().NotBeNull();
            produto?.Nome.Should().Be(request.Nome);
            produto?.Quantidade.Should().NotBeNull();
            produto?.Quantidade.Should().Be(request.Quantidade);
            produto?.Preco.Should().NotBeNull();
            produto?.Preco.Should().BeGreaterThan(0);
            produto?.Preco.Should().Be(request.Preco);
            produto?.FornecedorId.Should().Be(request.FornecedorId);
            produto?.NomeFornecedor.Should().Be(fornecedorEscolhido.Nome);
        }

        [Fact]
        public void UpdateProduct_Successfully()
        {
            //criando a requisição / solicitação para a API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //pegando uma categoria qualquer para teste
            var response = client.GetAsync("/api/fornecedores/listar-fornecedores").Result;

            //capturando a resposta obtida pela API
            var content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            dynamic objeto = JsonConvert.DeserializeObject<dynamic>(content);

            List<FornecedorResponseDto> fornecedor = JsonConvert.DeserializeObject<List<FornecedorResponseDto>>(objeto.data.ToString());

            //pegando um fornecedor qualquer para o teste
            var fornecedorEscolhido = fornecedor[new Random().Next(0, fornecedor.Count - 1)];

            //criando os dados do teste
            var request = new Faker<ProdutoRequestDto>()
                .RuleFor(p => p.Nome, f => f.Commerce.ProductName())
                .RuleFor(p => p.Preco, f => Convert.ToDecimal(f.Commerce.Price(1, 9999, 2, string.Empty)))
                .RuleFor(p => p.Quantidade, f => f.Random.Number(0, 1000))
                .RuleFor(p => p.FornecedorId, fornecedorEscolhido.Id)
                .Generate();

            //serializar os dados da requisição em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //executando a chamada para o endpoint de cadastro de produto
            response = client.PostAsync("/api/produtos/cadastrar-produto", jsonRequest)?.Result; //json

            //verificando se a API retornou código 201 (HTTP CREATED)
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            //capturando a resposta obtida pela API
            content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            objeto = JsonConvert.DeserializeObject<dynamic>(content);

            ProdutoResponseDto produto = JsonConvert.DeserializeObject<ProdutoResponseDto>(objeto.data.ToString());


            //recuperando produto cadastrado
            response = client.GetAsync($"/api/produtos/obter-produto/{produto.Id}")?.Result; //json

            //capturando a resposta obtida pela API
            content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            objeto = JsonConvert.DeserializeObject<dynamic>(content);

            ProdutoResponseDto produtoCadastrado = JsonConvert.DeserializeObject<ProdutoResponseDto>(objeto.data.ToString());

            //alterando os dados do teste
            var faker = new Faker();
            produtoCadastrado.Nome = faker.Commerce.ProductName();

            //serializar os dados da requisição em JSON
            jsonRequest = new StringContent(JsonConvert.SerializeObject(produtoCadastrado), Encoding.UTF8, "application/json");

            //executando a chamada para o endpoint de alteração de produto
            response = client.PutAsync($"/api/produtos/alterar-produto/{produtoCadastrado.Id}", jsonRequest)?.Result; //json

            //verificando se a API retornou código 200 (HTTP OK)
            response.StatusCode.Should().Be(HttpStatusCode.OK);


            //capturando a resposta obtida pela API
            content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            objeto = JsonConvert.DeserializeObject<dynamic>(content);

            produto = JsonConvert.DeserializeObject<ProdutoResponseDto>(objeto.data.ToString());

            //verificando o conteudo dos dados do usuario
            produto?.Id.Should().NotBeNull();
            produto?.Nome.Should().Be(produtoCadastrado.Nome);
            produto?.Quantidade.Should().NotBeNull();
            produto?.Quantidade.Should().Be(produtoCadastrado.Quantidade);
            produto?.Preco.Should().NotBeNull();
            produto?.Preco.Should().BeGreaterThan(0);
            produto?.Preco.Should().Be(produtoCadastrado.Preco);
            produto?.FornecedorId.Should().Be(produtoCadastrado.FornecedorId);
            produto?.NomeFornecedor.Should().Be(produtoCadastrado.NomeFornecedor);
        }

        [Fact]
        public void CreateProduct_ProductNameAlreadyRegistered()
        {
            //criando a requisição / solicitação para a API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //pegando uma categoria qualquer para teste
            var response = client.GetAsync("/api/fornecedores/listar-fornecedores").Result;

            //capturando a resposta obtida pela API
            var content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            dynamic objeto = JsonConvert.DeserializeObject<dynamic>(content);

            List<FornecedorResponseDto> fornecedor = JsonConvert.DeserializeObject<List<FornecedorResponseDto>>(objeto.data.ToString());

            //pegando um fornecedor qualquer para o teste
            var fornecedorEscolhido = fornecedor[new Random().Next(0, fornecedor.Count - 1)];

            //criando os dados do teste do produto 1
            var request = new Faker<ProdutoRequestDto>()
                .RuleFor(p => p.Nome, f => f.Commerce.ProductName())
                .RuleFor(p => p.Preco, f => Convert.ToDecimal(f.Commerce.Price(1, 9999, 2, string.Empty)))
                .RuleFor(p => p.Quantidade, f => f.Random.Number(0, 1000))
                .RuleFor(p => p.FornecedorId, fornecedorEscolhido.Id)
                .Generate();

            var firstProductName = request.Nome;

            //serializar os dados da requisição em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //executando a chamada para o endpoint de cadastro de produto
            response = client.PostAsync("/api/produtos/cadastrar-produto", jsonRequest)?.Result; //json

            //verificando se a API retornou código 201 (HTTP CREATED)
            response.StatusCode.Should().Be(HttpStatusCode.Created);


            //criando os dados do teste do produto 2
            request = new Faker<ProdutoRequestDto>()
                .RuleFor(p => p.Nome, firstProductName)
                .RuleFor(p => p.Preco, f => Convert.ToDecimal(f.Commerce.Price(1, 9999, 2, string.Empty)))
                .RuleFor(p => p.Quantidade, f => f.Random.Number(0, 1000))
                .RuleFor(p => p.FornecedorId, fornecedorEscolhido.Id)
                .Generate();

            //serializar os dados da requisição em JSON
            jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //executando a chamada para o endpoint de cadastro de produto
            response = client.PostAsync("/api/produtos/cadastrar-produto", jsonRequest)?.Result; //json

            //verificando se a API retornou código 201 (HTTP CREATED)
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //capturando a resposta obtida pela API
            content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            objeto = JsonConvert.DeserializeObject<dynamic>(content);

            string errorMessage = objeto.message.ToString();

            //verificando o conteudo da mensagem de erro
            errorMessage.Should().Be("Já existe um produto com este nome cadastrado no sistema!");
        }
    }
}
