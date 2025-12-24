using Less.Utils.Mapper.SourceGenerators;
using Less.Utils.Mapper.Services;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Hosting;
using System.Collections.Immutable;

namespace Less.Utils.Mapper.Test
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethodGenerator()
        {
            IEnumerable<MetadataReference> references =
                from assembly in AppDomain.CurrentDomain.GetAssemblies().Concat([typeof(MapToAttribute).Assembly])
                where !assembly.IsDynamic
                let reference = MetadataReference.CreateFromFile(assembly.Location)
                select reference;
            string source = """
                using Less.Utils.Mapper;

                namespace Less.Utils.Test;

                public class TestClass1
                {
                    public int Id { get; set; }
                    public string Name { get; set; }
                }

                [MapTo(typeof(TestClass1))]
                public class TestClass2
                {
                    public int Id { get; set; }
                    public string Description { get; set; }
                }
                """;

            var syntaxTree = CSharpSyntaxTree.ParseText(source);
            var compilation = CSharpCompilation.Create("Test", [syntaxTree], references);
            var driver = CSharpGeneratorDriver.Create(new MapperGenerator()).WithUpdatedParseOptions((CSharpParseOptions)syntaxTree.Options);
            _ = driver.RunGeneratorsAndUpdateCompilation(compilation, out Compilation outputCompilation, out ImmutableArray<Diagnostic> diagnostics);

            foreach (var generatedTree in outputCompilation.SyntaxTrees)
            {
                Console.WriteLine(generatedTree.FilePath);
                Console.WriteLine(generatedTree.ToString());
            }
        }

        public class TestService1
        {

        }

        public class TestService2
        {

        }

        public class TestServiceMapper : IMapper<TestService1, TestService2>, IMapper<TestService2, TestService1>
        {
            public TestService2 MapTo(TestService1 from)
            {
                return new TestService2();
            }

            public TestService1 MapTo(TestService2 from)
            {
                return new TestService1();
            }
        }

        [TestMethod]
        public void TestService()
        {
            var builder = new HostBuilder();
            builder.ConfigureServices(service =>
            {
                service.AddMappers(typeof(TestServiceMapper).Assembly);
            });
            var host = builder.Build();
        }
    }
}
