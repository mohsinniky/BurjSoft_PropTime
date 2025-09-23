using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Day9_onwards
{
    // Interfaces
    public interface ITransientService
    {
        string GetId();
    }

    public interface IScopedService
    {
        string GetId();
    }

    public interface ISingletonService
    {
        string GetId();
    }

    // Implementations
    public class TransientService : ITransientService
    {
        private readonly string _id;

        public TransientService()
        {
            _id = Guid.NewGuid().ToString().Substring(0, 8);
            Console.WriteLine($"TransientService created with ID: {_id}");
        }

        public string GetId() => _id;
    }

    public class ScopedService : IScopedService
    {
        private readonly string _id;

        public ScopedService()
        {
            _id = Guid.NewGuid().ToString().Substring(0, 8);
            Console.WriteLine($"ScopedService created with ID: {_id}");
        }

        public string GetId() => _id;
    }

    public class SingletonService : ISingletonService
    {
        private readonly string _id;

        public SingletonService()
        {
            _id = Guid.NewGuid().ToString().Substring(0, 8);
            Console.WriteLine($"SingletonService created with ID: {_id}");
        }

        public string GetId() => _id;
    }

    // Service that uses all three lifetimes
    public class ConsumerService
    {
        private readonly ITransientService _transient;
        private readonly IScopedService _scoped;
        private readonly ISingletonService _singleton;

        public ConsumerService(
            ITransientService transient,
            IScopedService scoped,
            ISingletonService singleton)
        {
            _transient = transient;
            _scoped = scoped;
            _singleton = singleton;
        }

        public void DisplayIds(string consumerName)
        {
            Console.WriteLine($"\n{consumerName}:");
            Console.WriteLine($"Transient ID: {_transient.GetId()}");
            Console.WriteLine($"Scoped ID: {_scoped.GetId()}");
            Console.WriteLine($"Singleton ID: {_singleton.GetId()}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Setup dependency injection container
            var services = new ServiceCollection();

            // Register services with different lifetimes - THIS IS WHERE IMPORTS ARE USED
            services.AddTransient<ITransientService, TransientService>();
            services.AddScoped<IScopedService, ScopedService>();
            services.AddSingleton<ISingletonService, SingletonService>();
            services.AddTransient<ConsumerService>();

            var serviceProvider = services.BuildServiceProvider();

            Console.WriteLine("=== DEMONSTRATING DEPENDENCY INJECTION LIFETIMES ===\n");

            // First scope
            Console.WriteLine("--- Scope 1 ---");
            using (var scope1 = serviceProvider.CreateScope())
            {
                var consumer1 = scope1.ServiceProvider.GetRequiredService<ConsumerService>();
                var consumer2 = scope1.ServiceProvider.GetRequiredService<ConsumerService>();

                consumer1.DisplayIds("Consumer 1 (Scope 1)");
                consumer2.DisplayIds("Consumer 2 (Scope 1)");
            }

            Console.WriteLine("\n--- Scope 2 ---");
            // Second scope
            using (var scope2 = serviceProvider.CreateScope())
            {
                var consumer3 = scope2.ServiceProvider.GetRequiredService<ConsumerService>();
                var consumer4 = scope2.ServiceProvider.GetRequiredService<ConsumerService>();

                consumer3.DisplayIds("Consumer 3 (Scope 2)");
                consumer4.DisplayIds("Consumer 4 (Scope 2)");
            }

            Console.WriteLine("\n=== LIFETIME EXPLANATION ===");
            Console.WriteLine("Transient: New instance EVERY TIME it's requested");
            Console.WriteLine("Scoped: Same instance within the SAME SCOPE, different across scopes");
            Console.WriteLine("Singleton: Same instance for the ENTIRE APPLICATION LIFETIME");
        }
    }
}