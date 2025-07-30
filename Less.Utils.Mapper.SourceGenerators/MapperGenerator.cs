using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Linq;
using System.Text;

namespace Less.Utils.Mapper.SourceGenerators
{
    /// <summary>
    /// Generat <see cref="IMapper{TFrom, TTo}"/> implement
    /// </summary>
    [Generator(LanguageNames.CSharp)]
    public class MapperGenerator : IIncrementalGenerator
    {
        /// <summary>
        /// Initialize generator
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            RegistMapFromSource(context);
        }

        private void RegistMapFromSource(IncrementalGeneratorInitializationContext context)
        {
            var pipeline = context.SyntaxProvider.ForAttributeWithMetadataName(
                "Less.Utils.Mapper.MapToAttribute",
                static (syntaxNode, cancellationToken) => syntaxNode is ClassDeclarationSyntax,
                static (context, cancellationToken) =>
                {
                    var symbolType = (INamedTypeSymbol)context.TargetSymbol;
                    var toTypeArg = symbolType.GetAttributes().Where(attr => attr.AttributeClass?.Name == "MapToAttribute").First().ConstructorArguments.First();
                    var toType = (INamedTypeSymbol)toTypeArg.Value!;
                    return new MapperClassModel(symbolType, toType);
                });

            context.RegisterSourceOutput(pipeline, static (context, model) =>
            {
                var fromMembers = model.FromType.GetMembers().Where(m => m is IPropertySymbol).Cast<IPropertySymbol>();
                var toMembers = model.ToType.GetMembers().Where(m => m is IPropertySymbol).Cast<IPropertySymbol>();

                var fromToTo = string.Join(Environment.NewLine, fromMembers.Select(f => (From: f, To: toMembers.FirstOrDefault(t => t.Name.Equals(f.Name, System.StringComparison.OrdinalIgnoreCase)))).Where(ft => ft.To != null).Select(ft => $"to.{ft.From.Name} = from.{ft.To.Name};"));
                var toToFrom = string.Join(Environment.NewLine, toMembers.Select(f => (From: f, To: fromMembers.FirstOrDefault(t => t.Name.Equals(f.Name, System.StringComparison.OrdinalIgnoreCase)))).Where(ft => ft.To != null).Select(ft => $"to.{ft.From.Name} = from.{ft.To.Name};"));

                var sourceText = SourceText.From($$"""
                    using Less.Utils.Mapper;

                    namespace {{model.FromTypeNamespace}}
                    {
                        [System.CodeDom.Compiler.GeneratedCode]
                        public class Mapper_{{model.FromType.Name}}_{{model.ToType.Name}} : IMapper<{{model.FromType}}, {{model.ToType}}>
                        {
                            public {{model.ToType}} MapTo({{model.FromType}} from)
                            {
                                var to = new {{model.ToType}}();

                                {{fromToTo}}

                                return to;
                            }

                            public {{model.FromType}} MapTo({{model.ToType}} from)
                            {
                                var to = new {{model.FromType}}();
                    
                                {{toToFrom}}
                    
                                return to;
                            }
                        }
                    }
                    """, Encoding.UTF8);
                context.AddSource($"Mapper_{model.FromType}_{model.ToType}.g.cs", sourceText);
            });
        }
    }
}
