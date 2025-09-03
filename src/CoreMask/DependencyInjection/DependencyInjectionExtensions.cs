using Microsoft.Extensions.DependencyInjection;
using PMart.CoreMask.Calculators;
using PMart.CoreMask.Parsers;
using PMart.CoreMask.Validators;

namespace PMart.CoreMask.DependencyInjection;

/// <summary>
/// The dependency injection extensions.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds the core mask tools.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddCoreMaskTools(this IServiceCollection services)
    {
        services.AddValidators();
        services.AddParsers();
        services.AddCalculators();

        return services;
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<ICoreNumbersInputValidator, CoreNumbersInputValidator>();
        services.AddScoped<ICoreMaskInputValidator, CoreMaskInputValidator>();
    }
    
    private static void AddParsers(this IServiceCollection services)
    {
        services.AddScoped<ICoreNumbersInputParser, CoreNumbersInputParser>();
        services.AddScoped<ICoreMaskInputParser, CoreMaskInputParser>();
    }
    
    private static void AddCalculators(this IServiceCollection services)
    {
        services.AddScoped<ICoreMaskCalculator, CoreMaskCalculator>();
        services.AddScoped<ICoreNumbersCalculator, CoreNumbersCalculator>();
    }
}