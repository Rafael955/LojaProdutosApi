using System;
using ProdutosApp.Domain.Interfaces.Repositories;
using ProdutosApp.Domain.Interfaces.Services;
using ProdutosApp.Domain.Services;
using ProdutosApp.Infra.Data.Repositories;
using ProdutosApp.Infra.Logging.Repositories;

namespace ProdutosApp.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void AddDependencyInjections(this IServiceCollection services)
    {
        services.AddTransient<IProdutoRepository, ProdutoRepository>();
        services.AddTransient<IFornecedorRepository, FornecedorRepository>();
        services.AddTransient<ILoggingRepository, LoggingRepository>();

        services.AddTransient<IProdutoDomainService, ProdutoDomainService>();
        services.AddTransient<IFornecedorDomainService, FornecedorDomainService>();


    }
}
