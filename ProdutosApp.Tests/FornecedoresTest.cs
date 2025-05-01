
using Azure;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProdutosApp.Domain.Dtos.Requests;
using ProdutosApp.Domain.Dtos.Responses;
using ProdutosApp.Domain.Entities;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace ProdutosApp.Tests
{
    public class FornecedoresTest
    {
        [Fact]
        public void CreateSupplier_Successfully()
        {
            var externalClient = new HttpClient();
            externalClient.BaseAddress = new Uri("http://localhost:5249"); // ou a porta correta da API externa
            
            //criando request para login
            var loginRequest = new
            {
                Email = "admin@lojaprodutosapp.com",
                Senha = "Admin@123"
            };

            //serializar os dados da requisição em JSON
            var jsonLoginRequest = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

            var login_response = externalClient.PostAsync("/api/usuarios/login-usuario", jsonLoginRequest)?.Result; //json

            //capturando a resposta obtida pela API
            var login_content = login_response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            dynamic loginUserDto = JsonConvert.DeserializeObject<dynamic>(login_content);


            //criando a requisição / solicitação para a API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //incluindo o token no Header do request
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginUserDto.token.ToString());

            //criando os dados do teste
            var request = new Faker<FornecedorRequestDto>()
                .RuleFor(p => p.Nome, f => f.Company.CompanyName())
                .Generate();

            //serializar os dados da requisição em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //executando a chamada para o endpoint de cadastro de produto
            var response = client.PostAsync("/api/fornecedores/cadastrar-fornecedor", jsonRequest)?.Result; //json

            //verificando se a API retornou código 201 (HTTP CREATED)
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            //capturando a resposta obtida pela API
            var content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            var objeto = JsonConvert.DeserializeObject<dynamic>(content);

            FornecedorResponseDto fornecedorDto = JsonConvert.DeserializeObject<FornecedorResponseDto>(objeto.data.ToString());

            //verificando o conteudo dos dados do usuario
            fornecedorDto?.Id.Should().NotBeNull();
            fornecedorDto?.Nome.Should().Be(request.Nome);
        }

        [Fact]
        public void UpdateSupplier_Successfully()
        {
            var externalClient = new HttpClient();
            externalClient.BaseAddress = new Uri("http://localhost:5249"); // ou a porta correta da API externa

            //criando request para login
            var loginRequest = new
            {
                Email = "admin@lojaprodutosapp.com",
                Senha = "Admin@123"
            };

            //serializar os dados da requisição em JSON
            var jsonLoginRequest = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");

            var login_response = externalClient.PostAsync("/api/usuarios/login-usuario", jsonLoginRequest)?.Result; //json

            //capturando a resposta obtida pela API
            var login_content = login_response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            dynamic loginUserDto = JsonConvert.DeserializeObject<dynamic>(login_content);
            

            //criando a requisição / solicitação para a API
            var client = new WebApplicationFactory<Program>().CreateClient();

            //incluindo o token no Header do request
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginUserDto.token.ToString());

            //criando os dados do teste
            var request = new Faker<FornecedorRequestDto>()
                .RuleFor(p => p.Nome, f => f.Company.CompanyName())
                .Generate();

            //serializar os dados da requisição em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //executando a chamada para o endpoint de cadastro de produto
            var response = client.PostAsync("/api/fornecedores/cadastrar-fornecedor", jsonRequest)?.Result; //json

            //verificando se a API retornou código 201 (HTTP CREATED)
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            //capturando a resposta obtida pela API
            var content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            var objeto = JsonConvert.DeserializeObject<dynamic>(content);

            FornecedorResponseDto fornecedorDto = JsonConvert.DeserializeObject<FornecedorResponseDto>(objeto.data.ToString());


            //recuperando produto cadastrado
            response = client.GetAsync($"/api/fornecedores/obter-fornecedor/{fornecedorDto.Id}")?.Result; //json

            //verificando se a API retornou código 200(HTTP OK)
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            //capturando a resposta obtida pela API
            content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            objeto = JsonConvert.DeserializeObject<dynamic>(content);

            FornecedorResponseDto fornecedorCadastrado = JsonConvert.DeserializeObject<FornecedorResponseDto>(objeto.data.ToString());


            //alterando os dados do teste
            var faker = new Faker();
            fornecedorCadastrado.Nome = faker.Commerce.ProductName();

            //serializar os dados da requisição em JSON
            jsonRequest = new StringContent(JsonConvert.SerializeObject(fornecedorCadastrado), Encoding.UTF8, "application/json");

            //executando a chamada para o endpoint de alteração de produto
            response = client.PutAsync($"/api/fornecedores/alterar-fornecedor/{fornecedorCadastrado.Id}", jsonRequest)?.Result; //json

            //verificando se a API retornou código 200 (HTTP OK)
            response.StatusCode.Should().Be(HttpStatusCode.OK);


            //capturando a resposta obtida pela API
            content = response.Content.ReadAsStringAsync()?.Result; //json

            //deserializando o JSON de resposta retornado pela API
            objeto = JsonConvert.DeserializeObject<dynamic>(content);

            fornecedorDto = JsonConvert.DeserializeObject<FornecedorResponseDto>(objeto.data.ToString());


            //verificando o conteudo dos dados do usuario
            fornecedorDto?.Id.Should().NotBeNull();
            fornecedorDto?.Nome.Should().Be(fornecedorCadastrado.Nome);
        }
    }
}
