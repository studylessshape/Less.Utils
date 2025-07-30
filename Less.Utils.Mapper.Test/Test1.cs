using Less.Utils.Mapper.SourceGenerators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace Less.Utils.Mapper.Test
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
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
    }
}
